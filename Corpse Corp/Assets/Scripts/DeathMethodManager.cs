using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMethodManager : MonoBehaviour
{
    //==== FIELDS ====
    public List<DeathMethod> deathMethods;

    public float money;
    
    // Start is called before the first frame update
    void Start()
    {
        money = 0;

        deathMethods = new List<DeathMethod>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
