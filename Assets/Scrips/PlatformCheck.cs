using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCheck : MonoBehaviour
{
    public LayerMask playerLayerMask;

    private BoxCollider2D boxCollider2D;
    void Start()
    {
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        playerLayerMask = LayerMask.GetMask("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsUnder()) //|| (IsOn() && Input.GetKey("s")))
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
    private bool IsUnder()
    {
        float extraHeightText = 0.1f;
        float platformHeight = 0.5f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x/2, +boxCollider2D.bounds.extents.y/2), boxCollider2D.bounds.size * platformHeight, 0f, Vector2.right, (boxCollider2D.bounds.extents.x), playerLayerMask);
        RaycastHit2D raycastHit2 = Physics2D.BoxCast(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x/ 2 + boxCollider2D.bounds.extents.y, boxCollider2D.bounds.extents.y + boxCollider2D.bounds.extents.y / 2), boxCollider2D.bounds.size * platformHeight, 0f, Vector2.right, (boxCollider2D.bounds.extents.x + boxCollider2D.bounds.extents.y * 2), playerLayerMask);
        Color rayColor;
        if (raycastHit.collider != null || raycastHit2.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }//* Vykreslovanie hitboxu
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText)/2, rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText)/2, rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x + extraHeightText, -boxCollider2D.bounds.extents.y), Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText) / 2, rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x + extraHeightText, boxCollider2D.bounds.extents.y), Vector2.down * (boxCollider2D.bounds.extents.y + extraHeightText) / 2, rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x + extraHeightText, boxCollider2D.bounds.extents.y), Vector2.right * (boxCollider2D.bounds.extents.x * 2) + new Vector2(extraHeightText*2,0), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x + extraHeightText, boxCollider2D.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2D.bounds.extents.x * 2) + new Vector2(extraHeightText * 2, 0), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.right * (boxCollider2D.bounds.extents.x * 2), rayColor);
        //Debug.Log(raycastHit.collider);//*/
        return (raycastHit.collider != null || raycastHit2.collider != null);
    }
    private bool IsOn()
    {
        float extraHeightText = 0.14f;
        float platformHeight = 0.6f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x/2 - 0.07f, -boxCollider2D.bounds.extents.y / 2), boxCollider2D.bounds.size * platformHeight, 0f, Vector2.right, (boxCollider2D.bounds.extents.x - extraHeightText), playerLayerMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.blue;
        }
        else
        {
            rayColor = Color.magenta;
        }//* Vykreslovanie hitboxu
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.up * (boxCollider2D.bounds.extents.y + extraHeightText) / 2, rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, 0), Vector2.up * (boxCollider2D.bounds.extents.y + extraHeightText) / 2, rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, -boxCollider2D.bounds.extents.y / 2 - extraHeightText/2), Vector2.right * (boxCollider2D.bounds.extents.x * 2), rayColor);
        return (raycastHit.collider != null);
    }
}
