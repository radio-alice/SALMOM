using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveAmt;

    public void Move(Vector3 moveData)
    {
        transform.Translate(moveData.normalized * moveAmt);
    }
}