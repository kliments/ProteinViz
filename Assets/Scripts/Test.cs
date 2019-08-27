using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
        GetComponent<Text>().text = Application.persistentDataPath;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown(KeyCode.R))
		{
			GameObject[] GameObjects = GameObject.FindGameObjectsWithTag("Protein");
			foreach (var protein in GameObjects)
			{
				protein.AddComponent<Academy.GestureAction>();
				protein.GetComponent<HoloToolkit.Unity.InputModule.HandDraggable>().enabled = false;
			}

			VoiceRecognizer.RotationMode = true;
		}
		if (Input.GetKeyDown(KeyCode.T))
		{
			GameObject[] GameObjects = GameObject.FindGameObjectsWithTag("Protein");
			foreach (var protein in GameObjects)
			{
				Destroy(protein.GetComponent<Academy.GestureAction>());
				protein.GetComponent<HoloToolkit.Unity.InputModule.HandDraggable>().enabled = true;
			}

			VoiceRecognizer.RotationMode = false;
		}

		if (Input.GetKeyDown(KeyCode.H))
		{
			Academy.Interactible.IsAllowedToHighlight = true;
		}
		if (Input.GetKeyDown(KeyCode.N))
		{
			Academy.Interactible.IsAllowedToHighlight = false;
			Academy.Interactible.once = true;
		}
	}
}
