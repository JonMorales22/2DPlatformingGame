﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	[HideInInspector]
	public bool isFacingRight = true;
	[HideInInspector]
	public bool isJumping = false;
	[HideInInspector]
	public bool isGrounded = false;

	public float walkSpeed=7.0f;
	public float maxSpeed=14.0f;
	public float jumpForce = 750.0f; 
	public int health;
	public int score = 0;

	public  Image[] heartArray = new Image[3];

	public Transform groundCheck;
	public LayerMask groundLayers;

	public Animator anim;
	public AudioClip[] audioArray = new AudioClip[4];
	public AudioClip footstep;
	public AudioClip deathsound;
	public AudioClip damage;

	private float groundCheckRadius = .2f;
	private AudioSource audiosource;
	private bool isDead = false;
	private Rigidbody2D player;
	private bool jumpState = false;
	// Use this for initialization
	void Awake(){
		audiosource = this.GetComponent<AudioSource>();
		anim = GetComponent<Animator> ();
		SetAnimationController (0);
		player = GetComponent<Rigidbody2D> ();

	}

	public void playFootStep()
	{
		if (isGrounded) {
			audiosource.clip = audioArray[0];
			audiosource.Play ();
		}
	}
	public void playDamageSound()
	{
		audiosource.clip = audioArray[1];
		audiosource.Play ();
	}
	void playDeathSound()
	{
		AudioSource.PlayClipAtPoint (audioArray[2],this.transform.position);
	}
	void playJumpSound()
	{
		
			audiosource.clip = audioArray [3];
			audiosource.Play ();	
	
	}

	void Update(){
		if (Input.GetKey(KeyCode.Space)) {
			if (isGrounded&&!isDead) {
				player.velocity = new Vector2 (player.velocity.x, jumpForce);
				//player.AddForce = new Vector2 (0, jumpForce);
				anim.SetTrigger ("Jump"); 
			}
		}
		//anim.SetBool ("isGrounded", isGrounded);
	
	}

	void FixedUpdate () {
		if (!isDead) {
			
			isGrounded = Physics2D.OverlapCircle (groundCheck.position + new Vector3 (0, -0.5f, 0), groundCheckRadius, groundLayers);


			float move = Input.GetAxis ("Horizontal");
			player.velocity = new Vector2 (move * walkSpeed, player.velocity.y);
			
			if ((move > 0.0f && !isFacingRight) || (move < 0.0 && isFacingRight)) {
				Flip ();
			}

			if (Input.GetKeyDown (KeyCode.Escape)) {
				SceneManager.LoadScene (2);
			}

			float vel = player.velocity.x;
			walkAnimation (Mathf.Abs (vel));
		}
	}

	void Flip()
	{
		isFacingRight = !isFacingRight;
		Vector3 playerScale = transform.localScale;
		playerScale.x = playerScale.x*-1;
		transform.localScale = playerScale;

	}
	void ApplyDamage(){
		if (health > 0) {
			playDamageSound ();
			int index = health;
			index--;
			heartArray [index].color = new Color (0, 0, 0, 1);
			health = index;
			if (health == 0) {
				isDead = true;
				StartCoroutine ("Die");
			}
		}
	}
	void IncreaseScore(int num)
	{
		score += num;
	}

	void walkAnimation(float vel){
		anim.SetFloat ("Velocity", vel);

	}
	IEnumerator Die(){
		anim.SetBool ("isDead", true);
		isDead = true;
		playDeathSound ();
		yield return new WaitForSeconds (2.0f);
		PlayerPrefs.SetInt ("247127CurrentPlayerScore", score);
		SceneManager.LoadScene (2);
	}



	void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.CompareTag ("Enemy")) {
			ApplyDamage ();
		}
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.CompareTag ("Coin"))
		{
			IncreaseScore(100);
		}
	}

	void SetAnimationController(int num)
	{
		anim.SetInteger ("AnimationState", num);
	}
}
