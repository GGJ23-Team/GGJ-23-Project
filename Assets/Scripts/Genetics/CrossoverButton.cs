using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** TODO: 
  - Adapt to the drag&drop system for getting the parent IDs
  - Condition for mutate if crossover parent with child
*/

public class CrossoverButton : MonoBehaviour
{
    public List<Creature> creatureSessionLog; // Shoud be a singleton
    public GameObject creaturePrefab;

    public RectTransform parentAContainer;
    public RectTransform parentBContainer;
    public RectTransform createdCreatureContainer;
    
    // Temporal Dirty Debug - - - -
    public string leftParentID;
    public string rightParentID;
    // - - - - - - - - - - - - - -
    private Creature parentA;
    private Creature parentB;


    public void PerformCrossover()
    {
        leftParentID = parentAContainer.GetChild(0).GetComponent<Creature>().id;
        rightParentID = parentBContainer.GetChild(0).GetComponent<Creature>().id;

        parentA = creatureSessionLog.Find(x => x.id == leftParentID);
        parentA = creatureSessionLog.Find(x => x.id == leftParentID);

        int childColor = InheritFeature(parentA.color, parentB.color, 3);
        int childForm = InheritFeature(parentA.form, parentB.form, 3);
        int childEyes = InheritFeature(parentA.eye, parentB.eye, 3);
        int childMouth = InheritFeature(parentA.mouth, parentB.mouth, 3);
      
        Creature childCreature = new Creature( 
                                      new List<string> { parentA.id, parentB.id },
                                      childColor, 
                                      childForm,
                                      childEyes,
                                      childMouth);

        creatureSessionLog.Add(childCreature);

        GameObject newCreature = Instantiate(creaturePrefab, transform.position, Quaternion.identity);
    }

    /** Mutate: get a feature neart to one of the parents' */
    public int MutateFeature(int targetFeature, int featureRange)
    {
      int direction = Random.Range(0, 2)-1;
      int ammount = Random.Range(0, 2);
      return targetFeature + (direction * ammount % featureRange);
    }

    /** Inherit the same feature value as one of the parents or mutate */
    public int InheritFeature(int featureA, int featureB, int featureRange, int mutationProbability = 0)
    {
      int probA = (100 - mutationProbability)/2;
      int probB = 100 - mutationProbability;

      int result = Random.Range(0, 100);
      if (result < probA)
      {
        return featureA;
      }
      else if (result < probB)
      {
        return featureB;
      }
      else
      {
        int targetFeature = Random.Range(0, 2) == 0 ? featureA : featureB;
        return MutateFeature(targetFeature, featureRange);
      }
    }

}
