using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
//using UnityEditor.UI;
using UnityEngine.UI;
using UnityEditor.SceneManagement;
using System.Linq;

public class Scientist : MonoBehaviour
{
    public Dictionary<string, string> combinations = new Dictionary<string, string>();
    public DeathMethod mainMethod;
    public string name;
    public int tier;
    public int price;
    public bool Purchased;
    public int numInOrder;
    public Sprite Icon;
    public GameObject jobIcons;

    private GameObject labIcon;
    private GameObject econIcon;

    public DeathMethodManager dmManager;

    public bool busy;

    // Start is called before the first frame update
    void Start()
    {
        if(!Purchased)
        {
            transform.GetChild(1).GetComponent<Button>().onClick.AddListener(ButtonClick);
        }
        
        dmManager = GameObject.FindObjectOfType<DeathMethodManager>();
        busy = false;
    }

    // Update is called once per frame
    void Update()
    {
       /* if(Purchased)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            numInOrder = -1;
        }
        else
        {
            //transform.position = new Vector3(transform.position.x, transform.parent.parent.position.y + 1800 + (10 * (11 - numInOrder)), 0);
        }*/
    }

    //Combines two scientists together (called only in ScientistManager.Start())
    public void ComboScientist(Scientist scientist, DeathMethod method /*, Sprite icon*/)
    {
        combinations.Add(scientist.name, method.name);
        scientist.combinations.Add(this.name, method.name);
        method.scientist1name = this.name;
        method.scientist2name = scientist.name;
        //method.Icon = icon;
    }

    private void ButtonClick()
    {
        Debug.Log(numInOrder);
        if (this.price <= dmManager.money) //Make Sure You Have Enough Money
        {
            /*foreach (Scientist s in GameObject.FindObjectOfType<ScientistManager>().scientists) //This foreach loop doesn't shift everything up
            {
                if (s.numInOrder > numInOrder)
                {
                    s.numInOrder--;
                    Debug.Log("Shifted " + s.name);
                }
            }*/
            this.Purchased = true;
            this.mainMethod.active = true;
            dmManager.money -= this.price;
            //transform.parent = GameObject.FindGameObjectWithTag("Bought Scientists").transform;
            GameObject.FindObjectOfType<UIManager>().AddToInventory(mainMethod);
            GameObject.FindObjectOfType<UIManager>().AddScientistToInventory(this);
            transform.GetChild(1).GetComponent<Button>().onClick.RemoveListener(ButtonClick);
            ReplaceButtonWithIcons(false);
            StartCoroutine(GameObject.FindObjectOfType<UIManager>().PrintDiscoveryMessage(3.0f, this.mainMethod));
        }
    }

    public void ReplaceButtonWithIcons(bool isStart)
    {
        //first, destroy the button part of the object
        GameObject.Destroy(transform.GetChild(1).gameObject);

        //next, replace them with icons
        labIcon = Instantiate(jobIcons, new Vector3(200, 0, 0), Quaternion.identity, transform);
        labIcon.GetComponent<EmployButton>().IsForEcon = false;
        labIcon.GetComponent<EmployButton>().scientist = this;
        labIcon.GetComponent<EmployButton>().PanelParent = GameObject.FindObjectsOfType<Tab>().Where(tab => tab.tabNum == 4).ToArray()[0].transform;

        econIcon = Instantiate(jobIcons, new Vector3(400, 0, 0), Quaternion.identity, transform);
        econIcon.GetComponent<EmployButton>().IsForEcon = true;
        econIcon.GetComponent<EmployButton>().scientist = this;
        econIcon.GetComponent<EmployButton>().PanelParent = GameObject.FindObjectsOfType<Tab>().Where(tab => tab.tabNum == 4).ToArray()[0].transform;

        if(!isStart)
        {
            labIcon.transform.position = new Vector3(200 + transform.position.x, transform.position.y, 0f);
            econIcon.transform.position = new Vector3(400 + transform.position.x, transform.position.y, 0f);
        }

    }
}
