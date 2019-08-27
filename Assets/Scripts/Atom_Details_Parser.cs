using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atom_Details_Parser : MonoBehaviour {
    public string name, size, mass;
	// Use this for initialization
	void Start () {
        name = "Atom: ";
        size = "Size: ";
        mass = "Mass: ";
	}
	
	// Update is called once per frame
	void Update () {
        //SetTextValues();
    }

    void SetTextValues()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = name;
        transform.GetChild(0).GetChild(1).GetComponent<Text>().text = size;
        transform.GetChild(0).GetChild(2).GetComponent<Text>().text = mass;
    }

    public void SetValues(GameObject atom)
    {
        //name = atom.GetComponent<At>
    }
}
