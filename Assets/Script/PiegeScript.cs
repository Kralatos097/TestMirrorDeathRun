using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PiegeScript : NetworkBehaviour
{
    private static List<PiegeScript> _piegeList;

    private void Start()
    {
        _piegeList ??= new List<PiegeScript>();
        _piegeList.Add(this);
    }

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

    public static void DeactivateTraps()
    {
        foreach (PiegeScript piege in _piegeList)
        {
            piege.enabled = false;
        }
    }
}