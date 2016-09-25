using UnityEngine;
using System.Collections;

public class DominoWall : MonoBehaviour {
    //Prefab for the basic domino in this wall
    public GameObject domino_prefab;
    /** The material for coloring the dominoes in this chain */
    public Material domino_material;
    /** Number of dominoes along the ground */
    public int num_dominoes = 10;

	// Use this for initialization
	void Start () {
        //The first domino's center is the same as the parent
        Vector3 first_domino_center = gameObject.transform.position;

        //Dominoes along the ground need to be rotated 90 degrees
        Quaternion ground_rotation = Quaternion.Euler(90, 0, 0);

        Domino domino_template = domino_prefab.GetComponent<Domino>();
        float ground_spacing = domino_template.Height - domino_template.Thickness;

        for(int i = 0; i < num_dominoes; i++) {
            Vector3 pos = new Vector3(i * ground_spacing, 0, 0);
            GameObject domino = (GameObject)Instantiate(domino_prefab, first_domino_center + pos, ground_rotation);

            //Mark the domino as a child of the domino wall
            domino.transform.parent = gameObject.transform;

            //set the child domino's material to match the one specified 
            //for the chain.
            domino.GetComponent<Renderer>().material = domino_material;
        }
	}
	
    void OnDrawGizmos() {
        //The first domino's center is the same as the parent
        Vector3 first_domino_center = transform.position;

        //Get the domino dimensions from the prefab
        Domino domino_template = domino_prefab.GetComponent<Domino>();
        Quaternion ground_rotation = Quaternion.Euler(90, 0, 0);
        Vector3 domino_dims = domino_template.Dimensions;
        float ground_spacing = domino_template.Height - domino_template.Thickness;

        //Vector that represents the distance between each pair of dominoes
        Vector3 delta = new Vector3(ground_spacing, 0, 0);

        //Get the position of the second and last dominoes
        Vector3 second_domino_center = first_domino_center + delta;
        Vector3 last_domino_center = first_domino_center + (num_dominoes - 1) * delta;

        //Draw the first, second and last dominoes to show the length of the domino
        //chain and the spacing.
        Gizmos.color = domino_material.color;
        DrawBox(first_domino_center, ground_rotation, domino_dims);
        DrawBox(second_domino_center, ground_rotation, domino_dims);
        DrawBox(last_domino_center, ground_rotation, domino_dims);
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
