using UnityEngine;
using System.Collections;

public class Catalyst : MonoBehaviour {
	public float thrust = 0;
	public Rigidbody body;

	void Start() {
		body = GetComponent<Rigidbody>();
		body.AddForce(0, 0, thrust);
	}
}
