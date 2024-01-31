using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistManager : MonoBehaviour
{
    Scientist[] scientists;

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

        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
