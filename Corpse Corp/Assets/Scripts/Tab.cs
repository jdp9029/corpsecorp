using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
    //Background image
    public Image bottomBar;

    //Logo of Tab
    public Sprite icon;

    //Out of the five tabs, index of this tab
    private int tabIndex;

    //Four points of this tab clicker
    private Vector2 topleftPoint;
    private Vector2 toprightPoint;
    private Vector2 bottomleftPoint;
    private Vector2 bottomrightPoint;

    // Start is called before the first frame update
    void Start()
    {
        bottomBar = transform.parent.GetComponent<Image>();

        //Sets up tabIndex and the four points
        float backgroundLength = bottomBar.transform.localScale.x * bottomBar.rectTransform.rect.width;
        float backgroundHeight = bottomBar.transform.localScale.y * bottomBar.rectTransform.rect.height;
        float backgroundX = bottomBar.transform.position.x;
        float backgroundY = bottomBar.transform.position.y;

        for(int i = 0; i < 5; i++)
        {
            if(bottomBar.transform.GetChild(i) == transform)
            {
                tabIndex = i;
                float leftX = backgroundX + (i * backgroundLength)/ 5;
                float rightX = backgroundX + ((i + 1)* backgroundLength)/ 5;
                float topY = backgroundY + (.5f * backgroundHeight);
                float bottomY = backgroundY - (.5f * backgroundHeight);

                topleftPoint = new Vector2(leftX, topY);
                toprightPoint = new Vector2(rightX, topY);
                bottomleftPoint = new Vector2(leftX, bottomY);
                bottomrightPoint = new Vector2(rightX, bottomY);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //three functions that get referenced in EventTrigger
    public void OnHoverStart()
    {
        transform.GetChild(0).localScale = new Vector3(1, 1.2f, 1);
    }
    public void OnHoverEnd()
    {
        transform.GetChild(0).localScale = Vector3.one;
    }
    public void OnMouseClick()
    {
        GameObject.FindObjectOfType<TabManager>().TabClicked(this);
    }
}
