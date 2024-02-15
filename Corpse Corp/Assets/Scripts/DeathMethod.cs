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

    public bool active; //Shows if this DeathMethod has been discovered
    public bool passivePurchased; //Shows if this DeathMethod has been passively purchased (this will constantly get updated between true and false)

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

        manager = GameObject.FindObjectOfType<DeathMethodManager>();
        manager.deathMethods.Add(this);

        passivePurchased = false;
    }
    //==== CONSTRUCTOR WITHOUT ICON OR DESCRIPTION ====
    public DeathMethod(string name, float rateOfSale, float price, string scientist1, string scientist2)
    {
        this.name = name;
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

        manager = GameObject.FindObjectOfType<DeathMethodManager>();
        manager.deathMethods.Add(this);

        passivePurchased = false;
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

            //Temporary Hardcoded Values
            this.price = 10;
            this.rateOfSale = 5;
        }
        else
        {
            this.active = false;

            //Temporary Hardcoded Values
            this.price = 20;
            this.rateOfSale = 10;
        }

        manager = GameObject.FindObjectOfType<DeathMethodManager>();
        manager.deathMethods.Add(this);

        passivePurchased = false;
    }
    //==== SUPER SIMPLE CONSTRUCTOR ====
    /*public DeathMethod(string name)
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

        passivePurchased = false;
    }*/

    //==== START ====
    void Start()
    {
        //Start does not get called
    }

    //==== UPDATE ====
    void Update()
    {
        //Nothing needs to happen here
    }

    public IEnumerator UpdateMoney()
    {
        manager.money += price;
        passivePurchased = true;
        //Debug.Log($"Added {price} money from {this.name}. Current Total: {manager.money}");

        yield return new WaitForSeconds(rateOfSale);

        passivePurchased = false;
    }
    private IEnumerator PassiveOnAndOff(float waitTime)
    {
        passivePurchased = true;
        yield return new WaitForSeconds(rateOfSale);
        passivePurchased = false;
    }
}
