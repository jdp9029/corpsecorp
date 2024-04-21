using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class IconPopulator : MonoBehaviour
{
    [SerializeField] GameObject iconPrefab;
    [SerializeField] GameObject purchasedScientistParent;

    List<Scientist> registeredPurchasedScientists = new List<Scientist>();

    // Update is called once per frame
    void Update()
    {
        if(registeredPurchasedScientists.Count != purchasedScientistParent.transform.childCount)
        {
            //figure out the new scientists
            for (int i = 0; i < purchasedScientistParent.transform.childCount; i++)
            {
                Scientist sci = purchasedScientistParent.transform.GetChild(i).GetComponent<Scientist>();

                //if we come across a new scientist, handle it accordingly
                if (!registeredPurchasedScientists.Contains(sci))
                {
                    registeredPurchasedScientists.Add(sci);

                    GameObject newIconAsset = Instantiate(iconPrefab, transform);
                    newIconAsset.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sci.name;
                    newIconAsset.transform.GetChild(1).GetComponent<Image>().sprite = sci.Icon;
                }
            }
        }
    }
}
