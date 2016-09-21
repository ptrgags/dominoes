using UnityEngine;
using System.Collections;

public class Catalyst : MonoBehaviour {
	public Vector3 thrust = new Vector3(0, 0, 0);

	void Start() {
		Rigidbody body = (Rigidbody)GetComponent<Rigidbody>();
		body.AddForce(thrust);
	}
}
