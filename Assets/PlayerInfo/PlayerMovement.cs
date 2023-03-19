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
    public AnimationCurve forceCurve;


    [Header("Player Dash Factors")]
    public int numDashes;
    public bool isDashing;
    public float dashForce;
    public float jumpForce;
    public float suspendedAirTime;
    public float dashDuration;
    public MissleMode missleModeRef;
    public AnimationCurve curve;


    // Start is called before the first frame update
    void Start()
    {
        numDashes = 1;
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
            StopAllCoroutines();
            DashForward();
        }
        if (isDashing) { return; }
        PlayerMoveInputs();
        ClampVelocity();
        //jump 
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            //print("jump");
            playerRB.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        }



    }

    private void FixedUpdate()
    {  
       moveDirection = orientation.forward * moveZ + orientation.right * moveX;         
        //else
        //{
        //    moveDirection = orientation.up * moveZ + orientation.right * moveX;

        //}
            
        playerRB.AddForce(moveDirection.normalized * playerSpeed /** 10.0f*/, ForceMode.Force);
        // cap speed
    }

    public void PlayerMoveInputs()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");
        
    }

    public void ClampVelocity()
    {
        Vector3 rawVel = new Vector3(playerRB.velocity.x, 0, playerRB.velocity.z);
        if(rawVel.magnitude > playerSpeed)
        {
            Vector3 newVel = rawVel.normalized * playerSpeed;
            playerRB.velocity = new Vector3(newVel.x, playerRB.velocity.y, newVel.z);
        }

        //print(playerRB.velocity.magnitude);
    }

    private void DashForward()
    {
        if(missleMode == false) { return; }
        print("dashed");
        StartCoroutine(DashingProcess());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && isGrounded == false && missleModeRef.isSpinning == true)
        {
            print("SMASH");

            // later on use animationcurve to determine level of force generated
            float forceGenerated = collision.relativeVelocity.y * .2f;
            forceGenerated = Mathf.Abs(Mathf.Clamp(forceGenerated, missleModeRef.minShake,missleModeRef.maxShake));
            print(forceGenerated);
            missleModeRef.shaker.GenerateImpulse(forceGenerated);
        }
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
        isDashing = true;
        playerRB.useGravity = false;

        Vector3 originalVel = playerRB.velocity;
        playerRB.velocity = Vector3.zero;
      
        float originalDrag = playerRB.drag;
        playerRB.drag = 0;
        RaycastHit Hitinfo;
        Vector3 result = Camera.main.transform.forward;
       // Vector3 dashLength;
        Vector3 maxDistance = Vector3.zero;
        bool hitGround = false;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out Hitinfo, 100.0f))
        {
            print("trigger or hard collider was hit");
            result = Hitinfo.point - transform.position;
            result = result.normalized;
            if(Hitinfo.collider.tag == "Ground")
            {
                print("hit the ground");
                hitGround = true;
                maxDistance = Hitinfo.point - transform.position; // use hitinfo to fix ground
            }  
        }

        float timer = 0;
        Vector3 startPos = transform.position;
       // Vector3 vel = Vector3.zero;
        
        while (timer < dashDuration)
        {
            if(!hitGround)
            playerRB.MovePosition(Vector3.Lerp(startPos, startPos + (dashForce * result), curve.Evaluate(timer/dashDuration)));
            else 
            { 
                if(maxDistance.magnitude <= (dashForce*result).magnitude)
                {
                    playerRB.MovePosition(Vector3.Lerp(startPos, startPos + .95f * maxDistance, curve.Evaluate(timer / dashDuration)));
                }
                else
                {
                    print("not too close to ground");
                    playerRB.MovePosition(Vector3.Lerp(startPos, startPos + (dashForce * result), curve.Evaluate(timer / dashDuration)));
                }
                
            }
            timer += Time.deltaTime;
            yield return null;
        }


        playerRB.velocity = Vector3.zero; // remove any momentum 

       // playerRB.AddForce((dashForce * result), ForceMode.Impulse);
        //yield return new WaitForSeconds(1f);
        //playerRB.velocity = Vector3.zero;

        //yield return new WaitForSeconds(1f);

       // Vector3 startVel = playerRB.velocity;
        //while(timer < .5f)
        //{
        //    playerRB.velocity = Vector3.Lerp(startVel, Vector3.zero, timer / .5f);
        //    timer += Time.deltaTime;
        //    yield return null;
        //}
        playerRB.useGravity = true;
      
        // yield return new WaitForSeconds(airTime);
        playerRB.drag = originalDrag;
        yield return new WaitForSeconds(suspendedAirTime);
        isDashing = false;
        moveX = 0;
        moveZ = 0;
        moveDirection = Vector3.zero;
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
       // Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(testRotation_x,testRotation_y,testRotation_z) * transform.forward * 5);
    }
}
