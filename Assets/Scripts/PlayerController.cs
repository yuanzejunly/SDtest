using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{

    public GameObject bulletPrefab;

    public Transform bulletSpawn;




    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }


    void Update()
    {
        if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.Space)) CmdFire();

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

    [Command]
    void CmdFire()
    {
        //spawn the bullet
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        //give bullet velocity
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        //spawn the bullet on client side
        NetworkServer.Spawn(bullet);

        //destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}