using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DMSlot : MonoBehaviour
{
    [SerializeField] RectTransform SciBox1;
    [SerializeField] RectTransform SciBox2;

    ScientistManager scientistManager;
    DeathMethodManager deathMethodManager;
    UIManager uiManager;

    [SerializeField] GameObject displayObject;

    [SerializeField] GameObject dropdownPanel;

    [SerializeField] TextMeshProUGUI dropdownText;

    bool activeDropdown = false;

    // Start is called before the first frame update
    void Start()
    {
        scientistManager = FindObjectOfType<ScientistManager>();
        deathMethodManager = FindObjectOfType<DeathMethodManager>();
        uiManager = FindObjectOfType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if both boxes are empty, this box should be empty 
        if(SciBox1.childCount == 0 && SciBox2.childCount == 0)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }

            activeDropdown = false;
        }
        
        //if one or both boxes are full, this box should be full
        else
        {
            //Declare & Set Scientists
            Scientist sci1 = null;
            Scientist sci2 = null;
            if (SciBox1.childCount > 0)
            {
                sci1 = uiManager.FindScientist(scientistManager, SciBox1.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text.Trim());
            }
            if (SciBox2.childCount > 0)
            {
                sci2 = uiManager.FindScientist(scientistManager, SciBox2.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text.Trim());
            }

            //if there are TWO scientists, but they CANNOT be matched, display there are no combo's
            if(sci1 != null && sci2 != null && !sci1.combinations.ContainsKey(sci2.name))
            {
                //instantiate the object
                GameObject obj = Instantiate(displayObject, transform);
                obj.GetComponent<DragDrop>().enabled = false;
                
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

                activeDropdown = false;
            }

            //if there is a match or if there is not two scientists:
            else
            {
                //instantiate the object
                GameObject obj = Instantiate(displayObject, transform);
                obj.GetComponent<DragDrop>().enabled = false;


                if (sci1 != null && sci2 == null) //If only box 1 is filled, fill with Sci1 main method
                {
                    obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci1.mainMethod.name;
                    dropdownText.text = $"BOOST {sci1.mainMethod.name}";
                }
                else if (sci1 == null && sci2 != null) //If only box 2 is filled, fill with Sci2 main method
                {
                    obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci2.mainMethod.name;
                    dropdownText.text = $"BOOST {sci2.mainMethod.name}";
                }
                else //If both boxes are filled, fill with the combo method
                {
                    if (uiManager.FindDeathMethod(deathMethodManager, sci1.combinations[sci2.name]).active) //If the death method has been discovered, ask to boost
                    {
                        obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci1.combinations[sci2.name];
                        dropdownText.text = $"BOOST {sci1.combinations[sci2.name]}";
                    }
                    else //If the death method hasn't been discovered, ask to research
                    {
                        obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci1.combinations[sci2.name];
                        dropdownText.text = $"RESEARCH {sci1.combinations[sci2.name]}";
                    }
                }

                activeDropdown = true;
            }    
        }

        PositionDropdown();
    }

    //==== FUNCTIONS ====
    public void FillBoxes(Scientist sci1, Scientist sci2)
    {
        //If either box is filled, clear it
        if (SciBox1.childCount != 0)
        {
            SciBox1.GetChild(0).GetComponent<DragDrop>().ResetPosition();
        }
        if (SciBox2.childCount != 0)
        {
            SciBox2.GetChild(0).GetComponent<DragDrop>().ResetPosition();
        }

        //Loop through inventory assets
        for (int i = 0; i < GameObject.Find("Combiner Tab").transform.GetChild(1).GetChild(0).GetChild(0).childCount; i++)
        {
            //Get the name of the asset
            string invAssetName = GameObject.Find("Combiner Tab").transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetChild(0).GetComponent<TMP_Text>().text;
            
            //If the name matches Scientist1, send the asset to the item slot
            if (invAssetName == sci1.name)
            {
                //Get Asset and Set Parent & Position
                RectTransform invRect = GameObject.Find("Combiner Tab").transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetComponent<RectTransform>();
                invRect.SetParent(SciBox1, false);
                invRect.localPosition = Vector2.zero;
            }
            else if (invAssetName == sci2.name)
            {
                //Get Asset and Set Parent & Position
                RectTransform invRect = GameObject.Find("Combiner Tab").transform.GetChild(1).GetChild(0).GetChild(0).GetChild(i).GetComponent<RectTransform>();
                invRect.SetParent(SciBox2, false);
                invRect.localPosition = Vector2.zero;
            }
        }

        //Fill SciBox1 & SciBox2 with passed-in Scientists
        /*
         Foreach inventory asset
            Find the one with the same name
            Set it as the child of SciBox1 or SciBox2
            Make sure position gets swapped to the ItemSlot
         */
    }

    private void PositionDropdown()
    {
        RectTransform rectTransform = dropdownPanel.GetComponent<RectTransform>();
        Debug.Log(rectTransform.rect.height + ", " + rectTransform.parent.GetComponent<RectTransform>().rect.height);

        if(activeDropdown)
        {
            if (rectTransform.localPosition.y > -rectTransform.rect.height)
            {
                rectTransform.localPosition = new Vector2(0, rectTransform.localPosition.y - (rectTransform.rect.height * Time.deltaTime));
            }

            if(rectTransform.localPosition.y < - rectTransform.rect.height)
            {
                rectTransform.localPosition = new Vector2(0, -rectTransform.rect.height);
            }
        }
        else
        {
            if (rectTransform.localPosition.y < 0)
            {
                rectTransform.localPosition = new Vector2(0, rectTransform.localPosition.y + (rectTransform.rect.height * Time.deltaTime));
            }

            if(rectTransform.localPosition.y > 0)
            {
                rectTransform.localPosition = Vector2.zero;
            }
        }
    }
}
