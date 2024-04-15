using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployButton : MonoBehaviour
{
    //whether or not this is a coin or a beaker button
    [HideInInspector] public bool IsForEcon;

    //the scientist associated with this button
    [HideInInspector] public Scientist scientist;

    //the panels that pop up when clicked
    public GameObject EconPanel;
    public GameObject LabPanel;

    //each of the options on the panel
    public GameObject PanelOptionPrefab;

    //the parent that gets associated with the panel
    [HideInInspector] public Transform PanelParent;

    //the coing an beaker sprites
    public Sprite EconSprite;
    public Sprite LabSprite;

    //the grey box that replaces this sprite when the scientist is busy
    [SerializeField] GameObject BusyObject;

    //the instance of said grey box
    private GameObject BusyObjInstance;

    //whether or not this button is active
    private bool buttonActive;

    //whether or not the busy object is active
    private bool busyObjectActive;

    //returns true if we are busy boosting, false if we are busy researching
    public bool busyForEcon = true;

    //the most recent method this scientist has boosted or researched
    public DeathMethod lastResearchedOrBoostedMethod;

    //calls to managers
    UIManager uiManager;
    DeathMethodManager dmm;

    // Start is called before the first frame update
    void Start()
    {
        //set up the managers
        uiManager = FindObjectOfType<UIManager>();
        dmm = GameObject.FindObjectOfType<DeathMethodManager>();

        //hook up this button
        GetComponent<Button>().onClick.AddListener(ButtonClick);

        //determines the activity of the button and the busy object
        buttonActive = true;
        busyObjectActive = false;

        //set up the sprite
        if(IsForEcon) { GetComponent<Image>().sprite = EconSprite; }
        else { GetComponent<Image>().sprite = LabSprite; }
    }

    // Update is called once per frame
    void Update()
    {
        //if the scientist is available
        if(!scientist.busy)
        {
            //if the busy object exists, destroy it
            if(busyObjectActive && !IsForEcon)
            {
                busyObjectActive = false;
                Destroy(BusyObjInstance);
                uiManager.slidersFilling.Remove(scientist.name);
            }

            //code for lab buttons when scientist isn't busy
            if(!IsForEcon)
            {
                //if we can't research with anyone, deactivate the button
                if (CompatibleResearchPartners().Count == 0)
                {
                    if (buttonActive) { DeActivateButton(); }
                }

                //if we can research with someone, activate the button
                else if (!buttonActive)
                {
                    ActivateButton();
                }
            }

            //code for econ buttons when scientist isn't busy
            else
            {
                //if there are no affordable boosts, don't show the coin
                if (AffordableBoosts().Count == 0)
                {
                    if(buttonActive) { DeActivateButton(); }
                }

                //if there are affordable boosts, do show the coin
                else if (!buttonActive)
                {
                    ActivateButton();
                }
            }
        }

        //if the scientist is busy
        else
        {
            //we never want an active button here
            DeActivateButton();

            //code to instantiate the busy object (we only do it from the lab icon bc we don't need it twice)
            if(!busyObjectActive && !IsForEcon)
            {
                busyObjectActive = true;
                BusyObjInstance = Instantiate(BusyObject, Vector3.zero, Quaternion.identity, transform.parent);
                BusyObjInstance.GetComponent<Image>().color = Color.grey;
                BusyObjInstance.GetComponent<RectTransform>().offsetMin = Vector3.zero;
                BusyObjInstance.GetComponent<RectTransform>().offsetMax = Vector3.zero;
            }

            //code to update the percentage of busy-ness completed
            if(BusyObjInstance != null)
            {
                if (BusyObjInstance.TryGetComponent<Slider>(out Slider slider))
                {
                    float timer = uiManager.slidersFilling[scientist.name];
                    float totalTime = busyForEcon ? lastResearchedOrBoostedMethod.boostTime : lastResearchedOrBoostedMethod.researchTime;


                    slider.value = timer / totalTime;
                }
            }


        }
    }

    //Hide and turn off the button
    private void DeActivateButton()
    {
        GetComponent<Button>().onClick.RemoveAllListeners();
        GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        buttonActive = false;
    }

    //Show and turn on the button
    private void ActivateButton()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClick);
        GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        buttonActive = true;
    }

    //Returns the list of affordable methods that this scientist can boost
    private List<DeathMethod> AffordableBoosts()
    {
        //all possible boosts that can be made
        List<DeathMethod> deathMethods = dmm.deathMethods.Where(
                dm => dm.active && (dm.scientist1name == scientist.name || dm.scientist2name == scientist.name) && !dm.beingBoosted).ToList();

        //all of these boosts that are affordable
        return deathMethods.Where(dm => dm.boostCost <= dmm.money).ToList();
    }

    //Returns the list of compatible research partners that this scientist can research with
    private List<Scientist> CompatibleResearchPartners()
    {
        ScientistManager scimgr = GameObject.FindObjectOfType<ScientistManager>();
        List<Scientist> scis = scimgr.scientists.Where(
                sci => sci.Purchased && scientist.combinations.ContainsKey(sci.name)).ToList();

        return scis.Where(sci => !sci.busy && !uiManager.FindDeathMethod(dmm, scientist.combinations[sci.name]).active).ToList();
    }

    //Code that gets called when this button gets clicked
    private void ButtonClick()
    {
        //If this is an econ button
        if(IsForEcon)
        {
            //instantiate the econ panel and populate it
            GameObject econPanel = Instantiate(EconPanel, PanelParent);
            econPanel.transform.SetAsLastSibling();
            econPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Boosts for " + scientist.name;

            //get the list of applicable death methods and save them
            Transform content = econPanel.transform.GetChild(2).GetChild(0).GetChild(0);

            //get the death methods we want to appear on the panel
            List<DeathMethod> deathMethods = AffordableBoosts();

            //instantiate all of the options
            foreach(DeathMethod deathMethod in deathMethods)
            {
                GameObject instance = Instantiate(PanelOptionPrefab, content);
                instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = deathMethod.name;
                instance.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Boost";

                //when a DM gets boosted, start the econ coroutine and destroy the econ panel
                instance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    uiManager.StartEconCoroutine(1.5f, scientist, deathMethod);
                    Destroy(econPanel);
                });
            }

            //also add the back arrow
            econPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { Destroy(econPanel); });
        }

        //if this is a lab button
        else
        {
            //instantiate the lab panel and populate it
            GameObject labPanel = Instantiate(LabPanel, PanelParent);
            labPanel.transform.SetAsLastSibling();
            labPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Research Partners for " + scientist.name;

            //get the list of applicable death methods and save them
            Transform content = labPanel.transform.GetChild(2).GetChild(0).GetChild(0);

            //get the scientists relevant for the list
            List<Scientist> scis = CompatibleResearchPartners();

            //instantiate all of the options
            foreach (Scientist sci in scis)
            {
                GameObject instance = Instantiate(PanelOptionPrefab, content);
                instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci.name;
                instance.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Research";


                //when a research gets started, start the research coroutine and then destroy the lab panel
                instance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                {
                    uiManager.StartResearchCoroutine(1.5f, scientist, sci);
                    Destroy(labPanel);
                });
            }

            //also add the back arrow
            labPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { Destroy(labPanel); });
        }
    }
}
