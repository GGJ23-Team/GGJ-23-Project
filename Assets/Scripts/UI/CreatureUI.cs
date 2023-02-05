using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureUI : MonoBehaviour
{
  [Header("Attributes")]
  [SerializeField] private Sprite bodySprite;
  [Range(0, 360)]
  [SerializeField] private int hueColor;
  [SerializeField] private Sprite eyesSprite;
  [SerializeField] private Sprite mouthSprite;

  [Header("Linked gameobjects")]
  [SerializeField] private Image bodyImage;
  [SerializeField] private Image eyesImage;
  [SerializeField] private Image mouthImage;

  private float hue;

  private void Awake()
  {
  }

  private void OnValidate()
  {
    bodyImage.sprite = bodySprite;
    hue = Mathf.InverseLerp(0, 360, (float)hueColor);
    bodyImage.color = Color.HSVToRGB(hue, 1, 1);

    eyesImage.sprite = eyesSprite;
    mouthImage.sprite = mouthSprite;
  }


}
