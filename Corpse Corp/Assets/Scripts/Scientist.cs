using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEditor.UI;
using UnityEngine.UI;

public class Scientist : MonoBehaviour
{
    public Dictionary<string, string> combinations;
    DeathMethod mainMethod;
    public string name;
    public int tier;
    public bool Purchased;

    [HideInInspector] public GameObject scientistHiringInstance;

    //when a new scientist is created
    public Scientist(string name, int tier, DeathMethod mainMethod, GameObject scientistHiringPrefab, int scientistNo)
    {
        //set up the combinations dictionary
        combinations = new Dictionary<string, string>();

        //apply the variables, including purchased
        this.tier = tier;
        this.name = name;
        this.mainMethod = mainMethod;
        
        if(name == "HS Dropout" || name == "HS Graduate")
        {
            Purchased = true;
        }
        else
        {
            Purchased = false;
        }

        //this method will only have one scientist, so apply accordingly
        mainMethod.scientist1name = this.name;

        //if this scientist is not purchased, we need to set up the button for it
        if(!Purchased)
        {
            //instantiates the object and puts it in it's place
            scientistHiringInstance = Instantiate(scientistHiringPrefab, FindObjectOfType<TabManager>().buttonTabs[3].transform.GetChild(1).GetChild(0).GetChild(0));

            //applies the appropriate text name
            scientistHiringInstance.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = name;

            //set up the position
            scientistHiringInstance.transform.position = new Vector3(scientistHiringInstance.transform.parent.parent.position.x + 240f, scientistHiringInstance.transform.parent.parent.position.y + (130 * (11 - scientistNo)), 0);
            
            //increase the size of the content box for scrolling purposes
            scientistHiringInstance.transform.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(
                scientistHiringInstance.transform.parent.GetComponent<RectTransform>().rect.width,
                scientistHiringInstance.transform.parent.GetComponent<RectTransform>().rect.height + 150f);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Combines two scientists together (called only in ScientistManager.Start())
    public void ComboScientist(Scientist scientist, DeathMethod method)
    {
        combinations.Add(scientist.name, method.name);
        scientist.combinations.Add(this.name, method.name);
        method.scientist1name = this.name;
        method.scientist2name = scientist.name;
    }
}
