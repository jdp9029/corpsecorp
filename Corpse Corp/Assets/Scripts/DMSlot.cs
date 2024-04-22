using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DMSlot : MonoBehaviour
{
    [SerializeField] RectTransform SciBox1;
    [SerializeField] RectTransform SciBox2;

    ScientistManager scientistManager;
    UIManager uiManager;

    [SerializeField] GameObject displayObject;

    // Start is called before the first frame update
    void Start()
    {
        scientistManager = FindObjectOfType<ScientistManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if neither box is full, this box should also be not full
        if(SciBox1.childCount == 0 || SciBox2.childCount == 0)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        
        //if both scientist box's are full, we need to figure out what to put in this box
        else
        {
            Scientist sci1 = uiManager.FindScientist(scientistManager, SciBox1.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text.Trim());
            Scientist sci2 = uiManager.FindScientist(scientistManager, SciBox2.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text.Trim());

            //first, can these two scientists be matched?

            //if not, display there are no combo's
            if(!sci1.combinations.ContainsKey(sci2.name))
            {
                //instantiate the object
                GameObject obj = Instantiate(displayObject, transform);
                
                //destroy the img
                Destroy(obj.transform.GetChild(1).gameObject);

                //center the text
                obj.transform.GetChild(0).GetComponent<RectTransform>().anchorMin = Vector2.zero;
                obj.transform.GetChild(0).GetComponent<RectTransform>().anchorMax = Vector2.one;
                obj.transform.GetChild(0).GetComponent<RectTransform>().offsetMin = Vector2.zero;
                obj.transform.GetChild(0).GetComponent<RectTransform>().offsetMax = Vector2.zero;
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().verticalAlignment = VerticalAlignmentOptions.Middle;

                //write "no combo"
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "No Combo";
            }

            //if there is a match, display the match
            else
            {
                //instantiate the object
                GameObject obj = Instantiate(displayObject, transform);

                //Write DM Name
                obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci1.combinations[sci2.name];
            }    

        }

    }

    //==== FUNCTIONS ====
    public void FillBoxes(Scientist sci1, Scientist sci2, DeathMethod dm)
    {

    }
}
