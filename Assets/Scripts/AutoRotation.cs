using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotation : MonoBehaviour {

    public static bool RotationEnabled;

	// Use this for initialization
	void Start () {
        RotationEnabled = false;
//        var BoundingBox = gameObject.GetComponent<HoloToolkit.Unity.UX.BoundingBoxRig>();
//        BoundingBox.GetBounds();
    }
	
	// Update is called once per frame
	void Update () {
        if (RotationEnabled)
        {
            // transform.Rotate(Vector3.right * Time.deltaTime * 5);

//            transform.RotateAround(gameObject.GetComponent<HoloToolkit.Unity.UX.BoundingBoxRig>().Centroid, Vector3.up, 10 * Time.deltaTime);
            transform.Rotate(Vector3.up, 10 * Time.deltaTime);
        }
    }
}
