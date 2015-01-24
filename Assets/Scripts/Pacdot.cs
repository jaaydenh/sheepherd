using UnityEngine;
using System.Collections;

public class Pacdot : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D co) {
        if (co.name == "pacman")
            Destroy(gameObject);
    }
}
