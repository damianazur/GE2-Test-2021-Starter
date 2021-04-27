using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Active:State
{
    public override void Think()
    {
        if (owner.GetComponent<Dog>().targetBall == null)
        {
            owner.ChangeState(new WaitForBall());
            return;
        }
    }
}

public class FetchBall: State
{
    public override void Enter()
    {
        GameObject targetBall = owner.GetComponent<Dog>().targetBall;

        owner.GetComponent<Seek>().targetGameObject = targetBall;
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        GameObject targetBall = owner.GetComponent<Dog>().targetBall;
        Vector3 targetPos = targetBall.transform.position;
        GameObject ballAttach = owner.transform.Find("dog").transform.Find("ballAttach").gameObject;

        float distToBall = Vector3.Distance(ballAttach.transform.position, targetPos);

        // Change to arrive when within certain distance of ball
        if (distToBall < 15.0f) {
            owner.GetComponent<Arrive>().targetGameObject = targetBall;
            owner.GetComponent<Arrive>().enabled = true;
            owner.GetComponent<Seek>().enabled = false;
        }
        
        // When ballAttach is almost touching ball then pick up the ball
        if (distToBall < 0.5f) {
            targetBall.transform.SetParent(ballAttach.transform);
            targetBall.transform.localPosition = new Vector3(targetBall.transform.localPosition.x, 0, targetBall.transform.localPosition.z);

            targetBall.GetComponent<Rigidbody>().isKinematic = true;
            targetBall.GetComponent<Rigidbody>().useGravity = false;

            owner.ChangeState(new ReturnToOwner());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Arrive>().enabled = false;
        owner.GetComponent<Seek>().enabled = false;
    }    
}

public class ReturnToOwner: State
{
    public override void Enter()
    {
        // Set owner as the seek target
        GameObject returnOwner = owner.GetComponent<Dog>().returnOwnerCamera;
        owner.GetComponent<Seek>().targetGameObject = returnOwner;
        owner.GetComponent<Seek>().enabled = true;
    }

    public override void Think()
    {
        float dropBallDistance = 10.0f;

        GameObject targetBall = owner.GetComponent<Dog>().targetBall;
        GameObject returnOwnerCamera =  owner.GetComponent<Dog>().returnOwnerCamera;
        Vector3 targetPos = targetBall.transform.position;
        float distToBall = Vector3.Distance(returnOwnerCamera.transform.position, targetPos);
        
        // Drop the ball
        if (distToBall < dropBallDistance) {
            targetBall.transform.SetParent(null);
            targetBall.GetComponent<Rigidbody>().isKinematic = false;
            targetBall.GetComponent<Rigidbody>().useGravity = true;
            owner.GetComponent<Seek>().enabled = false;
        }
    }

    public override void Exit()
    {
        
    }
}

public class WaitForBall: State
{
    public override void Enter()
    {
       
    }

    public override void Think()
    {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Ball");
        if (balls.Length != 0) {
            owner.GetComponent<Dog>().targetBall = balls[0];
            Debug.Log("ball" + balls[0].tag);
            owner.ChangeState(new FetchBall());
        }
    }

    public override void Exit()
    {
        
    }
}
