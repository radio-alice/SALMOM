using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveAmt;
    public float maxX;
    public float maxY;

    public void Move(Vector3 moveData)
    {
        if (transform.position.x >= maxX) moveData.x = -1;
        if (transform.position.x <= (-1 * maxX)) moveData.x = 1;
        if (transform.position.y >= maxY) moveData.y = -1;
        if (transform.position.y <= (-1 * (maxY - .1))) moveData.y = 1;
        transform.Translate(moveData.normalized * moveAmt);
    }
}