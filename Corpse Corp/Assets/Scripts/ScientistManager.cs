using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistManager : MonoBehaviour
{
    public Scientist[] scientists;

    // Start is called before the first frame update
    void Start()
    {       
        #region scientists initialized and combo'd
        scientists = new Scientist[]
        {
            new Scientist("HS Dropout", 1, new DeathMethod("Soft Pillow")),
            new Scientist("HS Graduate", 1, new DeathMethod("Pencil")),
            new Scientist("College Student", 1, new DeathMethod("Stress")),
            new Scientist("Veterinarian", 2, new DeathMethod("Rabies")),
            new Scientist("Chef", 2, new DeathMethod("Knife")),
            new Scientist("Barber", 2, new DeathMethod("Razor")),
            new Scientist("Construction Worker", 2, new DeathMethod("Hammer")),
            new Scientist("Mechanic", 2, new DeathMethod("Wrench")),
            new Scientist("Historian", 2, new DeathMethod("War")),
            new Scientist("Government Agent", 2, new DeathMethod("Assassination")),
            new Scientist("Biologist", 3, new DeathMethod("Infection")),
            new Scientist("Marine Biologist", 3, new DeathMethod("Piranhas")),
            new Scientist("Engineer", 3, new DeathMethod("Building Collapse")),
            new Scientist("Botanist", 3, new DeathMethod("Venus Flytrap")),
            new Scientist("Astronomer", 3, new DeathMethod("Black Hole")),
            new Scientist("Meteorologist", 3, new DeathMethod("Storm")),
            new Scientist("Geologist", 3, new DeathMethod("Earthquake")),
            new Scientist("Epidemiologist", 4, new DeathMethod("Influenza")),
            new Scientist("Nuclear Scientist", 4, new DeathMethod("Fusion Reaction")),
            new Scientist("Rocket Scientist", 4, new DeathMethod("Missile")),
            new Scientist("Physicist", 4, new DeathMethod("Fall Damage")),
            new Scientist("Radiologist", 4, new DeathMethod("Radiation")),
            new Scientist("Chemist", 4, new DeathMethod("Acid"))
        };

        //Debug.Log(scientists.Length);

        //combos should have the index of the scientist on the left always LESS than the index of the scientist on the right
        //ie. when we get to scientists[1] that does not need to match with scientists[0], that's already taken care of

        //all combos for HS dropout
        scientists[0].ComboScientist(scientists[1], new DeathMethod("Book"));
        scientists[0].ComboScientist(scientists[2], new DeathMethod("Herpes"));
        scientists[0].ComboScientist(scientists[4], new DeathMethod("Food Poisoning"));
        scientists[0].ComboScientist(scientists[6], new DeathMethod("Dropped Steel Beam"));
        scientists[0].ComboScientist(scientists[8], new DeathMethod("Gang Activity"));
        scientists[0].ComboScientist(scientists[11], new DeathMethod("Drowning"));
        scientists[0].ComboScientist(scientists[22], new DeathMethod("Broken Glass"));

        //hs graduate
        scientists[1].ComboScientist(scientists[2], new DeathMethod("Hit By Bus"));
        scientists[1].ComboScientist(scientists[5], new DeathMethod("Scissors"));
        scientists[1].ComboScientist(scientists[6], new DeathMethod("Scaffolding Collapse"));
        scientists[1].ComboScientist(scientists[22], new DeathMethod("Chemical Burn"));

        //college student
        scientists[2].ComboScientist(scientists[8], new DeathMethod("Extremism"));
        scientists[2].ComboScientist(scientists[13], new DeathMethod("Cocaine"));

        //vet
        scientists[3].ComboScientist(scientists[4], new DeathMethod("E. Coli"));
        scientists[3].ComboScientist(scientists[10], new DeathMethod("Anthrax"));
        scientists[3].ComboScientist(scientists[11], new DeathMethod("Shark Attack"));
        scientists[3].ComboScientist(scientists[17], new DeathMethod("Smallpox"));

        //chef
        scientists[4].ComboScientist(scientists[7], new DeathMethod("Kitchen Fire"));
        scientists[4].ComboScientist(scientists[8], new DeathMethod("Famine"));
        scientists[4].ComboScientist(scientists[10], new DeathMethod("Salmonella"));
        scientists[4].ComboScientist(scientists[13], new DeathMethod("Hemlock"));
        scientists[4].ComboScientist(scientists[17], new DeathMethod("Obesity"));
        scientists[4].ComboScientist(scientists[22], new DeathMethod("Poison"));

        //barber
        scientists[5].ComboScientist(scientists[9], new DeathMethod("Scalping"));
        scientists[5].ComboScientist(scientists[12], new DeathMethod("Electric Razor"));

        //construction worker
        scientists[6].ComboScientist(scientists[7], new DeathMethod("Jackhammer"));
        scientists[6].ComboScientist(scientists[12], new DeathMethod("Chainsaw"));

        //mechanic
        scientists[7].ComboScientist(scientists[12], new DeathMethod("Busted Engine"));
        scientists[7].ComboScientist(scientists[14], new DeathMethod("Faulty Airlock"));
        scientists[7].ComboScientist(scientists[20], new DeathMethod("Head-on Collision"));
        scientists[7].ComboScientist(scientists[22], new DeathMethod("Exhaust Inhalation"));

        //historian
        scientists[8].ComboScientist(scientists[10], new DeathMethod("Plague"));
        scientists[8].ComboScientist(scientists[12], new DeathMethod("Shipwreck"));
        scientists[8].ComboScientist(scientists[14], new DeathMethod("Big Bang"));
        scientists[8].ComboScientist(scientists[17], new DeathMethod("Black Death"));
        scientists[8].ComboScientist(scientists[19], new DeathMethod("Fireworks"));
        scientists[8].ComboScientist(scientists[22], new DeathMethod("Mustard Gas"));

        //gov't agent
        scientists[9].ComboScientist(scientists[11], new DeathMethod("Waterboarding"));
        scientists[9].ComboScientist(scientists[13], new DeathMethod("Ricin"));
        scientists[9].ComboScientist(scientists[16], new DeathMethod("Climate Change"));
        scientists[9].ComboScientist(scientists[18], new DeathMethod("The Football"));

        //biologist
        scientists[10].ComboScientist(scientists[11], new DeathMethod("Animal Attack"));
        scientists[10].ComboScientist(scientists[12], new DeathMethod("Submarine Implosion"));

        //marine biologist
        scientists[11].ComboScientist(scientists[14], new DeathMethod("Asphyxiation"));
        scientists[11].ComboScientist(scientists[15], new DeathMethod("Flood"));
        scientists[11].ComboScientist(scientists[16], new DeathMethod("Tsunami"));
        scientists[11].ComboScientist(scientists[19], new DeathMethod("Torpedo"));

        //engineer
        scientists[12].ComboScientist(scientists[20], new DeathMethod("Lab Explosion"));
        scientists[12].ComboScientist(scientists[22], new DeathMethod("Poison Grenade"));

        //botanist

        //astronomer
        scientists[14].ComboScientist(scientists[16], new DeathMethod("Meteorite"));
        scientists[14].ComboScientist(scientists[18], new DeathMethod("Supernova"));
        scientists[14].ComboScientist(scientists[19], new DeathMethod("Stranded in Space"));

        //meteorologist
        scientists[15].ComboScientist(scientists[16], new DeathMethod("Drought"));
        scientists[15].ComboScientist(scientists[22], new DeathMethod("Acid Rain"));

        //geologist
        scientists[16].ComboScientist(scientists[21], new DeathMethod("Uranium"));
        scientists[16].ComboScientist(scientists[22], new DeathMethod("Volcano"));

        //epidemiologist
        scientists[17].ComboScientist(scientists[21], new DeathMethod("Cancer"));

        //nuclear scientist
        scientists[18].ComboScientist(scientists[20], new DeathMethod("Atom Bomb"));
        scientists[18].ComboScientist(scientists[21], new DeathMethod("Fallout"));

        //rocket scientist

        //physicist

        //radiologist

        //chemist

        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
