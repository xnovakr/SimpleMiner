using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;
    public GameObject player;
    public Generator generator;
    // Start is called before the first frame update
    void Start()
    {
        generator = GameObject.Find("Generation").GetComponent<Generator>();
        playerTransform = player.transform;
    }

    // called after update and fixed update
    void LateUpdate()
    {//temp = camera position
        if (WorldBorderX())
        {
            Vector3 temp = transform.position;

            temp.x = playerTransform.position.x;

            transform.position = temp;
        }
        if (WorldBorderY())
        {
            Vector3 temp = transform.position;

            temp.y = playerTransform.position.y;

            transform.position = temp;
        }
    }

    bool WorldBorderX()
    {
        bool result;
        result = (player.transform.position.x - (generator.scale * 8) > generator.baseMinX) && 
            (player.transform.position.x + (generator.scale * 9) < generator.baseMaxX * generator.chunksCount);

        return result;
    }
    bool WorldBorderY()
    {
        bool result;
        result = (player.transform.position.y - (generator.scale * 5) > generator.baseMinY) &&
            (player.transform.position.y + (generator.scale * 5) < generator.baseMaxY);

        return result;
    }
}
