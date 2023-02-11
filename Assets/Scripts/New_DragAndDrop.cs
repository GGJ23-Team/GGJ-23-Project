using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class New_DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject computerPanel;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 lastPosition = Vector3.zero;
    private Transform lastSlot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        lastPosition = rectTransform.position;
        lastSlot = gameObject.transform.parent;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(GameObject.Find("Computer panel").transform);

        // Debug.Log("New_DragAndDrop: OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("New_DragAndDrop: OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Debug.Log("New_DragAndDrop: OnEndDrag");
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        if (transform.parent != null && transform.parent.tag != "Slot")
        {
            gameObject.transform.SetParent(lastSlot.transform);
            rectTransform.position = lastPosition;
        }
        else
        {
            lastSlot = rectTransform.gameObject.transform.parent;
            lastPosition = rectTransform.position;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("New_DragAndDrop: OnPointerDown");
    }
}
