using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetProtein : MonoBehaviour {
    public GameObject proteinToReset;
    private GameObject[] atomsButtons;
	// Use this for initialization
	void Start () {
        Invoke("SetProtein", 2.2f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void ResetThisProtein()
    {
        AutoRotation.RotationEnabled = false;
        proteinToReset.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
//        proteinToReset.transform.localRotation = Quaternion.identity;
        proteinToReset.transform.localRotation = Camera.main.transform.rotation;
        proteinToReset.transform.localPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
        //in case a button for transparency was pressed, reset it
        for(int i=0; i<atomsButtons.Length; i++)
        {
            atomsButtons[i].GetComponent<Atom_Selector>().transparencyOn = true;
            atomsButtons[i].GetComponent<Atom_Selector>().ToggleCurrentMaterialOfAtom();
            atomsButtons[i].GetComponent<ToggleProperties>().isHighlighted = false;
        }
    }

    private void SetProtein()
    {
        //transform.parent.GetChild(8) is the parent containing the buttons for atoms
        proteinToReset = transform.parent.GetChild(7).transform.GetChild(0).gameObject.GetComponent<Atom_Selector>().currentProtein;
        atomsButtons = new GameObject[5];
        int i = 0;
        foreach(Transform child in transform.parent.GetChild(7))
        {
            atomsButtons[i] = child.gameObject;
            i++;
        }

    }
}
