using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PiegeScript : NetworkBehaviour
{
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
}