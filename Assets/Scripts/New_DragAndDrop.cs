using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR;

public class New_DragAndDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject computerPanel;

    private RectTransform panelRect;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Transform lastSlot;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        panelRect = computerPanel.GetComponent<RectTransform>();
        lastSlot = gameObject.transform.parent;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        gameObject.transform.SetParent(GameObject.Find("Computer panel").transform);

        // Debug.Log("New_DragAndDrop: OnBeginDrag");
        canvasGroup.blocksRaycasts = false; // 
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("New_DragAndDrop: OnDrag");

        float x = (eventData.position.x - canvas.pixelRect.width/2);
        float y = (eventData.position.y - canvas.pixelRect.height/2);
        
        float scalefactorX = (1920 / canvas.pixelRect.width );
        float scalefactorY = (1080 / canvas.pixelRect.height);
    
        rectTransform.localPosition = new Vector2(x * scalefactorX, y * scalefactorY);

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("New_DragAndDrop: OnEndDrag");
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        

        if (transform.parent != null) {

            if ( transform.parent.parent.tag == "Bin" ) {
                Debug.Log("Drop on bin");
                // Do nothing, it will be Destoryed by Bin script

            } else if ( transform.parent.parent.name == "DesiredCreatureGO") {
                Debug.Log("Drop on desired slot");
                MoveToOriginalPosition();

            } else if ( transform.parent.tag == "Slot") {
                Debug.Log("Drop on slot");
                lastSlot = rectTransform.gameObject.transform.parent;

            } 
            else {
                Debug.Log("Drop on other");
                MoveToOriginalPosition();

            }
        } else {
            Debug.Log("Drop on null");
            MoveToOriginalPosition();
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Debug.Log("New_DragAndDrop: OnPointerDown");
    }

    private void MoveToOriginalPosition()
    {
        //Debug.Log("Move to original position");

        gameObject.transform.SetParent(lastSlot.transform);
        rectTransform.localPosition = Vector3.zero;
    }
}
