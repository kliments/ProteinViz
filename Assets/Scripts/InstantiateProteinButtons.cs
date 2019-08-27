using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateProteinButtons : MonoBehaviour {

    public GameObject prefab, buttonParent;
    public bool press;
    private int _nrOfDatasets;
	// Use this for initialization
	void Start () {
        _nrOfDatasets = GetComponent<PDB_Parser1>().FilePaths.Length;
        press = false;
    }
	
	// Update is called once per frame
	void Update () {
		if(press)
        {
            InstantiateButtons();
            press = false;
        }
	}

    public void InstantiateButtons()
    {
        //Vector3 pos = new Vector3()
        if(buttonParent == null)
        {
            buttonParent = GameObject.Find("AppBar(Clone)");
        }
        for(int i = 0; i < _nrOfDatasets; i++)
        {
            GameObject button = Instantiate(prefab, buttonParent.transform);
            //button.transform.localPosition =
        }
    }
}
