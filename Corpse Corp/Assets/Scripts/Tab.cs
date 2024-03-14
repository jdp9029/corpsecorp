using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    public int tabNum;
    public void OnMouseClick()
    {
        GameObject.FindObjectOfType<TabManager>().TabClicked(this);
    }
}
