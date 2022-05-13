using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EndLineScript : NetworkBehaviour
{
    [SerializeField] private GameObject VictoryCanvas;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Victory();
            Debug.Log("GG mec");
        }
    }

    [Command(requiresAuthority = false)]
    private void Victory()
    {
        DisplayCanvas();
        Invoke(nameof(StopServ), 5f);
        PiegeScript.DeactivateTraps();
        PlayerMovement.DeactivatePlayers();
    }

    [ClientRpc]
    private void DisplayCanvas()
    {
        GameObject canvasInst = Instantiate(VictoryCanvas);
        NetworkServer.Spawn(canvasInst);
    }

    private void StopServ()
    {
        //NetworkServer.Shutdown();
    }
}
