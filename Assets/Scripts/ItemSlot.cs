using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().transform.SetParent(GetComponent<RectTransform>().transform);
            eventData.pointerDrag.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            eventData.pointerDrag.GetComponent<RectTransform>().transform.localScale = new Vector3(100, 100);
            if (transform.parent != null && GetComponent<RectTransform>().tag == "Bin")
            {
                Destroy(eventData.pointerDrag.GetComponent<GameObject>().transform.gameObject);
            }
        }
    }
}
