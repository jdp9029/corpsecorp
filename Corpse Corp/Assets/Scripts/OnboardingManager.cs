using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnboardingManager : MonoBehaviour
{
    [SerializeField] RectTransform tab3content;
    [SerializeField] RectTransform tab4content;

    [HideInInspector] UnityAction nextStepAction;

    private bool initialGradStep = true;
    private bool initialBoostStep = true;
    [HideInInspector] public bool WalkthroughInProgress { get; private set; } = true;


    enum Steps
    {
        clickResearchButton = 1,
        clickOtherSci = 2,
        waitForBook = 3,
        clickEconTab = 4,
        scrollToBook = 5,
        selectHSGraduate = 6,
        selectBoost = 7,
        selectHireTab = 8,
        hitCoinOnDropout = 9,
        boostPillow = 10,
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
        if(!WalkthroughInProgress)
        {
            return;
        }

        switch(stepTracker)
        {
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

                EventTrigger econTab = tab3content.parent.parent.parent.GetComponent<EventTrigger>();
                EventTrigger.Entry econEntry = new EventTrigger.Entry();
                econEntry.eventID = EventTriggerType.PointerClick;
                if(econEntry.callback.GetPersistentEventCount() == 1)
                {
                    econEntry.callback.AddListener(delegate
                    {
                        stepTracker = Steps.scrollToBook;
                        econTab.triggers.Remove(econEntry);
                    });
                    econTab.triggers.Add(econEntry);
                }

                break;
            case Steps.scrollToBook:

                //-- this code already auto scrolls to book --
                //-- this code is all we need for this step --

                ScrollRect scrollRect = tab3content.parent.parent.GetComponent<ScrollRect>();
                RectTransform target = tab3content.GetChild(2).GetComponent<RectTransform>();

                tab3content.anchoredPosition =
                    (Vector2)scrollRect.transform.InverseTransformPoint(tab3content.position) - (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
                stepTracker = Steps.selectHSGraduate;

                break;
            case Steps.selectHSGraduate:

                 Button gradButton = tab3content.GetChild(2).GetChild(4).GetComponent<Button>();
                 if(initialGradStep)
                 {
                    gradButton.onClick.AddListener(delegate
                    {
                        stepTracker = Steps.selectBoost;
                    });

                    initialGradStep = false;
                 }

                break;
            case Steps.selectBoost:

                Button boostButton = tab3content.GetChild(2).GetChild(2).GetComponent<Button>();

                if (initialBoostStep)
                {
                    boostButton.onClick.AddListener(delegate
                    {
                        stepTracker = Steps.selectHireTab;
                    });

                    initialBoostStep = false;
                }

                break;
            case Steps.selectHireTab:

                EventTrigger labTab = tab4content.parent.parent.parent.GetComponent<EventTrigger>();
                EventTrigger.Entry labEntry = new EventTrigger.Entry();
                labEntry.eventID = EventTriggerType.PointerClick;
                if (labEntry.callback.GetPersistentEventCount() == 1)
                {
                    labEntry.callback.AddListener(delegate
                    {
                        stepTracker = Steps.scrollToBook;
                        labTab.triggers.Remove(labEntry);
                    });
                    labTab.triggers.Add(labEntry);
                }

                break;
            case Steps.hitCoinOnDropout:
                break;
            case Steps.boostPillow:
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
