using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    public Vector2 turnV;
    public float mouseSens;
    public PlayerMovement playerMove;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        turnV.x += Input.GetAxis("Mouse X") * mouseSens;
        turnV.y += Input.GetAxis("Mouse Y") * mouseSens;

        Vector3 mp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       // print(mp);
        //turnV.x = Mathf.Clamp(turnV.x, -90, 90);
        turnV.y = Mathf.Clamp(turnV.y, -90, 90);
    }

    private void FixedUpdate()
    {
        if(playerMove.missleMode == false)
        {
            transform.localRotation = Quaternion.Euler(-turnV.y, turnV.x, 0);
    
        }
        else
        {
            transform.localRotation = Quaternion.Euler(-turnV.y, turnV.x, 0);
         
        }
    }
}
