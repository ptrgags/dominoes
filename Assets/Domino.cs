using UnityEngine;
using System.Collections;

public class Domino : MonoBehaviour {
    //The position of the domino
    public Vector3 Position {
        get {
            return gameObject.transform.position;
        }
    }

	//The dimensions of the domino
    public Vector3 Dimensions {
        get {
            return gameObject.transform.localScale;
        }
    }

    //Specific names for the dimensions of the domino, regardless
    //of how it's rotated.
    public float Height {
		get {
            return Dimensions.y;
		}
	}

    public float Width {
        get {
            return Dimensions.z;
        }
    }

    public float Thickness {
        get {
            return Dimensions.x;
        }
    }

    //Specific names for the dimensions of the domino, regardless
    //of how it's rotated.
    public float HalfHeight {
        get {
            return Height / 2.0f;
        }
    }

    public float HalfWidth {
        get {
            return Width / 2.0f;
        }
    }

    public float HalfThickness {
        get {
            return Thickness / 2.0f;
        }
    }
}
