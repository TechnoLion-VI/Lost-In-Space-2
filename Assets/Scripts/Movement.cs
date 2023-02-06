using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float moveSpeed = 10.0f;
    public float runSpeed = 15f;
    public float jumpSpeed = 4.0f;

    // private Animator animator;
    private Transform _transform;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // animator = GetComponent<Animator>();
        _transform = GetComponent<Transform>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput,0,VerticalInput);
        float inputMagnitude = Mathf.Clamp01(movementDirection.magnitude);
        float speed = inputMagnitude * moveSpeed;
        movementDirection.Normalize();
        HandleMovement();
    }

    private void HandleMovement() {
        // Check if running
        // If holding down shift, run
        float speed = moveSpeed;

        // If the player is clicking shift
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = runSpeed;
        }


        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
		float deltaZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;

		_moveDirection = UnityEngine.Camera.main.transform.TransformDirection(new Vector3(deltaX, _moveDirection.y, deltaZ));
		// Don't move player up/down
		_moveDirection.y = 0f;

        // Check if grounded with raycast
        if (Physics.Raycast(_transform.position, Vector3.down, 1.1f) && Input.GetButton("Jump")) {
            _rigidbody.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
        }

        // Prevent player from clipping through walls
        if (Physics.Raycast(_transform.position, _moveDirection, 1.1f)) {
            _moveDirection = Vector3.zero;
        } else {
            _moveDirection = _moveDirection.normalized * moveSpeed;
        }

        _transform.position += _moveDirection * Time.deltaTime;


    }
}