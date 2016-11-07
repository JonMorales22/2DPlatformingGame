﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CatchScript : MonoBehaviour {

	private PlayerStats stats;
	// Use this for initialization
	void Start () {
		stats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D c){
		Debug.Log ("poo");
		if (c.gameObject.CompareTag ("Player")) {
			stats = c.gameObject.GetComponent<PlayerStats> ();

			Scene scene = SceneManager.GetActiveScene (); 
			SceneManager.LoadScene (scene.buildIndex);
		}
		else
		{
			Destroy (c.gameObject);
		}
	}
}
