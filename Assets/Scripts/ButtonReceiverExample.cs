// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using HoloToolkit.Unity;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.Receivers;
using HoloToolkit.Unity.InputModule;
using System.Linq;

namespace HoloToolkit.Unity.Examples
{
    public class ButtonReceiverExample : InteractionReceiver
    {
        public GameObject textObjectState;
        public Transform atomSelectorParent;
        private TextMesh txt;
        private GameObject[] proteins;

        void Start()
        {
            //txt = textObjectState.GetComponentInChildren<TextMesh>();
            Invoke("RepositionProteins", 1.1f);
            Invoke("SetInteractables", 1.3f);
        }

        protected override void FocusEnter(GameObject obj, PointerSpecificEventData eventData) {
//            Debug.Log(obj.name + " : FocusEnter");
            if (!obj.GetComponent<DataLoader>().proteinSprite.activeSelf)
            {
                obj.GetComponent<DataLoader>().proteinSprite.SetActive(true);
            }
        }

        protected override void FocusExit(GameObject obj, PointerSpecificEventData eventData) {
//            Debug.Log(obj.name + " : FocusExit");
            if (obj.GetComponent<DataLoader>().proteinSprite.activeSelf)
            {
                obj.GetComponent<DataLoader>().proteinSprite.SetActive(false);
            }
        }

        protected override void InputDown(GameObject obj, InputEventData eventData) {
//            Debug.Log(obj.name + " : InputDown");
            if (obj.GetComponent<DataLoader>() != null)
            {
                for(int i=0; i<proteins.Length; i++)
                {
                    /*if(obj.name == proteins[i].name)
                    {
                        proteins[i].transform.localPosition = new Vector3(0, 0, 0);

                        //update current protein in atomSelector buttons
                        foreach (Transform selectorChild in atomSelectorParent)
                        {
                            selectorChild.GetComponent<Atom_Selector>().currentProtein = proteins[i];
                        }
                    }
                    else
                    {
                        proteins[i].transform.localPosition = new Vector3(100, 0, 0);
                    }*/
                    if(obj.name == proteins[i].name)
                    {
                        if(obj.GetComponent<ToggleProperties>().isHighlighted)
                        {
                            //change material of the pressed button
                            obj.GetComponent<ToggleProperties>().isHighlighted = false;
                            //reposition the current protein far away
                            proteins[i].transform.localPosition = new Vector3(100, 0, 0);
                            proteins[i].SetActive(false); //daniel
                            proteins[i].GetComponent<DirectionIndicator>().DirectionIndicatorObject.SetActive(false); //daniel
                            
                        }
                        else
                        {
                            obj.GetComponent<ToggleProperties>().isHighlighted = true;
                            proteins[i].transform.localPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.2f; //daniel change position                    
//                            Debug.Log(proteins[i].name + " should be active now");
                            proteins[i].SetActive(true);
                            proteins[i].GetComponent<DirectionIndicator>().DirectionIndicatorObject.SetActive(true); //daniel
//                            Debug.Log(proteins[i].name + " should be active now");
                        }
                    }
                }

            }
            if(obj.tag == "AtomSelector")
            {
                obj.GetComponent<Atom_Selector>().ToggleCurrentMaterialOfAtom();

                //change material of the pressed button
                if(obj.GetComponent<ToggleProperties>().isHighlighted)
                {
                    obj.GetComponent<ToggleProperties>().isHighlighted = false;
                }
                else
                {
                    obj.GetComponent<ToggleProperties>().isHighlighted = true;
                }
            }

            if(obj.name == "AtomsMenu")
            {
                obj.GetComponent<ToggleAtomsMenu>().Toggle();
            }

            if(obj.name == "ResetProtein")
            {
                obj.GetComponent<ResetProtein>().ResetThisProtein();
            }

        }

//        protected override void InputUp(GameObject obj, InputEventData eventData) {
////            Debug.Log(obj.name + " : InputUp");
//        }

        private void RepositionProteins()
        {
            proteins = GameObject.FindGameObjectsWithTag("Protein");
            for (int i = 0; i < proteins.Length; i++)
            {
                
                if (proteins[i].name != "SmallProtein1")
                {
                    proteins[i].transform.localPosition = new Vector3(100f, 0, 0);
                    //proteins[i].SetActive(false);
                    proteins[i].GetComponent<DirectionIndicator>().DirectionIndicatorObject.SetActive(false);
                }
                else
                {
                    proteins[i].transform.localPosition = Camera.main.transform.position + Camera.main.transform.forward * 1.2f;
                    //proteins[i].SetActive(true);                   
                }
            }
        }

        private void SetInteractables()
        {
            GameObject[] proteinButtons = GameObject.FindGameObjectsWithTag("ProteinButton");
            GameObject[] atomButton = GameObject.FindGameObjectsWithTag("AtomSelector");
            List<GameObject> atomsMenu = new List<GameObject>();
            foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject)))
            {
                if (go.name == "AtomsMenu")
                {
                    atomsMenu.Add(go);
                    go.AddComponent<ToggleAtomsMenu>();
                }
                else if(go.name == "ResetProtein")
                {
                    atomsMenu.Add(go);
                }
            }

            for (int i = 0; i < proteinButtons.Length; i++)
            {
                interactables.Add(proteinButtons[i]);
            }
            for (int i = 0; i < atomButton.Length; i++)
            {
                interactables.Add(atomButton[i]);
            }
            foreach(GameObject obj in atomsMenu)
            {
                interactables.Add(obj);
            }
        }
    }
}
