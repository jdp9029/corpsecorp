using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //==== FIELDS FOR MATCH SCIENTISTS TAB ====
    [SerializeField] Dropdown dropdown1;
    [SerializeField] Dropdown dropdown2;
    [SerializeField] Button matchButton;

    public ScientistManager sciManager; //This will need to be initialized
    
    // Start is called before the first frame update
    void Start()
    {
        dropdown1 = GameObject.FindGameObjectsWithTag("Dropdown")[0].GetComponent<Dropdown>();
        dropdown2 = GameObject.FindGameObjectsWithTag("Dropdown")[1].GetComponent<Dropdown>();
    }

    // Update is called once per frame
    void Update()
    {
        //SciManager still needs a constructor to be initialized. Theoretically, this should fill out the options for the Match Scientists dropdowns
        for (int i = 0; i < sciManager.scientists.Count; i++)
        {
            if (sciManager.scientists[i].purchased)
            {
                dropdown1.options.Add(sciManager.scientists[i]/*.name*/);
                dropdown2.options.Add(sciManager.scientists[i]/*.name*/);
            }
        }

        if (matchButton.onClick != null)
        {

        }
    }
}
