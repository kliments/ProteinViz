using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PDBAtom : MonoBehaviour {
    public string ID;
    public Vector3 position;
    public string element;
    public bool elementMaterialSet;

    private Color _carbonColor, _hydrogenColor, _nitrogenColor, _oxygenColor, _sulphurColor;
    private Vector3 _carbonRadius, _hydrogenRadius, _nitrogenRadius, _oxygenRadius, _sulphurRadius;
	// Use this for initialization
	void Start () {
        SetValues();
    }
	
	// Update is called once per frame
	void Update () {
        if(!elementMaterialSet && element != null)
        {
            switch(element)
            {
                case "N":
                    //element is Nitrogen
                    GetComponent<MeshRenderer>().material.color = _nitrogenColor;
                    gameObject.transform.localScale = _nitrogenRadius;
                    gameObject.name = "Nitrogen";
                    break;
                case "O":
                    //element is Oxygen
                    GetComponent<MeshRenderer>().material.color = _oxygenColor;
                    gameObject.transform.localScale = _oxygenRadius;
                    gameObject.name = "Oxygen";
                    break;
                case "S":
                    //element is sulphur
                    GetComponent<MeshRenderer>().material.color = _sulphurColor;
                    gameObject.transform.localScale = _sulphurRadius;
                    gameObject.name = "Sulphur";
                    break;
                case "C":
                    //element is carbon
                    GetComponent<MeshRenderer>().material.color = _carbonColor;
                    gameObject.transform.localScale = _carbonRadius;
                    gameObject.name = "Carbon";
                    break;
                default:
                    //element is Hydrogen
                    GetComponent<MeshRenderer>().material.color = _hydrogenColor;
                    gameObject.transform.localScale = _hydrogenRadius;
                    gameObject.name = "Hydrogen";
                    break;
            }
            elementMaterialSet = true;
        }
	}
    
    private float NormalizeColor(float colorValue)
    {
        return (colorValue / 256);
    }

    private void SetValues()
    {
        elementMaterialSet = false;
        #region Colors
        _carbonColor = new Color(NormalizeColor(70f), NormalizeColor(70f), NormalizeColor(70f));
        _hydrogenColor = new Color(NormalizeColor(245f), NormalizeColor(245f), NormalizeColor(245f));
        _nitrogenColor = new Color(NormalizeColor(20f), NormalizeColor(40f), NormalizeColor(250f));
        _oxygenColor = new Color(NormalizeColor(255f), NormalizeColor(15f), NormalizeColor(15f));
        _sulphurColor = new Color(NormalizeColor(255f), NormalizeColor(255f), NormalizeColor(30f));
        #endregion

        #region Radius
        _carbonRadius = new Vector3(1.7f, 1.7f, 1.7f);
        _hydrogenRadius = new Vector3(1.2f, 1.2f, 1.2f);
        _nitrogenRadius = new Vector3(1.55f, 1.55f, 1.55f);
        _oxygenRadius = new Vector3(1.52f, 1.52f, 1.52f);
        _sulphurRadius = new Vector3(1.8f, 1.8f, 1.8f);
        #endregion 
    }
}
