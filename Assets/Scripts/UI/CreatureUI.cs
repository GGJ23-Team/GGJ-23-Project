using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureUI : MonoBehaviour
{
    [Header("Target Creature")]
    private CreatureSessionLog creatureSessionLog;
    private CreatureSpritePool creatureSpritePool;
    private Creature creature;
    [SerializeField] private string creatureID;

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
        creatureSessionLog = FindObjectOfType<CreatureSessionLog>();
        if (creatureSessionLog == null)
        {
            Debug.LogError("CreatureSessionLog not found.");
        }

        creatureSpritePool = FindObjectOfType<CreatureSpritePool>();
        if (creatureSpritePool == null)
        {
            Debug.LogError("CreatureSpritePool not found.");
        }
    }

    private void Start()
    {
        if (creatureID != null)
        {
            //UpdateCreatureData();
        }
    }

    private void OnValidate()
    {
        SetCreatureAspect();
    }

    private void UpdateCreatureData()
    {
        creature = creatureSessionLog.GetCreatureByID(creatureID);

        if (creature != null)
        {
            creatureID = creature.id;
            hueColor = creatureSpritePool.hueColor[creature.color];
            bodySprite = creatureSpritePool.bodySprites[creature.form];
            eyesSprite = creatureSpritePool.eyesSprites[creature.eye];
            mouthSprite = creatureSpritePool.mouthSprites[creature.mouth];
        }
        else
        {
            Debug.LogError("CreatureSessionLog not found.");
        }

        SetCreatureAspect();
    }

    private void SetCreatureAspect()
    {
        if (bodyImage != null)
        {
            bodyImage.sprite = bodySprite;
            hue = Mathf.InverseLerp(0, 360, (float)hueColor);
            bodyImage.color = Color.HSVToRGB(hue, 1, 1);
            eyesImage.sprite = eyesSprite;
            mouthImage.sprite = mouthSprite;
        }
    }

    public void SetCreatureID(string id)
    {
        creatureID = id;
        UpdateCreatureData();
    }


}
