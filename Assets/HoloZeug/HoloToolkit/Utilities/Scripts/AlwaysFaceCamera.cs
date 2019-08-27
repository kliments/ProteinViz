using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFaceCamera : MonoBehaviour {

    private Camera camera;
    private Vector3 direction;
	// Use this for initialization
	void Start () {
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
        direction = transform.position - camera.transform.position;
        transform.rotation = Quaternion.LookRotation(direction);
	}
}
