using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMethodManager : MonoBehaviour
{
    //==== FIELDS ====
    public List<DeathMethod> deathMethods = new List<DeathMethod>();

    [HideInInspector] public float money;
    [HideInInspector] public float moneyPerSecond;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Set moneyPerSecond to Zero
        moneyPerSecond = 0;
        
        //Update Money & Money per Second
        for (int i = 0; i < deathMethods.Count; i++)
        {
            //Update Money Per Second
            if (deathMethods[i].active)
            {
                moneyPerSecond += Mathf.Round(deathMethods[i].price / deathMethods[i].rateOfSale);
            }
        }
    }
}
