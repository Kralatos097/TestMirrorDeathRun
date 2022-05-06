using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class HostScript : NetworkBehaviour
{
    [SerializeField] private GameObject TraperCanvas;
    
    public void Start()
    {
        Instantiate(TraperCanvas);
    }
    
    /*[ClientRpc]
    private void DestroySelf()
    {
        Destroy(gameObject);
    }*/
}
