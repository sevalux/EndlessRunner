using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMotor : MonoBehaviour {

	private CharacterController controller;
    private PlayerInputController input;

    [SerializeField] private float laneDistance = 3.0f; //Length of the assets.
	[SerializeField] private float jumpForce = 4.0f;
	[SerializeField] private float gravity = 12.0f;
	[SerializeField] private float speed = 7.0f;

	private int desiredLane = 1; // 0 = left, 1 = middle, 2 = right
	private float verticalVelocity;
    private bool jumpInput = false;
    private bool slideInput = false;


    private void Awake() { controller = GetComponent<CharacterController>(); }

    private void Update()
    {
        // Calculate where we should be.
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
            targetPosition += Vector3.left * laneDistance;
        else if (desiredLane == 2)
            targetPosition += Vector3.right * laneDistance;


        // Calculate move delta
        Vector3 moveVector = Vector3.zero;

        // Where we should be, minus where we are to give us where we need to go to get there.
        moveVector.x = (targetPosition - transform.position).normalized.x * speed;


        // Calculate Y
        if (IsGrounded())
        {

            // If we are grounded, apply small gravity and allow jump.
            verticalVelocity = -0.1f; // Static small gravity to keep player grounded.
            if (Input.GetKeyDown(KeyCode.Space) || jumpInput)
            {

                // Jump
                verticalVelocity = jumpForce;
                jumpInput = false;
            }
        }
        else
        {

            // Apply strong gravity over time
            verticalVelocity -= gravity * Time.deltaTime;

            // Fast fall mechanic / slide aswell
            if (Input.GetKeyDown(KeyCode.Space) || slideInput)
            {

                verticalVelocity = -jumpForce;
                slideInput = false;
            }
        }


        moveVector.y = verticalVelocity;


        // To move the player forward
        // moveVector.z = speed;

        // Move player
        controller.Move(moveVector * Time.deltaTime);
    }

	public void MoveLane(bool goingRight){

		// Switch lanes based on bool
		desiredLane += (goingRight) ? 1 : -1;
		
		// Clamp our movement
		desiredLane = Mathf.Clamp(desiredLane, 0 ,2);
	}

    public void Jump()
    {
        // Game will jump on next frame
        jumpInput = true;
    }

    public void Slide()
    {
        // Game will slide on next frame
        slideInput = true;
    }

    private bool IsGrounded(){
		
		Ray groundRay = new Ray(
			new Vector3(
				controller.bounds.center.x,
				(controller.bounds.center.y - controller.bounds.extents.y) + 0.2f,
				controller.bounds.center.z), Vector3.down);
		Debug.DrawRay(groundRay.origin, groundRay.direction, Color.magenta, 1.0f);

		return (Physics.Raycast(groundRay, 0.2f + 0.1f));
	}
}
