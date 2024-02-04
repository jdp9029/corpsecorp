using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    Dictionary<string, string> combinations;
    DeathMethod mainMethod;
    public string name;
    public int tier;
    public bool Purchased;

    public Scientist(string name, int tier, DeathMethod mainMethod)
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

        mainMethod.scientist1 = this;
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
        Debug.Log("matching " + name + " to " + scientist.name + " and getting " + method.name);
        combinations.Add(scientist.name, method.name);
        scientist.combinations.Add(this.name, method.name);
        method.scientist1 = this;
        method.scientist2 = scientist;
    }
}
