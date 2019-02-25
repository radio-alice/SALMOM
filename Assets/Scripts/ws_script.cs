using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Newtonsoft.Json;
using Random = UnityEngine.Random;

public class ws_script : MonoBehaviour
{
    public GameObject player;
    public int NetworkSpeed = 5;
	public string url;

    private MoveData moveData;
    private List<GameObject> players = new List<GameObject>();

	IEnumerator Start ()
	{
		WebSocket w = new WebSocket (new Uri (url));
		yield return StartCoroutine (w.Connect ());

        // mark hosts with "HOST", will use second number for specifying sprite to return
        w.SendString("gameInit_na_na");

		while (true) {
			string reply = w.RecvString ();
			if (reply != null) {
                Debug.Log(reply);
                moveData = JsonConvert.DeserializeObject<MoveData>(reply);

                if (players.Any(obj => obj.name == moveData.id))
                {
                    //the player exists
                    players.SingleOrDefault(obj => obj.name == moveData.id)
                           .GetComponent<PlayerController>()
                           .Move(StringToVector3(moveData.position));
                }
                else if (moveData.position == "START")
                {
                    //checking if it is START if so we don't need to do anything
                    Debug.Log("START");
                    //instantiating the player
                    var newPlayer = Instantiate(player, 
                        new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0), Quaternion.identity);
                    //renaming them
                    newPlayer.name = moveData.id;
                    players.Add(newPlayer);
                    w.SendString("game_"+moveData.id+"_na");
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
