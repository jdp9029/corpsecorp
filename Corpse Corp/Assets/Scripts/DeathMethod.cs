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

    public Scientist scientist1;
    public Scientist scientist2;

    public bool active;

    //==== CONSTRUCTOR ====
    public DeathMethod(string name, string description, Sprite icon, float rateOfSale, float price, Scientist scientist1, Scientist scientist2, bool active)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.rateOfSale = rateOfSale;
        this.price = price;
        this.scientist1 = scientist1;
        this.scientist2 = scientist2;
        this.active = active;
    }
    //==== SIMPLE CONSTRUCTOR
    public DeathMethod(string name, Scientist scientist1, Scientist scientist2, bool active)
    {
        this.name = name;
        this.scientist1 = scientist1;
        this.scientist2 = scientist2;
        this.active = active;
    }
    //==== SUPER SIMPLE CONSTRUCTOR ====
    public DeathMethod(string name)
    {
        this.name = name;
    }

    //==== START ====
    void Start()
    {
        
    }

    //==== UPDATE ====
    void Update()
    {
        
    }
}
