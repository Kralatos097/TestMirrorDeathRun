using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    
    private Rigidbody2D _rb;
    private bool _isGrounded = true;
    
    private static List<PlayerMovement> _playerList;

    private void Start()
    {
        _playerList ??= new List<PlayerMovement>();
        
        _rb = GetComponent<Rigidbody2D>();
        _playerList.Add(this);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        if (!isLocalPlayer)
        {
            var camera = transform.Find("Main Camera");
            camera.GetComponent<Camera>().enabled = false;
            camera.GetComponent<AudioListener>().enabled = false;
            transform.GetComponentInChildren<SpriteMask>().gameObject.SetActive(false);
        }
    }

    [Command]
    private void CDestroySelf()
    {
        DestroySelf();
    }
    
    [ClientRpc]
    private void DestroySelf()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isLocalPlayer || isServer) return;
        /*Vector2 horizontalMove = Vector2.right * (Input.GetAxis("Horizontal") * speed * Time.deltaTime);
        _rb.MovePosition(_rb.position + horizontalMove);*/
        /*float horizontalMove = Input.GetAxis("Horizontal") * speed;
        _rb.velocity = Vector2.right*horizontalMove;*/
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        _rb.velocity = new Vector2(horizontalInput * speed, _rb.velocity.y);
        
        if(Input.GetButtonDown("Jump") && _isGrounded)
        {
            Debug.Log("Jump");
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }
    
    public void Death()
    {
        if (isLocalPlayer)
        {
            Debug.Log("RIP");
            CDestroySelf();
            NetworkManager.singleton.StopClient();
            Application.Quit();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Sol"))
        {
            _isGrounded = true;
        }
    }

    public static void DeactivatePlayers()
    {
        foreach (PlayerMovement player in _playerList)
        {
            player.enabled = false;
        }
    }
}
