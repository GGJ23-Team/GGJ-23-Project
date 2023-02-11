using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureUI : MonoBehaviour
{
    [Header("Target Creature")]
    [HideInInspector] public CreatureSpritePool creatureSpritePool;
    private Creature creature;
    [SerializeField] private string creatureID;

    [Header("Attributes")]
    [SerializeField] private int bodySprite;
    [SerializeField] private int hueColor;
    [SerializeField] private int eyesSprite;
    [SerializeField] private int mouthSprite;

    [Header("Linked gameobjects")]
    [SerializeField] private Image bodyImage;
    [SerializeField] private Image eyesImage;
    [SerializeField] private Image mouthImage;

    private float hue;

    private void Awake()
    {
        creatureSpritePool = FindObjectOfType<CreatureSpritePool>();
        if (creatureSpritePool == null)
        {
            Debug.LogError("CreatureSpritePool not found.");
        }

        //Comonents in children of this GO prefab
        bodyImage = transform.GetChild(0).GetComponent<Image>();
        eyesImage = transform.GetChild(1).GetComponent<Image>();
        mouthImage = transform.GetChild(2).GetComponent<Image>();
        creature = null;
    }

    private void OnValidate()
    {
        //SetCreatureAspect();
    }

    private void UpdateCreatureData(bool force = true) //for goal creature
    {
        if (force) creature = CreatureSessionLog.Instance.GetCreatureByID(creatureID);

        Debug.Log("Creature: " + creature);
        creatureID = creature.id;
        hueColor = creature.color;
        bodySprite = creature.form;
        eyesSprite = creature.eye;
        mouthSprite = creature.mouth;

        SetCreatureAspect();
    }

    private void SetCreatureAspect()
    {
        if (bodyImage != null)
        {
            bodyImage.sprite = creatureSpritePool.bodySprites[bodySprite];
            bodyImage.color = creatureSpritePool.color[hueColor];
            eyesImage.sprite = creatureSpritePool.eyesSprites[eyesSprite];
            mouthImage.sprite = creatureSpritePool.mouthSprites[mouthSprite];
        }
    }

    public void SetCreatureID(string id)
    {
        this.creatureID = id;
        UpdateCreatureData();
    }

    public void SetCreature(Creature c, bool force = true){
        creature = c;
        UpdateCreatureData(force);
    }

    public string GetCreatureID()
    {
        return creatureID;
    }

    public Creature GetCreature() {
        return creature;
    }

    public int[] GetGenetics(){
        return new int[] {bodySprite, hueColor, eyesSprite, mouthSprite};
    }


}
