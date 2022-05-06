using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    public GameObject HostPrefab;
    
    public override void OnStartClient()
    {
        base.OnStartClient();

        if(isServer)
        {
            zzzzzzz();
        }
        Destroy(this);
    }
    
    private void zzzzzzz()
    {
        Instantiate(HostPrefab);
        Destroy(gameObject);
    }
}
