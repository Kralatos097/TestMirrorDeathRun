using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField] private GameObject HostPrefab;
    
    private GameObject TraperCanvas;
    private static GameObject HostInst;
    
    public override void OnStartClient()
    {
        base.OnStartClient();
        
        TraperCanvas = GameObject.Find("HostCanvas");

        if(isServer && isLocalPlayer)
        {
            SpawnHost();
        }
        else if(isClientOnly && isLocalPlayer)
        {
            //détruit les objets non nécéssaire aux client
            Destroy(TraperCanvas);
            CRandomColor();
        }
    }
    
    [Server]
    private void SpawnHost()
    {
        HostInst = Instantiate(HostPrefab);
        NetworkServer.Spawn(HostInst);
        TraperCanvas.GetComponent<Canvas>().enabled = true;
        Destroy(gameObject);
        ActivateHost();
    }
    
    private void ActivateHost()
    {
        HostInst.GetComponent<Camera>().enabled = true;
        HostInst.GetComponent<AudioListener>().enabled = true;
    }

    [Command]
    private void CRandomColor()
    {
        Color color = Random.ColorHSV();
        RandomColor(color);
    }

    [ClientRpc]
    private void RandomColor(Color color)
    {
        GetComponent<Renderer>().material.color = color;
    }
}
