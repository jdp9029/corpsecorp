using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public DeathMethodManager manager;

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
        manager = GameObject.FindObjectOfType<DeathMethodManager>();
        manager.deathMethods.Add(this);
    }
    //==== SIMPLE CONSTRUCTOR
    public DeathMethod(string name, Scientist scientist1, Scientist scientist2, bool active)
    {
        this.name = name;
        this.scientist1 = scientist1;
        this.scientist2 = scientist2;
        this.active = active;
        manager = GameObject.FindObjectOfType<DeathMethodManager>();
        manager.deathMethods.Add(this);
    }
    //==== SUPER SIMPLE CONSTRUCTOR ====
    public DeathMethod(string name)
    {
        this.name = name;
        this.active = false;
        manager = GameObject.FindObjectOfType<DeathMethodManager>();
        manager.deathMethods.Add(this);
    }

    //==== START ====
    void Start()
    {
        
    }

    //==== UPDATE ====
    void Update()
    {
        if(!manager.deathMethods.Contains(this)) //If the DeathMethodManager list doesn't contain this DeathMethod, add this DeathMethod to the DeathMethodManager
        {
            manager.deathMethods.Add(this);
        }
    }
}
