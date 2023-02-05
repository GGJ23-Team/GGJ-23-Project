using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Vector2 initialPosition;
    private Vector2 offset;
    GameObject slot;
    private bool isHoveringTarget = false;
    float fitMargin = .66f;

    void Start()
    {

        initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        offset = (Vector2)transform.position - (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    

    private void OnMouseDrag()
    {
        Vector2 cursorScreenPoint = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        transform.position = cursorScreenPoint;

        // Verifica si se encuentra a menos de la mitad de su tama�o de distancia de un objetivo
        
        if (slot != null)
        {
            float distance = Vector2.Distance(transform.position, slot.transform.position);
            float fitDistance = GetComponent<SpriteRenderer>().bounds.size.magnitude * fitMargin;

            //Transform slotFrame = slot.transform.GetChild(0);
            
            if (distance < fitDistance)
            {
                Debug.Log("Est� sobre slot");
                isHoveringTarget = true;
                //slotFrame.GetComponent<SpriteRenderer>().color = Color.green;
                //slot.GetComponent<SpriteRenderer>().color = Color.green;
            } else
            {
                isHoveringTarget = false;
                //slotFrame.GetComponent<SpriteRenderer>().color = Color.white;
                //slot.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }

        
    }

    private void OnMouseUp()
    {
        if (isHoveringTarget)
        {
            transform.position = slot.transform.position;
            initialPosition = transform.position;
        }
        else
        {
            transform.position = initialPosition;
        }
        /*else
        {
            //transform.position = initialPosition;
            //if (!GetComponent<BoxCollider2D>().IsTouching(GameObject.Find("Cage").GetComponent<BoxCollider2D>()))
            if (!GameObject.Find("Cage").GetComponent<BoxCollider2D>().bounds.Contains(gameObject.GetComponent<BoxCollider2D>().bounds.center))
            {
                Debug.Log("No tocando");
                transform.position = initialPosition;
            } else
            {
                Debug.Log("Tocando");
                initialPosition = transform.position;
            }
        }*/

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Slot")) //TAG  de los slot
        {
            Debug.Log("SLOT SLOT SLOT");
            slot = collision.gameObject;
        }
    }
}