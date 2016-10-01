using UnityEngine;
using System.Collections;

public class Catalyst : MonoBehaviour {

    void OnCollisionExit(Collision collisionInfo) {
        Destroy(gameObject);
    }
}
