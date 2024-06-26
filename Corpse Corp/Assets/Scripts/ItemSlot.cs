using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.GetComponent<DragDrop>() == null) { return; }

            if(transform.childCount == 1)
            {
                transform.GetChild(0).GetComponent<DragDrop>().ResetPosition();
            }

            eventData.pointerDrag.transform.SetParent(this.transform, false);
            eventData.pointerDrag.GetComponent<RectTransform>().localPosition = Vector2.zero;
        }
    }
}
