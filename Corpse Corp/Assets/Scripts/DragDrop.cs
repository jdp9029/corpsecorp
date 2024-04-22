using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    //the rect that the item came from, plus what number child it was
    private RectTransform draggableObjectsParent;
    int numInOrder;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("Bottom Bar").GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();

        draggableObjectsParent = transform.parent.GetComponent<RectTransform>();
        numInOrder = draggableObjectsParent.childCount - 1;
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
