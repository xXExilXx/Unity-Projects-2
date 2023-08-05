using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomButton;
using Photon.VR.Cosmetics;
public class CosmeticEqiper : MonoBehaviour
{
    public Button goLeft;
    public Button goRight;
    public Button equip;
    public List<ItemDisplay> itemDisplays = new List<ItemDisplay>();

    private int index;
    private Terminal terminal;
    
    // Start is called before the first frame update
    void Start()
    {
        terminal = FindObjectOfType<Terminal>();
        SortItems();
    }

    void SortItems()
    {
        foreach(var items in terminal.boughtItems)
        {
            itemDisplays.Add(new ItemDisplay(items.Id, items.Id));
        }
    }
    
    GameObject FindGameobject(string itemid)
    {
        GameObject gameobject = GameObject.Find($"");
        return gameobject;
    }

    // Update is called once per frame
    void Update()
    {
        if (goLeft.ispressed)
        {
            index--;
            if (index < 0)
            {
                index = terminal.boughtItems.Count - 1;
            }
            UpdateItem();
            goLeft.reset = true;
        }
        else if (goRight.ispressed)
        {
            index++;
            if (index >= terminal.boughtItems.Count)
            {
                index = 0;
            }
            UpdateItem();
            goRight.reset = true;
        }
    }

    void UpdateItem()
    {

    }
}

[System.Serializable]
public class ItemDisplay
{
    public string id;
    public string gameObjectName;

    public ItemDisplay(string id, string gameObjectName)
    {
        this.id = id;
        this.gameObjectName = gameObjectName;
    }
}