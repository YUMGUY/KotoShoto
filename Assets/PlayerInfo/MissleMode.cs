using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleMode : MonoBehaviour
{
    public GameObject playerMissle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E))
        {
            print("Entering Missle Mode");
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
    }
}
