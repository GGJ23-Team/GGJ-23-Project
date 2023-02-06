using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CreatureSessionLog : MonoBehaviour
{
    [SerializeField] private int creaturePoolAmount;
    [SerializeField] private GameObject parentGO;
    [SerializeField] private GameObject prefabCreatureInSlot;
    [SerializeField] private GameObject computerPanel;
    [SerializeField] private GameObject creatureSpritePool;
    public List<Creature> creatureLog;


    private void Awake()
    {
        creatureLog = new List<Creature>();
        CreateRandomCreatureLog();
        InstantiateCreatureLog();
    }

    private void CreateRandomCreatureLog()
    {
        for (int i = 0; i < creaturePoolAmount; i++)
        {
            var firstCreature = new Creature(new List<string> { "", "" });
            creatureLog.Add(firstCreature);
        }
    }

    private void InstantiateCreatureLog()
    {
        if (creatureLog.Count == 0 || creatureLog == null)
        {
            Debug.Log("creature log is empty!");
            return;
        }

        string tag = "Slot";
        Transform[] childrenWithTag = computerPanel.GetComponentsInChildren<Transform>(includeInactive: false)
            .Where(t => t.CompareTag(tag))
            .ToArray();

        for (int i = 0; i < creatureLog.Count; i++)
        {
            GameObject randomCreature = BuildCreatureGO(i);
            randomCreature.GetComponent<Creature>().id = creatureLog[i].id;
            randomCreature.GetComponent<Creature>().eye = creatureLog[i].eye;
            randomCreature.GetComponent<Creature>().mouth = creatureLog[i].mouth;
            randomCreature.GetComponent<Creature>().color = creatureLog[i].color;
            randomCreature.GetComponent<Creature>().form = creatureLog[i].form;
            GameObject creatureGO = Instantiate(randomCreature, childrenWithTag[i].transform);
        }
    }

    private GameObject BuildCreatureGO(int index)
    {
        var desiredBody = creatureSpritePool.GetComponent<CreatureSpritePool>().bodySprites[creatureLog[index].form];
        var desiredEye = creatureSpritePool.GetComponent<CreatureSpritePool>().eyesSprites[creatureLog[index].eye];
        var desiredMouth = creatureSpritePool.GetComponent<CreatureSpritePool>().mouthSprites[creatureLog[index].mouth];
        var desiredColor = creatureSpritePool.GetComponent<CreatureSpritePool>().color[creatureLog[index].color];
        return BuildDefinitiveCreature(desiredBody, desiredEye, desiredMouth, desiredColor);
    }

    public GameObject BuildDefinitiveCreature(Sprite desiredBody, Sprite desiredEye, Sprite desiredMouth, Color desiredColor)
    {
        prefabCreatureInSlot.transform.GetChild(0).GetComponent<Image>().sprite = desiredBody;
        prefabCreatureInSlot.transform.GetChild(0).GetComponent<Image>().color = desiredColor;
        prefabCreatureInSlot.transform.GetChild(1).GetComponent<Image>().sprite = desiredEye;
        prefabCreatureInSlot.transform.GetChild(2).GetComponent<Image>().sprite = desiredMouth;
        return prefabCreatureInSlot;
    }

    public void AddCreature(Creature creature)
    {
        creatureLog.Add(creature);
    }

    public void RemoveCreature(Creature creature)
    {
        bool found = false;
        foreach (Creature creatureInLog in creatureLog)
        {
            if (creatureInLog.id == creature.id)
            {
                creatureLog.Remove(creatureInLog);
            }
        }
        if (!found)
        {
            Debug.LogError("Creature: " + creature.name + " not found in log.");
        }
    }

    public Creature GetCreatureByID(string id)
    {
        foreach (Creature creature in creatureLog)
        {
            if (creature.id == id)
            {
                return creature;
            }
        }
        return null;
    }
}
