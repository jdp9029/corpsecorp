using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMethod : MonoBehaviour
{
    //==== FIELDS ====
    public string name;
    public string description;
    public Sprite icon;

    public float rateOfSale;
    public float price;

    //==== CONSTRUCTOR ====
    public DeathMethod(string name, string description, Sprite icon, float rateOfSale, float price)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.rateOfSale = rateOfSale;
        this.price = price;
    }
    public DeathMethod(string name)
    {
        this.name = name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
