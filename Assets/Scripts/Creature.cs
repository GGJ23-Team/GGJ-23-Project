using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public string id;
    // public string name;
    public List<string> parentsID;
    public List<string> childrenID;

    int color;
    int form;
    int eye;
    int mouth;

    public Creature(string creatureId,
                    List<string> creatureParentsID,
                    int creatureColor=-1,
                    int creatureForm=-1,
                    int creatureEye=-1,
                    int creatureMouth=-1)
    {
        id = creatureId;
        // name = creatureName;
        parentsID = creatureParentsID;
        childrenID = new List<string>();

        if(creatureColor == -1){
            color = Random.Range(0, 4);
        } else {
            color = creatureColor;
        }

        if(creatureForm == -1){
            form = Random.Range(0, 4);
        } else {
            form = creatureForm;
        }

        if(creatureEye == -1){
            eye = Random.Range(0, 4);
        } else {
            eye = creatureEye;
        }

        if(creatureMouth == -1){
            mouth = Random.Range(0, 4);
        }else {
            mouth = creatureMouth;
        }

        Debug.Log("New Creature: " + name + " created.");
    }

    public void AddChild(Creature child)
    {
        childrenID.Add(child.id);
        Debug.Log("Child: " + child.name + " added to " + name);
    }
}


