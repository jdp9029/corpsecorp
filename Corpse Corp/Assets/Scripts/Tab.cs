using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    //FRAMEWORK INCOMPLETE - JAMES WILL CONTINUE BUILDING OUT -
    //- PROBABLY DOESNT MAKE SENSE YET

    public Image bottomBar;
    public Sprite icon;

    private int tabIndex;
    private float leftPoint;
    private float rightPoint;
    private float centerPoint;

    // Start is called before the first frame update
    void Start()
    {
        float backgroundLength = bottomBar.transform.localScale.x * bottomBar.rectTransform.rect.width;
        float backgroundHeight = bottomBar.transform.localScale.y * bottomBar.rectTransform.rect.height;
        float backgroundX = bottomBar.transform.position.x;
        float backgroundY = bottomBar.transform.position.y;

        for(int i = 0; i < 5; i++)
        {
            if(transform.GetChild(i) == transform)
            {
                tabIndex = i;
                leftPoint = i / 5;
                rightPoint = (i + 1)/ 5;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
