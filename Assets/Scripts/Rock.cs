using UnityEngine;
using System.Collections;

// Controls the rock
public class Rock : Objects {

	// Initializes everything 
	void Awake() {
		mBody = this.GetComponent<Rigidbody> ();
	}

	// The rock is not held at first
	void Start() {
		this.gameObject.SetActive (false);
	}

	// Controls the physics of the rock
	void FixedUpdate() {

		// Holds the rock
		if (attachedWand && isHolding) {
			mBody.transform.position = attachedWand.transform.position;
		}

		// Applies a spin and horizontal push to the rock
		mBody.AddForce (mForce);
		mBody.AddTorque (mForce);
	}
		
	// Makes the rock the held object
	public override void hold(WandController wand) {
		holdSameHand ();
		attachedWand = wand;
	}

	// Throws the rock
	public override void fling(WandController wand) {

		// Only throw if the wand is interacting with the rock and not paused
		if (wand == attachedWand && !Pause_Menu.isPaused) {
			isHolding = false;

			mBody.velocity = wand.GetComponent<WandController> ().
				controller.velocity * Constants.ROCK_SPEED;
			mBody.angularVelocity = wand.GetComponent<WandController> ().
				controller.angularVelocity;
		}
	}

	// Controls collisions
	void OnCollisionEnter (Collision collision) {

		// If the object hits the ground, reset the score, teleport the object 
		// back to the player and randomize the horizontal force
		if (collision.gameObject.tag != Constants.BUCKET && 
			collision.gameObject.tag != Constants.OBJECT) {

			mForce = randForce ();
			hold (attachedWand);
		}
	}
}
