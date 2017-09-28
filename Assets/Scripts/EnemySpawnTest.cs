using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTest : MonoBehaviour
{
    public string startGameEvent = "gameStart";
    public string endGameEvent = "gameEnd";

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            EventManager.TriggerEvent(startGameEvent);
        }

        if (Input.GetKeyDown("e"))
        {
            EventManager.TriggerEvent(endGameEvent);
        }
	}
}
