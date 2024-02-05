using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //==== FIELDS ====
    public ScientistManager sciManager;

    #region Match Scientists (Fields)
    TMP_Dropdown dropdown1;
    TMP_Dropdown dropdown2;
    GameObject matchButton;

    List<string> purchasedScientists; //This contains a list of ALL purchased scientists
    List<string> newScientistToAdd; //This only contains the name of a purchased scientist at any one time, for the purposes of adding to the dropdowns, and is then cleared
    GameObject[] dropdowns; //A placeholder array to find the dropdowns in the Inspector

    Scientist currentScientist1;
    Scientist currentScientist2;
    #endregion

    #region Hire Scientists (Fields)

    #endregion

    #region See Death Methods (Fields)

    #endregion

    //==== START ====
    void Start()
    {
        #region Match Scientists (Start)
        
        //Initialize Dropdown Components
        if (dropdowns == null)
        {
            dropdowns = GameObject.FindGameObjectsWithTag("Dropdown");
        }
        dropdown1 = dropdowns[0].GetComponent<TMP_Dropdown>();
        dropdown2 = dropdowns[1].GetComponent<TMP_Dropdown>();

        //Initialize Lists
        purchasedScientists = new List<string>();
        newScientistToAdd = new List<string>();

        //Initialize Match Button
        matchButton = GameObject.FindGameObjectWithTag("MatchButton");

        #endregion

        #region Hire Scientists (Start)

        #endregion

        #region See Death Methods (Start)

        #endregion
    }

    //==== UPDATE ====
    void Update()
    {
        #region Match Scientists (Update)

        //Fill In Dropdowns
        for (int i = 0; i < sciManager.scientists.Length; i++)
        {
            //If the scientist is purchased & hasn't been added to purchasedScientists already
            if (sciManager.scientists[i].Purchased && !purchasedScientists.Contains(sciManager.scientists[i].name))
            {
                //Add to both lists
                purchasedScientists.Add(sciManager.scientists[i].name);
                newScientistToAdd.Add(sciManager.scientists[i].name);

                //Use newScientistToAdd to add the singular new option to the dropdown menus
                dropdown1.AddOptions(newScientistToAdd);
                dropdown2.AddOptions(newScientistToAdd);

                //Clear newScientistToAdd so it can be used again later
                newScientistToAdd.Clear();
            }
        }

        //Set Current Scientists Selected
        currentScientist1 = FindScientist(sciManager, dropdown1.options[dropdown1.value].text);
        currentScientist2 = FindScientist(sciManager, dropdown2.options[dropdown2.value].text);

        //Set Highlights in Dropdown2 to show possible match options
        if (dropdown2.transform.Find("Dropdown List")) //If the dropdown list exists...
        {
            for (int i = 0; i < dropdown2.options.Count; i++) //Loop through all options
            {
                if (currentScientist1.combinations.ContainsKey(dropdown2.options[i].text)) //If there's a match...
                {
                    //Get a reference to the option's ColorBlock, change it, and set it as the reference
                    ColorBlock colors = dropdown2.transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").transform.Find($"Item {i}: {dropdown2.options[i].text}").GetComponent<Toggle>().colors;
                    colors.normalColor = new Color(0, 0, 255);
                    dropdown2.transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").transform.Find($"Item {i}: {dropdown2.options[i].text}").GetComponent<Toggle>().colors = colors;
                }
            }
        }

        //Determine If There's a Match
        if (currentScientist1.combinations.ContainsKey(currentScientist2.name)) //If there's a match...
        {
            matchButton.GetComponent<Image>().color = Color.green;
            matchButton.GetComponent<Button>().onClick.AddListener(ActivateMatch); //Make sure the button can only be used if there's a match
        }
        else //If there's not a match...
        {
            matchButton.GetComponent<Image>().color = Color.red;
            matchButton.GetComponent<Button>().onClick.RemoveListener(ActivateMatch); //hopefully this means that it can't be used when red
        }

        #endregion

        #region Hire Scientists (Update)

        #endregion

        #region See Death Methods (Update)

        #endregion
    }

    //==== FUNCTIONS ====

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

    //Helper Function (maybe temporary) to Activate Matches
    void ActivateMatch()
    {
        //This has nothing right now but will eventually just be toggling the newly discovered Death Method's active property to true
    }
    //FUTURE NOTE: if an object has a Renderer, you can toggle visibility by using GetComponent<Renderer>.enabled = !GetComponent<Renderer>.enabled;
    //I have not tested this but that's what the internet told me
    //Can also (supposedly) use Component.GetComponentsInChildren to toggle components of all children at once
}