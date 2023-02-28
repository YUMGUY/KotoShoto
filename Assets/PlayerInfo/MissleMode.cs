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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && processActivated == false)
        {
            print("Entering Missle Mode");
            StartCoroutine(MissleModeActivate());
            processActivated = true;
        }
    }


    public IEnumerator MissleModeActivate()
    {
        print("co");
        //transform.rotation = Quaternion.Euler(90, 0, 0);
        //turningFactor.enabled = false;
        //turningFactor.turnV = Vector2.zero;
        //cam1.Priority = 0;
        //cam2.Priority = 1;
       // camTarget.transform.localPosition = new Vector3(0, -5, 0f);
      //  camTarget.transform.localRotation = Quaternion.Euler(-90, 0, 0);
       // cam1.Follow = camTarget.transform;
       
        yield return new WaitForSeconds(.1f);

        turningFactor.enabled = true;
        yield return null;
        processActivated = false;
    }
}
