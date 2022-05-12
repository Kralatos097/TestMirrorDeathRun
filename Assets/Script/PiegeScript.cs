using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PiegeScript : NetworkBehaviour
{
    [ClientRpc]
    public /*virtual*/ void ActivateTrap()
    {
        GetComponent<Rigidbody2D>().simulated = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerMovement>().Death();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMovement>().Death();
        }
    }
}