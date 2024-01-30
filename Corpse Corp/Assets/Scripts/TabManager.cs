using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    GameObject[] buttonTabs;

    // Start is called before the first frame update
    void Start()
    {
        buttonTabs = GetComponentsInChildren<GameObject>();

        //look through bloodshed skilltree node code and find out the code for hovering, selecting
        //apply it here so that the height of the selected tab is a bit bigger and the color is a bit different

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
