using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DeathMethod : MonoBehaviour
{
    //==== FIELDS ====
    public string name;
    public string description;
    public Sprite Icon;

    public float rateOfSale;
    public float price;

    public string scientist1name;
    public string scientist2name;

    public bool active; //Shows if this DeathMethod has been discovered

    DeathMethodManager manager;

    public bool instantiated; //This makes sure that the Ecpnomy tab only instantiates one instance of each death method

    public float boostValue; //How much the price will get boosted by
    public float boostCost; //How much it costs to boost
    public float boostTime; //How long the boost will take
    public int boostIncrement = 1; //How many times (-1) that this DM has been boosted
    public bool beingBoosted;
    public float researchTime; //how long it takes to research this death method

    //==== CONSTRUCTOR ====
    public DeathMethod(string name, string description, Sprite icon, float rateOfSale, float price, string scientist1, string scientist2)
    {
        this.name = name;
        this.description = description;
        this.Icon = icon;
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

        instantiated = false;

        boostValue = Mathf.Round(price / 2);
        boostCost = price * 5;
        boostTime = rateOfSale * boostIncrement;
        researchTime = boostTime * 3;
        beingBoosted = false;
    }
    //==== CONSTRUCTOR WITHOUT ICON OR DESCRIPTION ====
    public DeathMethod(string name, string scientist1, string scientist2, float price, float rateOfSale)
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

        instantiated = false;

        boostValue = price / 2;
        boostCost = price * 5;
        boostTime = rateOfSale * boostIncrement;
        researchTime = boostTime * 3;
        beingBoosted = false;
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

        instantiated = false;

        boostValue = price / 2;
        boostCost = price * 5;
        boostTime = rateOfSale * boostIncrement;
        researchTime = boostTime * 3;
        beingBoosted = false;
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

    public void UpdateMoney()
    {
        manager.money += price;
    }
}
