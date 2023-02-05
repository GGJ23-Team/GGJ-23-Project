using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public GameObject book;
    private void OnMouseDown()
    {
        Debug.Log("LIBROOOO");
        book.SetActive(true);
    }
}