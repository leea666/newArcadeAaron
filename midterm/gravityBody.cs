using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent (typeof (Rigidbody))]
public class gravityBody : MonoBehaviour {

//	gravityAttractor planet ;
//
//	void Awake() {
//		planet = GameObject.FindWithTag ("Planet").GetComponent<gravityAttractor>();
//		GetComponent<Rigidbody>().useGravity = false;
//		GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
//	}
//
//	void FixedUpdate() {
//		planet.Attract (transform);
//	}

	gravityAttractor planet;
	Rigidbody rigidbody;

	void Awake () {
		planet = GameObject.FindWithTag("Planet").GetComponent<gravityAttractor>();
		rigidbody = GetComponent<Rigidbody> ();

		// Disable rigidbody gravity and rotation as this is simulated in GravityAttractor script
		rigidbody.useGravity = false;
		rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
	}

	void FixedUpdate () {
		// Allow this body to be influenced by planet's gravity
		planet.Attract(rigidbody);
	}

}
