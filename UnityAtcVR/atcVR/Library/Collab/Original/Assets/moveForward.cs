using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public class jsonDataFormat{

	public string planeLane;
	public int planeNumber;

}
public class moveForward : MonoBehaviour {

	int movementSpeed = 5;
	public addPlanes other;
	GameObject thePlane; 

	addPlanes planeScript; 
	int index;
	jsonDataFormat format = new jsonDataFormat();


	Vector3 startPoint, endPoint;
	float startTime;
	int buttonClickedVal = 0;
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
		string lane = null;
		WWW www = GET("https://atcvrservplanegrid-fbrkysyfnd.now.sh/lane");
		string json = www.text;
		Debug.Log (json);
		char[] delimiterChars = { ':',',','"','{','}'};
		string [] words = json.Split(delimiterChars);

		string laneVal = words[5];
		int planeVal = Int32.Parse(words [10]);
		Debug.Log (laneVal);
		Debug.Log (planeVal);
		lane = laneVal;
		string switchVal;
		if (buttonPressed ()) {
			switchVal = laneVal;
		}
		switchVal = laneVal;

		switch(switchVal) {
		case "a":
			laneA ();
			break;
		case "b":
			laneB ();
			break;
		case "c":
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

	bool buttonPressed(){
		return Input.GetMouseButtonDown(0);
	}

	// The "GET" method is apart because it could be called from many methods.
	public WWW GET (string url)
	{
		// Create the WWW object and provide the url of this web request.
		WWW www = new WWW (url);

		// Run the web call in the background.
		StartCoroutine (WaitForRequest (www));

		// Do nothing until the response is complete.
		while (!www.isDone) {


		}

		// Deliver the result to the method that called this one.
		return www;
	}

	// Run the web call and deliver the result as it is arriving.
	private IEnumerator WaitForRequest (WWW www)
	{
		yield return www;
	}
			

}
