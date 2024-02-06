using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathMethodManager : MonoBehaviour
{
    //==== FIELDS ====
    public List<DeathMethod> deathMethods;

    [HideInInspector] public float money;

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

    public void AddToList(DeathMethod method)
    {
        deathMethods.Add(method);
        Debug.Log(deathMethods.Count);
        Debug.Log($"{method.name} added.");
    }

    public void PrintList()
    {
        Debug.Log(deathMethods.Count);
        for (int i = 0; i < deathMethods.Count; i++)
        {
            Debug.Log(deathMethods[i].name);
        }
        Debug.Log("List Printed");
    }
}
