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

    [SerializeField] ScrollRect tab3scrollView;
    [SerializeField] ScrollRect tab4scrollView;

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

    private void Update()
    {
        if(toggledOnEcon && tab3scrollView.verticalNormalizedPosition <= .99f)
        {
            toggledOnEcon = false;
            ToggleParent();
        }
        
        if(toggledOnHire && tab4scrollView.verticalNormalizedPosition <= .99f)
        {
            toggledOnHire = false;
            ToggleParent();
        }
    }

    public void SwitchToTab(bool econTab)
    {
        if(!pressedEconYet && econTab)
        {
            tab3scrollView.verticalNormalizedPosition = 1;
            pressedEconYet = true;
            toggledOnEcon = true;
            toggledOnHire = false;
        }
        else if(!pressedHireYet && !econTab)
        {
            tab4scrollView.verticalNormalizedPosition = 1;
            pressedHireYet = true;
            toggledOnHire = true;
            toggledOnEcon = false;
        }
        else
        {
            if(econTab)
            {
                tab3scrollView.verticalNormalizedPosition = 1;
            }
            else
            {
                tab4scrollView.verticalNormalizedPosition = 1;
            }

            toggledOnHire = false;
            toggledOnEcon = false;
        }
        ToggleParent();
    }

    void ToggleParent()
    {
        for (int i = 0; i < TutorialParentEconScreen.childCount; i++)
        {
            if(toggledOnEcon && tab3scrollView.verticalNormalizedPosition <= .99f)
            {
                tab3scrollView.verticalNormalizedPosition = 1;
            }    

            TutorialParentEconScreen.GetChild(i).gameObject.SetActive(toggledOnEcon);
        }
        for (int i = 0; i < TutorialParentHireScreen.childCount; i++)
        {
            if (toggledOnHire && tab4scrollView.verticalNormalizedPosition <= .99f)
            {
                tab4scrollView.verticalNormalizedPosition = 1;
            }

            TutorialParentHireScreen.GetChild(i).gameObject.SetActive(toggledOnHire);
        }
    }
}
