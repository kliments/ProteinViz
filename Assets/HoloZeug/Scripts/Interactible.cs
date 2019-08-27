// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using System.Collections.Generic;

namespace Academy
{
    /// <summary>
    /// The Interactible class flags a GameObject as being "Interactible".
    /// Determines what happens when an Interactible is being gazed at.
    /// </summary>
    public class Interactible : MonoBehaviour, IFocusable, IInputClickHandler
    {
        [Tooltip("Audio clip to play when interacting with this hologram.")]
        public AudioClip TargetFeedbackSound;
        private AudioSource audioSource;

        private Material[] defaultMaterials;

        [SerializeField]
        private InteractibleAction interactibleAction;

        //daniel's code
        private List<GameObject> _whatToHighlight = new List<GameObject>();
        // private List<Color> _startColorList = new List<Color>();
        private List<Material> _startMaterialList = new List<Material>();
        public Material HighlightMaterial;
        public Material OutlineHighlightMaterial;
        public static bool IsAllowedToHighlight = true;
        public static bool once;

        private void Start()
        {
            defaultMaterials = GetComponent<Renderer>().materials;

            // Add a BoxCollider if the interactible does not contain one.
            Collider collider = GetComponentInChildren<Collider>();
            if (collider == null)
            {
                gameObject.AddComponent<BoxCollider>();
            }

            EnableAudioHapticFeedback();

            //daniel's code
            HighlightMaterial = GameObject.Find("ScriptManager").GetComponent<Interactible>().HighlightMaterial;
            OutlineHighlightMaterial = GameObject.Find("ScriptManager").GetComponent<Interactible>().OutlineHighlightMaterial;
        }

        private void EnableAudioHapticFeedback()
        {
            // If this hologram has an audio clip, add an AudioSource with this clip.
            if (TargetFeedbackSound != null)
            {
                audioSource = GetComponent<AudioSource>();
                if (audioSource == null)
                {
                    audioSource = gameObject.AddComponent<AudioSource>();
                }

                audioSource.clip = TargetFeedbackSound;
                audioSource.playOnAwake = false;
                audioSource.spatialBlend = 1;
                audioSource.dopplerLevel = 0;
            }
        }

        void IFocusable.OnFocusEnter()
        {
            
           // for (int i = 0; i < defaultMaterials.Length; i++)
           // {
                // Highlight the material when gaze enters.
                //defaultMaterials[i].EnableKeyword("_ENVIRONMENT_COLORING");

                //daniels code:
            if (IsAllowedToHighlight)
            {
                var parent = gameObject.transform.parent.parent;

                while (parent.name != "TER")
                {
                    parent = parent.parent;
                }

                Transform[] children = parent.GetComponentsInChildren<Transform>();
                //                var counter = 0;
                foreach (Transform child in children)
                {
                    if (child.name == "Molecule")
                    {
                        //                        counter++;
                        //                        Debug.Log(counter);
                        var renderer = child.GetComponent<Renderer>();
                        //var startColor = Color.black;
                        var startMaterial = HighlightMaterial;

                        if (child.GetComponent<Renderer>().material.name.Contains("VerticesOutline"))
                        {
                            try
                            {
                                // startColor = renderer.sharedMaterial.color;
                                startMaterial = renderer.sharedMaterial;
                            }
                            catch (Exception e)
                            {
                                Debug.Log("nope");
                                throw;
                            }

                            /*
                                                    if (renderer.material.name.Contains("VerticesOutline"))
                                                    {
                                                        switch (renderer.material.name)
                                                        {
                                                                case "VerticesOutline - Carbon":
                                                                    startColor = Color.gray;
                                                                    Debug.Log("grey");
                                                                    break;
                                                                case "VerticesOutline - Hydrogen":
                                                                    startColor = Color.white;
                                                                    break;
                                                                case "VerticesOutline - Oxygen":
                                                                    startColor = Color.red;
                                                                    break;
                                                                case "VerticesOutline - Nitrogen":
                                                                    startColor = Color.blue;
                                                                    break;
                                                                case "VerticesOutline - Sulphur":
                                                                    startColor = Color.yellow;
                                                                    break;
                                                        }
                                                    }
                                                    */

                            _startMaterialList.Add(startMaterial);
                            //renderer.material.color = Color.magenta;
                            renderer.material = OutlineHighlightMaterial;
                            _whatToHighlight.Add(child.gameObject);
                        }
                        else
                        {
                            try
                            {
                                // startColor = renderer.sharedMaterial.color;
                                startMaterial = renderer.sharedMaterial;
                            }
                            catch (Exception e)
                            {
                                Debug.Log("nope");
                                throw;
                            }

                            /*
                                                    if (renderer.material.name.Contains("VerticesOutline"))
                                                    {
                                                        switch (renderer.material.name)
                                                        {
                                                                case "VerticesOutline - Carbon":
                                                                    startColor = Color.gray;
                                                                    Debug.Log("grey");
                                                                    break;
                                                                case "VerticesOutline - Hydrogen":
                                                                    startColor = Color.white;
                                                                    break;
                                                                case "VerticesOutline - Oxygen":
                                                                    startColor = Color.red;
                                                                    break;
                                                                case "VerticesOutline - Nitrogen":
                                                                    startColor = Color.blue;
                                                                    break;
                                                                case "VerticesOutline - Sulphur":
                                                                    startColor = Color.yellow;
                                                                    break;
                                                        }
                                                    }
                                                    */

                            _startMaterialList.Add(startMaterial);
                            //renderer.material.color = Color.magenta;
                            renderer.material = HighlightMaterial;
                            _whatToHighlight.Add(child.gameObject);
                        }
                    }




                }
            }
            
      



           // }
        }

        void IFocusable.OnFocusExit()
        {
            for (int i = 0; i < defaultMaterials.Length; i++)
            {
                // Remove highlight on material when gaze exits.
                // defaultMaterials[i].DisableKeyword("_ENVIRONMENT_COLORING");

                //daniels code:
                //gameObject.GetComponent<Renderer>().material.color = startColor;

                var counter = 0;
                

                if (IsAllowedToHighlight && !once)
                {
                    foreach (var moleculePart in _whatToHighlight)
                    {
                        moleculePart.GetComponent<Renderer>().material = _startMaterialList[counter];                                       
                        counter++;
                    }

                   
                }

                if (!IsAllowedToHighlight && once)
                {
                    foreach (var moleculePart in _whatToHighlight)
                    {
                        moleculePart.GetComponent<Renderer>().material = _startMaterialList[counter];                                       
                        counter++;
                    }

                    once = false;
                }

               
            }
        }

        void IInputClickHandler.OnInputClicked(InputClickedEventData eventData)
        {
            // Play the audioSource feedback when we gaze and select a hologram.
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }

            // Perform a Tagalong action.
            if (interactibleAction != null)
            {
                interactibleAction.PerformAction();
            }
        }

        private void OnDestroy()
        {
            if(defaultMaterials!=null)
            {
                foreach (Material material in defaultMaterials)
                {
                    Destroy(material);
                }
            }
        }
    }
}