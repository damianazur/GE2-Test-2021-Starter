using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmonicTailWagging : MonoBehaviour
{
    public float baseFrequency = 2;
    public float amplitude = 10;
    public float theta = 0;
    // Start is called before the first frame update
    void Start()
    {
        theta = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Tail wagging is increased with the velocity of the dog
        Vector3 dogVelocity = transform.parent.gameObject.GetComponent<Boid>().velocity;

        float angle = Mathf.Sin(theta) * amplitude;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.up);
        transform.localRotation = q;
        theta += Mathf.PI * 2.0f * Time.deltaTime * baseFrequency * dogVelocity.magnitude;
    }
}
