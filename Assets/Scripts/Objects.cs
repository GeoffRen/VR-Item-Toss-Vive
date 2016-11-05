using UnityEngine;
using System.Collections;

// Base class for the ball and rock
public abstract class Objects : MonoBehaviour {

	// Whether the object is being held or not
	public bool isHolding;

	// The object's rigidbody
	protected Rigidbody mBody;

	// The horizontal force
	protected Vector3 mForce;

	// The wand interacting with the object 
	protected WandController attachedWand;

	// Makes the object the held object
	public abstract void hold(WandController wand);

	// Throws the object
	public abstract void fling(WandController wand);

	// Sets an object to have zero movement
	protected void setZero() {
		this.transform.rotation = Quaternion.identity;
		this.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}

	// Creates and displays a random force to the HUD
	protected Vector3 randForce() {

		// Displays the force
		Vector3 force = Vector3.right * Random.Range (-50, 50);
		HUD.set (force);

		return force;
	}

	// Holds the object
	public void holdSameHand() {
		setZero ();
		isHolding = true;
	}
}
