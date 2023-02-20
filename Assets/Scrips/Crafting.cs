using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    public PlayerSettings playerSettings;
    public GameObject inventory;
    public GameObject[] craftingButtons;
    private void Start()
    {
        playerSettings.playerDMG = 1;
    }
    public void CraftIronPick()
    {
        if (inventory.GetComponent<Inventory>().BlockCount(3) >= 20 && inventory.GetComponent<Inventory>().BlockCount(11) >= 10)
        {
            inventory.GetComponent<Inventory>().InventoryUpdate(3, -20);
            inventory.GetComponent<Inventory>().InventoryUpdate(11, -10);
            playerSettings.playerDMG = 5;
            craftingButtons[0].SetActive(false);
        }
    }
    public void CraftSilverPick()
    {
        if (inventory.GetComponent<Inventory>().BlockCount(3) >= 40 && inventory.GetComponent<Inventory>().BlockCount(12) >= 15)
        {
            inventory.GetComponent<Inventory>().InventoryUpdate(3, -40);
            inventory.GetComponent<Inventory>().InventoryUpdate(12, -15);
            playerSettings.playerDMG = 10;
            craftingButtons[1].SetActive(false);
        }
    }
    public void CraftUranPick()
    {
        if (inventory.GetComponent<Inventory>().BlockCount(11) >= 20 && inventory.GetComponent<Inventory>().BlockCount(13) >= 20)
        {
            inventory.GetComponent<Inventory>().InventoryUpdate(11, -20);
            inventory.GetComponent<Inventory>().InventoryUpdate(13, -20);
            playerSettings.playerDMG = 15;
            craftingButtons[2].SetActive(false);
        }
    }
    public void CraftSaphirePick()
    {
        if (inventory.GetComponent<Inventory>().BlockCount(12) >= 30 && inventory.GetComponent<Inventory>().BlockCount(14) >= 20)
        {
            inventory.GetComponent<Inventory>().InventoryUpdate(12, -30);
            inventory.GetComponent<Inventory>().InventoryUpdate(14, -20);
            playerSettings.playerDMG = 20;
            craftingButtons[3].SetActive(false);
        }
    }
}
