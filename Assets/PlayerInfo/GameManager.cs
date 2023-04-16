using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;
    public int numDashes;
    public GameObject playerRef;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDash()
    {
        numDashes++;
    }
    public void AddPlayerDamage()
    {

    }

    public void AddPlayerSpeed()
    {

    }
}
