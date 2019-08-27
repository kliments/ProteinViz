using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using System;
using HoloToolkit.Unity.InputModule;

public class VoiceRecognizer : MonoBehaviour {
    public GameObject menu;
    public GameObject[] resetButtons;
    public GameObject dataLoader;
    public static bool RotationMode;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    // Use this for initialization
    void Start () {

        keywords.Add("show menu", () =>
        {
            menu.SetActive(true);
          //  Debug.Log("show Menu");
        });
        keywords.Add("hide menu", () =>
        {
            menu.SetActive(false);
          //  Debug.Log("Hide Menu");
        });
        keywords.Add("Enable Highlighting", () =>
        {
            Academy.Interactible.IsAllowedToHighlight = true;
            // Debug.Log("Enable Highlighting");
        });
        keywords.Add("Disable Highlighting", () =>
        {
            Academy.Interactible.IsAllowedToHighlight = false;
            Academy.Interactible.once = true;
            //  Debug.Log("Disable Highlighting");
        });
        keywords.Add("Enable Autorotation", () =>
        {
            AutoRotation.RotationEnabled = true;
            //  Debug.Log("Disable Highlighting");
        });
        keywords.Add("Disable Autorotation", () =>
        {
            AutoRotation.RotationEnabled = false;
            //  Debug.Log("Disable Highlighting");
        });
        keywords.Add("Reset Scene", () =>
        {
            if (resetButtons.Length == 0)
            {
                resetButtons = GameObject.FindGameObjectsWithTag("ResetButton");
            }
            if(dataLoader == null)
            {
                dataLoader = GameObject.Find("DataLoader");
            }
            foreach (var reset in resetButtons)
            {
                reset.GetComponent<ResetProtein>().ResetThisProtein();
                if(reset.GetComponent<ResetProtein>().proteinToReset.name != "SmallProtein1")
                {
                    //relocate everything except the small protein
                    reset.GetComponent<ResetProtein>().proteinToReset.transform.localPosition = new Vector3(100, 0, 0);
                }
            }
            foreach(Transform child in dataLoader.transform)
            {
                if(child.name == "SmallProtein1")
                {
                    child.gameObject.GetComponent<ToggleProperties>().isHighlighted = true;
                }
                else
                {
                    child.gameObject.GetComponent<ToggleProperties>().isHighlighted = false;
                }
            }
        });
        keywords.Add("Rotation Mode", () =>
        {
            //Academy.GestureAction.ManualRotationEnabled = true;
            GameObject[] GameObjects = GameObject.FindGameObjectsWithTag("Protein");
            foreach (var protein in GameObjects)
            {
                protein.AddComponent<Academy.GestureAction>();
                protein.GetComponent<HoloToolkit.Unity.InputModule.HandDraggable>().enabled = false;
            }

            RotationMode = true;

        });
        keywords.Add("Translation Mode", () =>
        {
           // Academy.GestureAction.ManualRotationEnabled = false;
            GameObject[] GameObjects = GameObject.FindGameObjectsWithTag("Protein");
            foreach (var protein in GameObjects)
            {
               Destroy(protein.GetComponent<Academy.GestureAction>());
               protein.GetComponent<HoloToolkit.Unity.InputModule.HandDraggable>().enabled = true;
            }

            RotationMode = false;

        });
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }
}
