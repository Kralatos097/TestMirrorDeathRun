using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using Mirror.Examples.NetworkRoom;
using UnityEngine;

public class HostScript : NetworkBehaviour
{
    [SerializeField] private GameObject VictoryCanvas;
    
    public static bool HostWin = false;
    
    public override void OnStopServer()
    {
        base.OnStopServer();
        NetworkManager.singleton.StopClient();
    }

    private void Update()
    {
        if (isClientOnly) return;
        if (NetworkRoomManagerExt.singleton.numPlayers <= 1 && EndLineScript.PiggysWin == false)
        {
            HostVictory();
            HostWin = true;
        }
    }

    [Command(requiresAuthority = false)]
    private void HostVictory()
    {
        DisplayCanvas();
        Invoke(nameof(StopServ), 5f);
        PiegeScript.DeactivateTraps();
    }

    [ClientRpc]
    private void DisplayCanvas()
    {
        Instantiate(VictoryCanvas);
    }

    private void StopServ()
    {
        NetworkServer.Shutdown();
    }
}
