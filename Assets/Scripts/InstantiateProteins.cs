using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateProteins : MonoBehaviour {
    public GameObject[] proteins;
	// Use this for initialization
	void Start () {
        for(int i = 0; i < proteins.Length; i++)
        {
            if(gameObject.name == proteins[i].name)
            {
                GameObject protein = Instantiate(proteins[i]);
            }
            if(i != 3)
            {
                proteins[i].SetActive(false);
            }
            else
            {
                proteins[i].SetActive(true);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
