using UnityEngine;
using System.Collections;

public class DominoWall : DominoStructure {
    /** Number of dominoes along the ground */
    public int num_dominoes = 10;

    private Quaternion ground_rotation = Quaternion.Euler(90, 0, 0);
    private Quaternion wall_rotation = Quaternion.Euler(90, 90, 0);
    private Quaternion end_wall_rotation = Quaternion.Euler(0, 90, 0);

    private Vector3 ground_spacing_delta {
        get {
            return new Vector3(
                domino_template.Height - domino_template.Thickness,
                0,
                0);
        }
    }

    private Vector3 half_layer_delta {
        get {
            return new Vector3(0, 2 * domino_template.Width, 0);
        }
    }

    private Vector3 first_wall_offset {
        get {
            return new Vector3(
                domino_template.HalfHeight - domino_template.HalfThickness, 
                domino_template.Width, 
                0);
        }
    }

    private Vector3 first_wall_offset2 {
        get {
            return new Vector3(
                domino_template.Height - domino_template.Thickness 
                + domino_template.HalfHeight - domino_template.HalfThickness, 
                domino_template.Width, 
                0);
        }
    }

    private Vector3 wall_spacing_delta {
        get {
            return new Vector3(
                domino_template.Height - domino_template.Thickness, 
                0, 
                0);
        }
    }

    private Vector3 center_to_side_delta {
        get {
            return new Vector3(
                0, 
                0, 
                domino_template.HalfHeight - domino_template.HalfThickness);
        }
    }

	// Use this for initialization
	void Start () {
        //Horizontal dominoes
        for (int i = 0; i < num_dominoes; i++) {
            Vector3 pos = i * ground_spacing_delta;
            add_domino(pos, ground_rotation);
            //add_domino(pos + half_layer_delta, ground_rotation);
        }

        //First layer of walls
        for (int i = 0; i < num_dominoes - 1; i++) {
            Vector3 pos = i * wall_spacing_delta + first_wall_offset;
            pos += i % 2 == 0 ? center_to_side_delta : -center_to_side_delta;
            add_domino(pos, wall_rotation);
        }

        //Upright domino at each end
        Vector3 end_to_side = new Vector3(0, 0, domino_template.HalfHeight - domino_template.HalfWidth);
        Vector3 end_height = new Vector3(
            0, domino_template.HalfWidth + domino_template.HalfHeight, 0);
        Vector3 last_domino_pos = (num_dominoes - 1) * ground_spacing_delta;
        Vector3 side_vector = (num_dominoes % 2 == 0) ? -end_to_side : end_to_side;
        add_domino(end_height - end_to_side);
        add_domino(last_domino_pos + end_height + side_vector);

        //Upright dominoes on the wall at each end
        Vector3 end_to_side_wall = new Vector3(0, 0, domino_template.HalfHeight - domino_template.HalfThickness);
        Vector3 end_wall_height = new Vector3(
            0, domino_template.Width + domino_template.HalfWidth + domino_template.HalfHeight, 0);
        Vector3 first_end_wall_offset = new Vector3(domino_template.HalfWidth - domino_template.HalfThickness, 0, 0);
        side_vector = (num_dominoes % 2 == 0) ? end_to_side_wall : -end_to_side_wall;
        Vector3 last_end_pos = (num_dominoes - 1) * ground_spacing_delta;
        add_domino(first_end_wall_offset + end_wall_height + end_to_side_wall, end_wall_rotation);
        add_domino(last_end_pos - first_end_wall_offset + end_wall_height + side_vector, end_wall_rotation);

        //Second layer of horizontal dominoes
        for (int i = 1; i < num_dominoes - 1; i++) {
            Vector3 pos = i * ground_spacing_delta + half_layer_delta;
            add_domino(pos, ground_rotation);
        }

        //Second layer of walls
        for (int i = 0; i < num_dominoes - 1; i++) {
            Vector3 pos = i * wall_spacing_delta + first_wall_offset;
            pos += i % 2 == 0 ? -center_to_side_delta : center_to_side_delta;
            add_domino(pos + half_layer_delta, wall_rotation);
        }

        //Right wall in first half, left wall in second half
        /*for (int i = 0; i < num_dominoes / 2; i++) {
            Vector3 pos = i * wall_spacing_delta + first_wall_offset;
            add_domino(pos + center_to_side_delta, wall_rotation);
            add_domino(pos - center_to_side_delta + half_layer_delta, wall_rotation);
        }*/

        //Right wall in second half, left wall in first half
        /*for (int i = 0; i < (num_dominoes - 1) / 2; i++) {
            Vector3 pos = i * wall_spacing_delta + first_wall_offset2;
            add_domino(pos - center_to_side_delta, wall_rotation);
            add_domino(pos + center_to_side_delta + half_layer_delta, wall_rotation);
        }*/
        //Debug.Break();
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
