using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleAtomsMenu : MonoBehaviour
{
    private GameObject menu;
    private bool active, swipeUp, swipeDown;
    private Vector3 upPos, downPos;

    private GameObject highlightingBox;
    private Color pressedColor, originalColor;
    
    // Use this for initialization
    void Start()
    {
        active = true;
        menu = transform.parent.GetChild(7).gameObject;
        upPos = new Vector3(-0.236f, 0, 0);
        downPos = new Vector3(-0.236f, -0.136f, 0);
        swipeDown = true;

        highlightingBox = Instantiate(transform.GetChild(1).gameObject);
        highlightingBox.transform.parent = transform;
        highlightingBox.transform.localPosition = new Vector3(0, -0.0002774671f, -0.0103f);
        highlightingBox.transform.localRotation = Quaternion.identity;
        highlightingBox.transform.localScale = new Vector3(0.12f, 0.12f, 0.01f);
        originalColor = transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().material.color;
        pressedColor = transform.parent.GetChild(7).transform.GetChild(0).gameObject.GetComponent<Atom_Selector>().currentProtein.GetComponent<ColorProperty>().color;
    }

    // Update is called once per frame
    void Update()
    {
        if(swipeUp)
        {
            menu.transform.localPosition = Vector3.Lerp(menu.transform.localPosition, upPos, 10 * Time.deltaTime);
            if (Vector3.Distance(menu.transform.localPosition,upPos)<0.025f)
            {
                menu.transform.localPosition = upPos;
                swipeUp = false;
                menu.SetActive(false);
            }
            highlightingBox.GetComponent<MeshRenderer>().material.color = originalColor;
        }
        if(swipeDown)
        {
            menu.SetActive(true);
            menu.transform.localPosition = Vector3.Lerp(menu.transform.localPosition, downPos, 10 * Time.deltaTime);
            if (Vector3.Distance(menu.transform.localPosition, downPos) < 0.025f)
            {
                menu.transform.localPosition = downPos;
                swipeDown = false;
            }
            highlightingBox.GetComponent<MeshRenderer>().material.color = pressedColor;
        }
    }

    public void Toggle()
    {
        if(active)
        {
            active = false;
            swipeUp = true;
        }
        else
        {
            active = true;
            swipeDown = true;
        }
    }
}
