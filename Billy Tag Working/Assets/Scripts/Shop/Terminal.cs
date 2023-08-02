using CustomButton;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.VR.Cosmetics;
using Photon.VR;
using System.Linq;

public class Terminal : MonoBehaviour
{
    public TextMeshProUGUI text;
    [SerializeField] public List<CosmeticClass> items = new List<CosmeticClass>();
    [SerializeField] public List<CosmeticClass> boughtItems = new List<CosmeticClass>();
    public CoinManager CoinManager;
    public Button yes;
    public Button no;
    public Button start;

    private int totalCost;
    private bool wasStarted;
    private bool isSelecting;

    private void Start()
    {
        LoadPurchasedItems();
        text.text = $"Welcome {PlayerPrefs.GetString("Username")}";
    }

    // Update is called once per frame
    void Update()
    {
        if (items.Count > 0 || !wasStarted)
        {
            if (start.ispressed)
            {
                CalculateTotalCost();
                wasStarted = true;
                start.reset = true;
                StartCoroutine(Begin());
            }
        }
    }

    private void ResetState()
    {
        text.text = $"Welcome {PlayerPrefs.GetString("Username")}";
        items.Clear();
        wasStarted = false;
        totalCost = 0;
    }

    IEnumerator Begin()
    {
        text.text = $"Do you want to buy these items for {totalCost}?: {string.Join(", ", items)}";
        isSelecting = true;

        // Wait for user input
        while (isSelecting)
        {
            yield return null;

            if (yes.ispressed)
            {
                Buy();
                isSelecting = false;
            }
            else if (no.ispressed)
            {
                ResetState();
                isSelecting = false;
            }
        }
    }

    void Buy()
    {
        if (CoinManager.GetCoins() <= totalCost)
        {
            text.text = "Not Enough Money!";
            ResetState();
        }
        else
        {
            items = items.GroupBy(item => item.Id).Select(group => group.First()).ToList();
            items.RemoveAll(item => boughtItems.Exists(boughtItem => boughtItem.Id == item.Id));
            CoinManager.ModifyCoins(totalCost);
            boughtItems.AddRange(items);
            SaveBoughtItems();
            StartCoroutine(Proceed());
        }
    }

    IEnumerator Proceed()
    {
        foreach (var item in items)
        {
            text.text = $"Do you want to equip the {item.DisplayName}?";

            bool isChoosing = true;

            // Wait for user input to equip or not
            while (isChoosing)
            {
                yield return null;

                if (yes.ispressed)
                {
                    Equip(item.Id, item.CosmeticType);
                    isChoosing = false;
                }
                else if (no.ispressed)
                {
                    isChoosing = false;
                }
            }
        }

        ResetState();
    }

    void Equip(string cosmeticId, CosmeticType cosmeticType)
    {
        PhotonVRManager.SetCosmetic(cosmeticType, "");
        PhotonVRManager.SetCosmetic(cosmeticType, cosmeticId);
    }

    private void LoadPurchasedItems()
    {
        string purchasedItems = PlayerPrefs.GetString("PurchasedItems", "");
        if (purchasedItems != null)
        {
            string[] purchasedItemsArray = purchasedItems.Split(';');
            foreach (string item in purchasedItemsArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] itemData = item.Split('|');
                    if (itemData.Length == 4)
                    {
                        string displayName = itemData[0];
                        string id = itemData[1];
                        int price = int.Parse(itemData[2]);
                        CosmeticType cosmeticType = (CosmeticType)System.Enum.Parse(typeof(CosmeticType), itemData[3]);
                        boughtItems.Add(new CosmeticClass(displayName, id, price, cosmeticType));
                    }
                }
            }
        }
    }

    void SaveBoughtItems()
    {
        List<string> purchasedItems = new List<string>();
        foreach (var item in items)
        {
            purchasedItems.Add($"{item.DisplayName}|{item.Id}|{item.Price}|{item.CosmeticType}");
        }

        string purchasedItemsStr = string.Join(";", purchasedItems.ToArray());
        PlayerPrefs.SetString("PurchasedItems", purchasedItemsStr);
    }

    void CalculateTotalCost()
    {
        totalCost = 0;
        foreach (var item in items)
        {
            totalCost += item.Price;
        }
    }
}

[System.Serializable]
public class CosmeticClass
{
    public string DisplayName;
    public string Id;
    public int Price;
    public CosmeticType CosmeticType;

    public CosmeticClass(string displayName, string id, int price, CosmeticType cosmeticType)
    {
        DisplayName = displayName;
        Id = id;
        Price = price;
        CosmeticType = cosmeticType;
    }
    public override string ToString()
    {
        return DisplayName;
    }
}
