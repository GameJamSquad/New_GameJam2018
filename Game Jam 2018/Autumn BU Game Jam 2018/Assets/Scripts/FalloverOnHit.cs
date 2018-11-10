using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalloverOnHit : MonoBehaviour {

	private Rigidbody rb;
	private BoxCollider bc;

	void Start () {
		Debug.Log ("test");
		rb = GetComponent<Rigidbody> ();
		bc = GetComponent<BoxCollider>();
	}

	void OnCollisionEnter (Collision col)
	{
		StartCoroutine (WaitTime ());
	}

	IEnumerator WaitTime()
	{
		Debug.Log ("Gravity Enabled");
		rb.useGravity = true;
		yield return new WaitForSeconds(5);
		Debug.Log("Box Colider Destroyed");
		Destroy (bc);
		yield return new WaitForSeconds(2);
		Debug.Log("Object Destroyed");
		Destroy (gameObject);
	}
}
