using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog : MonoBehaviour
{
    public GameObject targetBall;

    public GameObject returnOwnerCamera;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<StateMachine>().ChangeState(new Active());        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
