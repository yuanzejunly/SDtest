using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum Status { Ready, Waiting, Ingame };

public class PlayerController : NetworkBehaviour
{

    public GameObject bulletPrefab;

    public Transform bulletSpawn;

    public Text gamestatus;

    public Status mystatus;


    

    //public SteamVR_TrackedObject trackedObj;
    private SteamVR_TrackedController[] controller;

    private SteamVR_ControllerManager vrManager;

    public override void OnStartLocalPlayer()
    {
        // Get VR manager from scene
        vrManager = FindObjectOfType(typeof(SteamVR_ControllerManager)) as SteamVR_ControllerManager;
        if (!vrManager)
        {
            Debug.LogError("Cannot find VR Manager");
        }

        // Access vr tracked controller, and assign event
        controller = (SteamVR_TrackedController[])vrManager.GetComponentsInChildren<SteamVR_TrackedController>();

        foreach(SteamVR_TrackedController c in controller)
        {
            c.TriggerClicked += TriggerClicked;
        }

        EventManager.StartListening("gameEnd", EndGame);

        mystatus = Status.Ready;
    }


    void Update()
    {
        if (!isLocalPlayer) return;

        UpdateStatus();

    }

    void TriggerClicked(object sender, ClickedEventArgs e)
    {

        ChkStatus();
    }


    void EndGame() {
        mystatus = Status.Ready;
    }

    void UpdateStatus() {
        switch (mystatus)
        {
            case Status.Ready:
                gamestatus.text = "Fire a bullet to anywhere to start the game";
                break;
            case Status.Waiting:
                gamestatus.text = "Wait for other players to get ready...";
                break;
            case Status.Ingame:
                gamestatus.text = "";
                break;
        }
    }

    void ChkStatus()
    {
        switch (mystatus)
        {
            case Status.Ready:
                mystatus = Status.Waiting;
                break;
            case Status.Waiting:
                break;
            case Status.Ingame:
                CmdFire();
                break;
        }
    }

    public void StartGame() {
        mystatus = Status.Ingame;
    }


    [Command]
    void CmdFire()
    { 
        //spawn the bullet
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

        //give bullet velocity
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        bullet.GetComponent<Bullet>().SetParPlayer(gameObject);

        //spawn the bullet on client side
        NetworkServer.Spawn(bullet);

        //destroy the bullet after 2 seconds
        Destroy(bullet, 3.0f);
    }
}