using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class HostScript : NetworkBehaviour
{
    public override void OnStopServer()
    {
        base.OnStopServer();
        NetworkManager.singleton.StopClient();
    }
}
