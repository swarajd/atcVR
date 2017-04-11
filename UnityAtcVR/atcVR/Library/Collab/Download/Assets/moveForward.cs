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

	public bool assignedLane = false;

	// Use this for initialization
	void Start () {
		thePlane = GameObject.Find("Plane");

		planeScript = thePlane.GetComponent<addPlanes>();
		index = planeScript.queue.Count;

		startPoint = transform.position;
		startTime = Time.time;
		endPoint = new Vector3 (0, 0, 0);
	}

	// Update is called once per frame
	void Update () {
		if (name == "initPlane")
			return;

		if (transform.position.x <= -40) {
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

	public void laneA() {
		endPoint = new Vector3 (15, 0, 20);
		assignedLane = true;
	}
	public void laneB() {
		endPoint = new Vector3 (15, 0, 0);
		assignedLane = true;
	}
	public void laneC() {
		endPoint = new Vector3 (15, 0, -20);
		assignedLane = true;
	}

	void land() {
		transform.position = Vector3.Lerp (transform.position, endPoint, (Time.time - startTime) / 0.1F);
	}
}

