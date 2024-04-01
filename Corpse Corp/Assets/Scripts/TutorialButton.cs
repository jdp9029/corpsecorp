using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TutorialButton : MonoBehaviour
{
    //the rect transforms with the objects that for each tab
    [SerializeField] RectTransform TutorialParentEconScreen;
    [SerializeField] RectTransform TutorialParentHireScreen;

    //whether or not the tutorial objects are being shown
    bool toggledOnEcon;
    bool toggledOnHire;

    //whether or not these tabs have been pressed (tutorial turns on when open)
    bool pressedEconYet = false;
    bool pressedHireYet = false;

    // Start is called before the first frame update
    void Start()
    {
        toggledOnHire = true;
        toggledOnEcon = false;
        ToggleParent();
        GetComponent<Button>().onClick.AddListener(delegate
        {
            if (FindObjectOfType<TabManager>().selectedTab == 3)
            {
                toggledOnEcon = !toggledOnEcon;
            }
            else
            {
                toggledOnHire = !toggledOnHire;
            }

            ToggleParent();
        });
    }

    public void SwitchToTab(bool econTab)
    {
        if(!pressedEconYet && econTab)
        {
            pressedEconYet = true;
            toggledOnEcon = true;
            toggledOnHire = false;
        }
        else if(!pressedHireYet && !econTab)
        {
            pressedHireYet = true;
            toggledOnHire = true;
            toggledOnEcon = false;
        }
        else
        {
            toggledOnHire = false;
            toggledOnEcon = false;
        }
        ToggleParent();
    }

    void ToggleParent()
    {
        for (int i = 0; i < TutorialParentEconScreen.childCount; i++)
        {
            TutorialParentEconScreen.GetChild(i).gameObject.SetActive(toggledOnEcon);
        }
        for (int i = 0; i < TutorialParentHireScreen.childCount; i++)
        {
            TutorialParentHireScreen.GetChild(i).gameObject.SetActive(toggledOnHire);
        }
    }
}
