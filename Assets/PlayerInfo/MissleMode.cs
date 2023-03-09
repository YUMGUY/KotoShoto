using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MissleMode : MonoBehaviour
{
    public GameObject playerMissle;
    public GameObject camTarget;
    public MouseRotation turningFactor;
    public CinemachineVirtualCamera cam1;
    public CinemachineVirtualCamera cam2;
    public bool processActivated;

    [Header("Spinning Factors")]
    public float rotationSpeed;
    public float fallingSpeed;
    public bool isSpinning;
    private float rotationTimer;
    public AnimationCurve smoothRotCurve;
    

    [SerializeField]
    private Rigidbody playerRB;
    // Start is called before the first frame update
    void Start()
    {
        playerRB = transform.parent.GetComponent<Rigidbody>();
        isSpinning = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.E) && processActivated == false)
        //{
        //    print("Entering Missle Mode");
        //    StartCoroutine(MissleModeActivate());
        //    processActivated = true;
        //}

        if(Input.GetKey(KeyCode.R))
        {
            
            // smoothly begin rotation

            transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
            playerRB.AddForce(new Vector3(0.0f,-1 * fallingSpeed,0.0f),ForceMode.Acceleration);
            isSpinning = true;
        }
        else
        {
            isSpinning = false;
        }

        //if (Input.GetKey(KeyCode.F))
        //{
        //    print("Switching to vcam 2");
        //    cam1.gameObject.SetActive(false);
        //    cam2.gameObject.SetActive(true);
        //}
        //else
        //{
        //    print("Switching to vcam 1");
        //    cam1.gameObject.SetActive(true);
        //    cam2.gameObject.SetActive(false);
        //}
    }


    public IEnumerator MissleModeActivate()
    {
        print("missle mode activated");
       
        yield return new WaitForSeconds(.1f);

        turningFactor.enabled = true;
        yield return null;
        processActivated = false;
    }
}
