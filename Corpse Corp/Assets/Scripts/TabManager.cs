using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] public Tab[] buttonTabs;

    // Start is called before the first frame update
    void Start()
    {
        TabClicked(buttonTabs[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Occurs when a tab is selected
    /// </summary>
    /// <param name="tabClicked">The tab that was selected</param>
    public void TabClicked(Tab tabClicked)
    {
        //we need to loop through each of the button tabs
        foreach (Tab tab in buttonTabs)
        {
            //we are going to make the other four tabs inactive

            //we do not set the tabs THEMSELVES to be inactive because they still need to be clickable

            //instead we set the contents of the tabs to be inactive so they're invisible

            //i starts at 1 because we skip the first element, which colors/displays the tabs themselves
            for(int i = 1; i < tab.transform.childCount; i++)
            {
                GameObject gameObject = tab.transform.GetChild(i).gameObject;
                gameObject.SetActive(tab == tabClicked);
            }

            //enlarge the size of the selected tab so we have an idea of what we are doing here
            if(tabClicked == tab)
            {
                tab.transform.GetChild(0).localScale = new Vector3(1, 1.2f, 1);
            }
            else
            {
                tab.transform.GetChild(0).localScale = Vector3.one;
            }
        }
    }
}
