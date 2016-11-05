using UnityEngine;
using System.Collections;

// Controls the ball
public class Sphere : Objects {

	// Initializes everything 
	void Awake() {
		mBody = this.GetComponent<Rigidbody> ();
	}

	// The ball is held at first
	void Start() {
		Physics.gravity = Constants.BALL_GRAVITY;
	}

	// Controls the physics of the ball
	void FixedUpdate() {

		// Holds the ball
		if (attachedWand && isHolding) {
			mBody.transform.position = attachedWand.transform.position;
		}

		// Applies a spin and horizontal push to the ball
		mBody.AddForce (mForce);
		mBody.AddTorque (mForce);
	}

	// Makes the ball the held object
	public override void hold(WandController wand) {
		holdSameHand ();
		attachedWand = wand;
	}

	// Throws the ball
	public override void fling(WandController wand) {

		// Only throw if the wand is interacting with the ball and not paused
		if (wand == attachedWand && !Pause_Menu.isPaused) {
			isHolding = false;

			mBody.velocity = wand.GetComponent<WandController> ().
				controller.velocity * Constants.BALL_SPEED;
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
