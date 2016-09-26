using UnityEngine;
using System.Collections;

/**
 * Base class for a structure of Dominoes.
 */
public class DominoStructure : MonoBehaviour {
    //Prefab for the basic domino in this wall
    public GameObject domino_prefab;
    /** The material for coloring the dominoes in this structure */
    public Material domino_material;

    /**
     * Cast the prefab to a Domino object.
     */
    protected Domino domino_template {
        get {
            return domino_prefab.GetComponent<Domino>();
        }
    }

    /**
     * Add a domino as a child of this structure. The subclasses must provide
     * the position and rotation.
     * 
     * offset -- the offset from the Structure's origin
     * rotation -- any rotation besides the rotation of the Structure as a whole
     */
    protected void add_domino(Vector3 offset) {
        add_domino(offset, Quaternion.identity);
    }
    protected void add_domino(Vector3 offset, Quaternion rotation) {
        GameObject domino = (GameObject)Instantiate(domino_prefab);
        domino.transform.parent = transform;
        domino.transform.localPosition = offset;
        domino.transform.localRotation = rotation;
        domino.GetComponent<Renderer>().material = domino_material;
    }

    /**
     * Draw a wireframe box that represents a domino. It is up
     * to the subclass to provide position, rotation and scale.
     * This is meant to be used within OnDrawGizmos()
     */
    protected void draw_domino_outline(Vector3 offset, Quaternion rotation, Vector3 scale) {
        //Save the old matrix
        Matrix4x4 old_matrix = Gizmos.matrix;

        //Add a transformation to the graphics context
        Matrix4x4 new_matrix = Matrix4x4.TRS(
            transform.position + transform.rotation * offset, 
            transform.rotation * rotation, 
            scale);
        Gizmos.matrix *= new_matrix;

        //Draw the wireframe box
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        //Restore the old matrix
        Gizmos.matrix = old_matrix;
    }
}
