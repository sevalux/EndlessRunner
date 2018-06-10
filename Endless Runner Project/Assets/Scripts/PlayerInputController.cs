using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerInputController : MonoBehaviour {

	public PlayerMotor motor;

	private void Awake() {
		motor = GetComponent<PlayerMotor>();
	}
	
	private void Update () {
		
		// Gather input on which lane we should be on
		if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)){
			motor.MoveLane(false);
		}
		
		if(Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)){
			motor.MoveLane(true);
		}
	}
}
