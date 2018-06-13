using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {

	[Header("Player Object")]
	[SerializeField] private Transform target;

	[Header("Camera Settings")]

	[SerializeField] float smoothing = 5f;
	[SerializeField] float lookOffset = 0f;
	[SerializeField] float xOffset = 0f;

	private Vector3 offset;

	private void Start() {
		offset = transform.position - target.position;
	}

	private void Update() {
		Vector3 offsetPosition = new Vector3(target.position.x + currentLane(), target.position.y, target.position.z);
		Vector3 targetCamPos = offsetPosition + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing);

		transform.LookAt(target);
	}


	private float currentLane(){

		// 0,3f is extra room incase of 0.00123123 value on x axis
		if(target.position.x < -0.3f){
			lookOffset = xOffset;
		} else if (target.position.x > 0.3f){
			lookOffset = -xOffset;
		} else {
			lookOffset = 0;
		}
		return lookOffset;
	}
}
