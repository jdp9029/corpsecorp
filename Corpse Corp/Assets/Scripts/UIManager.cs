using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UIManager : MonoBehaviour
{
    //==== FIELDS ====
    [SerializeField] public ScientistManager sciManager;
    [SerializeField] DeathMethodManager dmManager;

    [SerializeField] TMP_Text statBar;

    [SerializeField] GameObject discoveryBanner;
    [SerializeField] GameObject discoveryParent;

    DMSlot dmslot;

    #region Clicker (Fields)
    List<DeathMethod> activeDeathMethods;
    List<GameObject> dmPrefabList;
    [SerializeField] GameObject dmPrefab;
    [SerializeField] Transform dmContent; //This is ScrollViewPanel --> View --> Content

    #endregion

    #region Hire Scientists (Fields)
    public List<GameObject> hirePrefabs;
    public Dictionary<string, float> slidersFilling;
    public List<EmployButton> employButtons;
    #endregion

    //==== START ====
    void Start()
    {
        /*#region Match Scientists (Start)

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

        #endregion*/

        /*#region See Death Methods (Start)
        textObjects = GameObject.FindObjectsOfType<TMP_Text>();
        for (int i = 0; i < textObjects.Length; i++)
        {
            if (textObjects[i].name == "DM List")
            {
                dmList = textObjects[i];
            }
        }
        listText = "";
        #endregion*/

        #region Clicker (Start)
        activeDeathMethods = new List<DeathMethod>();
        dmPrefabList = new List<GameObject>();

        dmslot = GameObject.Find("DMBox").GetComponent<DMSlot>();
        #endregion

        #region Hire Scientists (Start)
        //hirePrefabs = GameObject.FindGameObjectsWithTag("ForHirePrefab");
        //UnityEngine.Debug.Log(hirePrefabs.Length);
        slidersFilling = new Dictionary<string, float>();
        employButtons = new List<EmployButton>();



        #endregion
    }

    //==== UPDATE ====
    void Update()
    {
       /* #region Match Scientists (Update)

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
                    if (FindDeathMethod(dmManager, currentScientist2.combinations[dropdown1.options[i].text]).active) //If the DM is active, change color of dropdown option to gray
                    {
                        ColorBlock colors = dropdown1.transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").transform.Find($"Item {i}: {dropdown1.options[i].text}").GetComponent<Toggle>().colors;
                        colors.normalColor = new Color(0.5f, 0.5f, 0.5f, 1);
                        colors.selectedColor = new Color(0.5f, 0.5f, 0.5f, 1);
                        dropdown1.transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").transform.Find($"Item {i}: {dropdown1.options[i].text}").GetComponent<Toggle>().colors = colors;
                    }
                }
            }
        }

        if (dropdown2.transform.Find("Dropdown List")) //If the dropdown 2 list exists...
        {
            for (int i = 0; i < dropdown2.options.Count; i++) //Loop through all dropdown 2 options
            {
                if (currentScientist1.combinations.ContainsKey(dropdown2.options[i].text)) //If there's a match...
                {
                    if (FindDeathMethod(dmManager, currentScientist1.combinations[dropdown2.options[i].text]).active) //If the DM is active, change color of dropdown option to gray
                    {
                        ColorBlock colors = dropdown2.transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").transform.Find($"Item {i}: {dropdown2.options[i].text}").GetComponent<Toggle>().colors;
                        colors.normalColor = new Color(0.5f, 0.5f, 0.5f, 1);
                        colors.selectedColor = new Color(0.5f, 0.5f, 0.5f, 1);
                        dropdown2.transform.Find("Dropdown List").transform.Find("Viewport").transform.Find("Content").transform.Find($"Item {i}: {dropdown2.options[i].text}").GetComponent<Toggle>().colors = colors;
                    }
                }
            }
        }

        //Determine If There's a Match
        if (currentScientist1.combinations.ContainsKey(currentScientist2.name)) //If there's a match...
        {
            if (FindDeathMethod(dmManager, currentScientist1.combinations[currentScientist2.name]).active) //If that death method is already discovered, turn button yellow & turn it off
            {
                matchButton.GetComponent<Image>().color = Color.yellow;
                matchButton.GetComponent<Button>().onClick.RemoveAllListeners(); //Button can no longer be used
                matchBtnActive = false;
            }
            else //If it's a new match, turn button green and activate it
            {
                matchButton.GetComponent<Image>().color = Color.green;
                if (!matchBtnActive)
                {
                    matchButton.GetComponent<Button>().onClick.AddListener(delegate { ActivateMatch(currentScientist1, currentScientist2); }); //Make sure the button can only be used if there's a match
                    matchBtnActive = true;
                }
            }
        }
        else //If there's not a match, turn button gray & deactivate it
        {
            matchButton.GetComponent<Image>().color = Color.gray;
            matchButton.GetComponent<Button>().onClick.RemoveAllListeners(); //Button can no longer be used
            matchBtnActive = false;
        }

        #endregion
*/
        /*
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
        */
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

        //Sort Active DM Array by Price
        for (int i = 0; i < activeDeathMethods.Count - 1; i++)
        {
            for (int j = 0; j < activeDeathMethods.Count - i - 1; j++)
            {
                if (activeDeathMethods[j].price > activeDeathMethods[j + 1].price)
                {
                    DeathMethod tempDM = activeDeathMethods[j];
                    activeDeathMethods[j] = activeDeathMethods[j + 1];
                    activeDeathMethods[j + 1] = tempDM;
                }
            }
        }

        //Loop Through & Instantiate Active Death Method Prefabs
        for (int i = 0; i < activeDeathMethods.Count; i++)
        {
            if (!activeDeathMethods[i].instantiated) //Check if it's instantiated to only instantiate it once
            {
                GameObject dmInst = Instantiate(dmPrefab, Vector3.zero, Quaternion.identity, dmContent); //Instantiate Prefab
                dmPrefabList.Add(dmInst); //Add to prefab list

                //Set Info
                dmInst.transform.GetChild(0).GetComponent<TMP_Text>().text = activeDeathMethods[i].name; //DMName
                dmInst.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = $"<b>${activeDeathMethods[i].price} / {activeDeathMethods[i].rateOfSale} seconds</b>"; //BaseLoadRect

                DeathMethod thisDM = FindDeathMethod(dmManager, dmInst.transform.GetChild(0).GetComponent<TMP_Text>().text);
                if (thisDM.scientist2name != null) //If there is a Scientist 2
                {
                    dmInst.transform.GetChild(3).GetChild(1).GetComponent<TMP_Text>().text = $"{thisDM.scientist1name} & {thisDM.scientist2name}";
                }
                else //If there's only Scientist 1
                {
                    dmInst.transform.GetChild(3).GetChild(1).GetComponent<TMP_Text>().text = $"{thisDM.scientist1name}";
                }

                activeDeathMethods[i].instantiated = true;
            }
        }

        //Sort Death Method Prefabs By Price
        for (int i = 0; i < dmPrefabList.Count - 1; i++)
        {
            for (int j = 0; j < dmPrefabList.Count - i - 1; j++)
            {
                //Find Death Methods
                DeathMethod dm1 = FindDeathMethod(dmManager, dmPrefabList[j].transform.GetChild(0).GetComponent<TMP_Text>().text);
                DeathMethod dm2 = FindDeathMethod(dmManager, dmPrefabList[j + 1].transform.GetChild(0).GetComponent<TMP_Text>().text);

                RectTransform dm1rect = dmPrefabList[j].transform.GetComponent<RectTransform>();
                RectTransform dm2rect = dmPrefabList[j + 1].transform.GetComponent<RectTransform>();

                //If one is priced higher than the other,swap their positions
                if (dm1.price < dm2.price)
                {
                    //Swap Rect Positions
                    Vector2 tempPos = new Vector2(dm1rect.position.x, dm1rect.position.y);
                    dm1rect.position = new Vector2(dm1rect.position.x, dm2rect.position.y);
                    dm2rect.position = tempPos;

                    //Swap Positions In List
                    GameObject tempDMPrefab = dmPrefabList[j];
                    dmPrefabList[j] = dmPrefabList[j + 1];
                    dmPrefabList[j + 1] = tempDMPrefab;
                }
            }
        }

        //Within Each Death Method Prefab...
        foreach(GameObject dmP in dmPrefabList)
        {
            DeathMethod dm = FindDeathMethod(dmManager, dmP.transform.GetChild(0).GetComponent<TMP_Text>().text);

            //Get Scaling Rect
            RectTransform boostRect = dmP.transform.Find("BaseLoadRect").Find("ScalingLoadRect").GetComponent<RectTransform>();

            //Increment size by RateOfSale
            IncrementAnchor(dm.rateOfSale, boostRect);
            if (boostRect.anchorMax.x >= 1)
            {
                dm.UpdateMoney();
                boostRect.anchorMax = new Vector2(0.00000001f, boostRect.anchorMax.y);
                boostRect.offsetMin = Vector2.zero;
                boostRect.offsetMax = Vector2.zero;
            }
            
            //Update BaseRect Text
            dmP.transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>().text = $"<b>${Mathf.Round(dm.price)} / {Mathf.Round(dm.rateOfSale)} seconds</b>";

            //Remove Listeners From Boost Button
            dmP.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();

            //Grey out the boost button if too expensive (and we are not actively upgrading it)
            if (dmManager.money < dm.boostCost && !dm.beingBoosted)
            {
                dmP.transform.GetChild(2).GetComponent<Image>().color = Color.gray;
            }
            else
            {
                dmP.transform.GetChild(2).GetComponent<Image>().color = Color.white;
            }

            //Check if Scientist 2 is Null
            if (dm.scientist2name != null) //If Not
            {
                //Hook Up Boost Button, if we can afford it & Scientists aren't busy
                if (dmManager.money > dm.boostCost && !FindScientist(sciManager, dm.scientist1name).busy && !FindScientist(sciManager, dm.scientist2name).busy)
                {
                    dmP.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(BoostDMEcon(dm, FindScientist(sciManager, dm.scientist1name), FindScientist(sciManager, dm.scientist2name))); });
                }
            }
            else //If this is a pure DM (no scientist 2 exists)
            {
                //Hook Up the Boost Button if we can afford it & Scientist isn't busy
                if (dmManager.money > dm.boostCost && !FindScientist(sciManager, dm.scientist1name).busy)
                {
                    dmP.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { StartCoroutine(BoostDMEcon(dm, FindScientist(sciManager, dm.scientist1name))); });
                }
            }
            //---

            //Moves how much of the boost bar gets filled mid-upgrade
            boostRect = dmP.transform.GetChild(2).Find("ScalingLoadRect").GetComponent<RectTransform>();

            if (dm.beingBoosted)
            {
                IncrementAnchor(dm.boostTime, boostRect);
            }
            //If we're not mid upgrade, it should not be filled at all
            else
            {
                boostRect.anchorMax = new Vector2(0.00000001f, boostRect.anchorMax.y);
                boostRect.offsetMin = Vector2.zero;
                boostRect.offsetMax = Vector2.zero;
            }

            //Check to see if/why Scientist1 is busy, and scale its scale rect accordingly
            boostRect = dmP.transform.GetChild(3).Find("ScalingLoadRect").GetComponent<RectTransform>();
        }
        #endregion

        #region Hire Scientists (Update)
        for (int i = 0; i < hirePrefabs.Count; i++) //Loop thru hire prefabs
        {
            if (hirePrefabs[i].GetComponent<Scientist>().Purchased) { continue; }
            if (dmManager.money < hirePrefabs[i].GetComponent<Scientist>().price) //If you don't have enough money to purchase a scientist, deactivate button & change color
            {
                hirePrefabs[i].transform.GetChild(1).GetComponent<Image>().color = Color.gray;
            }
            else
            {
                hirePrefabs[i].transform.GetChild(1).GetComponent<Image>().color = Color.green;
            }
        }

        SortScientistsByCost();

        for(int i = 0; i < slidersFilling.Count; i++)
        {
            string slider = slidersFilling.ElementAt(i).Key;
            slidersFilling[slider] += Time.deltaTime;
        }

        //a comment that an be removed right here
        //if there are employ buttons, set them up
        if(GameObject.FindObjectsOfType<EmployButton>().Count() > 0)
        {
            employButtons = GameObject.FindObjectsOfType<EmployButton>().ToList();
        }
        #endregion

        statBar.text = $"<i>MONEY: ${Mathf.Round(dmManager.money)} ; ${Mathf.Round(dmManager.moneyPerSecond)} / s</i>";
    }

    //==== FUNCTIONS ====

    //Helper Function to Find Scientists by Name
    public Scientist FindScientist(ScientistManager manager, string name)
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
    public DeathMethod FindDeathMethod(DeathMethodManager manager, string name)
    {
        DeathMethod method = null;
        foreach (DeathMethod dm in manager.deathMethods)
        {
            if (dm.name == name)
            {
                method = dm;
                return method;
            }
        }
        return method;
    }

    //Helper Function to Activate Matches
    public void ActivateMatch(Scientist sci1, Scientist sci2)
    {
        for (int i = 0; i < dmManager.deathMethods.Count; i++)
        {
            if (dmManager.deathMethods[i].name == sci1.combinations[sci2.name])
            {
                dmManager.deathMethods[i].active = true;
                StartCoroutine(PrintDiscoveryMessage(1.5f, dmManager.deathMethods[i]));
                //GameObject.FindObjectOfType<UIManager>().AddToInventory(dmManager.deathMethods[i]);
            }
        }
    }

    public void SetHirePrefabText(GameObject prefabInstance, int price)
    {
        prefabInstance.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "$" + price.ToString();
    }

    //Coroutine that prints a congrats message for waitTime seconds
    public IEnumerator PrintDiscoveryMessage(float waitTime, DeathMethod dm)
    {
        GameObject discoveryInst = Instantiate(discoveryBanner, Vector3.zero, Quaternion.identity, discoveryParent.transform);
        discoveryInst.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Congratulations! You've Discovered <b>{dm.name}!</b>";
        discoveryInst.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
        discoveryInst.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
        discoveryInst.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
        discoveryInst.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        yield return new WaitForSeconds(waitTime);
        discoveryInst.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        Destroy(discoveryInst.gameObject);
    }

    //Coroutine that prints a congrats message for waitTime seconds
    public IEnumerator PrintBoostCompleteMessage(float waitTime, Scientist sci, DeathMethod dm)
    {
        GameObject discoveryInst = Instantiate(discoveryBanner, Vector3.zero, Quaternion.identity, discoveryParent.transform);
        discoveryInst.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{sci.name} has successfully boosted {dm.name}";
        discoveryInst.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
        discoveryInst.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
        discoveryInst.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
        discoveryInst.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        yield return new WaitForSeconds(waitTime);
        discoveryInst.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        Destroy(discoveryInst.gameObject);
    }

    /*public void AddToInventory(DeathMethod dm)
    {
        GameObject obj = Instantiate(InventoryAssetPrefab, Tab1Content.transform);
        obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = dm.name;
        //obj.transform.GetChild(1).GetComponent<Image>().sprite = dm.Icon; commented out for now due to compiler reasons
    }*/

    /*public void AddScientistToInventory(Scientist sci)
    {
        GameObject obj = Instantiate(InventoryAssetPrefab, Tab1ContentScientists.transform);
        obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci.name;
        obj.transform.GetChild(1).GetComponent<Image>().sprite = sci.Icon;
    }*/

    //Boost DM Price After A Certain Amount of Time (BoostCost)
    public IEnumerator BoostDMEcon(DeathMethod deathMethod, Scientist sci1, Scientist sci2 = null)
    {       
        //make the scientist busy and set up the DM to being boosted
        sci1.busy = true;
        if (sci2 != null)
        {
            sci2.busy = true;
        }
        deathMethod.beingBoosted = true;
        //deathMethod.scientistBoostingThis = sci;

        //pay the price for the boost
        dmManager.money -= deathMethod.boostCost;

        //set up the buttons accordingly
        /*FindButtonAssociatedWithScientist(sci.name).lastResearchedOrBoostedMethod = deathMethod;
        FindButtonAssociatedWithScientist(sci.name).busyForEcon = true;*/
        slidersFilling.Add(sci1.name, 0f);
        if (sci2 != null)
        {
            slidersFilling.Add(sci2.name, 0f);
        }

        //Wait until the boost has concluded
        yield return new WaitForSeconds(deathMethod.boostTime);

        //change all the appropriate values for the DM
        deathMethod.price += deathMethod.boostValue;
        deathMethod.boostIncrement++;
        deathMethod.boostValue = deathMethod.price / 2;
        deathMethod.boostCost = deathMethod.price * 5;
        deathMethod.boostTime = deathMethod.rateOfSale * deathMethod.boostIncrement;
        deathMethod.beingBoosted = false;
        //deathMethod.scientistBoostingThis = null;

        //change all the appropriate values for the scientist
        slidersFilling.Remove(sci1.name);
        if (sci2 != null)
        {
            slidersFilling.Remove(sci2.name);
        }
        sci1.busy = false;
        if (sci2 != null)
        {
            sci2.busy = false;
        }

        //start the coroutine for completing the boost
        StartCoroutine(PrintBoostCompleteMessage(1.5f,sci1,deathMethod)); //This needs to be changed to both Scientists
    }
    
    //Coroutine that does the research between two scientists
    public IEnumerator StartResearch(Scientist sci1, Scientist sci2)
    {
        //Make them busy
        sci1.busy = true;
        sci2.busy = true;

        //Set up the attributes for their EmployButtons
        /*FindButtonAssociatedWithScientist(sci1.name).lastResearchedOrBoostedMethod = FindDeathMethod(dmManager, sci1.combinations[sci2.name]);
        FindButtonAssociatedWithScientist(sci2.name).lastResearchedOrBoostedMethod = FindDeathMethod(dmManager, sci1.combinations[sci2.name]);
        FindButtonAssociatedWithScientist(sci1.name).busyForEcon = false;
        FindButtonAssociatedWithScientist(sci2.name).busyForEcon = false;*/

        //Add them to the sliders being filled
        slidersFilling.Add(sci1.name, 0f);
        slidersFilling.Add(sci2.name, 0f);

        //wait until their research has concluded
        yield return new WaitForSeconds(FindDeathMethod(dmManager, sci1.combinations[sci2.name]).researchTime);

        //make them not busy and remove them from the sliders being filled
        sci1.busy = false;
        sci2.busy = false;
        slidersFilling.Remove(sci1.name);
        slidersFilling.Remove(sci2.name);

        //activate the match between them
        ActivateMatch(sci1, sci2);
    }

    //Coroutine that prints a research start message for waitTime seconds
    public IEnumerator ResearchStartBanner(float waitTime, Scientist sci1, Scientist sci2)
    {
        GameObject discoveryInst = Instantiate(discoveryBanner, Vector3.zero, Quaternion.identity, discoveryParent.transform);
        discoveryInst.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{sci1.name} and {sci2.name} are researching together";
        discoveryInst.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
        discoveryInst.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
        discoveryInst.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
        discoveryInst.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        yield return new WaitForSeconds(waitTime);

        discoveryInst.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        Destroy(discoveryInst.gameObject);
    }

    //Coroutine that prints a econ boost start message for waitTime seconds
    public IEnumerator EconStartBanner(float waitTime, Scientist sci, DeathMethod dm)
    {
        GameObject discoveryInst = Instantiate(discoveryBanner, Vector3.zero, Quaternion.identity, discoveryParent.transform);
        discoveryInst.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{sci.name} is boosting profits for {dm.name}";
        discoveryInst.GetComponent<RectTransform>().anchorMin = new Vector2(0f, 0f);
        discoveryInst.GetComponent<RectTransform>().anchorMax = new Vector2(1f, 1f);
        discoveryInst.GetComponent<RectTransform>().offsetMin = new Vector2(0f, 0f);
        discoveryInst.GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        yield return new WaitForSeconds(waitTime);

        discoveryInst.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
        Destroy(discoveryInst.gameObject);
    }

    //Start the coroutine for a research being discovered
    public void StartPrintDiscoveryCoroutine(float waitTime, DeathMethod dm)
    {
        StartCoroutine(PrintDiscoveryMessage(waitTime, dm));
    }

    //Start the coroutine for an econ being made (both the banner and the actual econ itself)
    public void StartEconCoroutine(float waitTime, Scientist sci, DeathMethod dm)
    {
        StartCoroutine(EconStartBanner(waitTime, sci, dm));
        StartCoroutine(BoostDMEcon(dm, sci));
    }

    //Start the coroutine for a research being made (both the banner and the actual research itself)
    public void StartResearchCoroutine(float waitTime, Scientist sci1, Scientist sci2)
    {
        StartCoroutine(ResearchStartBanner(waitTime, sci1, sci2));
        StartCoroutine(StartResearch(sci1,sci2));
    }

    //Switch which scientist is chosen on the DMPanel
    public void SwitchScientist(DeathMethod dm)
    {
        dm.sci1Chosen = !dm.sci1Chosen;
    }

    //Get the EmployButton associated with a scientist (we only need the lab version of the button)
    /*private EmployButton FindButtonAssociatedWithScientist(string scientistName)
    {
        foreach (EmployButton eb in employButtons)
        {
            if (eb.scientist.name == scientistName && !eb.IsForEcon)
            {
                return eb;
            }
        }
        return null;
    }*/

    private void SortScientistsByCost()
    {
        for (int i = 1; i < hirePrefabs.Count; i++)
        {
            if (hirePrefabs[i].GetComponent<Scientist>().price < hirePrefabs[i-1].GetComponent<Scientist>().price || 
                (hirePrefabs[i].GetComponent<Scientist>().Purchased && !hirePrefabs[i - 1].GetComponent<Scientist>().Purchased))
            {
                hirePrefabs[i].GetComponent<Scientist>().numInOrder--;
                hirePrefabs[i - 1].GetComponent<Scientist>().numInOrder++;

                GameObject s1 = hirePrefabs[i];
                GameObject s2 = hirePrefabs[i - 1];

                hirePrefabs[i] = s2;
                hirePrefabs[i - 1] = s1;

                //SortScientistsByCost(); //The purchasing works without the recursive call for some reason. With the recursive call, a Stack Overflow is caused
            }
        }
    }

    private void IncrementAnchor(float totalWaitTime, RectTransform objectIncrement)
    {
        float increment = Time.deltaTime / totalWaitTime;

        objectIncrement.anchorMax = new Vector2(objectIncrement.anchorMax.x + increment, objectIncrement.anchorMax.y);
        objectIncrement.offsetMin = Vector2.zero;
        objectIncrement.offsetMax = Vector2.zero;
    }
}
