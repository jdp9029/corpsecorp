using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMethodManager : MonoBehaviour
{
    //==== FIELDS ====
    public List<DeathMethod> deathMethods = new List<DeathMethod>();

    [HideInInspector] public float money;

    // Start is called before the first frame update
    void Start()
    {
        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < deathMethods.Count; i++)
        {
            if (deathMethods[i].active && !deathMethods[i].passivePurchased)
            {
                StartCoroutine(deathMethods[i].UpdateMoney());
            }
        }
    }
}
