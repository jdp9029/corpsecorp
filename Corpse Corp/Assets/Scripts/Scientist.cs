using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Scientist : MonoBehaviour
{
    Dictionary<Scientist, DeathMethod> combinations;
    DeathMethod mainMethod;
    string name;
    int tier;
    bool purchased;

    public Scientist(string name, int tier, DeathMethod mainMethod)
    {
        this.tier = tier;
        this.name = name;
        this.mainMethod = mainMethod;
        
        if(name == "HS Dropout" || name == "HS Graduate")
        {
            purchased = true;
        }
        else
        {
            purchased = false;
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

    void ComboScientist(Scientist scientist, DeathMethod method)
    {
        combinations.Add(scientist, method);
        scientist.ComboScientist(this, method);
    }
}
