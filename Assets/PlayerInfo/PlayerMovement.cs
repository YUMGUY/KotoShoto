using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Body Factors")]
    public Rigidbody playerRB;
    public float playerSpeed;
    public bool isGrounded;
    [SerializeField]
    private float moveX;
    [SerializeField]
    private float moveZ;
    public Vector3 moveDirection;


    [Header("Player Dash Factors")]
    public float dashForce;
    public float jumpForce;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveInputs();
        //jump 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            playerRB.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }

       

        
    }

    private void FixedUpdate()
    {
        moveDirection = transform.forward * moveZ + transform.right * moveX;
        playerRB.AddForce(moveDirection.normalized * playerSpeed, ForceMode.Force);
        
    }

    public void PlayerMoveInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        
    }

    public void ClampVelocity()
    {

    }

    private void DashForward()
    {

    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision != null)
        {
            if(collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isGrounded = false;
            }
        }
    }
}
