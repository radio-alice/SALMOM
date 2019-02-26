using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveAmt;
    public float maxX;
    public float maxY;
    SpriteRenderer spriteR;
    int turn;

    private void Awake()
    {
        turn = Random.Range(0, 1);
        spriteR = GetComponent<SpriteRenderer>();
        transform.eulerAngles = new Vector3(0, 0, (-90 * turn) + 45);
        if (turn == 0) turn = -1;
    }
    public void AttachSprite(string file) {
        file = file.Substring(0, file.Length - 4);
        Sprite sprite = Resources.Load<Sprite>(file);
        spriteR.sprite = sprite;
    }

    public void Move(Vector3 moveData)
    {
        if (transform.position.x >= maxX) moveData.x = -1;
        if (transform.position.x <= (-1 * maxX)) moveData.x = 1;
        if (transform.position.y >= maxY) moveData.y = -1;
        if (transform.position.y <= (-1 * (maxY - .1))) moveData.y = 1;
        transform.Translate(moveData.normalized * moveAmt);
        transform.eulerAngles += new Vector3(0, 0, turn * 90);
        turn *= -1;
    }

}