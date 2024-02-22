using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScientistManager : MonoBehaviour
{
    public Scientist[] scientists;

    [SerializeField] GameObject scientistForHirePrefab;

    [SerializeField] Transform tab4content;

    [SerializeField] UIManager uiManager;

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

        //all combos for HS dropout
        scientists[0].ComboScientist(scientists[1], new DeathMethod("Book", scientists[0].name, scientists[1].name));
        scientists[0].ComboScientist(scientists[2], new DeathMethod("Herpes", scientists[0].name, scientists[2].name));
        scientists[0].ComboScientist(scientists[4], new DeathMethod("Food Poisoning", scientists[0].name, scientists[4].name));
        scientists[0].ComboScientist(scientists[6], new DeathMethod("Dropped Steel Beam", scientists[0].name, scientists[6].name));
        scientists[0].ComboScientist(scientists[8], new DeathMethod("Gang Activity", scientists[0].name, scientists[8].name));
        scientists[0].ComboScientist(scientists[11], new DeathMethod("Drowning", scientists[0].name, scientists[11].name));
        scientists[0].ComboScientist(scientists[14], new DeathMethod("Helmet Removal", scientists[0].name, scientists[14].name));
        scientists[0].ComboScientist(scientists[22], new DeathMethod("Broken Glass", scientists[0].name, scientists[22].name));
        scientists[0].ComboScientist(scientists[23], new DeathMethod("Botched Surgery", scientists[0].name, scientists[23].name));

        //hs graduate
        scientists[1].ComboScientist(scientists[2], new DeathMethod("Hit By Bus", scientists[1].name, scientists[2].name));
        scientists[1].ComboScientist(scientists[3], new DeathMethod("Mosquito Bite", scientists[1].name, scientists[3].name));
        scientists[1].ComboScientist(scientists[5], new DeathMethod("Scissors", scientists[1].name, scientists[5].name));
        scientists[1].ComboScientist(scientists[6], new DeathMethod("Scaffolding Collapse", scientists[1].name, scientists[6].name));
        scientists[1].ComboScientist(scientists[9], new DeathMethod("Military Service", scientists[1].name, scientists[9].name));
        scientists[1].ComboScientist(scientists[22], new DeathMethod("Chemical Burn", scientists[1].name, scientists[22].name));

        //college student
        scientists[2].ComboScientist(scientists[3], new DeathMethod("Snake Charmed", scientists[2].name, scientists[3].name));
        scientists[2].ComboScientist(scientists[8], new DeathMethod("Extremism", scientists[2].name, scientists[8].name));
        scientists[2].ComboScientist(scientists[9], new DeathMethod("Debt", scientists[2].name, scientists[9].name));
        scientists[2].ComboScientist(scientists[13], new DeathMethod("Cocaine", scientists[2].name, scientists[13].name));
        scientists[2].ComboScientist(scientists[23], new DeathMethod("Misdiagnosis", scientists[2].name, scientists[23].name));

        //vet
        scientists[3].ComboScientist(scientists[4], new DeathMethod("E. Coli", scientists[3].name, scientists[4].name));
        scientists[3].ComboScientist(scientists[5], new DeathMethod("Hairball Ingestion", scientists[3].name, scientists[5].name));
        scientists[3].ComboScientist(scientists[7], new DeathMethod("Dog Driving", scientists[3].name, scientists[7].name));
        scientists[3].ComboScientist(scientists[10], new DeathMethod("Anthrax", scientists[3].name, scientists[10].name));
        scientists[3].ComboScientist(scientists[11], new DeathMethod("Shark Attack", scientists[3].name, scientists[11].name));
        scientists[3].ComboScientist(scientists[17], new DeathMethod("Chicken Pox", scientists[3].name, scientists[17].name));
        scientists[3].ComboScientist(scientists[23], new DeathMethod("Leeches", scientists[3].name, scientists[23].name));

        //chef
        scientists[4].ComboScientist(scientists[6], new DeathMethod("Eating a Nail", scientists[4].name, scientists[6].name));
        scientists[4].ComboScientist(scientists[7], new DeathMethod("Kitchen Fire", scientists[4].name, scientists[7].name));
        scientists[4].ComboScientist(scientists[8], new DeathMethod("Famine", scientists[4].name, scientists[8].name));
        scientists[4].ComboScientist(scientists[9], new DeathMethod("Cyanide", scientists[4].name, scientists[9].name));
        scientists[4].ComboScientist(scientists[10], new DeathMethod("Salmonella", scientists[4].name, scientists[10].name));
        scientists[4].ComboScientist(scientists[13], new DeathMethod("Hemlock", scientists[4].name, scientists[13].name));
        scientists[4].ComboScientist(scientists[17], new DeathMethod("Obesity", scientists[4].name, scientists[17].name));
        scientists[4].ComboScientist(scientists[22], new DeathMethod("Poison", scientists[4].name, scientists[22].name));

        //barber
        scientists[5].ComboScientist(scientists[9], new DeathMethod("Scalping", scientists[5].name, scientists[9].name));
        scientists[5].ComboScientist(scientists[12], new DeathMethod("Electric Razor", scientists[5].name, scientists[12].name));

        //construction worker
        scientists[6].ComboScientist(scientists[7], new DeathMethod("Jackhammer", scientists[6].name, scientists[7].name));
        scientists[6].ComboScientist(scientists[8], new DeathMethod("Barbed Wire", scientists[6].name, scientists[8].name));
        scientists[6].ComboScientist(scientists[12], new DeathMethod("Chainsaw", scientists[6].name, scientists[12].name));

        //mechanic
        scientists[7].ComboScientist(scientists[9], new DeathMethod("Booby Traps", scientists[7].name, scientists[9].name));
        scientists[7].ComboScientist(scientists[12], new DeathMethod("Busted Engine", scientists[7].name, scientists[12].name));
        scientists[7].ComboScientist(scientists[14], new DeathMethod("Faulty Airlock", scientists[7].name, scientists[14].name));
        scientists[7].ComboScientist(scientists[20], new DeathMethod("Head-on Collision", scientists[7].name, scientists[20].name));
        scientists[7].ComboScientist(scientists[22], new DeathMethod("Exhaust Inhalation", scientists[7].name, scientists[22].name));

        //historian
        scientists[8].ComboScientist(scientists[10], new DeathMethod("Plague", scientists[8].name, scientists[10].name));
        scientists[8].ComboScientist(scientists[12], new DeathMethod("Shipwreck", scientists[8].name, scientists[12].name));
        scientists[8].ComboScientist(scientists[14], new DeathMethod("Big Bang", scientists[8].name, scientists[14].name));
        scientists[8].ComboScientist(scientists[15], new DeathMethod("Wildfire", scientists[8].name, scientists[15].name));
        scientists[8].ComboScientist(scientists[17], new DeathMethod("Black Death", scientists[8].name, scientists[17].name));
        scientists[8].ComboScientist(scientists[19], new DeathMethod("Fireworks", scientists[8].name, scientists[19].name));
        scientists[8].ComboScientist(scientists[22], new DeathMethod("Mustard Gas", scientists[8].name, scientists[22].name));

        //gov't agent
        scientists[9].ComboScientist(scientists[11], new DeathMethod("Waterboarding", scientists[9].name, scientists[11].name));
        scientists[9].ComboScientist(scientists[13], new DeathMethod("Ricin", scientists[9].name, scientists[13].name));
        scientists[9].ComboScientist(scientists[16], new DeathMethod("Climate Change", scientists[9].name, scientists[16].name));
        scientists[9].ComboScientist(scientists[18], new DeathMethod("The Football", scientists[9].name, scientists[18].name));

        //biologist
        scientists[10].ComboScientist(scientists[11], new DeathMethod("Animal Attack", scientists[10].name, scientists[11].name));
        scientists[10].ComboScientist(scientists[12], new DeathMethod("Submarine Implosion", scientists[10].name, scientists[12].name));

        //marine biologist
        scientists[11].ComboScientist(scientists[14], new DeathMethod("Asphyxiation", scientists[11].name, scientists[14].name));
        scientists[11].ComboScientist(scientists[15], new DeathMethod("Flood", scientists[11].name, scientists[15].name));
        scientists[11].ComboScientist(scientists[16], new DeathMethod("Tsunami", scientists[11].name, scientists[16].name));
        scientists[11].ComboScientist(scientists[19], new DeathMethod("Torpedo", scientists[11].name, scientists[19].name));

        //engineer
        scientists[12].ComboScientist(scientists[20], new DeathMethod("Lab Explosion", scientists[12].name, scientists[20].name));
        scientists[12].ComboScientist(scientists[22], new DeathMethod("Poison Grenade", scientists[12].name, scientists[22].name));

        //botanist

        //astronomer
        scientists[14].ComboScientist(scientists[16], new DeathMethod("Meteorite", scientists[14].name, scientists[16].name));
        scientists[14].ComboScientist(scientists[18], new DeathMethod("Supernova", scientists[14].name, scientists[18].name));
        scientists[14].ComboScientist(scientists[19], new DeathMethod("Stranded in Space", scientists[14].name, scientists[19].name));

        //meteorologist
        scientists[15].ComboScientist(scientists[16], new DeathMethod("Drought", scientists[15].name, scientists[16].name));
        scientists[15].ComboScientist(scientists[22], new DeathMethod("Acid Rain", scientists[15].name, scientists[22].name));

        //geologist
        scientists[16].ComboScientist(scientists[21], new DeathMethod("Uranium", scientists[16].name, scientists[21].name));
        scientists[16].ComboScientist(scientists[22], new DeathMethod("Volcano", scientists[16].name, scientists[22].name));

        //epidemiologist
        scientists[17].ComboScientist(scientists[21], new DeathMethod("Cancer", scientists[17].name, scientists[21].name));

        //nuclear scientist
        scientists[18].ComboScientist(scientists[20], new DeathMethod("Atom Bomb", scientists[18].name, scientists[20].name));
        scientists[18].ComboScientist(scientists[21], new DeathMethod("Fallout", scientists[18].name, scientists[21].name));

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
            uiManager.SetHirePrefabText(scientists[i].gameObject, scientists[i].price);
            //Debug.Log($"{scientists[i].name}: {scientists[i].price}");
        }

        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Essentially acts as the new scientist constructor
    private Scientist CreateScientist(string name, int tier, string deathMethodName, int numInOrder)
    {
        GameObject instantiation = Instantiate(scientistForHirePrefab, Vector3.zero, Quaternion.identity, tab4content.GetChild(0));
        Scientist scientist = instantiation.GetComponent<Scientist>();

        //essentially make up the scientist constructor here
        scientist.name = name;
        scientist.tier = tier;
        scientist.mainMethod = new DeathMethod(deathMethodName, name, null);

        //set up whether they are purchased
        if (name == "HS Dropout" || name == "HS Graduate")
        {
            scientist.Purchased = true;
            scientist.transform.parent = GameObject.FindGameObjectWithTag("Bought Scientists").transform;
        }
        else
        {
            scientist.Purchased = false;
        }

        //because two scientists are purchased, move the other ones up the list
        numInOrder -= 2;
        scientist.numInOrder = numInOrder;

        //set up the appropriate text name
        instantiation.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = name;

        //set up the position of the object on tab number 4
        if (!scientist.Purchased)
        {
            //position the text
            instantiation.transform.GetChild(0).GetComponent<RectTransform>().position = new Vector3(320, /*instantiation.transform.parent.parent.GetComponent<RectTransform>().rect.y*/ - (numInOrder * 150), 0);

            /*instantiation.transform.position = new Vector3(instantiation.transform.parent.parent.position.x + 100f, 
                instantiation.transform.parent.parent.position.y + 500 + (130 * (11 - numInOrder)), 0);*/

            //increase the size of the content box for scrolling purposes
            tab4content.GetComponent<RectTransform>().sizeDelta = new Vector2(
                tab4content.GetComponent<RectTransform>().rect.width,
                tab4content.GetComponent<RectTransform>().rect.height + 200f);
            /*tab4content.GetComponent<RectTransform>().position = new Vector3(tab4content.GetComponent<RectTransform>().position.x, -500, 0); //Set scroll window to start at top*/
        }

        uiManager.hirePrefabs.Add(instantiation);
        
        return scientist;
    }
}
