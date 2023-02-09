using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public CreatureSessionLog creatureLog;
    public string id;
    // public string name;
    public List<string> parentsID;
    public List<string> childrenID;
    public int generation;

    public int color;
    public int form;
    public int eye;
    public int mouth;

    public Creature(List<string> creatureParentsID, int creatureColor = -1, int creatureForm = -1, int creatureEye = -1, int creatureMouth = -1)
    {
        id = System.Guid.NewGuid().ToString();
        parentsID = creatureParentsID;
        childrenID = new List<string>();

        if(creatureParentsID == null || creatureParentsID.Count == 0)
        {
            generation = 0;
        } 
        else
        {   
            var parentsGen = new int[2];
            for (var i = 0; i < 2; i++)
            {
                parentsGen[i] = creatureLog.GetCreatureByID(creatureParentsID[i]).generation;   
            }
            generation = Mathf.Max(parentsGen) + 1;
        }

        var values = new int[] { creatureColor, creatureForm, creatureEye, creatureMouth };
        var ranges = new int[] { 4, 4, 4, 4 };

        for (int i = 0; i < values.Length; i++)
        {
            if (values[i] == -1)
            {
                values[i] = Random.Range(0, ranges[i]);
            }
        }

        color = values[0];
        form = values[1];
        eye = values[2];
        mouth = values[3];
    }


    public void AddChild(Creature child)
    {
        childrenID.Add(child.id);
        Debug.Log("Child: " + child.name + " added to " + name);
    }
}


