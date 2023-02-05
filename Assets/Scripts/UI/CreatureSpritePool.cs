using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpritePool : MonoBehaviour
{
  [Header("Genetic treats")]
  
  [Range(0, 360)]
  public List<int> hueColor;
  public List<Sprite> bodySprites;
  public List<Sprite> eyesSprites;
  public List<Sprite> mouthSprites;
}
