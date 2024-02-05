using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    [SerializeField] Tab[] tabs;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TabManager Start is being called");
        TabClicked(tabs[2]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TabClicked(Tab tabClicked)
    {
        foreach (Tab tab in tabs)
        {
            tab.gameObject.SetActive(tab == tabClicked);
        }
    }
}
