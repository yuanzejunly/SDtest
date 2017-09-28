using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    private GameObject player;
    private float speed = 1f;

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("player")[Random.Range(0, 1)];
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "bullet" || collision.gameObject.tag == "player") Destroy(gameObject);

    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.LookAt(player.transform);
    }

}