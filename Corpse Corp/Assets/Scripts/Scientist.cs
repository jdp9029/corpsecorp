using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
//using UnityEditor.UI;
using UnityEngine.UI;

public class Scientist : MonoBehaviour
{
    public Dictionary<string, string> combinations = new Dictionary<string, string>();
    public DeathMethod mainMethod;
    public string name;
    public int tier;
    public int price;
    public bool Purchased;
    public int numInOrder;

    public DeathMethodManager dmManager;

    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).GetComponent<Button>().onClick.AddListener(ButtonClick);
        
        dmManager = GameObject.FindObjectOfType<DeathMethodManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Purchased)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            numInOrder = -1;
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.parent.parent.position.y + 1800 + (10 * (11 - numInOrder)), 0);
        }
    }

    //Combines two scientists together (called only in ScientistManager.Start())
    public void ComboScientist(Scientist scientist, DeathMethod method)
    {
        combinations.Add(scientist.name, method.name);
        scientist.combinations.Add(this.name, method.name);
        method.scientist1name = this.name;
        method.scientist2name = scientist.name;
    }

    private void ButtonClick()
    {
        Debug.Log(numInOrder);
        if (this.price <= dmManager.money) //Make Sure You Have Enough Money
        {
            foreach (Scientist s in GameObject.FindObjectOfType<ScientistManager>().scientists) //This foreach loop doesn't shift everything up
            {
                if (s.numInOrder > numInOrder)
                {
                    s.numInOrder--;
                    Debug.Log("Shifted " + s.name);
                }
            }
            this.Purchased = true;
            this.mainMethod.active = true;
            dmManager.money -= this.price;
            transform.parent.parent.GetComponent<RectTransform>().sizeDelta = new Vector2(transform.parent.parent.GetComponent<RectTransform>().sizeDelta.x,
                transform.parent.parent.GetComponent<RectTransform>().sizeDelta.y - 100);
            transform.parent = GameObject.FindGameObjectWithTag("Bought Scientists").transform;
        }
    }
}
