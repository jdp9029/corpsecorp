using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEditor.UI;
using UnityEngine.UI;

public class Scientist : MonoBehaviour
{
    public Dictionary<string, string> combinations = new Dictionary<string, string>();
    public DeathMethod mainMethod;
    public string name;
    public int tier;
    public bool Purchased;
    public int numInOrder;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.parent.parent.position.x + 240f,
            transform.parent.parent.position.y + (130 * (11 - numInOrder)), 0);

        if(Purchased)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            numInOrder = -1;
        }
    }

    //Combines two scientists together (called only in ScientistManager.Start())
    public void ComboScientist(Scientist scientist, DeathMethod method)
    {
        combinations.Add(scientist.name, method.name);
        scientist.combinations.Add(this.name, method.name);
        method.scientist1name = this.name;
        method.scientist2name = scientist.name;
    }
}
