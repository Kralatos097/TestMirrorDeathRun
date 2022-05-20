using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EndLineScript : NetworkBehaviour
{
    [SerializeField] private GameObject VictoryCanvas;

    public static bool PiggysWin = false;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && HostScript.HostWin == false)
        {
            PlayerVictory();
            PiggysWin = true;
            Debug.Log("GG mec");
        }
    }

    [Command(requiresAuthority = false)]
    private void PlayerVictory()
    {
        DisplayCanvas();
        Invoke(nameof(StopServ), 5f);
        PiegeScript.DeactivateTraps();
        //PlayerMovement.DeactivatePlayers();
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
