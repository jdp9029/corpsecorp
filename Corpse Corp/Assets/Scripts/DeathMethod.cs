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

    public string scientist1name;
    public string scientist2name;

    public bool active;

    DeathMethodManager manager;

    //==== CONSTRUCTOR ====
    public DeathMethod(string name, string description, Sprite icon, float rateOfSale, float price, string scientist1, string scientist2)
    {
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.rateOfSale = rateOfSale;
        this.price = price;
        this.scientist1name = scientist1;
        this.scientist2name = scientist2;

        if (name == "Soft Pillow" || name == "Pencil")
        {
            this.active = true;
        }
        else
        {
            this.active = false;
        }
    }
    //==== SIMPLE CONSTRUCTOR ====
    public DeathMethod(string name, string scientist1, string scientist2)
    {
        this.name = name;
        this.scientist1name = scientist1;
        this.scientist2name = scientist2;

        if (name == "Soft Pillow" || name == "Pencil")
        {
            this.active = true;
        }
        else
        {
            this.active = false;
        }

        manager = GameObject.FindObjectOfType<DeathMethodManager>();
        manager.deathMethods.Add(this);
    }
    //==== SUPER SIMPLE CONSTRUCTOR ====
    public DeathMethod(string name)
    {
        this.name = name;
        if (name == "Soft Pillow" || name == "Pencil")
        {
            this.active = true;
        }
        else
        {
            this.active = false;
        }

        manager = GameObject.FindObjectOfType<DeathMethodManager>();
        manager.deathMethods.Add(this);
    }

    //==== START ====
    void Start()
    {
        //Start does not get called
    }

    //==== UPDATE ====
    void Update()
    {
        /*if(!manager.deathMethods.Contains(this)) //If the DeathMethodManager list doesn't contain this DeathMethod, add this DeathMethod to the DeathMethodManager
        {
            manager.deathMethods.Add(new DeathMethod(this.name));
        }*/
    }
}
