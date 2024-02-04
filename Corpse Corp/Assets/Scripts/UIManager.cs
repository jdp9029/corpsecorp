using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //==== FIELDS FOR MATCH SCIENTISTS TAB ====
    TMP_Dropdown dropdown1;
    TMP_Dropdown dropdown2;
    Button matchButton;

    public ScientistManager sciManager;

    List<string> purchasedScientists;
    List<string> newScientistToAdd;
    GameObject[] dropdowns;
    
    //==== START ====
    void Start()
    {
        //Initialize Dropdown Components
        if (dropdowns == null)
        {
            dropdowns = GameObject.FindGameObjectsWithTag("Dropdown");
        }
        dropdown1 = dropdowns[0].GetComponent<TMP_Dropdown>();
        dropdown2 = dropdowns[1].GetComponent<TMP_Dropdown>();

        purchasedScientists = new List<string>();
        newScientistToAdd = new List<string>();
    }

    //==== UPDATE ====
    void Update()
    {
        //Fill In Dropdowns
        for (int i = 0; i < sciManager.scientists.Length; i++)
        {
            if (sciManager.scientists[i].Purchased && !purchasedScientists.Contains(sciManager.scientists[i].name))
            {
                purchasedScientists.Add(sciManager.scientists[i].name);
                newScientistToAdd.Add(sciManager.scientists[i].name);

                dropdown1.AddOptions(newScientistToAdd);
                dropdown2.AddOptions(newScientistToAdd);
                newScientistToAdd.Clear();
            }
        }
        //if (matchButton.onClick != null)
        {

        }
    }
}
