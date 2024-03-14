using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScientistManager : MonoBehaviour
{
    public Scientist[] scientists;

    [SerializeField] GameObject scientistForHirePrefab;

    [SerializeField] Transform tab4content;

    [SerializeField] UIManager uiManager;

    //icons for scientists
    [SerializeField] List<Sprite> icons;

    [SerializeField] List<Sprite> pureIcons;

    [SerializeField] List<Sprite> unpureIcons;

    // Start is called before the first frame update
    void Start()
    {
        #region scientists initialized and combo'd
        scientists = new Scientist[]
        {
            CreateScientist("HS Dropout", 1, "Soft Pillow", 0),
            CreateScientist("HS Graduate", 1, "Pencil", 1),
            CreateScientist("College Student", 1, "Stress", 2),
            CreateScientist("Veterinarian", 2, "Rabies", 3),
            CreateScientist("Chef", 2, "Knife", 4),
            CreateScientist("Barber", 2, "Razor", 5),
            CreateScientist("Construction Worker", 2, "Hammer", 6),
            CreateScientist("Mechanic", 2, "Wrench", 7),
            CreateScientist("Historian", 2, "War", 8),
            CreateScientist("Government Agent", 2, "Assassination", 9),
            CreateScientist("Biologist", 3, "Infection", 10),
            CreateScientist("Marine Biologist", 3, "Piranhas", 11),
            CreateScientist("Engineer", 3, "Building Collapse", 12),
            CreateScientist("Botanist", 3, "Venus Flytrap", 13),
            CreateScientist("Astronomer", 3, "Black Hole", 14),
            CreateScientist("Meteorologist", 3, "Storm", 15),
            CreateScientist("Geologist", 3, "Earthquake", 16),
            CreateScientist("Epidemiologist", 4, "Influenza", 17),
            CreateScientist("Nuclear Scientist", 4, "Fusion Reaction", 18),
            CreateScientist("Rocket Scientist", 4, "Missile", 19),
            CreateScientist("Physicist", 4, "Fall Damage", 20),
            CreateScientist("Radiologist", 4, "Radiation", 21),
            CreateScientist("Chemist", 4, "Acid", 22),
            CreateScientist("Doctor", 4, "Defibrillator", 23)







            /*new Scientist("HS Dropout", 1, new DeathMethod("Soft Pillow", "HS Dropout", null), scientistForHirePrefab, 0),
            new Scientist("HS Graduate", 1, new DeathMethod("Pencil", "HS Graduate", null), scientistForHirePrefab, 1),
            new Scientist("College Student", 1, new DeathMethod("Stress", "College Student", null), scientistForHirePrefab, 2),
            new Scientist("Veterinarian", 2, new DeathMethod("Rabies", "Veterinarian", null), scientistForHirePrefab, 3),
            new Scientist("Chef", 2, new DeathMethod("Knife", "Chef", null), scientistForHirePrefab, 4),
            new Scientist("Barber", 2, new DeathMethod("Razor", "Barber", null), scientistForHirePrefab, 5),
            new Scientist("Construction Worker", 2, new DeathMethod("Hammer", "Construction Worker", null), scientistForHirePrefab, 6),
            new Scientist("Mechanic", 2, new DeathMethod("Wrench", "Mechanic", null), scientistForHirePrefab, 7),
            new Scientist("Historian", 2, new DeathMethod("War", "Historian", null), scientistForHirePrefab, 8),
            new Scientist("Government Agent", 2, new DeathMethod("Assassination", "Government Agent", null), scientistForHirePrefab, 9),
            new Scientist("Biologist", 3, new DeathMethod("Infection", "Biologist", null), scientistForHirePrefab, 10),
            new Scientist("Marine Biologist", 3, new DeathMethod("Piranhas", "Marine Biologist", null), scientistForHirePrefab, 11),
            new Scientist("Engineer", 3, new DeathMethod("Building Collapse", "Engineer", null), scientistForHirePrefab, 12),
            new Scientist("Botanist", 3, new DeathMethod("Venus Flytrap", "Botanist", null), scientistForHirePrefab, 13),
            new Scientist("Astronomer", 3, new DeathMethod("Black Hole", "Astronomer", null), scientistForHirePrefab, 14),
            new Scientist("Meteorologist", 3, new DeathMethod("Storm", "Meteorologist", null), scientistForHirePrefab, 15),
            new Scientist("Geologist", 3, new DeathMethod("Earthquake", "Geologist", null), scientistForHirePrefab, 16),
            new Scientist("Epidemiologist", 4, new DeathMethod("Influenza", "Epidemiologist", null), scientistForHirePrefab, 17),
            new Scientist("Nuclear Scientist", 4, new DeathMethod("Fusion Reaction", "Nuclear Scientist", null), scientistForHirePrefab, 18),
            new Scientist("Rocket Scientist", 4, new DeathMethod("Missile", "Rocket Scientist", null), scientistForHirePrefab, 19),
            new Scientist("Physicist", 4, new DeathMethod("Fall Damage", "Physicist", null), scientistForHirePrefab, 20),
            new Scientist("Radiologist", 4, new DeathMethod("Radiation", "Radiologist", null), scientistForHirePrefab, 21),
            new Scientist("Chemist", 4, new DeathMethod("Acid", "Chemist", null), scientistForHirePrefab, 22)*/
        };

        //Debug.Log(scientists.Length);

        //combos should have the index of the scientist on the left always LESS than the index of the scientist on the right
        //ie. when we get to scientists[1] that does not need to match with scientists[0], that's already taken care of

        //NOTE: DeathMethod constructor goes Name, Scientist1 Name, Scientist2 Name, Price, Rate of Sale

        //all combos for HS dropout
        scientists[0].ComboScientist(scientists[1], new DeathMethod("Book", scientists[0].name, scientists[1].name, 4, 3));
        scientists[0].ComboScientist(scientists[2], new DeathMethod("Herpes", scientists[0].name, scientists[2].name, 7, 5));
        scientists[0].ComboScientist(scientists[4], new DeathMethod("Food Poisoning", scientists[0].name, scientists[4].name, 10, 4));
        scientists[0].ComboScientist(scientists[6], new DeathMethod("Dropped Steel Beam", scientists[0].name, scientists[6].name, 20, 15));
        scientists[0].ComboScientist(scientists[8], new DeathMethod("Gang Activity", scientists[0].name, scientists[8].name, 30, 20));
        scientists[0].ComboScientist(scientists[11], new DeathMethod("Drowning", scientists[0].name, scientists[11].name, 50, 4));
        scientists[0].ComboScientist(scientists[14], new DeathMethod("Helmet Removal", scientists[0].name, scientists[14].name, 40, 3));
        scientists[0].ComboScientist(scientists[22], new DeathMethod("Broken Glass", scientists[0].name, scientists[22].name, 32, 1));
        scientists[0].ComboScientist(scientists[23], new DeathMethod("Botched Surgery", scientists[0].name, scientists[23].name, 95, 3));

        //hs graduate
        scientists[1].ComboScientist(scientists[2], new DeathMethod("Hit By Bus", scientists[1].name, scientists[2].name, 15, 10));
        scientists[1].ComboScientist(scientists[3], new DeathMethod("Mosquito Bite", scientists[1].name, scientists[3].name, 6, 2));
        scientists[1].ComboScientist(scientists[5], new DeathMethod("Scissors", scientists[1].name, scientists[5].name, 8, 2));
        scientists[1].ComboScientist(scientists[6], new DeathMethod("Scaffolding Collapse", scientists[1].name, scientists[6].name, 20, 15));
        scientists[1].ComboScientist(scientists[9], new DeathMethod("Military Service", scientists[1].name, scientists[9].name, 30, 20));
        scientists[1].ComboScientist(scientists[22], new DeathMethod("Chemical Burn", scientists[1].name, scientists[22].name, 68, 2));

        //college student
        scientists[2].ComboScientist(scientists[3], new DeathMethod("Snake Charmed", scientists[2].name, scientists[3].name, 11, 10));
        scientists[2].ComboScientist(scientists[8], new DeathMethod("Extremism", scientists[2].name, scientists[8].name, 40, 25));
        scientists[2].ComboScientist(scientists[9], new DeathMethod("Debt", scientists[2].name, scientists[9].name, 12, 3));
        scientists[2].ComboScientist(scientists[13], new DeathMethod("Cocaine", scientists[2].name, scientists[13].name, 75, 5));
        scientists[2].ComboScientist(scientists[23], new DeathMethod("Misdiagnosis", scientists[2].name, scientists[23].name, 100, 3));

        //vet
        scientists[3].ComboScientist(scientists[4], new DeathMethod("E. Coli", scientists[3].name, scientists[4].name, 12, 2));
        scientists[3].ComboScientist(scientists[5], new DeathMethod("Hairball Ingestion", scientists[3].name, scientists[5].name, 9, 2));
        scientists[3].ComboScientist(scientists[7], new DeathMethod("Dog Driving", scientists[3].name, scientists[7].name, 35, 8));
        scientists[3].ComboScientist(scientists[10], new DeathMethod("Anthrax", scientists[3].name, scientists[10].name, 60, 4));
        scientists[3].ComboScientist(scientists[11], new DeathMethod("Shark Attack", scientists[3].name, scientists[11].name, 80, 5));
        scientists[3].ComboScientist(scientists[17], new DeathMethod("Chicken Pox", scientists[3].name, scientists[17].name, 40, 1));
        scientists[3].ComboScientist(scientists[23], new DeathMethod("Leeches", scientists[3].name, scientists[23].name, 80, 2));

        //chef
        scientists[4].ComboScientist(scientists[6], new DeathMethod("Eating a Nail", scientists[4].name, scientists[6].name, 15, 2));
        scientists[4].ComboScientist(scientists[7], new DeathMethod("Kitchen Fire", scientists[4].name, scientists[7].name, 40, 7));
        scientists[4].ComboScientist(scientists[8], new DeathMethod("Famine", scientists[4].name, scientists[8].name, 70, 10));
        scientists[4].ComboScientist(scientists[9], new DeathMethod("Cyanide", scientists[4].name, scientists[9].name, 80, 10));
        scientists[4].ComboScientist(scientists[10], new DeathMethod("Salmonella", scientists[4].name, scientists[10].name, 15, 1));
        scientists[4].ComboScientist(scientists[13], new DeathMethod("Hemlock", scientists[4].name, scientists[13].name, 70, 4));
        scientists[4].ComboScientist(scientists[17], new DeathMethod("Obesity", scientists[4].name, scientists[17].name, 72, 2));
        scientists[4].ComboScientist(scientists[22], new DeathMethod("Poison", scientists[4].name, scientists[22].name, 120, 3));

        //barber
        scientists[5].ComboScientist(scientists[9], new DeathMethod("Scalping", scientists[5].name, scientists[9].name, 40, 8));
        scientists[5].ComboScientist(scientists[12], new DeathMethod("Electric Razor", scientists[5].name, scientists[12].name, 16, 1));

        //construction worker
        scientists[6].ComboScientist(scientists[7], new DeathMethod("Jackhammer", scientists[6].name, scientists[7].name, 25, 4));
        scientists[6].ComboScientist(scientists[8], new DeathMethod("Barbed Wire", scientists[6].name, scientists[8].name, 25, 4));
        scientists[6].ComboScientist(scientists[12], new DeathMethod("Chainsaw", scientists[6].name, scientists[12].name, 35, 2));

        //mechanic
        scientists[7].ComboScientist(scientists[9], new DeathMethod("Booby Traps", scientists[7].name, scientists[9].name, 35, 7));
        scientists[7].ComboScientist(scientists[12], new DeathMethod("Busted Engine", scientists[7].name, scientists[12].name, 50, 3));
        scientists[7].ComboScientist(scientists[14], new DeathMethod("Faulty Airlock", scientists[7].name, scientists[14].name, 110, 7));
        scientists[7].ComboScientist(scientists[20], new DeathMethod("Head-on Collision", scientists[7].name, scientists[20].name, 150, 4));
        scientists[7].ComboScientist(scientists[22], new DeathMethod("Exhaust Inhalation", scientists[7].name, scientists[22].name, 150, 4));

        //historian
        scientists[8].ComboScientist(scientists[10], new DeathMethod("Plague", scientists[8].name, scientists[10].name, 175, 10));
        scientists[8].ComboScientist(scientists[12], new DeathMethod("Shipwreck", scientists[8].name, scientists[12].name, 125, 8));
        scientists[8].ComboScientist(scientists[14], new DeathMethod("Big Bang", scientists[8].name, scientists[14].name, 225, 12));
        scientists[8].ComboScientist(scientists[15], new DeathMethod("Wildfire", scientists[8].name, scientists[15].name, 200, 12));
        scientists[8].ComboScientist(scientists[17], new DeathMethod("Black Death", scientists[8].name, scientists[17].name, 300, 6));
        scientists[8].ComboScientist(scientists[19], new DeathMethod("Fireworks", scientists[8].name, scientists[19].name, 140, 3));
        scientists[8].ComboScientist(scientists[22], new DeathMethod("Mustard Gas", scientists[8].name, scientists[22].name, 300, 7));

        //gov't agent
        scientists[9].ComboScientist(scientists[11], new DeathMethod("Waterboarding", scientists[9].name, scientists[11].name, 75, 4));
        scientists[9].ComboScientist(scientists[13], new DeathMethod("Ricin", scientists[9].name, scientists[13].name, 70, 4));
        scientists[9].ComboScientist(scientists[16], new DeathMethod("Climate Change", scientists[9].name, scientists[16].name, 160, 9));
        scientists[9].ComboScientist(scientists[18], new DeathMethod("The Football", scientists[9].name, scientists[18].name, 500, 12));

        //biologist
        scientists[10].ComboScientist(scientists[11], new DeathMethod("Animal Attack", scientists[10].name, scientists[11].name, 28, 1));
        scientists[10].ComboScientist(scientists[12], new DeathMethod("Submarine Implosion", scientists[10].name, scientists[12].name, 260, 10));

        //marine biologist
        scientists[11].ComboScientist(scientists[14], new DeathMethod("Asphyxiation", scientists[11].name, scientists[14].name, 100, 4));
        scientists[11].ComboScientist(scientists[15], new DeathMethod("Flood", scientists[11].name, scientists[15].name, 70, 3));
        scientists[11].ComboScientist(scientists[16], new DeathMethod("Tsunami", scientists[11].name, scientists[16].name, 140, 6));
        scientists[11].ComboScientist(scientists[19], new DeathMethod("Torpedo", scientists[11].name, scientists[19].name, 550, 8));

        //engineer
        scientists[12].ComboScientist(scientists[20], new DeathMethod("Lab Explosion", scientists[12].name, scientists[20].name, 400, 7));
        scientists[12].ComboScientist(scientists[22], new DeathMethod("Poison Grenade", scientists[12].name, scientists[22].name, 450, 8));

        //botanist

        //astronomer
        scientists[14].ComboScientist(scientists[16], new DeathMethod("Meteorite", scientists[14].name, scientists[16].name, 300, 12));
        scientists[14].ComboScientist(scientists[18], new DeathMethod("Supernova", scientists[14].name, scientists[18].name, 700, 12));
        scientists[14].ComboScientist(scientists[19], new DeathMethod("Stranded in Space", scientists[14].name, scientists[19].name, 300, 5));

        //meteorologist
        scientists[15].ComboScientist(scientists[16], new DeathMethod("Drought", scientists[15].name, scientists[16].name, 200, 8));
        scientists[15].ComboScientist(scientists[22], new DeathMethod("Acid Rain", scientists[15].name, scientists[22].name, 600, 10));

        //geologist
        scientists[16].ComboScientist(scientists[21], new DeathMethod("Uranium", scientists[16].name, scientists[21].name, 400, 7));
        scientists[16].ComboScientist(scientists[22], new DeathMethod("Volcano", scientists[16].name, scientists[22].name, 500, 8));

        //epidemiologist
        scientists[17].ComboScientist(scientists[21], new DeathMethod("Cancer", scientists[17].name, scientists[21].name, 300, 4));

        //nuclear scientist
        scientists[18].ComboScientist(scientists[20], new DeathMethod("Atom Bomb", scientists[18].name, scientists[20].name, 1100, 14));
        scientists[18].ComboScientist(scientists[21], new DeathMethod("Fallout", scientists[18].name, scientists[21].name, 315, 4));

        //rocket scientist

        //physicist

        //radiologist

        //chemist

        //doctor

        //Loop Through Scientist Combos To Automatically Determine Scientist Prices
        for (int i = 0; i < scientists.Length; i++)
        {
            if (scientists[i].tier == 1) //If it's a Tier One, skip it or hardcode price if it's College Student
            {
                if (scientists[i].name == "College Student")
                {
                    scientists[i].price = 200;
                }
            }
            else if (scientists[i].tier == 2) //Tier 2 price equals # of matches times 350
            {
                scientists[i].price = scientists[i].combinations.Count * 350;
            }
            else if (scientists[i].tier == 3) //Tier 3 price equals # of matches times 2000
            {
                scientists[i].price = scientists[i].combinations.Count * 2000;
            }
            else if (scientists[i].tier == 4) //Tier 4 price equals # of matches times 5000
            {
                scientists[i].price = scientists[i].combinations.Count * 5000;
            }

            if (!scientists[i].Purchased)
            {
                uiManager.SetHirePrefabText(scientists[i].gameObject, scientists[i].price);
            }
            //Debug.Log($"{scientists[i].name}: {scientists[i].price}");
        }

        //Sort by price
        //SortScientistsByCost();

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Essentially acts as the new scientist constructor
    private Scientist CreateScientist(string name, int tier, string deathMethodName, int numInOrder)
    {
        GameObject instantiation = Instantiate(scientistForHirePrefab, Vector3.zero, Quaternion.identity, tab4content);
        Scientist scientist = instantiation.GetComponent<Scientist>();

        //essentially make up the scientist constructor here
        scientist.name = name;
        scientist.tier = tier;

        //Construct Inherent DeathMethods based on DM Name
        switch (deathMethodName)
        {
            case "Soft Pillow":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 2, 2);
                break;
            case "Pencil":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 2, 2);
                break;
            case "Stress":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 2, 1);
                break;
            case "Rabies":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 30, 6);
                break;
            case "Knife":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 15, 3);
                break;
            case "Razor":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 10, 2);
                break;
            case "Hammer":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 10, 2);
                break;
            case "Wrench":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 10, 2);
                break;
            case "War":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 50, 8);
                break;
            case "Assassination":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 60, 10);
                break;
            case "Infection":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 200, 9);
                break;
            case "Piranhas":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 45, 2);
                break;
            case "Building Collapse":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 90, 3);
                break;
            case "Venus Flytrap":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 26, 1);
                break;
            case "Black Hole":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 400, 15);
                break;
            case "Storm":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 80, 3);
                break;
            case "Earthquake":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 150, 7);
                break;
            case "Influenza":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 75, 1);
                break;
            case "Fusion Reaction":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 900, 12);
                break;
            case "Missile":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 550, 7);
                break;
            case "Fall Damage":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 135, 2);
                break;
            case "Radiation":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 76, 1);
                break;
            case "Acid":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 150, 2);
                break;
            case "Defibrillator":
                scientist.mainMethod = new DeathMethod(deathMethodName, name, null, 225, 3);
                break;
        }

        //set up the icon
        scientist.Icon = icons[numInOrder];

        //set up the icons for the pure death methods (commented out for now due to compiler reasons)
        //scientist.mainMethod.Icon = pureIcons[numInOrder];


        //set up whether they are purchased
        if (name == "HS Dropout" || name == "HS Graduate")
        {
            scientist.Purchased = true;
            //instantiation.transform.parent = GameObject.FindGameObjectWithTag("Bought Scientists").transform;
            GameObject.FindObjectOfType<UIManager>().AddToInventory(scientist.mainMethod);
            GameObject.FindObjectOfType<UIManager>().AddScientistToInventory(scientist);
            scientist.ReplaceButtonWithIcons(true);
        }
        else
        {
            scientist.Purchased = false;
        }

        //because two scientists are purchased, move the other ones up the list
        //numInOrder -= 2;
        scientist.numInOrder = numInOrder;

        //set up the appropriate text name
        instantiation.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = name;

        //set up the position of the object on tab number 4
        if (!scientist.Purchased)
        {
            uiManager.hirePrefabs.Add(instantiation);
        }
        
        return scientist;
    }

    private void SortScientistsByCost()
    {
        Scientist[] unPurchasedScientists = scientists.Where(scientist => !scientist.Purchased).ToArray();

        for(int i = 1; i < unPurchasedScientists.Length; i++)
        {
            if (unPurchasedScientists[i].price < unPurchasedScientists[i - 1].price)
            {
                unPurchasedScientists[i].numInOrder--;
                unPurchasedScientists[i - 1].numInOrder++;

                Scientist s1 = unPurchasedScientists[i];
                Scientist s2 = unPurchasedScientists[i - 1];

                unPurchasedScientists[i] = s2;
                unPurchasedScientists[i - 1] = s1;

                SortScientistsByCost();
            }
        }
    }
}
