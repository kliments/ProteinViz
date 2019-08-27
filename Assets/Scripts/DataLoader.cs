using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DataLoader : MonoBehaviour {

    public StreamReader Sr;
    public FileStream fS;
    public string[] FilePaths;
    public List<GameObject> Atoms;
    public GameObject proteinPrefab, atomSelectorPrefab;
    public Transform atomParent;
    public Material material;
    public int dataReference;

    public static int CCounter;
    public static int NCounter;
    public static int SCounter;
    public static int HCounter;
    public static int OCounter;
    
    public GameObject SimpleSphere, proteinSprite;

    private GameObject hydrogenParent, oxygenParent, sulphurParent, carbonParent, nitrogenParent, bigProtein1, mediumProtein1, mediumProtein2, smallProtein1, smallProtein2;
    private string _line, _filePath;
    private Vector3 _location, boundss, bounds1;
    private float _scaleCoef;
    private int moleculePrefab;


    private GameObject protein, atomSelector;
    // Use this for initialization
    void Start () {
        /*
        if (Application.persistentDataPath == "C:/Data/Users/lsiun/AppData/Local/Packages/Template3D_pzq3xp76mxafg/LocalState")
        {
            //app is started on the Hololens

            //Filepath for building with Hololens
            string str = Application.persistentDataPath;
            FilePaths = Directory.GetFiles(Application.persistentDataPath, "*.pdb");
            _filePath = FilePaths[dataReference];
            _scaleCoef = 0.5f;
        }
        else
        {
            //app is started on the PC
            FilePaths = Directory.GetFiles(@"..\IA_ProteinViz\Assets\Resources\Data", "*.pdb");
            _filePath = FilePaths[dataReference];
            _scaleCoef = 0.00035f;
        }
        //
        switch (dataReference)
        {
            case 0:
                bigProtein1 = new GameObject();
                bigProtein1.name = "BigProtein1";
                bigProtein1.tag = "Protein";
                bigProtein1.SetActive(false);
                bigProtein1.transform.localPosition = new Vector3(0, 0, 0);
                bigProtein1.AddComponent<AutoRotation>();
                bigProtein1.AddComponent<MeshRenderer>();
                break;
            case 1:
                mediumProtein1 = new GameObject();
                mediumProtein1.name = "MediumProtein1";
                mediumProtein1.tag = "Protein";
                mediumProtein1.SetActive(false);
                mediumProtein1.transform.localPosition = new Vector3(0, 0, 0);
                mediumProtein1.AddComponent<AutoRotation>();
                mediumProtein1.AddComponent<MeshRenderer>();

                boundss = mediumProtein1.GetComponent<Renderer>().bounds.center;
                float dist = Vector3.Distance(boundss, mediumProtein1.transform.localPosition);
                Vector3 dir = Vector3.zero - boundss;
                break;
            case 2:
                mediumProtein2 = new GameObject();
                mediumProtein2.name = "MediumProtein2";
                mediumProtein2.tag = "Protein";
                mediumProtein2.SetActive(false);
                mediumProtein2.transform.localPosition = new Vector3(0, 0, 0);
                mediumProtein2.AddComponent<AutoRotation>();
                mediumProtein2.AddComponent<MeshRenderer>();

                bounds1 = mediumProtein2.GetComponent<Renderer>().bounds.center;
                float dist1 = Vector3.Distance(bounds1, mediumProtein2.transform.localPosition);
                Vector3 dir1 = Vector3.zero - bounds1;
                break;
            case 3:
                smallProtein1 = new GameObject();
                smallProtein1.name = "SmallProtein1";
                smallProtein1.tag = "Protein";
                smallProtein1.transform.position = new Vector3(0, 0, 0);
                smallProtein1.AddComponent<AutoRotation>();
                smallProtein1.AddComponent<MeshRenderer>();
                break;
            default:
                smallProtein2 = new GameObject();
                smallProtein2.name = "SmallProtein2";
                smallProtein2.tag = "Protein";
                smallProtein2.SetActive(false);
                smallProtein2.transform.position = new Vector3(0, 0, 0);
                smallProtein2.AddComponent<AutoRotation>();
                smallProtein2.AddComponent<MeshRenderer>();
                break;
        }
        moleculePrefab = 1;
        fS = new FileStream(_filePath, FileMode.Open);
        Sr = new StreamReader(fS);
        ReadData();

        Debug.Log(gameObject.name + "   " + transform.localPosition);
        Debug.Log(gameObject.name + "   " + transform.position);*/
        protein = Instantiate(proteinPrefab);
        protein.name = gameObject.name;
        protein.tag = "Protein";
        atomSelector = Instantiate(atomSelectorPrefab);
        Invoke("SetParent", 1f);
    }

    // Update is called once per frame
    void Update () {

    }

    private void SetParent()
    {
        Transform parent = GameObject.Find(protein.name + "AppBar").transform;
        atomSelector.transform.parent = parent.GetChild(0);
        atomSelector.transform.localPosition = new Vector3(-0.236f, -0.136f, 0f);
        atomSelector.transform.localRotation = Quaternion.identity;
        atomSelector.transform.localScale = new Vector3(1, 1, 1);
    }

    public void ReadData()
    {
        Read();
    }
    
    private void Read()
    {
        int counter = 0;
        while ((_line = Sr.ReadLine()) != null)
        {
            if (_line.Substring(0, 4).Contains("ATOM"))
            {
                // GameObject atom = GameObject.CreatePrimitive(PrimitiveType.Cube);
                GameObject atom = Instantiate(SimpleSphere);
                atom.GetComponent<MeshRenderer>().material = material;
                atom.AddComponent<PDBAtom>();
                atom.GetComponent<PDBAtom>().ID = _line.Substring(12,3).Replace(" ", string.Empty);
                float x = (float)Convert.ToDouble(_line.Substring(31, 7).Replace(" ", string.Empty)) * _scaleCoef;
                float y = (float)Convert.ToDouble(_line.Substring(39, 7).Replace(" ", string.Empty)) * _scaleCoef;
                float z = (float)Convert.ToDouble(_line.Substring(47, 7).Replace(" ", string.Empty)) * _scaleCoef;
                atom.GetComponent<PDBAtom>().position.x = x;
                atom.GetComponent<PDBAtom>().position.y = y;
                atom.GetComponent<PDBAtom>().position.z = z;
                atom.GetComponent<PDBAtom>().element = _line.Substring(77, 3).Replace(" ", string.Empty);
                atom.transform.localPosition = atom.GetComponent<PDBAtom>().position;
                atom.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                atom.GetComponent<MeshRenderer>().receiveShadows = false;
                Atoms.Add(atom);
            }

            else if (_line.Substring(0, 3).Contains("TER"))
            {
                SortAndCombine(Atoms);
                Atoms = new List<GameObject>();
            }
            counter++;
        }
        Sr.DiscardBufferedData();
        Sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
    }

    private GameObject createCombinedMolecule(List<GameObject> objects, Transform parent, Color color)
    {
        GameObject ret = null;
        int sphereNrVertices = 515;
        int vertexCount = sphereNrVertices * objects.Count;
        if (vertexCount > System.UInt16.MaxValue)
        {
            GameObject combinedMolecule = new GameObject("combinedMolecule");
            combinedMolecule.transform.parent = parent;
            int objectsPerRun = (int)System.Math.Floor(System.UInt16.MaxValue / (double)sphereNrVertices);
            int index = 0;
            while (index < objects.Count)
            {
                createMoleculeObject(objects.GetRange(index, index + objectsPerRun >= objects.Count ? objects.Count - index : objectsPerRun), combinedMolecule.transform, color);
                index += objectsPerRun;
            }
            ret = combinedMolecule;

        }
        else
        {
            ret = createMoleculeObject(objects, parent, color);
        }

        foreach (GameObject o in objects)
        {
            Destroy(o);
        }
        return ret;
    }

    private GameObject createMoleculeObject(List<GameObject> objects, Transform parent, Color color)
    {
        //"realobject"
        GameObject molecule = new GameObject("Molecule");
        molecule.transform.parent = parent;
        molecule.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        molecule.AddComponent<MeshFilter>();
        molecule.AddComponent<MeshRenderer>();
        molecule.AddComponent<Academy.Interactible>(); //add the microsoft script for the highlighting, also needs a collider
        molecule.AddComponent<MeshCollider>(); //so add the collider here
        molecule.GetComponent<MeshCollider>().sharedMesh = molecule.GetComponent<MeshFilter>().mesh; //also needs a mesh for the collider
        MeshFilter filter = molecule.GetComponent<MeshFilter>();
        MeshRenderer renderer = molecule.GetComponent<MeshRenderer>();
        renderer.material = material;
        renderer.material.color = color;
        mergeChildren(molecule, objects, filter);
        /*string path = "Assets/Resources/Prefabs/Proteins/molecule" + moleculePrefab.ToString() + ".fbx";
        moleculePrefab++;
        AssetDatabase.CreateAsset(molecule.GetComponent<MeshFilter>().mesh, path);*/

        return molecule;
    }

    private void mergeChildren(GameObject parent, List<GameObject> objects, MeshFilter target)
    {
        CombineInstance[] combine = new CombineInstance[objects.Count];
        for (int i = 0; i < objects.Count; i++)
        {
            //make sure the points are aligned with the scatterplot
            objects[i].transform.parent = parent.transform;
            combine[i].mesh = objects[i].GetComponent<MeshFilter>().sharedMesh;
            combine[i].transform = objects[i].transform.localToWorldMatrix;
        }

        target.mesh.CombineMeshes(combine);
    }

    private void SortAndCombine(List<GameObject> list)
    {
        

        CCounter = 0;
        NCounter = 0;
        SCounter = 0;
        HCounter = 0;
        OCounter = 0;


        string element = null;
        GameObject ter = new GameObject();
        //ter.transform.parent = atomParent;
        ter.name = "TER";
        //set ter as parent to molecules
        ParentDeclaration(ter);
        //set protein as parent to ter
        ParentAtom(ter);
        List<GameObject> cAtoms = new List<GameObject>();
        List<GameObject> hAtoms = new List<GameObject>();
        List<GameObject> nAtoms = new List<GameObject>();
        List<GameObject> oAtoms = new List<GameObject>();
        List<GameObject> sAtoms = new List<GameObject>();
        foreach (GameObject atom in list)
        {
            element = atom.GetComponent<PDBAtom>().element;
            switch (element)
            {
                case "C":
                    //bla bla
                    cAtoms.Add(atom);
                    CCounter++;
                    break;
                case "H":
                    //bla bla
                    hAtoms.Add(atom);
                    HCounter++;
                    break;
                case "N":
                    //bla bla
                    nAtoms.Add(atom);
                    NCounter++;
                    break;
                case "O":
                    //bla bla
                    oAtoms.Add(atom);
                    OCounter++;
                    break;
                default:
                    //bla bla
                    sAtoms.Add(atom);
                    SCounter++;
                    break;
            }
        }
        if (cAtoms.Count > 0) createCombinedMolecule(cAtoms, carbonParent.transform, Color.gray);
        if (hAtoms.Count > 0) createCombinedMolecule(hAtoms, hydrogenParent.transform, Color.white);
        if (nAtoms.Count > 0) createCombinedMolecule(nAtoms, nitrogenParent.transform, Color.blue);
        if (oAtoms.Count > 0) createCombinedMolecule(oAtoms, oxygenParent.transform, Color.red);
        if (sAtoms.Count > 0) createCombinedMolecule(sAtoms, sulphurParent.transform, Color.yellow);

        

    }

    private void ParentDeclaration(GameObject parent)
    {
        carbonParent = new GameObject();
        carbonParent.name = "carbonParent";
        carbonParent.tag = "Carbon";
        carbonParent.transform.parent = parent.transform;

        hydrogenParent = new GameObject();
        hydrogenParent.name = "hydrogenParent";
        hydrogenParent.tag = "Hydrogen";
        hydrogenParent.transform.parent = parent.transform;

        nitrogenParent = new GameObject();
        nitrogenParent.name = "nitrogenParent";
        nitrogenParent.tag = "Nitrogen";
        nitrogenParent.transform.parent = parent.transform;

        oxygenParent = new GameObject();
        oxygenParent.name = "oxygenParent";
        oxygenParent.tag = "Oxygen";
        oxygenParent.transform.parent = parent.transform;

        sulphurParent = new GameObject();
        sulphurParent.name = "sulphurParent";
        sulphurParent.tag = "Sulphur";
        sulphurParent.transform.parent = parent.transform;
    }

    private void ParentAtom(GameObject ter)
    {
        switch(dataReference)
        {
            case 0:
                ter.transform.parent = bigProtein1.transform;
                break;
            case 1:
                ter.transform.parent = mediumProtein1.transform;
                break;
            case 2:
                ter.transform.parent = mediumProtein2.transform;
                break;
            case 3:
                ter.transform.parent = smallProtein1.transform;
                break;
            default:
                ter.transform.parent = smallProtein2.transform;
                break;
        }
    }
}
