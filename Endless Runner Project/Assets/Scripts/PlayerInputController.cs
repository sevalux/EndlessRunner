using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerInputController : MonoBehaviour
{
    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;
    public PlayerMotor motor;

    // private void Start() { StartCoroutine(CheckHorizontalSwipes()); Debug.Log("CHECKING SWIPES"); }

    private void Awake()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        Swipe();
        // Gather input on which lane we should be on
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            motor.MoveLane(false);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            motor.MoveLane(true);
        }
    }

    private void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                // Get initial touch position in 2D space
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }
            if (t.phase == TouchPhase.Ended)
            {
                // Get last touch position in 2D space
                secondPressPos = new Vector2(t.position.x, t.position.y);

                // Create Vector using initial and final touch positions.
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Normalize the Vector
                currentSwipe.Normalize();

                // Check to see if the Vector is an input.
                Debug.Log("Current Swipe: " + currentSwipe.x + " " + currentSwipe.y);
                if (currentSwipe.y > 0f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    // JUMP
                    Debug.Log("Up Swipe");
                }
                else if (currentSwipe.y < 0f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    // SLIDE
                    Debug.Log("Down Swipe");
                }
                else if (currentSwipe.x < 0f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    motor.MoveLane(false); // MOVE LEFT
                    Debug.Log("Left Swipe");
                }
                else if (currentSwipe.x > 0f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    motor.MoveLane(true); // MOVE RIGHT
                    Debug.Log("Right Swipe");
                }
                else
                {
                    Debug.Log("Bad Swipe"); // Swipe wasn't in any particular direction.
                }
                
            }
        }
    }
}