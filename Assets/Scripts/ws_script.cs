using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Newtonsoft.Json;
using Random = UnityEngine.Random;

public class ws_script : MonoBehaviour
{
    public GameObject player;
    public int NetworkSpeed = 5;
	public string ip = "localhost";
	public string port = "8000";

    private MoveData moveData;

	IEnumerator Start ()
	{
		WebSocket w = new WebSocket (new Uri ("ws://" + ip + ":" + port));
		yield return StartCoroutine (w.Connect ());

		while (true) {
			string reply = w.RecvString ();
			if (reply != null) {
                string str = reply.ToString();
                moveData = JsonConvert.DeserializeObject<MoveData>(str);
                if (GameObject.Find(moveData.id))
                {
                    //the player exists
                    GameObject.Find(moveData.id).GetComponent<PlayerController>().Move(StringToVector3(moveData.position));
                }
                else
                {
                    //the player does not exist we must instantiate it
                    if (moveData.position == "START")
                    {
                        //checking if it is START if so we don't need to do anything
                        Debug.Log("START");
                        //instantiating the player
                        var newPlayer = Instantiate(player, 
                            new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0), Quaternion.identity);
                        //renaming them
                        newPlayer.name = moveData.id;
                    }
                }
            }
			if (w.error != null) {
				Debug.LogError ("Error: " + w.error);
				break;
			}
			yield return 0;
		}
		w.Close ();
	}

    public static Vector3 StringToVector3(string sVector)
    {
        // Remove the parentheses
        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        // split the items
        string[] sArray = sVector.Split(',');

        // store as a Vector3
        Vector3 result = new Vector3(
            float.Parse(sArray[0]),
            float.Parse(sArray[1]),
            float.Parse(sArray[2]));

        return result;
    }
}

public class MoveData
{
    public string id;
    public string position;
};
