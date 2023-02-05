using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatureSessionLog : MonoBehaviour
{
    private List<Creature> creatureLog;
    [SerializeField] private int creaturePoolAmount;
    private List<int> randomIDList = new List<int>();
    [SerializeField] private GameObject parentGO;
    [SerializeField] private GameObject prefabCreatureInSlot;
    [SerializeField] private GameObject slotsPositionGO;
    private List<Transform> slotsPositionList = new List<Transform>();

    private void Awake()
    {
        creatureLog = new List<Creature>();
        CreateRandomCreatureLog();
        slotsPositionList.AddRange(slotsPositionGO.GetComponentsInChildren<Transform>().Where(t => t != slotsPositionGO.transform));
        Debug.Log("slotsPositionList.Count: " + slotsPositionList.Count);
        //GetChildTransforms();
        InstantiateCreatureLog();

        //PlaceCreaturesInPosition();
    }

    private void CreateRandomCreatureLog()
    {
        for (int i = 0; i < creaturePoolAmount; i++)
        {
            //int randomID = Random.Range(0, 100);
            //while (randomIDList.Contains(randomID))
            //{
            //    randomID = Random.Range(0, 100);
            //}
            //randomIDList.Add(randomID);
            Creature creature = new Creature(new List<string> { "", "" });
            creatureLog.Add(creature);
        }
        foreach (Creature creatureInLog in creatureLog)
        {
            Debug.Log(creatureInLog.id);
            Debug.Log(creatureInLog.eye);
        }
    }
    private void InstantiateCreatureLog()
    {
        if(creatureLog.Count == 0 || creatureLog == null)
        {
            Debug.Log("creature log is empty!");
            return;
        }

        if(!GetSlotsTransforms())
            return;

        for (int i = 0; i < creatureLog.Count; i++)
        {
            Debug.Log("i: " + i);
            Debug.Log(i + ": " + slotsPositionList[i].position);
            GameObject creatureGO = Instantiate(prefabCreatureInSlot, parentGO.transform);
            creatureGO.transform.position = slotsPositionList[i].position;
            creatureGO.transform.localScale = new Vector3(200, 200);
        }
    }

    private void GetChildTransforms()
    {
        if (slotsPositionList.Count == 0 || slotsPositionList == null)
        {
            Debug.Log("slotsPositionList is empty!");
            return;
        }

        foreach (Transform slotPosition in slotsPositionList)
        {
            //slotsPositionList.Add(slotPosition);
            Debug.Log("slotPosition.name: " + slotPosition.name);
            Debug.Log("slotPosition.position: " + slotPosition.position);
        }
    }

    private bool GetSlotsTransforms()
    {
        if (slotsPositionList.Count == 0 || slotsPositionList == null)
        {
            Debug.Log("slotsPositionList is empty!");
            return false;
        }

        foreach (Transform creatureTransform in slotsPositionList)
        {
            Debug.Log("creatureTransform: " + creatureTransform.position);
        }
        return true;
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
