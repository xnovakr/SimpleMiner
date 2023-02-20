using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BlockPlacer : MonoBehaviour
{
    public bool blockPlace = false;
    public bool blockCheck = false;
    public bool miningSwitch = false;

    GameObject inve;
    public Generator generator;
    public GameSettings settings;
    public GameObject[] blockPrefabs;
    EventSystem eventSys;
    GameObject block;
    private void Awake()
    {
        EmptyHand();
        settings.currentTool = 0;
        inve = GameObject.Find("Inventory");
        eventSys = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        generator = GameObject.Find("Generation").GetComponent<Generator>();
    }
    void Update()
    {
        ToolUpdate(settings.currentTool);
        if (Input.GetMouseButtonDown(0) && blockPlace && !blockCheck && !SpaceCheck())
        {
            BlockUpdate();
            if (inve.GetComponent<Inventory>().BlockCount(block.GetComponent<BlockMiner>().blockID) > 0)
            {
                Instantiate(block, new Vector2(Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).x / generator.scale) * generator.scale, Mathf.Round(Camera.main.ScreenToWorldPoint(Input.mousePosition).y / generator.scale) * generator.scale), Quaternion.identity);
                inve.GetComponent<Inventory>().InventoryUpdate(block.GetComponent<BlockMiner>().blockID, -1);
            }
        }
    }
    void BlockUpdate()
    {
        block = blockPrefabs[settings.currentBlock];
    }
    public void ToolUpdate(int tool)
    {
        switch (tool)
        {
            case 0:
                EmptyHand();
                break;
            case 1:
                MineHand();
                break;
            case 2:
                BuildHand();
                break;
            default:
                break;
        }
    }
    public bool SpaceCheck()
    {
        return eventSys.IsPointerOverGameObject();
    }
    public void BlockCheckTrue()
    {
        blockCheck = true;
    }
    public void BlockCheckFalse()
    {
        blockCheck = false;
    }
    public void MineHand()
    {
        blockPlace = false;
        miningSwitch = true;
    }
    public void BuildHand()
    {
        blockPlace = true;
        miningSwitch = false;
    }
    public void EmptyHand()
    {
        blockPlace = false;
        miningSwitch = false;
    }
}
