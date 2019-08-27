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

public class PDB_Parser : MonoBehaviour {
    public StreamReader Sr;
    public FileStream fS;
    public string[] FilePaths;
    public List<GameObject> Atoms;
    public Transform atomParent;
    private string _line, _filePath;
    private Vector3 _location;
    private float _scaleCoef;

    // Use this for initialization
    void Start () {
        //TODO relative paths! The commented line below doesn't work, it should though.
        // FilePaths = Directory.GetFiles(@"..\Assets\Resources\TestData", "*.pdb");
        
        if(Application.persistentDataPath == "C:/Data/Users/lsiun/AppData/Local/Packages/Template3D_pzq3xp76mxafg/LocalState")
        {
            //app is started on the Hololens

            //Filepath for building with Hololens
            FilePaths = Directory.GetFiles(Application.persistentDataPath, "*.pdb");
            _filePath = FilePaths[2];
            _scaleCoef = 1;
        }
        else
        {
            //app is started on the PC
            FilePaths = Directory.GetFiles(@"..\IA_ProteinViz\Assets\Resources\TestData", "*.pdb");
           _filePath = FilePaths[4];
            _scaleCoef = 0.001f;
        }
        //Filepath for testing on Daniel's PC


        fS = new FileStream(_filePath, FileMode.Open);
        Sr = new StreamReader(fS);
        Read();
        
        /*var testFilePath = Application.persistentDataPath;
        using (StreamReader sr = new StreamReader(new FileStream(testFilePath, FileMode.Open)))
        {
            int counter = 0;
            while ((_line = sr.ReadLine()) != null)
            {
                //replace multiple empty spaces with one 
                string currentLine = System.Text.RegularExpressions.Regex.Replace(_line, @"\s+", " ");
                string[] cleanAtomLine = currentLine.Split(' ');
                if (cleanAtomLine[0].Contains("ATOM"))
                {
                    GameObject atom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    atom.AddComponent<Atom>();
                    atom.tag = "Atom";
                    atom.GetComponent<Atom>().ID = cleanAtomLine[1];
                    atom.GetComponent<Atom>().position.x = (float)Convert.ToDouble(cleanAtomLine[6]) * 0.001f;
                    atom.GetComponent<Atom>().position.y = (float)Convert.ToDouble(cleanAtomLine[7]) * 0.001f;
                    atom.GetComponent<Atom>().position.z = (float)Convert.ToDouble(cleanAtomLine[8]) * 0.001f;
                    atom.transform.localPosition = atom.GetComponent<Atom>().position;
                    atom.GetComponent<Atom>().element = cleanAtomLine[cleanAtomLine.Length - 2];
                    atom.transform.parent = gameObject.transform;
                    atom.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    atom.GetComponent<MeshRenderer>().receiveShadows = false;
                    //Destroy(atom.GetComponent<SphereCollider>());
                    Atoms.Add(atom);
                }
                counter++;
            }
         }*/
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
                GameObject atom = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                atom.AddComponent<PDBAtom>();
                atom.GetComponent<PDBAtom>().ID = cleanAtomLine[1];
                atom.GetComponent<PDBAtom>().position.x = (float)Convert.ToDouble(cleanAtomLine[6]) * _scaleCoef;
                atom.GetComponent<PDBAtom>().position.y = (float)Convert.ToDouble(cleanAtomLine[7]) * _scaleCoef;
                atom.GetComponent<PDBAtom>().position.z = (float)Convert.ToDouble(cleanAtomLine[8]) * _scaleCoef;
                atom.transform.localPosition = atom.GetComponent<PDBAtom>().position;
                atom.GetComponent<PDBAtom>().element = cleanAtomLine[cleanAtomLine.Length - 2];
                atom.transform.parent = atomParent;
                atom.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                atom.GetComponent<MeshRenderer>().receiveShadows = false;
      //          Destroy(atom.GetComponent<SphereCollider>());
                Atoms.Add(atom);
            }
            counter++;
        }
        //atomParent.localScale -= new Vector3(0.9f, 0.9f, 0.9f);
        atomParent.localScale = new Vector3(0.01f, 0.01f, 0.01f);
       // Sr.Close();
    }
}
//@