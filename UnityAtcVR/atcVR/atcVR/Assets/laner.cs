using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class laner : MonoBehaviour {

	public GameObject thePlane;
	public moveForward moveScript;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		string lane = null;
		WWW www = GET("https://limitless-oasis-38724.herokuapp.com/lane");
		string json = www.text;
		char[] delimiterChars = { ':',',','"','{','}'};
		string [] words = json.Split(delimiterChars);

		string laneVal = words[5];
		int planeVal = Int32.Parse(words [10]);
		lane = laneVal;
		string switchVal;
		switchVal = laneVal;

		assign (planeVal, laneVal);

	}

	void assign(int plane, string lane) {

			string id = "Plane" + (plane-1);
			GameObject obj = GameObject.Find (id);
			if (obj == null) {
				return;
			}
			moveScript = obj.GetComponent<moveForward> ();
			selectLane (moveScript, lane);

		Debug.Log ("Lane Selected for Plane" + moveScript.index + "is Lane " + lane);
	}

	void selectLane(moveForward mF, string lane) {
		switch (lane) {
		case "a":
			mF.laneA ();
			break;
		case "b":
			mF.laneB ();
			break;
		case "c":
			mF.laneC();
			break;
		}
	}

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
