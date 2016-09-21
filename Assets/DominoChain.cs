using UnityEngine;
using System.Collections;

public class DominoChain : MonoBehaviour {
	public GameObject domino_prefab;
	public int num_dominoes = 10;
	public float spacing = 0.2f;

	// Use this for initialization
	void Start () {
		Vector3 first_domino_center = gameObject.transform.position;
		Quaternion rotation = gameObject.transform.rotation;
		for (int i = 0; i < num_dominoes; i++) {
			Vector3 pos = new Vector3 (i * spacing, 0, 0);
			GameObject obj = (GameObject)Instantiate(domino_prefab, first_domino_center + rotation * pos, rotation);
			obj.transform.parent = gameObject.transform;
		}
	}

	void OnDrawGizmos() {
		Vector3 first_domino_center = gameObject.transform.position;
		Quaternion rotation = gameObject.transform.rotation;
		Vector3 last_offset = new Vector3 ((num_dominoes - 1) * spacing, 0, 0);
		Vector3 last_domino_center = first_domino_center + rotation * last_offset;
		Gizmos.color = Color.red;
		Gizmos.DrawSphere (first_domino_center, 0.1f);
		Gizmos.DrawLine (first_domino_center, last_domino_center);
		Gizmos.DrawSphere (last_domino_center, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
