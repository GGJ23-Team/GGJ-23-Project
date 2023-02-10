using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureNode : MonoBehaviour
{
    public string id;
    public List<string> parentsID;
    public List<string> childrenID;
    public List<string> partnersID;
    public int generation;

    public void SetNode(Creature creature)
    {
        id = creature.id;
        parentsID = creature.parentsID;
        childrenID = creature.childrenID;
        partnersID = creature.partnersID;
        generation = creature.generation;
    }
}
