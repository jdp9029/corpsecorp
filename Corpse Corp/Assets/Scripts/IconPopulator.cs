using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconPopulator : MonoBehaviour
{
    [SerializeField] GameObject sciIconHolderPrefab;
    [SerializeField] GameObject iconPrefab;
    [SerializeField] GameObject purchasedScientistParent;

    List<Scientist> registeredPurchasedScientists = new List<Scientist>();

    // Start is called before the first frame update
    void Start()
    {
        //start by instantiating three scroll thingies
        
        Instantiate(sciIconHolderPrefab, transform);
        Instantiate(sciIconHolderPrefab, transform);
        Instantiate(sciIconHolderPrefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(registeredPurchasedScientists.Count != purchasedScientistParent.transform.childCount)
        {
            //if we need are populating the original three scrollviews
            if(purchasedScientistParent.transform.childCount < 12)
            {
                //figure out the new scientists
                for (int i = 0; i < purchasedScientistParent.transform.childCount; i++)
                {
                    Scientist sci = purchasedScientistParent.transform.GetChild(i).GetComponent<Scientist>();

                    //if we come across a new scientist, handle it accordingly
                    if (!registeredPurchasedScientists.Contains(sci))
                    {
                        registeredPurchasedScientists.Add(sci);

                        int child = 0;

                        if(registeredPurchasedScientists.Count <= 4)
                        {
                            child = 0;
                        }
                        else if(registeredPurchasedScientists.Count <= 8)
                        {
                            child = 1;
                        }   
                        else
                        {
                            child = 2;
                        }

                        GameObject newIconAsset = Instantiate(iconPrefab, transform.GetChild(child).GetChild(0).GetChild(0));
                        newIconAsset.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci.name;
                        newIconAsset.transform.GetChild(1).GetComponent<Image>().sprite = sci.Icon;
                    }
                }


                //don't do the rest of the code
                return;
            }    

            //figure out the new scientists
            for(int i = 0; i < purchasedScientistParent.transform.childCount ; i++)
            {
                Scientist sci = purchasedScientistParent.transform.GetChild(i).GetComponent<Scientist>();

                //if we come across a new scientist, handle it accordingly
                if(!registeredPurchasedScientists.Contains(sci))
                {
                    registeredPurchasedScientists.Add(sci);

                    //if we do not need a new holder
                    if (Mathf.Ceil((float)(registeredPurchasedScientists.Count - 1) / 4f) == Mathf.Ceil(((float)registeredPurchasedScientists.Count / 4f)))
                    {
                        RectTransform lastHolderContent = transform.GetChild(transform.childCount - 1).GetChild(0).GetChild(0).GetComponent<RectTransform>();

                        GameObject newIconAsset = Instantiate(iconPrefab, lastHolderContent);
                        newIconAsset.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci.name;
                        newIconAsset.transform.GetChild(1).GetComponent<Image>().sprite = sci.Icon;
                    }

                    //if we do need a new holder
                    else
                    {
                        GameObject newHolder = Instantiate(sciIconHolderPrefab, transform);

                        GameObject newIconAsset = Instantiate(iconPrefab, newHolder.transform.GetChild(0).GetChild(0));
                        newIconAsset.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci.name;
                        newIconAsset.transform.GetChild(1).GetComponent<Image>().sprite = sci.Icon;
                    }
                }
            }
        }
    }
}
