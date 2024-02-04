using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //==== FIELDS FOR MATCH SCIENTISTS TAB ====
    Dropdown dropdown1;
    Dropdown dropdown2;
    Button matchButton;

    public ScientistManager sciManager;

    List<string> purchasedScientists;
    
    // Start is called before the first frame update
    void Start()
    {
        dropdown1 = GameObject.FindGameObjectsWithTag("Dropdown")[0].GetComponent<Dropdown>();
        dropdown2 = GameObject.FindGameObjectsWithTag("Dropdown")[1].GetComponent<Dropdown>();

        purchasedScientists = new List<string>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(sciManager.scientists[0].name);

        //SciManager still needs a constructor to be initialized. Theoretically, this should fill out the options for the Match Scientists dropdowns
        for (int i = 0; i < sciManager.scientists.Length; i++)
        {
            Debug.Log(sciManager.scientists[i].name);
            if (sciManager.scientists[i].Purchased && !purchasedScientists.Contains(sciManager.scientists[i].name))
            {
                purchasedScientists.Add(sciManager.scientists[i].name);
            }
        }
        dropdown1.AddOptions(purchasedScientists);
        dropdown2.AddOptions(purchasedScientists);

        if (matchButton.onClick != null)
        {

        }
    }
}
