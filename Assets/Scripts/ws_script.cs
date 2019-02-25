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

        // mark game connection with "game", will use second number for player id, 
        // third for selecting which sprite to use
        w.SendString("gameInit_na_na");

		while (true) {
			string reply = w.RecvString ();
			if (reply != null) {
                moveData = JsonConvert.DeserializeObject<MoveData>(reply);

                if (players.Any(obj => obj.name == moveData.id))
                {
                    //the player exists
                    var currentPlayer = players.SingleOrDefault(obj => obj.name == moveData.id);

                    //if socket was closed, remove player
                    if (moveData.position == "close")
                    {
                        currentPlayer.SetActive(false);
                    }
                    //else move player as specified
                    else
                    { 
                        currentPlayer.GetComponent<PlayerController>()
                                     .Move(StringToVector3(moveData.position));
                    }
                }
                else if (moveData.position == "START")
                {
                    //instantiating the player
                    var newPlayer = Instantiate(player, 
                        new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0), Quaternion.identity);
                    //renaming them
                    newPlayer.name = moveData.id;
                    players.Add(newPlayer);
                    w.SendString("game_"+moveData.id+"_You Started!");
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
