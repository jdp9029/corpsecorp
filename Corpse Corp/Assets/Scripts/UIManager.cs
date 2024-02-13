using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    bool matchBtnActive;

    List<string> purchasedScientists; //This contains a list of ALL purchased scientists
    List<string> newScientistToAdd; //This only contains the name of a purchased scientist at any one time, for the purposes of adding to the dropdowns, and is then cleared
    GameObject[] dropdowns; //A placeholder array to find the dropdowns in the Inspector

    Scientist currentScientist1;
    Scientist currentScientist2;

    TMP_Text discoveryBanner;
    #endregion

    #region See Death Methods (Fields)
    TMP_Text[] textObjects;
    TMP_Text dmList;
    string listText;
    #endregion

    #region Clicker (Fields)
    [SerializeField] Button clickerButton;
    [SerializeField] Button leftButton;
    [SerializeField] Button rightButton;
    [SerializeField] TMP_Text clickerText;

    int dmIndex;
    bool leftButtonActive;
    bool rightButtonActive;

    List<DeathMethod> activeDeathMethods;
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
        matchBtnActive = false;

        discoveryBanner = GameObject.Find("Discovery Banner").GetComponent<TMP_Text>();

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

        #region Clicker (Start)
        dmIndex = 0;
        leftButtonActive = false;
        rightButtonActive = false;

        activeDeathMethods = new List<DeathMethod>();
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

        //Set Highlights in Dropdowns to show possible match options
        if (dropdown1.transform.Find("Dropdown List")) //If the dropdown 1 list exists...
        {
            for (int i = 0; i < dropdown1.options.Count; i++) //Loop through all dropdown 1 options
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
            if (!matchBtnActive)
            {
                matchButton.GetComponent<Button>().onClick.AddListener(ActivateMatch); //Make sure the button can only be used if there's a match
                matchBtnActive = true;
            }
        }
        else //If there's not a match...
        {
            matchButton.GetComponent<Image>().color = Color.red;
            matchButton.GetComponent<Button>().onClick.RemoveListener(ActivateMatch); //hopefully this means that it can't be used when red
            matchBtnActive = false;
        }

        #endregion

        #region See Death Methods (Update)
        listText = ""; //Clear Text Placeholder
        for (int i = 0; i < dmManager.deathMethods.Count; i++) //Loop through Death Methods
        {
            if (dmManager.deathMethods[i].active) //If a Death Method is active (discovered / default from a purchased scientist)...
            {
                listText += $"{dmManager.deathMethods[i].name} - {dmManager.deathMethods[i].scientist1name}"; //Add the name & scientist name to the text placeholder
                if (dmManager.deathMethods[i].scientist2name != null) //If it's not a default from a purchased scientist...
                {
                    listText += $" x {dmManager.deathMethods[i].scientist2name}"; //Add in second scientist name
                }
                listText += "\n";
            }
        }
        dmList.text = listText; //Set the death method list text to the text placeholder string
        #endregion

        #region Clicker (Update)
        //Populate Active DM Array
        activeDeathMethods.Clear();
        for (int i = 0; i < dmManager.deathMethods.Count; i++)
        {
            if (dmManager.deathMethods[i].active)
            {
                activeDeathMethods.Add(dmManager.deathMethods[i]);
            }
        }
        
        //Fill In Clicker Button (DM Name for now, will be DM Icon)
        clickerButton.transform.GetChild(0).GetComponent<TMP_Text>().text = activeDeathMethods[dmIndex].name;
        
        //Left Button Functionality
        if (dmIndex - 1 >= 0)
        {
            leftButton.GetComponent<Image>().color = Color.white;
            if (!leftButtonActive)
            {
                leftButton.onClick.AddListener(SubtractIndex);
                leftButtonActive = true;
            }
        }
        else
        {
            leftButton.GetComponent<Image>().color = Color.gray;
            leftButton.onClick.RemoveListener(SubtractIndex);
            leftButtonActive = false;
        }

        //Right Button Functionality
        if (dmIndex + 1 < activeDeathMethods.Count)
        {
            rightButton.GetComponent<Image>().color = Color.white;
            if (!rightButtonActive)
            {
                rightButton.onClick.AddListener(AddIndex);
                rightButtonActive = true;
            }
        }
        else
        {
            rightButton.GetComponent<Image>().color = Color.gray;
            rightButton.onClick.RemoveListener(AddIndex);
            rightButtonActive = false;
        }
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

    //Helper Function to Activate Matches
    void ActivateMatch()
    {
        for (int i = 0; i < dmManager.deathMethods.Count; i++)
        {
            if (dmManager.deathMethods[i].name == currentScientist1.combinations[currentScientist2.name])
            {
                dmManager.deathMethods[i].active = true;
                StartCoroutine(PrintDiscoveryMessage(5.0f, dmManager.deathMethods[i]));
            }
        }
    }

    //Coroutine that prints a congrats message for waitTime seconds
    private IEnumerator PrintDiscoveryMessage(float waitTime, DeathMethod dm)
    {
        discoveryBanner.text = $"Congratulations! You've Discovered {dm.name}!";
        yield return new WaitForSeconds(waitTime);
        discoveryBanner.text = "";
    }
    private void AddIndex() { dmIndex++; }
    private void SubtractIndex() { dmIndex--; }
}
