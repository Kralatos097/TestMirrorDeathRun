using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField] private GameObject HostPrefab;
    
    private GameObject TraperCanvas;
    
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
        }
        Destroy(this);
    }
    
    private void SpawnHost()
    {
        Instantiate(HostPrefab);
        Destroy(gameObject);
    }
}
