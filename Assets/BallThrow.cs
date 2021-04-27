using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrow : MonoBehaviour
{
    private GameObject mainCamera;
    public GameObject ballPrefab;
    public float ballVelocity = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            // Spawn ball
            GameObject ball = GameObject.Instantiate<GameObject>(ballPrefab);

            // Set position and rotation to that of the camera
            ball.transform.position = mainCamera.transform.position;
            ball.transform.rotation = mainCamera.transform.rotation;
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

            // Add force to ball
            ballRigidbody.AddForce(ball.transform.forward * ballVelocity, ForceMode.Impulse);
        }
    }
}
