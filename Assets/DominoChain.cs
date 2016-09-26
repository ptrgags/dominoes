using UnityEngine;
using System.Collections;

/**
 * Chain of dominoes in a straight line
 */
public class DominoChain : DominoStructure {
	/** Number of dominoes in the chain */
	public int num_dominoes = 10;
	/** Spacing between each dominoes */
	public float spacing = 0.2f;

    /**
     * Turn the spacing parameter into 
     * a vector that points along the x-axis.
     */
    private Vector3 spacing_delta {
        get {
            return new Vector3(spacing, 0, 0);
        }
    }

    /**
     * Get the position of an arbitrary domino
     * in the chain.
     */
    private Vector3 get_domino_center(int index) {
        return index * spacing_delta;
    }


    /**
     * Create all the dominoes in the chain
     */
    void Start () {
        //Create dominoes in a straight line
        for (int i = 0; i < num_dominoes; i++) {
            Vector3 pos = i * spacing_delta;
            add_domino(pos);
        }
    }

    /**
     * Draw the first, second and last dominoes
     * to show the length and spacing of the chain. 
     */
	void OnDrawGizmos() {
        Gizmos.color = domino_material.color;
        Quaternion rotation = Quaternion.identity;
        Vector3 dims = domino_template.Dimensions;
        draw_domino_outline(get_domino_center(0), rotation, dims);
        draw_domino_outline(get_domino_center(1), rotation, dims);
        draw_domino_outline(get_domino_center(num_dominoes - 1), rotation, dims);
        Gizmos.DrawLine(
            transform.position + transform.rotation * get_domino_center(0), 
            transform.position + transform.rotation * get_domino_center(num_dominoes - 1));
	}
}
