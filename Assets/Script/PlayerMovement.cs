using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed;
    
    private Rigidbody2D _rb;
    //private Vector2 _nextPos;
    private Vector2 _horizontalMove;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        if (!isLocalPlayer)
        {
            var camera = transform.Find("Main Camera");
            camera.GetComponent<Camera>().enabled = false;
            camera.GetComponent<AudioListener>().enabled = false;
            //I also disabled this component so it also doesn't move the other player
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalMove = Vector2.right * (Input.GetAxis("Horizontal") * speed/100);
        
        /*float horizontalMove = Input.GetAxis("Horizontal") * speed;
        _rb.velocity = Vector2.right*horizontalMove; */
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _horizontalMove);
    }
}
