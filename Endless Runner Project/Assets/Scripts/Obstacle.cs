using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

	// Temporary logic

	[Header("Set to Platform Speed")]
	[SerializeField] private float speed;

	// Update is called once per frame
	private void Update () {
		transform.Translate(Vector3.back * (speed * Time.deltaTime));
	}
}
