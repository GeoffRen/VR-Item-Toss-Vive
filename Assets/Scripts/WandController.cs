using UnityEngine;
using System.Collections;

// Controls wand behavior
public class WandController : MonoBehaviour {

	// The grip button
	private Valve.VR.EVRButtonId gripButton = 
		Valve.VR.EVRButtonId.k_EButton_Grip;

	// The trigger button
	private Valve.VR.EVRButtonId triggerButton = 
		Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;

	// The wand
	public SteamVR_Controller.Device controller 
	{ get { return SteamVR_Controller.Input((int)trackedObj.index); } }

	// The wand
	private SteamVR_TrackedObject trackedObj;

	// The pause menu
	private GameObject mPause_Menu;

	// The ball/rock
	private Objects mItem;

	// Initializes everything
	void Awake() {
		mPause_Menu = GameObject.Find(Constants.PAUSE_MENU);

		// Sets the ball to be the current item
		Objects tmp = GameObject.Find (Constants.BALL).GetComponent<Sphere> ();
		mItem = tmp;

		// Makes sure that only one wand is holding the ball and rock
		if (!tmp.isHolding) {
			tmp.hold (this);
		}

		tmp = GameObject.Find (Constants.ROCK).GetComponent<Rock> ();
		if (!tmp.isHolding) {
			tmp.hold (this);
		}

		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Controls button interaction
	void Update () {

		// Pressing the grip button pauses the game
		if (controller.GetPressDown(gripButton)) {
			Pause_Menu.disableGame ();	
			mPause_Menu.SetActive (true);
		}

		// Pressing the trigger button throws or holds the item
		if (controller.GetPressDown (triggerButton) && mItem) {
			if (mItem.isHolding) {
				mItem.fling (this);

			} else {
				mItem.hold (this);
			}
		}
	}

	// Switches the current item to be the rock 
	// Called when the rock is selected in the object selection menu
	public void rockSwitch() {
		mItem = GameObject.Find (Constants.ROCK).GetComponent<Rock> ();
	}

	// Switches the current item to be the ball 
	// Called when the ball is selected in the object selection menu
	public void ballSwitch() {
		mItem = GameObject.Find (Constants.BALL).GetComponent<Sphere> ();
	}
}

