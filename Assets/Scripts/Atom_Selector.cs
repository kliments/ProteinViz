using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom_Selector : MonoBehaviour {
    public Material[] materials;
    public GameObject currentProtein;
    public Transform proteinParent;

    private List<GameObject> _carbonAtoms, _hydrogenAtoms, _oxygenAtoms, _nitrogenAtoms, _sulphurAtoms, _carbonParent, _hydrogenParent, _oxygenParent, _nitrogenParent, _sulphurParent;
    public bool transparencyOn;
    private Material _originalMaterial;
    private int _counter;
    // Use this for initialization
    void Start () {
        Invoke("SetCurrentProtein", 1.2f);
        _counter = 0;
        transparencyOn = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private void DeclareVariables()
    {
        _carbonAtoms = new List<GameObject>();
        _hydrogenAtoms = new List<GameObject>();
        _oxygenAtoms = new List<GameObject>();
        _nitrogenAtoms = new List<GameObject>();
        _sulphurAtoms = new List<GameObject>();
        _carbonParent = new List<GameObject>();
        _hydrogenParent = new List<GameObject>();
        _oxygenParent = new List<GameObject>();
        _nitrogenParent = new List<GameObject>();
        _sulphurParent = new List<GameObject>();
    }

    public void SetCurrentProtein()
    {
        DeclareVariables();
        //name of the protein that should be set to current
        string str = transform.parent.transform.parent.transform.parent.name.Replace("AppBar", "");
        //list of all proteins existing
        GameObject[] proteins = GameObject.FindGameObjectsWithTag("Protein");
        for(int i=0; i<proteins.Length; i++)
        {
            if(proteins[i].name == str)
            {
                currentProtein = proteins[i];
            }
        }
        Transform[] allChildren = currentProtein.GetComponentsInChildren<Transform>();
        foreach(Transform child in allChildren)
        {
            if(child.gameObject.tag == "Carbon")
            {
                _carbonParent.Add(child.gameObject);
            }
            else if(child.gameObject.tag == "Hydrogen")
            {
                _hydrogenParent.Add(child.gameObject);
            }
            else if (child.gameObject.tag == "Oxygen")
            {
                _oxygenParent.Add(child.gameObject);
            }
            else if (child.gameObject.tag == "Nitrogen")
            {
                _nitrogenParent.Add(child.gameObject);
            }
            else if (child.gameObject.tag == "Sulphur")
            {
                _sulphurParent.Add(child.gameObject);
            }
        }

        for(int i=0; i<_carbonParent.Count; i++)
        {
            foreach(Transform child in _carbonParent[i].transform)
            {
                Transform[] children = child.GetComponentsInChildren<Transform>();
                for(int j=0; j<children.Length; j++)
                {
                    if(children[j].name == "Molecule")
                    {
                        _carbonAtoms.Add(children[j].gameObject);
                    }
                }
            }
        }

        for (int i = 0; i < _hydrogenParent.Count; i++)
        {
            foreach (Transform child in _hydrogenParent[i].transform)
            {
                Transform[] children = child.GetComponentsInChildren<Transform>();
                for (int j = 0; j < children.Length; j++)
                {
                    if (children[j].name == "Molecule")
                    {
                        _hydrogenAtoms.Add(children[j].gameObject);
                    }
                }
            }
        }

        for (int i = 0; i < _oxygenParent.Count; i++)
        {
            foreach (Transform child in _oxygenParent[i].transform)
            {
                Transform[] children = child.GetComponentsInChildren<Transform>();
                for (int j = 0; j < children.Length; j++)
                {
                    if (children[j].name == "Molecule")
                    {
                        _oxygenAtoms.Add(children[j].gameObject);
                    }
                }
            }
        }

        for (int i = 0; i < _nitrogenParent.Count; i++)
        {
            foreach (Transform child in _nitrogenParent[i].transform)
            {
                Transform[] children = child.GetComponentsInChildren<Transform>();
                for (int j = 0; j < children.Length; j++)
                {
                    if (children[j].name == "Molecule")
                    {
                        _nitrogenAtoms.Add(children[j].gameObject);
                    }
                }
            }
        }

        for (int i = 0; i < _sulphurParent.Count; i++)
        {
            foreach (Transform child in _sulphurParent[i].transform)
            {
                Transform[] children = child.GetComponentsInChildren<Transform>();
                for (int j = 0; j < children.Length; j++)
                {
                    if (children[j].name == "Molecule")
                    {
                        _sulphurAtoms.Add(children[j].gameObject);
                    }
                }
            }
        }
        //parsing through TER
        /*foreach (Transform child in currentProtein.transform)
        {
            //parsing through specific atom parents
            foreach(Transform obj in child)
            {
                if(obj.gameObject.name == "carbonParent")
                {
                    foreach(Transform m in obj)
                    {
                        if (m.gameObject.name == "combinedMolecule")
                        {
                            foreach (Transform mol in m)
                            {
                                _carbonAtoms.Add(mol.gameObject);
                            }
                        }
                        else
                        {
                            _carbonAtoms.Add(m.gameObject);
                        }
                    }
                }
                else if(obj.gameObject.name == "hydrogenParent")
                {
                    foreach (Transform m in obj)
                    {
                        if (m.gameObject.name == "combinedMolecule")
                        {
                            foreach (Transform mol in m)
                            {
                                _hydrogenAtoms.Add(mol.gameObject);
                            }
                        }
                        else
                        {
                            _hydrogenAtoms.Add(m.gameObject);
                        }
                    }
                }
                else if(obj.gameObject.name == "nitrogenParent")
                {
                    foreach (Transform m in obj)
                    {
                        if (m.gameObject.name == "combinedMolecule")
                        {
                            foreach (Transform mol in m)
                            {
                                _nitrogenAtoms.Add(mol.gameObject);
                            }
                        }
                        else
                        {
                            _nitrogenAtoms.Add(m.gameObject);
                        }
                    }
                }
                else if(obj.gameObject.name =="oxygenParent")
                {
                    foreach (Transform m in obj)
                    {
                        if (m.gameObject.name == "combinedMolecule")
                        {
                            foreach (Transform mol in m)
                            {
                                _oxygenAtoms.Add(mol.gameObject);
                            }
                        }
                        else
                        {
                            _oxygenAtoms.Add(m.gameObject);
                        }
                    }
                }
                else if(obj.gameObject.name == "sulphurParent")
                {
                    foreach (Transform m in obj)
                    {
                        if (m.gameObject.name == "combinedMolecule")
                        {
                            foreach (Transform mol in m)
                            {
                                _sulphurAtoms.Add(mol.gameObject);
                            }
                        }
                        else
                        {
                            _sulphurAtoms.Add(m.gameObject);
                        }
                    }
                }
            }
        }*/
    }

    public void ToggleCurrentMaterialOfAtom()
    {
        if(gameObject.name == "Carbon" && _carbonAtoms.Count>0)
        {
            //set original material only the first time
            if(_counter == 0)
            {
                _originalMaterial = _carbonAtoms[0].GetComponent<MeshRenderer>().material;
                _counter++;
            }
            //toggle between original material and transparent
            if(transparencyOn)
            {
                for(int i = 0; i<_carbonAtoms.Count; i++)
                {
                    _carbonAtoms[i].GetComponent<MeshRenderer>().material = _originalMaterial;
                }
                transparencyOn = false;
            }
            else
            {
                for(int i = 0; i<_carbonAtoms.Count; i++)
                {
                    _carbonAtoms[i].GetComponent<MeshRenderer>().material = materials[0];
                }
                transparencyOn = true;
            }
        }
        else if(gameObject.name == "Hydrogen" && _hydrogenAtoms.Count > 0)
        {//set original material only the first time
            if (_counter == 0)
            {
                _originalMaterial = _hydrogenAtoms[0].GetComponent<MeshRenderer>().material;
                _counter++;
            }

            //toggle between original material and transparent
            if (transparencyOn)
            {
                for (int i = 0; i < _hydrogenAtoms.Count; i++)
                {
                    _hydrogenAtoms[i].GetComponent<MeshRenderer>().material = _originalMaterial;
                }
                transparencyOn = false;
            }
            else
            {
                for (int i = 0; i < _hydrogenAtoms.Count; i++)
                {
                    _hydrogenAtoms[i].GetComponent<MeshRenderer>().material = materials[1];
                }
                transparencyOn = true;
            }
        }
        else if (gameObject.name == "Nitrogen" && _nitrogenAtoms.Count > 0)
        {
            //set original material only the first time
            if (_counter == 0)
            {
                _originalMaterial = _nitrogenAtoms[0].GetComponent<MeshRenderer>().material;
                _counter++;
            }
            //toggle between original material and transparent
            if (transparencyOn)
            {
                for (int i = 0; i < _nitrogenAtoms.Count; i++)
                {
                    _nitrogenAtoms[i].GetComponent<MeshRenderer>().material = _originalMaterial;
                }
                transparencyOn = false;
            }
            else
            {
                for (int i = 0; i < _nitrogenAtoms.Count; i++)
                {
                    _nitrogenAtoms[i].GetComponent<MeshRenderer>().material = materials[2];
                }
                transparencyOn = true;
            }
        }
        else if(gameObject.name == "Oxygen" && _oxygenAtoms.Count > 0)
        {
            //set original material only the first time
            if (_counter == 0)
            {
                _originalMaterial = _oxygenAtoms[0].GetComponent<MeshRenderer>().material;
                _counter++;
            }
            //toggle between original material and transparent
            if (transparencyOn)
            {
                for (int i = 0; i < _oxygenAtoms.Count; i++)
                {
                    _oxygenAtoms[i].GetComponent<MeshRenderer>().material = _originalMaterial;
                }
                transparencyOn = false;
            }
            else
            {
                for (int i = 0; i < _oxygenAtoms.Count; i++)
                {
                    _oxygenAtoms[i].GetComponent<MeshRenderer>().material = materials[3];
                }
                transparencyOn = true;
            }
        }
        else if(gameObject.name == "Sulphur" && _sulphurAtoms.Count > 0)
        {
            //set original material only the first time
            if (_counter == 0)
            {
                _originalMaterial = _sulphurAtoms[0].GetComponent<MeshRenderer>().material;
                _counter++;
            }
            //toggle between original material and transparent
            if (transparencyOn)
            {
                for (int i = 0; i < _sulphurAtoms.Count; i++)
                {
                    _sulphurAtoms[i].GetComponent<MeshRenderer>().material = _originalMaterial;
                }
                transparencyOn = false;
            }
            else
            {
                for (int i = 0; i < _sulphurAtoms.Count; i++)
                {
                    _sulphurAtoms[i].GetComponent<MeshRenderer>().material = materials[4];
                }
                transparencyOn = true;
            }
        }
    }
}
