using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour
{
    public Texture2D openHandTexture;
    public Texture2D dragHandTexture;
    Vector2 hotspot;
    // Start is called before the first frame update
    void Start()
    {
        hotspot = new Vector2(openHandTexture.width / 2, openHandTexture.height / 2);
        Cursor.SetCursor(openHandTexture, hotspot/*Vector2.zero*/, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(dragHandTexture, hotspot/*Vector2.zero*/, CursorMode.Auto);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(openHandTexture, hotspot/*Vector2.zero*/, CursorMode.Auto);
        }
    }
}
