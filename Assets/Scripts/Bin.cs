using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Bin : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        // Debug.Log("Bin: OnDrop");
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().transform.SetParent(GetComponent<RectTransform>().transform);
            TrashCreature(eventData.pointerDrag.GetComponent<RectTransform>());
        }
    }

    public void TrashCreature(Transform t)
    {
        CreatureSessionLog.Instance.SetDeletedCreature( t.GetComponent<CreatureUI>().GetCreatureID() );
        Destroy(t.GetComponent<RectTransform>().gameObject);
    }
}
