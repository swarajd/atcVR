using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class moveForward : MonoBehaviour {

	int movementSpeed = 5;
	GameObject thePlane; 
	public addPlanes planeScript; 
	public int index;

	Vector3 startPoint, endPoint;
	float startTime;

	bool emergency = false;
	float emergencyTimer;

	public bool assignedLane = false;

	// Use this for initialization.
	void Start () {
		thePlane = GameObject.Find("Plane");

		planeScript = thePlane.GetComponent<addPlanes>();
		index = planeScript.queue.Count;

		startPoint = transform.position;
		startTime = Time.time;
		endPoint = new Vector3 (0, 0, 0);

		System.Random rand = new System.Random ();
		if (rand.Next (5) == 1) {
			emergency = true;
			emergencyTimer = 0F;
			Debug.Log ("Heart Attack on Plane" + index);
			Renderer renderer = gameObject.GetComponent<Renderer> ();
			Material newMat = Resources.Load("airplane_02", typeof(Material)) as Material;
			renderer.material = newMat;
		}
	}

	// Update is called once per frame
	void Update () {
		if (name == "initPlane")
			return;

		checkEmergency ();

		if (transform.position.x <= -40) {
			if (gameObject.GetComponent<Renderer> ().enabled) {
				planeScript.score += 10;
				if (emergency)
					planeScript.score += 20;
			}
			gameObject.GetComponent<Renderer> ().enabled = false;

		}
			
		if (transform.position.y <= 1) {
			transform.position += Vector3.left * Time.deltaTime * movementSpeed;
		} else if (transform.position.x <= 20) {
			land ();
		} else {
			transform.position += Vector3.left * Time.deltaTime * movementSpeed;
		}
	}

	void checkEmergency() {
		if (emergency) {
			emergencyTimer += Time.deltaTime;
			if (emergencyTimer >= 10) {
				Debug.Log ("Person Died on Plane" + index);
			}
		}
	}
	public void laneA() {
		endPoint = new Vector3 (15, 0, 20);
		assignedLane = true;
		if (emergency)
			land ();
	}
	public void laneB() {
		endPoint = new Vector3 (15, 0, 0);
		assignedLane = true;
		if (emergency)
			land ();
	}
	public void laneC() {
		endPoint = new Vector3 (15, 0, -20);
		assignedLane = true;
		if (emergency)
			land ();
	}

	void land() {
		if(transform.position.y >=10)
			transform.position = Vector3.Lerp (transform.position, endPoint, (Time.time - startTime) / 0.1F);
	}


}

