using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSessionLog : MonoBehaviour
{
    private List<Creature> creatureLog;

    private void Awake()
    {
        creatureLog = new List<Creature>();
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
