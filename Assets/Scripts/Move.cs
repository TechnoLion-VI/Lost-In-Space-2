using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Get the player's transform
    private Transform _transform;
    private Rigidbody _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        // Get the player's transform
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move the player on input 
        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        // Rotate player on input
        float rotationInput = Input.GetAxis("Mouse X");

        // Rotate 
        Quaternion cameraRot = UnityEngine.Camera.main.transform.rotation;
        // Only rotate on the Y axis
        cameraRot.x = 0;
        cameraRot.z = 0;
        transform.rotation = cameraRot;

        // Move in direction the player is facing using a raycast
        Vector3 movementDirection = new Vector3(horizontalInput,0,VerticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        float speed = inputMagnitude * 10f;
        movementDirection.Normalize();
        _rigidbody.velocity = movementDirection * speed;

        // Check if grounded with raycast
        if (Physics.Raycast(_transform.position, Vector3.down, 1.1f) && Input.GetButton("Jump")) {
            _rigidbody.AddForce(Vector3.up * 4f, ForceMode.Impulse);
        }

        



    }
}
