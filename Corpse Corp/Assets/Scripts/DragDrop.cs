using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private UIManager uiManager;
    private ScientistManager sciManager;
    [SerializeField] RectTransform busyScientistParent;

    //the rect that the item came from, plus what number child it was
    private RectTransform draggableObjectsParent;
    int numInOrder;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Bottom Bar").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        uiManager = FindObjectOfType<UIManager>();
        sciManager = FindObjectOfType<ScientistManager>();
        busyScientistParent = GameObject.Find("Busy Scientist Parent").GetComponent<RectTransform>();

        draggableObjectsParent = transform.parent.GetComponent<RectTransform>();
        numInOrder = draggableObjectsParent.childCount - 1;
    }

    void Update()
    {
        //if we are dealing with a scientist
        if(transform.childCount == 2)
        {
            //if we are busy, take this scientist away
            if (uiManager.FindScientist(sciManager, transform.GetChild(0).GetComponent<TextMeshProUGUI>().text).busy)
            {
                transform.parent = busyScientistParent;
                transform.localPosition = Vector2.zero;
            }

            //if we become unbusy, bring this scientist back
            else if(transform.parent == busyScientistParent)
            {
                ResetPosition();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;

        transform.parent = canvas.transform;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        //if we have not been dragged to an item slot, just return to the draggable objects parent
        if(transform.parent.GetComponent<ItemSlot>() == null)
        {
            ResetPosition();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void ResetPosition() //Resets a Scientist back to its position in the scrollview
    {
        transform.SetParent(draggableObjectsParent, false);

        for (int i = 0; i < draggableObjectsParent.childCount - 1 - numInOrder; i++)
        {
            draggableObjectsParent.GetChild(numInOrder).SetAsLastSibling();
        }
    }
}
