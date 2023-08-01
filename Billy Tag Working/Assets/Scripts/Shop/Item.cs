using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomButton;
using Photon.VR.Cosmetics;

public class Item : MonoBehaviour
{
    public Button button;
    [Header("Info")]
    public string DisplayName;
    public string Id;
    public int Price;
    public CosmeticType CosmeticType;

    private Terminal terminal;

    private void Start()
    {
        terminal = FindObjectOfType<Terminal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (button.ispressed)
        {
            CosmeticClass cosmetic = new CosmeticClass(DisplayName, Id, Price, CosmeticType);
            terminal.items.Add(cosmetic);
            button.reset = true;
        }
    }
}