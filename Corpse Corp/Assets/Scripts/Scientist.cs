using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Scientist : MonoBehaviour
{
    public Dictionary<string, string> combinations;
    DeathMethod mainMethod;
    public string name;
    public int tier;
    public bool Purchased;

    [HideInInspector] public GameObject scientistHiringInstance;

    public Scientist(string name, int tier, DeathMethod mainMethod, GameObject scientistHiringPrefab, int scientistNo)
    {
        combinations = new Dictionary<string, string>();

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

        mainMethod.scientist1name = this.name;

        scientistHiringInstance = Instantiate(scientistHiringPrefab, FindObjectOfType<TabManager>().buttonTabs[3].transform.GetChild(1).GetChild(0).GetChild(0));
        scientistHiringInstance.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = name;
        scientistHiringInstance.transform.position = new Vector3(transform.parent.parent.position.x, transform.parent.parent.position.y + (90 * (11 - scientistNo)), 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ComboScientist(Scientist scientist, DeathMethod method)
    {
        combinations.Add(scientist.name, method.name);
        scientist.combinations.Add(this.name, method.name);
        method.scientist1name = this.name;
        method.scientist2name = scientist.name;
    }
}
