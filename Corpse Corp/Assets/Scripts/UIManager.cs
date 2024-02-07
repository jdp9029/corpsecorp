using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //==== FIELDS ====
    [SerializeField] public ScientistManager sciManager;
    [SerializeField] DeathMethodManager dmManager;

    #region Match Scientists (Fields)
    TMP_Dropdown dropdown1;
    TMP_Dropdown dropdown2;
    GameObject matchButton;
    bool btnActive;

    List<string> purchasedScientists; //This contains a list of ALL purchased scientists
    List<string> newScientistToAdd; //This only contains the name of a purchased scientist at any one time, for the purposes of adding to the dropdowns, and is then cleared
    GameObject[] dropdowns; //A placeholder array to find the dropdowns in the Inspector

    Scientist currentScientist1;
    Scientist currentScientist2;
    #endregion

    #region See Death Methods (Fields)
    TMP_Text[] textObjects;
    TMP_Text dmList;
    string listText;
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
        btnActive = false;

        #endregion

        #region See Death Methods (Start)
        textObjects = GameObject.FindObjectsOfType<TMP_Text>();
        for (int i = 0; i < textObjects.Length; i++)
        {
            if (textObjects[i].name == "DM List")
            {
                dmList = textObjects[i];
            }
        }
        listText = "";
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
        if (dropdown1.transform.Find("Dropdown List")) //If the dropdown 2 list exists...
        {
            for (int i = 0; i < dropdown1.options.Count; i++) //Loop through all dropdown 2 options
            {
                if (currentScientist2.combinations.ContainsKey(dropdown1.options[i].text)) //If there's a match...
                {
                    //Get a reference to the option's ColorBlock, change it, and set it as the reference
                    ColorBlock colors = dropdown1.transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").transform.Find($"Item {i}: {dropdown1.options[i].text}").GetComponent<Toggle>().colors;
                    colors.normalColor = new Color(0, 1, 1, 1);
                    colors.selectedColor = new Color(0, 1, 1, 1);
                    dropdown1.transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").transform.Find($"Item {i}: {dropdown1.options[i].text}").GetComponent<Toggle>().colors = colors;
                }
            }
        }

        if (dropdown2.transform.Find("Dropdown List")) //If the dropdown 2 list exists...
        {
            for (int i = 0; i < dropdown2.options.Count; i++) //Loop through all dropdown 2 options
            {
                if (currentScientist1.combinations.ContainsKey(dropdown2.options[i].text)) //If there's a match...
                {
                    //Get a reference to the option's ColorBlock, change it, and set it as the reference
                    ColorBlock colors = dropdown2.transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").transform.Find($"Item {i}: {dropdown2.options[i].text}").GetComponent<Toggle>().colors;
                    colors.normalColor = new Color(0, 1, 1, 1);
                    colors.selectedColor = new Color(0, 1, 1, 1);
                    dropdown2.transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").transform.Find($"Item {i}: {dropdown2.options[i].text}").GetComponent<Toggle>().colors = colors;
                }
            }
        }

        //Determine If There's a Match
        if (currentScientist1.combinations.ContainsKey(currentScientist2.name)) //If there's a match...
        {
            matchButton.GetComponent<Image>().color = Color.green;
            if (!btnActive)
            {
                matchButton.GetComponent<Button>().onClick.AddListener(ActivateMatch); //Make sure the button can only be used if there's a match
                btnActive = true;
            }
        }
        else //If there's not a match...
        {
            matchButton.GetComponent<Image>().color = Color.red;
            matchButton.GetComponent<Button>().onClick.RemoveListener(ActivateMatch); //hopefully this means that it can't be used when red
            btnActive = false;
        }

        #endregion

        #region See Death Methods (Update)
        listText = "";
        for (int i = 0; i < dmManager.deathMethods.Count; i++)
        {
            if (dmManager.deathMethods[i].active)
            {
                listText += $"{dmManager.deathMethods[i].name} - {dmManager.deathMethods[i].scientist1name}"; //Need to figure out a way to construct initial DMs
                if (dmManager.deathMethods[i].scientist2name != null)
                {
                    listText += $" x {dmManager.deathMethods[i].scientist2name}";
                }
                listText += "\n";
            }
        }
        dmList.text = listText;
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
                scientist = manager.scientists[i];
                return scientist;
            }
        }
        return scientist;
    }

    //Helper Function (maybe temporary) to Activate Matches
    void ActivateMatch()
    {
        for (int i = 0; i < dmManager.deathMethods.Count; i++)
        {
            if (dmManager.deathMethods[i].name == currentScientist1.combinations[currentScientist2.name])
            {
                dmManager.deathMethods[i].active = true;
            }
        }
    }
}
