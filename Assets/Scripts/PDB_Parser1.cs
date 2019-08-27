using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Windows;

#if WINDOWS_UWP
using Windows.Storage;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using System;
#endif

public class PDB_Parser1 : MonoBehaviour {
    public StreamReader Sr;
    public FileStream fS;
    public string[] FilePaths;
    public List<GameObject> Atoms;
    public Transform atomParent;
    public Material material;

    public int CCounter;
    public int NCounter;
    public int SCounter;
    public int HCounter;
    public int OCounter;

    private GameObject hydrogenParent, oxygenParent, sulphurParent, carbonParent, nitrogenParent;
    private string _line, _filePath;
    private Vector3 _location;
    private float _scaleCoef;

    // Use this for initialization
    void Start () {
        //TODO relative paths! The commented line below doesn't work, it should though.
        
        if(Application.persistentDataPath == "C:/Data/Users/lsiun/AppData/Local/Packages/Template3D_pzq3xp76mxafg/LocalState")
        {
            //app is started on the Hololens

            //Filepath for building with Hololens
            FilePaths = Directory.GetFiles(Application.persistentDataPath, "*.pdb");
            _filePath = FilePaths[4];
            _scaleCoef = 0.5f;
        }
        else
        {
            //app is started on the PC
            FilePaths = Directory.GetFiles(@"..\IA_ProteinViz\Assets\Resources\TestData", "*.pdb");
           _filePath = FilePaths[4];
            _scaleCoef = 0.0005f;
        }
        fS = new FileStream(_filePath, FileMode.Open);
        Sr = new StreamReader(fS);
        Read();
    }

    // Update is called once per frame
    void Update () {

	}

    private void Read()
    {
        int counter = 0;
        while ((_line = Sr.ReadLine()) != null)
        {
            //replace multiple empty spaces with one 
            string currentLine = System.Text.RegularExpressions.Regex.Replace(_line, @"\s+", " ");
            string[] cleanAtomLine = currentLine.Split(' ');
            if (cleanAtomLine[0].Contains("ATOM"))
            {
                GameObject atom = GameObject.CreatePrimitive(PrimitiveType.Cube);
                atom.GetComponent<MeshRenderer>().material = material;
                atom.AddComponent<PDBAtom>();
                atom.GetComponent<PDBAtom>().ID = cleanAtomLine[1];
                atom.GetComponent<PDBAtom>().position.x = (float)Convert.ToDouble(cleanAtomLine[6]) * _scaleCoef;
                atom.GetComponent<PDBAtom>().position.y = (float)Convert.ToDouble(cleanAtomLine[7]) * _scaleCoef;
                atom.GetComponent<PDBAtom>().position.z = (float)Convert.ToDouble(cleanAtomLine[8]) * _scaleCoef;
                atom.GetComponent<PDBAtom>().element = cleanAtomLine[cleanAtomLine.Length - 2];
                atom.transform.localPosition = atom.GetComponent<PDBAtom>().position;
                atom.transform.parent = atomParent;
                atom.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                atom.GetComponent<MeshRenderer>().receiveShadows = false;
                Atoms.Add(atom);
            }

            else if(cleanAtomLine[0].Equals("TER"))
            {
                SortAndCombine(Atoms);
                Atoms = new List<GameObject>();
            }
            counter++;
        }
        atomParent.localScale = new Vector3(0.01f, 0.01f, 0.01f);
    }

    private GameObject createCombinedMolecule(List<GameObject> objects, Transform parent, Color color)
    {
        GameObject ret = null;
        int sphereNrVertices = 515;
        int vertexCount = sphereNrVertices * objects.Count;
        if(vertexCount > System.UInt16.MaxValue)
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
        molecule.AddComponent<MeshFilter>();
        molecule.AddComponent<MeshRenderer>();
        molecule.AddComponent<Academy.Interactible>(); //add this so gaze highlighting and bounding boxes work, needs a collider though!
        molecule.AddComponent<MeshCollider>(); //so add the collider here
        molecule.GetComponent<MeshCollider>().sharedMesh = molecule.GetComponent<MeshFilter>().mesh; //also needs a mesh for the collider
        MeshFilter filter = molecule.GetComponent<MeshFilter>();
        MeshRenderer renderer = molecule.GetComponent<MeshRenderer>();
        renderer.material = material;
        renderer.material.color = color;
        mergeChildren(molecule, objects, filter);
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


    string element = null;
        GameObject ter = new GameObject();
        ter.transform.parent = atomParent;
        ter.name = "TER";
        ParentDeclaration(ter);
        List<GameObject> cAtoms = new List<GameObject>();
        List<GameObject> hAtoms = new List<GameObject>();
        List<GameObject> nAtoms = new List<GameObject>();
        List<GameObject> oAtoms = new List<GameObject>();
        List<GameObject> sAtoms = new List<GameObject>();
        foreach(GameObject atom in list)
        {
            element = atom.GetComponent<PDBAtom>().element;
            switch(element)
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
        carbonParent.transform.parent = parent.transform;
        hydrogenParent = new GameObject();
        hydrogenParent.name = "hydrogenParent";
        hydrogenParent.transform.parent = parent.transform;
        nitrogenParent = new GameObject();
        nitrogenParent.name = "nitrogenParent";
        nitrogenParent.transform.parent = parent.transform;
        oxygenParent = new GameObject();
        oxygenParent.name = "oxygenParent";
        oxygenParent.transform.parent = parent.transform;
        sulphurParent = new GameObject();
        sulphurParent.name = "sulphurParent";
        sulphurParent.transform.parent = parent.transform;
    }
}
//@