using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_Shooter : MonoBehaviour {
    private GameObject _cursor, _camera, _detailsCanvas;
    private Vector3 _cursorPos, _cameraPos, _direction, _cameraDir;
	// Use this for initialization
	void Start () {
        _cursor = GameObject.Find("DefaultCursor");
        _camera = Camera.main.gameObject;
        _detailsCanvas = GameObject.Find("DetailsPanel");
	}
	
	// Update is called once per frame
	void Update ()
    {
        _cameraPos = _camera.transform.position;
        _cameraDir = _camera.transform.forward * 1000;
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Key pressed!");
            Debug.DrawRay(_cameraPos, _cameraDir, Color.red, 2.0f);
            RaycastHit hit;
            if (Physics.Raycast(_cameraPos, _cameraDir, out hit, Mathf.Infinity))
            {
                if(hit.collider.tag == "Atom")
                {
                    Debug.Log("Atom is being hit!!");

                }
            }
        }
	}
}
