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
            new Scientist("HS Dropout", 1),
            new Scientist("HS Graduate", 1),
            new Scientist("College Student", 1),
            new Scientist("Veterinarian", 2),
            new Scientist("Chef", 2),
            new Scientist("Barber", 2),
            new Scientist("Construction Worker", 2),
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
