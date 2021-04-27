using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BallThrow : MonoBehaviour
{
    private GameObject mainCamera;
    public GameObject ballPrefab;
    public float ballMaxVelocity = 20.0f;
    private float chargeValue = 0.0f;
    private float chargeTimeSec = 1.0f;
    public Slider slider;

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
        if (Input.GetKey("space"))
        {
            if (chargeValue < 1.0f) {
                chargeValue += Time.deltaTime * chargeTimeSec;
                slider.value = chargeValue;
            }
        }

        // When key is released throw the ball
        if (Input.GetKeyUp("space"))
        {
            // Spawn ball
            GameObject ball = GameObject.Instantiate<GameObject>(ballPrefab);

            // Set position and rotation to that of the camera
            ball.transform.position = mainCamera.transform.position;
            ball.transform.rotation = mainCamera.transform.rotation;
            Rigidbody ballRigidbody = ball.GetComponent<Rigidbody>();

            // Add force to ball
            ballRigidbody.AddForce(ball.transform.forward * ballMaxVelocity * chargeValue, ForceMode.Impulse);

            // Reset charge
            chargeValue = 0.0f;
            slider.value = 0.0f;
        }
    }
}
