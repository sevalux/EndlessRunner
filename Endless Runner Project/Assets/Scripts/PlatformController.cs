using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

	[SerializeField] private float speed;
	[SerializeField] private float resetPosition = -61f;

	[SerializeField] private float startPosition = 120f;


	// Update is called once per frame
	private void Update () {
		transform.Translate(Vector3.back * (speed * Time.deltaTime));

		float length = transform.localScale.z;
		if(transform.localPosition.z <= resetPosition){
			Vector3 newPos = new Vector3(transform.position.x, transform.position.y, startPosition);
			transform.position = newPos; 
		}
	}
		
}
