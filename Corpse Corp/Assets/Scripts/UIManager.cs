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
    GameObject matchButton;

    public ScientistManager sciManager;

    List<string> purchasedScientists; //This contains a list of ALL purchased scientists
    List<string> newScientistToAdd; //This only contains the name of a purchased scientist at any one time, for the purposes of adding to the dropdowns, and is then cleared
    GameObject[] dropdowns; //A placeholder array to find the dropdowns in the Inspector

    Scientist currentScientist1;
    Scientist currentScientist2;
    
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

        matchButton = GameObject.FindGameObjectWithTag("MatchButton");
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

        //Set Current Scientists Selected
        currentScientist1 = FindScientist(sciManager, dropdown1.options[dropdown1.value].text);
        currentScientist2 = FindScientist(sciManager, dropdown2.options[dropdown2.value].text);

        //Determine If There's a Match
        if (currentScientist1.combinations.ContainsKey(currentScientist2.name))
        {
            matchButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            matchButton.GetComponent<Image>().color = Color.red;
        }
    }

    //Helper Function to Find Scientists by Name
    Scientist FindScientist(ScientistManager manager, string name)
    {
        Scientist scientist = null;
        for (int i = 0; i < manager.scientists.Length; i++)
        {
            if (manager.scientists[i].name == name)
            {
                //Debug.Log(manager.scientists[i].name);
                scientist = manager.scientists[i];
                return scientist;
            }
        }
        return scientist;
    }
}
