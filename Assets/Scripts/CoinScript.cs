﻿using UnityEngine;
using System.Collections;

public class CoinScript : MonoBehaviour {
	//public GameObject coinGB;
	public int value = 100;
	public AudioClip coinSound;
	public void PlaySound()
	{
		AudioSource.PlayClipAtPoint (coinSound,transform.position);
	}
	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.CompareTag ("Player")) {

		}
	}
}
