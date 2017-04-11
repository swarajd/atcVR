using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addPlanes : MonoBehaviour {

	private GameObject s;
	private ArrayList myNodes;
	public Queue queue = new Queue();


	public int score;
	float timer = 0f;

	void Start ()
	{
		myNodes = new ArrayList();
		queue = new Queue();
		score = 0;
		create ();
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (timer >= 8) {
			create ();
		}
		Debug.Log (score);
	}

	void create() {
		GameObject iS = GameObject.Find ("initPlane");
		iS.GetComponent<Renderer> ().enabled = true;
		s = Instantiate (iS);//, new Vector3 (queue.Count*1000 + 1000, 3000F, 2000F), Quaternion.identity);
		//s = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		s.transform.position = new Vector3 (100F, 20F, 0F);
		s.transform.Rotate (0, -90, 0);
		s.name = "Plane" + queue.Count;

		GameObject textObj = GameObject.Find ("PlaneCount");
		TextMesh textMesh = textObj.GetComponent<TextMesh> ();
		textMesh.text = "Plane " + (queue.Count + 1) + " just arrived";
		/*
		GameObject scoreObj = GameObject.Find ("ScoreText");
		TextMesh scoreMesh = scoreObj.GetComponent<TextMesh> ();
		scoreMesh.text = "Score: " + score;*/


		queue.Enqueue (s);
		timer = 0f;
		iS.GetComponent<Renderer> ().enabled = false;
		System.Random rand = new System.Random ();
	}
}
