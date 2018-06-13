using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour {

	[SerializeField] private List<Transform> lanes = new List<Transform>();
	[SerializeField] private List<GameObject> obstacles = new List<GameObject>();
	[SerializeField] private float spawnRate = 1.2f;


	private void Start(){
		StartCoroutine(SpawnObject());
	}

	IEnumerator SpawnObject(){

		yield return new WaitForSeconds(spawnRate);
		Instantiate(RandomObject(), RandomLane().position, Quaternion.identity );

		StartCoroutine(SpawnObject());
	}

	public GameObject RandomObject(){
		
		int i = Random.Range(0,obstacles.Count);
		print(obstacles[i]);
		
		return obstacles[i];
	}

	public Transform RandomLane(){
		
		int i = Random.Range(0,3); // Since there will 3 lanes
		
		return lanes[i];
	}
	
}
