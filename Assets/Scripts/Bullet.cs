using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : MonoBehaviour {

    private GameObject parPlayer;

    public void SetParPlayer(GameObject parPlayer_i) {
        parPlayer = parPlayer_i;
    }


    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;

        if (hit.tag != "enemy") return;

        var health = parPlayer.GetComponent<Health>();

        if (health != null) health.IncreaseScore();

        Destroy(gameObject);
    }

}