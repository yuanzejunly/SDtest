using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class manager : MonoBehaviour {

    private bool isInGame = false;

    void Update()
    {
        if (!isInGame)
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("player"))
            {
                if (p.GetComponent<PlayerController>().mystatus != Status.Waiting) return;
            }

            EventManager.TriggerEvent("gameStart");
            isInGame = true;
        }
        else
        {
            foreach (GameObject p in GameObject.FindGameObjectsWithTag("player"))
            {
                if (p.GetComponent<PlayerController>().GetComponent<Health>().currScore == 10)
                {
                    EventManager.TriggerEvent("gameEnd");
                    isInGame = false;
                }
            }


        }
    }
}
