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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ButtonClick()
    {
        if(IsForEcon)
        {
            //instantiate the econ panel and populate it
            GameObject econPanel = Instantiate(EconPanel, PanelParent);
            econPanel.transform.SetAsLastSibling();

            //get the list of applicable death methods and save them
            Transform content = econPanel.transform.GetChild(2).GetChild(0).GetChild(0);
            DeathMethodManager dmm = GameObject.FindObjectOfType<DeathMethodManager>();

            List<DeathMethod> deathMethods = dmm.deathMethods.Where(dm => dm.scientist1name == scientist.name || dm.scientist2name == scientist.name).ToList();

            foreach(DeathMethod deathMethod in deathMethods)
            {
                GameObject instance = Instantiate(PanelOptionPrefab, content);
                instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = deathMethod.name;
                instance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ScientistBoostEcon(scientist,deathMethod,econPanel); });
                instance.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Boost";
            }

            //also add the back arrow
            EconPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { Destroy(econPanel); });
        }

        else
        {
            //instantiate the econ panel and populate it
            GameObject labPanel = Instantiate(LabPanel, PanelParent);
            labPanel.transform.SetAsLastSibling();

            //get the list of applicable death methods and save them
            Transform content = labPanel.transform.GetChild(2).GetChild(0).GetChild(0);
            DeathMethodManager dmm = GameObject.FindObjectOfType<DeathMethodManager>();

            List<DeathMethod> deathMethods = dmm.deathMethods.Where(dm => dm.scientist1name == scientist.name || dm.scientist2name == scientist.name).ToList();

            foreach (DeathMethod deathMethod in deathMethods)
            {
                GameObject instance = Instantiate(PanelOptionPrefab, content);
                instance.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = deathMethod.name;
                instance.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { ScientistBoostEcon(scientist, deathMethod, labPanel); });
                instance.transform.GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().text = "Boost";
            }

            //also add the back arrow
            EconPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { Destroy(labPanel); });
        }
    }

    private void ScientistBoostEcon(Scientist sci, DeathMethod deathMethod, GameObject econPanel)
    {
        //do the thing

        Destroy(econPanel);
    }
}