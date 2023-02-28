using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Body Factors")]
    public Rigidbody playerRB;
    public float playerSpeed;
    public bool isGrounded;
    public bool missleMode;
    public Transform orientation;
    [SerializeField]
    private float moveX;
    [SerializeField]
    private float moveZ;
    public Vector3 moveDirection;
    public float testRotation_x;
    public float testRotation_y;
    public float testRotation_z;


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
        if (Input.GetKey(KeyCode.E))
        {
            missleMode = true;
        }

        if(Input.GetKeyDown(KeyCode.B))
        {
            DashForward();
        }
       // PlayerMoveInputs();
        //jump 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            print("jump");
            playerRB.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }

        



    }

    private void FixedUpdate()
    {
        if (missleMode == false)
        {
            moveDirection = orientation.forward * moveZ + orientation.right * moveX;
        }
            
        else
        {
            moveDirection = orientation.up * moveZ + orientation.right * moveX;

        }
            
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
        if(missleMode == false) { return; }
        print("hit");
        StartCoroutine(DashingProcess());
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


    private IEnumerator DashingProcess()
    {
        playerRB.useGravity = false;
        RaycastHit Hitinfo;
        Vector3 result = Camera.main.transform.forward;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out Hitinfo, 100.0f))
        {
            print("trigger was hit");
            result = Hitinfo.point - transform.position;
            result = result.normalized;
            
        }
        playerRB.AddForce((dashForce * result), ForceMode.Impulse); 


        //yield return new WaitForSeconds(1f);
        //playerRB.velocity = Vector3.zero;
        yield return null;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(testRotation_x,testRotation_y,testRotation_z) * transform.forward * 5);
    }
}
