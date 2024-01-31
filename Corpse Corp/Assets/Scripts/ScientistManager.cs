using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScientistManager : MonoBehaviour
{
    Scientist[] scientists;

    // Start is called before the first frame update
    void Start()
    {
        scientists = new Scientist[]
        {
            new Scientist("HS Dropout", 1, new DeathMethod("Soft Pillow")),
            new Scientist("HS Graduate", 1),
            new Scientist("College Student", 1),
            new Scientist("Veterinarian", 2),
            new Scientist("Chef", 2),
            new Scientist("Barber", 2),
            new Scientist("Construction Worker", 2),
            new Scientist("Mechanic", 2),
            new Scientist("Historian", 2),
            new Scientist("Government Agent", 2),
            new Scientist("Biologist", 3),
            new Scientist("Marine Biologist", 3),
            new Scientist("Engineer", 3),
            new Scientist("Botanist", 3),
            new Scientist("Astronomer", 3),
            new Scientist("Meteorologist", 3),
            new Scientist("Geologist", 3),
            new Scientist("Epidemiologist", 4),
            new Scientist("Nuclear Scientist", 4),
            new Scientist("Rocket Scientist", 4),
            new Scientist("Physicist", 4),
            new Scientist("Radiologist", 4),
            new Scientist("Chemist", 4)
        };


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
