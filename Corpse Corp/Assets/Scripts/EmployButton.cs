using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EmployButton : MonoBehaviour
{
    [HideInInspector] public bool IsForEcon;
    [HideInInspector] public Scientist scientist;
    public GameObject EconPanel;
    public GameObject LabPanel;
    public GameObject PanelOptionPrefab;
    [HideInInspector] public Transform PanelParent;
    public Sprite EconSprite;
    public Sprite LabSprite;

    private bool buttonActive;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClick);
        buttonActive = true;

        if(IsForEcon)
        {
            GetComponent<Image>().sprite = EconSprite;
        }
        else
        {
            GetComponent<Image>().sprite = LabSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsForEcon)
        {
            if (CompatibleResearchPartners().Count == 0)
            {
                //if we can't research, dont
                GetComponent<Button>().onClick.RemoveAllListeners();
                GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                buttonActive = false;
            }
            else if (!buttonActive)
            {
                //if we can research, do
                GetComponent<Button>().onClick.AddListener(ButtonClick);
                GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                buttonActive = true;
            }
        }
    }

    private List<Scientist> CompatibleResearchPartners()
    {
        ScientistManager scimgr = GameObject.FindObjectOfType<ScientistManager>();
        List<Scientist> scis = scimgr.scientists.Where(
                sci => sci.Purchased && scientist.combinations.ContainsKey(sci.name)).ToList();

        DeathMethodManager dmm = GameObject.FindObjectOfType<DeathMethodManager>();

        return scis.Where(sci => !GameObject.FindObjectOfType<UIManager>().FindDeathMethod(dmm, scientist.combinations[sci.name]).active).ToList();
    }

    private void ButtonClick()
    {
        if(IsForEcon)
        {
            //instantiate the econ panel and populate it
            GameObject econPanel = Instantiate(EconPanel, PanelParent);
            econPanel.transform.SetAsLastSibling();
            econPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Boosts for " + scientist.name;

            //get the list of applicable death methods and save them
            Transform content = econPanel.transform.GetChild(2).GetChild(0).GetChild(0);
            DeathMethodManager dmm = GameObject.FindObjectOfType<DeathMethodManager>();

            //get the death methods we want to appear on the panel
            List<DeathMethod> deathMethods = dmm.deathMethods.Where(
                dm => dm.active && (dm.scientist1name == scientist.name || dm.scientist2name == scientist.name)).ToList();

            //instantiate all of the options
            foreach(DeathMethod deathMethod in deathMethods)
            {
                GameObject instance = Instantiate(PanelOptionPrefab, content);
                instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = deathMethod.name;
                instance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ScientistBoostEcon(scientist,deathMethod,econPanel); });
                instance.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Boost";
            }

            //also add the back arrow
            econPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { Destroy(econPanel); });
        }

        else
        {
            //instantiate the econ panel and populate it
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
                instance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { LabCombo(scientist, sci, labPanel); });
                instance.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Research";
            }

            //also add the back arrow
            labPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { Destroy(labPanel); });
        }
    }

    //use a scientist to boost the econ
    private void ScientistBoostEcon(Scientist sci, DeathMethod deathMethod, GameObject econPanel)
    {
        //do the thing

        Destroy(econPanel);
    }

    //combine two scientists
    private void LabCombo(Scientist sci1, Scientist sci2, GameObject labPanel)
    {
        GameObject.FindObjectOfType<UIManager>().ActivateMatch(sci1, sci2);

        Destroy(labPanel);
    }
}
