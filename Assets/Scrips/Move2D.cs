using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move2D : MonoBehaviour
{
    public float speedX = 5;
    public float jumpY = 5;

    float moveSpeed = 0f;
    public bool isGrounded = false;
    bool goLeft = false;
    bool goRight = false;
    
    public LayerMask groundLayerMask;
    public LayerMask platformLayerMask;
    public LayerMask mapBorderMask;

    Animator animator;

    private BoxCollider2D boxCollider2D;
    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = transform.GetComponent<BoxCollider2D>();
        groundLayerMask = LayerMask.GetMask("Ground");
        platformLayerMask = LayerMask.GetMask("Platform");
        mapBorderMask = LayerMask.GetMask("MapEdge");
        Stop();
    }

    void Update()
    {
        //isGrounded = IsGrounded();
        Movement();
        MovementAnim();
        if (goLeft)
        {
            GoLeft();
            SetWalking(true);
        }
        else if (goRight)
        {
            GoRight();
            SetWalking(true);
        }
        else
        {
            Stop();
        }
    }
    void Movement()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            GoLeft();
        }
        else if (Input.GetKey("d") || Input.GetKey("right"))
        {
             GoRight();
        }
        else if (((Input.GetKeyUp("a") || Input.GetKeyUp("left")) && moveSpeed < 0) || ((Input.GetKeyUp("d") || Input.GetKeyUp("right")) && moveSpeed > 0))
        {
            Stop();
            SetWalking(false);
        }
        else 
        {
            SetWalking(false);

        }
        transform.position += new Vector3(1f, 0f, 0f) * Time.deltaTime * moveSpeed;
    }
    public void GoRight()
    {
        if (RightColider())
        {
            moveSpeed = speedX;
            transform.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            Stop();
        }
    }
    public void GoLeft()
    {
        if (LeftColider())
        {
            moveSpeed = -speedX;
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            Stop();
        }
    }
    public void GoLeftButton()
    {
        goLeft = true;
    }
    public void GoRightButton()
    {
        goRight = true;
    }
    public void Stop()
    {
        moveSpeed = 0;
        goLeft = false;
        goRight = false;
    }
    public void Jump()
    {
        if (IsGrounded())
        {
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, jumpY, 0);
        }
    }    
    private bool IsGrounded()
    {
        float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, extraHeightText, groundLayerMask);
        //BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance = Mathf.Infinity, int layerMask = Physics2D.AllLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity); 
        RaycastHit2D raycastHit2 = Physics2D.BoxCast(boxCollider2D.bounds.center - new Vector3 (boxCollider2D.bounds.extents.x - extraHeightText, boxCollider2D.bounds.extents.y), new Vector2(boxCollider2D.bounds.extents.x/2, extraHeightText*2), 0f, Vector2.right, boxCollider2D.bounds.extents.x + extraHeightText*2, platformLayerMask);
        Color rayColor;
        if (raycastHit.collider != null || raycastHit2.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }//* Vykreslovanie hitboxu
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(boxCollider2D.bounds.extents.x,-boxCollider2D.bounds.extents.y + extraHeightText), Vector2.down * (extraHeightText*2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y - extraHeightText), Vector2.down * (extraHeightText*2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y), Vector2.right * (boxCollider2D.bounds.extents.x * 2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y + extraHeightText), Vector2.right * (boxCollider2D.bounds.extents.x * 2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y - extraHeightText), Vector2.right * (boxCollider2D.bounds.extents.x * 2), rayColor);
        //Debug.Log(raycastHit.collider);//*/
        if (raycastHit.collider != null)
        {
            return (raycastHit.collider.gameObject.tag == "Ground");
        }
        if (raycastHit2.collider != null)
        {
            return (raycastHit2.collider.gameObject.tag == "Platform");
        }
        return false;
    }
    private bool LeftColider()
    {
        float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.left, extraHeightText, groundLayerMask);
        RaycastHit2D raycastHit2 = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.left, extraHeightText, mapBorderMask);
        Color rayColor;
        if (raycastHit.collider != null || raycastHit2.collider != null)
        {
            rayColor = Color.blue;
        }
        else
        {
            rayColor = Color.red;
        }//* Vykreslovanie hitboxu
        //                         zaciatok                                                            smer vlavo(-1,0) * sirka colideru osy X + odsadenie
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(0, boxCollider2D.bounds.extents.y), Vector2.down * (boxCollider2D.bounds.extents.y * 2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(0, boxCollider2D.bounds.extents.y), Vector2.left * (boxCollider2D.bounds.extents.x + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(0, boxCollider2D.bounds.extents.y), Vector2.left * (boxCollider2D.bounds.extents.x + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(-boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y), Vector2.down * (boxCollider2D.bounds.extents.y * 2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(-boxCollider2D.bounds.extents.x - extraHeightText, boxCollider2D.bounds.extents.y), Vector2.down * (boxCollider2D.bounds.extents.y * 2), rayColor);
        //Debug.Log(raycastHit.collider);//*/

        if (raycastHit.collider != null)
        {
            return false;
        }
        if (raycastHit2.collider != null)
        {
            return false;
        }
        return true;
    }
    private bool RightColider()
    {
        float extraHeightText = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.right, extraHeightText, groundLayerMask);
        RaycastHit2D raycastHit2 = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.right, extraHeightText, mapBorderMask);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.blue;
        }
        else
        {
            rayColor = Color.red;
        }//* Vykreslovanie hitboxu
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(0, boxCollider2D.bounds.extents.y), Vector2.down * (boxCollider2D.bounds.extents.y * 2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(0, boxCollider2D.bounds.extents.y), Vector2.right * (boxCollider2D.bounds.extents.x + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center - new Vector3(0, boxCollider2D.bounds.extents.y), Vector2.right * (boxCollider2D.bounds.extents.x + extraHeightText), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(+boxCollider2D.bounds.extents.x, boxCollider2D.bounds.extents.y), Vector2.down * (boxCollider2D.bounds.extents.y * 2), rayColor);
        Debug.DrawRay(boxCollider2D.bounds.center + new Vector3(+boxCollider2D.bounds.extents.x + extraHeightText, boxCollider2D.bounds.extents.y), Vector2.down * (boxCollider2D.bounds.extents.y * 2), rayColor);
        //Debug.Log(raycastHit.collider);//*/
        if (raycastHit.collider != null)
        {
            return false;
        }
        if (raycastHit2.collider != null)
        {
            return false;
        }
        return true;
    }
    public void SetWalking(bool state)
    {
        animator.SetBool("isWalking", state);
    }
    void MovementAnim()
    {
        if ((Input.GetKeyDown("a") || Input.GetKeyDown("left")) || (Input.GetKeyDown("d") || Input.GetKeyDown("right")))
        {
            SetWalking(true);
        }
    }
}
