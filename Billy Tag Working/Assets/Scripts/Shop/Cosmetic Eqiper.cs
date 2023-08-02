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

    private int index;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goLeft.ispressed)
        {
            index--;
            if (index < 0)
            {
                index = CosmeticsManager.EquippedCosmetics.Count - 1;
            }
            goLeft.reset = true;
        }
        else if (goRight.ispressed)
        {
            index++;
            if (index >= CosmeticsManager.EquippedCosmetics.Count)
            {
                index = 0;
            }
            goRight.reset = true;
        }
    }
}
