using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMiner : MonoBehaviour
{
    public PlayerSettings playerSettings;

    GameObject inve;
    GameObject blockPlacer;
    GameObject generator;

    Vector3 truePos;


    BoxCollider2D boxCollider2D;
    LayerMask playerLayerMask;
    int playerDMG = 1;
    public bool mining;
    float mineRange = 0f;
    public int blockID = 0;
    public int blockLife = 1;

    private float blockScale; 
    private void Start()
    {
        playerSettings.playerDMG = 1;
        mineRange = playerSettings.miningReach;
        playerLayerMask = LayerMask.GetMask("Player");
        inve =  GameObject.Find("Inventory");
        blockPlacer = GameObject.Find("PlacemantGrid");
        generator = GameObject.Find("Generation");
        blockScale = generator.GetComponent<Generator>().scale;
        mining = blockPlacer.GetComponent<BlockPlacer>().miningSwitch;
    }
    private void LateUpdate()
    {
        if (gameObject.GetComponent<Renderer>().isVisible)
        {
            truePos.x = Mathf.Floor((0.01f + transform.position.x) / blockScale) * blockScale;
            truePos.y = Mathf.Floor((0.01f + transform.position.y) / blockScale) * blockScale;
            transform.position = truePos;
            PlayerCheck();
            mining = blockPlacer.GetComponent<BlockPlacer>().miningSwitch;
        }
    }
    private void OnMouseUpAsButton()
    {
        if (mining && !blockPlacer.GetComponent<BlockPlacer>().SpaceCheck())
        {
            Debug.Log(playerDMG + "  " + playerSettings.playerDMG);
            BlockHit();
        }
    }
    private void OnMouseEnter()
    {
        blockPlacer.GetComponent<BlockPlacer>().BlockCheckTrue();
    }
    private void OnMouseExit()
    {
        blockPlacer.GetComponent<BlockPlacer>().BlockCheckFalse();
    }
    public void BlockHit()
    {
        playerDMG = playerSettings.playerDMG;
        if (PlayerCheck())
        {
            blockLife-= 1 * playerDMG;
        }
        else
        {
            Debug.Log("Out of range m8");
        }
        if (blockLife <= 0)
        {
            inve.GetComponent<Inventory>().InventoryUpdate(blockID, 1);
            /*
            Debug.Log(generator.GetComponent<Generator>().BlockCheck(gameObject, gameObject.transform.position.x, gameObject.transform.position.y)[0] + "x     y"+
                generator.GetComponent<Generator>().BlockCheck(gameObject, gameObject.transform.position.x, gameObject.transform.position.y)[1]);
            generator.GetComponent<Generator>().BlockReplace(gameObject, gameObject.transform.position.x, gameObject.transform.position.y, 3);
            //*///Replacement bloku
            Destroy(this.gameObject);
        }
    }
    public bool PlayerCheck()
    {   
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        float offset = mineRange * (boxCollider2D.bounds.extents.x * 2);
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size*(1 + 2*mineRange), 0f, Vector2.zero, 0, playerLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.blue;
        }
        else
        {
            rayColor = Color.red;
        }
        /*Vykreslovanie dostupnosti bloku
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(-boxCollider2D.bounds.extents.x/2, boxCollider2D.bounds.extents.y/2), Vector2.right * (boxCollider2D.bounds.extents.x), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(+boxCollider2D.bounds.extents.x/2, boxCollider2D.bounds.extents.y/2), Vector2.right * (boxCollider2D.bounds.extents.x), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(-boxCollider2D.bounds.extents.x/2, boxCollider2D.bounds.extents.y/2), Vector2.down  * (boxCollider2D.bounds.extents.y), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(+boxCollider2D.bounds.extents.x/2, boxCollider2D.bounds.extents.y/2), Vector2.down  * (boxCollider2D.bounds.extents.y), rayColor);
        //*/
        /* Vykreslovanie hitboxu
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(-boxCollider2D.bounds.extents.x - offset, boxCollider2D.bounds.extents.y + offset), Vector2.right * (boxCollider2D.bounds.extents.x * 2 + offset * 2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3( boxCollider2D.bounds.extents.x + offset, boxCollider2D.bounds.extents.y + offset), Vector2.right * (boxCollider2D.bounds.extents.x * 2 + offset * 2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(-boxCollider2D.bounds.extents.x - offset, boxCollider2D.bounds.extents.y + offset), Vector2.down  * (boxCollider2D.bounds.extents.y * 2 + offset * 2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3( boxCollider2D.bounds.extents.x + offset, boxCollider2D.bounds.extents.y + offset), Vector2.down  * (boxCollider2D.bounds.extents.y * 2 + offset * 2), rayColor);
        //Debug.Log(raycastHit.collider);//*/
        return raycastHit.collider != null;
    }
}
