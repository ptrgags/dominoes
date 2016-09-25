using UnityEngine;
using System.Collections;

/**
 * Chain of dominoes in a straight line
 */
public class DominoChain : MonoBehaviour {
	/** The prefab that represents a Domino */
	public GameObject domino_prefab;
	/** The material for coloring the dominoes in this chain */
	public Material domino_material;
	/** Number of dominoes in the chain */
	public int num_dominoes = 10;
	/** Spacing between each dominoes */
	public float spacing = 0.2f;

	// Use this for initialization
	void Start () {
		//The first domino's center is the same as the parent
		Vector3 first_domino_center = gameObject.transform.position;

		//Each domino is rotated the same as the parent
		Quaternion rotation = gameObject.transform.rotation;

		//Create dominoes in a straight line
		for (int i = 0; i < num_dominoes; i++) {
			//Create a new child GameObject from the Domino prefab.
			Vector3 pos = new Vector3(i * spacing, 0, 0);
			GameObject domino = (GameObject)Instantiate(domino_prefab, first_domino_center + rotation * pos, rotation);

			//Mark the domino as a child of the domino chain
			domino.transform.parent = gameObject.transform;

			//set the child domino's material to match the one specified 
			//for the chain.
			domino.GetComponent<Renderer>().material = domino_material;
		}
	}

	void OnDrawGizmos() {
		//The first domino's center is the same as the parent
		Vector3 first_domino_center = gameObject.transform.position;

		//All dominoes are rotated the same as the parent
		Quaternion rotation = gameObject.transform.rotation;

		//Get the domino dimensions from the prefab
        Vector3 domino_dims = domino_prefab.GetComponent<Domino>().Dimensions;

		//Vector that represents the distance between each pair of dominoes
		Vector3 delta = rotation * (new Vector3(spacing, 0, 0));

		//Get the position of the second and last dominoes
		Vector3 second_domino_center = first_domino_center + delta;
		Vector3 last_domino_center = first_domino_center + (num_dominoes - 1) * delta;

		//Draw the first, second and last dominoes to show the length of the domino
		//chain and the spacing.
		Gizmos.color = domino_material.color;
		DrawBox(first_domino_center, rotation, domino_dims);
		DrawBox(second_domino_center, rotation, domino_dims);
		DrawBox(last_domino_center, rotation, domino_dims);
		Gizmos.DrawLine(first_domino_center, last_domino_center);
	}

	/**
	 * Draw a wireframe box Gizmo with rotation
	 */
	void DrawBox(Vector3 position, Quaternion rotation, Vector3 scale) {
		//Save the old matrix
		Matrix4x4 old_matrix = Gizmos.matrix;

		//Add a transformation to the graphics context
		Matrix4x4 new_matrix = Matrix4x4.TRS(position, rotation, scale);
		Gizmos.matrix *= new_matrix;

		//Draw the wireframe box
		Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

		//Restore the old matrix
		Gizmos.matrix = old_matrix;
	}
}
