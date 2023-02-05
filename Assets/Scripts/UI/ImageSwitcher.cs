using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitcher : MonoBehaviour
{
    public Sprite image1;
    public Sprite image2;
    public float switchTime = 1.0f;

    private Image image;
    private float currentImage = 0.5f;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = image1;
        StartCoroutine(SwitchImage());
    }

    IEnumerator SwitchImage()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchTime);
            currentImage = (currentImage == 1) ? 2 : 1;
            image.sprite = (currentImage == 1) ? image1 : image2;
        }
    }
}
