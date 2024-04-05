using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    [SerializeField] RectTransform tab3content;
    [SerializeField] RectTransform tab4content;

    [HideInInspector] UnityAction nextStepAction;


    enum Steps
    {
        clickResearchButton = 1,
        clickOtherSci = 2,
        waitForBook = 3,
        clickEconTab = 4,
        scrollToBook = 5,
        selectHSDropout = 6,
        selectBoost = 7,
        selectHireTab = 8,
        hitCoinOnGrad = 9,
        boostPencil = 10,
        summaryScreen = 11
    }

    Steps stepTracker = Steps.clickResearchButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(stepTracker)
        {
            //if we are telling the 
            case Steps.clickResearchButton:

                //add a listener if there is only one (the one that will open the panel)
                if(tab4content.GetChild(0).GetChild(2).GetComponent<Button>().onClick.GetPersistentEventCount() == 1)
                {
                    nextStepAction = () =>
                    {
                        stepTracker = Steps.clickOtherSci;
                        tab4content.GetChild(0).GetChild(2).GetComponent<Button>().onClick.RemoveListener(nextStepAction);
                    };
                    tab4content.GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(nextStepAction);
                }
                break;

            case Steps.clickOtherSci:
                
                Button researchButton = tab4content.parent.parent.parent.GetChild(2).GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>();

                //add a listener if there is only one (the one that will start the research)
                if(researchButton.onClick.GetPersistentEventCount() == 1)
                {
                    nextStepAction = () =>
                    {
                        stepTracker = Steps.waitForBook;
                        researchButton.onClick.RemoveListener(nextStepAction);
                    };
                    researchButton.onClick.AddListener(nextStepAction);
                }

                break;

            case Steps.waitForBook:

                EventTrigger tabTrigger = tab4content.parent.parent.parent.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerClick;

                if (!tab4content.GetChild(0).GetComponent<Scientist>().busy && entry.callback.GetPersistentEventCount() == 0)
                {
                    entry.callback.AddListener(delegate
                    {
                        stepTracker = Steps.clickEconTab;
                        tabTrigger.triggers.Remove(entry);
                    });
                    tabTrigger.triggers.Add(entry);
                }

                break;
            case Steps.clickEconTab:
                break;
            case Steps.scrollToBook:
                break;
            case Steps.selectHSDropout:
                break;
            case Steps.selectBoost:
                break;
            case Steps.selectHireTab:
                break;
            case Steps.hitCoinOnGrad:
                break;
            case Steps.boostPencil:
                break;
            case Steps.summaryScreen:
                break;
        }
    }

    //Click the research button
    //Click the other scientist on the panel
    //Send them to the econ tab
    //scroll down to book
    //select hs dropout
    //boost
    //go back to the scientist's tab
    //have them hit the coin on grad
    //have them boost pencil
    //summary screen
}
