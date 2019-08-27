using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity.UX;
using UnityEngine;

public class PerformanceOptimizer : MonoBehaviour
{

	private bool OnlyOnce = false;
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update () {
		if (gameObject.transform.position.x == 100 && !OnlyOnce)
		{
			gameObject.GetComponent<BoundingBoxRig>().enabled = false;
			gameObject.GetComponent<AutoRotation>().enabled = false;
//			gameObject.GetComponent<DirectionIndicator>().enabled = false;
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			gameObject.GetComponent<HandDraggable>().enabled = false;
			OnlyOnce = true;
		}
		if (gameObject.transform.position.x != 100 && OnlyOnce)
		{
			gameObject.GetComponent<BoundingBoxRig>().enabled = true;
			gameObject.GetComponent<AutoRotation>().enabled = true;
//			gameObject.GetComponent<DirectionIndicator>().enabled = true;
			gameObject.GetComponent<MeshRenderer>().enabled = true;
			gameObject.GetComponent<HandDraggable>().enabled = true;
			OnlyOnce = false;
		}
	}
}
