using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] Tab[] buttonTabs;

    // Start is called before the first frame update
    void Start()
    {
        TabClicked(buttonTabs[1]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TabClicked(Tab tabClicked)
    {
        foreach (Tab tab in buttonTabs)
        {
            tab.gameObject.SetActive(tab == tabClicked);
        }
    }
}
