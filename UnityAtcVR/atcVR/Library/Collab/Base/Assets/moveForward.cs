using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class moveForward : MonoBehaviour {

	int movementSpeed = 5;
	public addPlanes other;
	GameObject thePlane; 
	addPlanes planeScript; 
	int index;

	Vector3 startPoint, endPoint;
	float startTime;

	bool stopped = false;

	// Use this for initialization
	void Start () {
		//if (gameObject.name == "initPlane")
		//	Destroy (this);
		thePlane = GameObject.Find("Plane");
		planeScript = thePlane.GetComponent<addPlanes>();
		index = planeScript.queue.Count;

		startPoint = transform.position;
		startTime = Time.time;
		endPoint = new Vector3 (0, 0, 0);
		System.Random random = new System.Random();
		switch (random.Next (0, 3)) {
		case 0:
			laneA ();
			break;
		case 1:
			laneB ();
			break;
		case 2:
			laneC();
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		if (name == "initPlane")
			return;
		if (transform.position.y <= 1) {
			transform.position += Vector3.left * Time.deltaTime * movementSpeed;
		}
		else 
		if (transform.position.x <= 20) {
			transform.position = Vector3.Lerp (startPoint, endPoint, (Time.time - startTime) / 0.5F);
		} else {
			transform.position += Vector3.left * Time.deltaTime * movementSpeed;
		}
	}

	void laneA() {
		endPoint = new Vector3 (15, 0, 20);
	}
	void laneB() {
		endPoint = new Vector3 (15, 0, 0);
	}
	void laneC() {
		endPoint = new Vector3 (15, 0, -20);
	}
}
