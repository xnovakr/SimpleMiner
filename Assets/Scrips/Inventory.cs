using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    Text dirtInv;
    Text stoneInv;
    Text ironInv;
    Text silverInv;
    Text uranInv;
    Text sapphireInv;

    GameObject dirt;
    GameObject stone;
    GameObject iron;
    GameObject silver;
    GameObject uran;
    GameObject saphire;


    int dirtCount = 0;
    int stoneCount = 0;
    int ironCount = 0;
    int silverCount = 0;
    int uranCount = 0;
    int saphireCount = 0;

    void Start()
    {
        InventorySetup();
        dirtInv.text = "" + dirtCount;
        stoneInv.text = "" + stoneCount;
        ironInv.text = "" + ironCount;
        silverInv.text = "" + silverCount;
        uranInv.text = "" + uranCount;
        sapphireInv.text = "" + saphireCount;
    }
    void Update()
    {
        HideEmpty();
    }

    public void InventoryUpdate(int blockID, int blockCount)
    {
        switch(blockID)
        {
            case 1:
            case 2:
                dirtCount += blockCount;
                dirtInv.text = "" + dirtCount;
                break;
            case 3:
                stoneCount += blockCount;
                stoneInv.text = "" + stoneCount;
                break;
            case 11:
                ironCount += blockCount;
                ironInv.text = "" + ironCount;
                break;
            case 12:
                silverCount += blockCount;
                silverInv.text = "" + silverCount;
                break;
            case 13:
                uranCount += blockCount;
                uranInv.text = "" + uranCount;
                break;
            case 14:
                saphireCount += blockCount;
                sapphireInv.text = "" + saphireCount;
                break;
            default:
                Debug.Log("Wrong block ID");
                break;
        }
    }
    public int BlockCount(int blockID)
    {
        switch (blockID)
        {
            case 1:
            case 2:
                return dirtCount;
            case 3:
                return stoneCount;
            case 11:
                return ironCount;
            case 12:
                return silverCount;
            case 13:
                return uranCount;
            case 14:
                return saphireCount;
            default:
                return -5;
        }
    }
    void InventorySetup()
    {
        dirtInv = GameObject.Find("Dirt").GetComponent<Text>();
        stoneInv = GameObject.Find("Stone").GetComponent<Text>();
        ironInv = GameObject.Find("Iron").GetComponent<Text>();
        silverInv = GameObject.Find("Silver").GetComponent<Text>();
        uranInv = GameObject.Find("Uranium").GetComponent<Text>();
        sapphireInv = GameObject.Find("Saphire").GetComponent<Text>();

        dirt = GameObject.Find("Dirt");
        stone = GameObject.Find("Stone");
        iron = GameObject.Find("Iron");
        silver = GameObject.Find("Silver");
        uran = GameObject.Find("Uranium");
        saphire = GameObject.Find("Saphire");
    }
    void HideEmpty()
    {
        if (dirtCount <= 0)
        {
            dirt.SetActive(false);
        }
        else
        {
            dirt.SetActive(true);
        }
        if (stoneCount <= 0)
        {
            stone.SetActive(false);
        }
        else
        {
            stone.SetActive(true);
        }
        if (ironCount <= 0)
        {
            iron.SetActive(false);
        }
        else
        {
            iron.SetActive(true);
        }
        if (silverCount <= 0)
        {
            silver.SetActive(false);
        }
        else
        {
            silver.SetActive(true);
        }
        if (uranCount <= 0)
        {
            uran.SetActive(false);
        }
        else
        {
            uran.SetActive(true);
        }
        if (saphireCount <= 0)
        {
            saphire.SetActive(false);
        }
        else
        {
            saphire.SetActive(true);
        }
    }
}
