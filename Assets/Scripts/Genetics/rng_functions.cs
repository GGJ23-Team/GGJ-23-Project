using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RNG_functions : MonoBehaviour
{ 
    /** We asume that features are designed as a range of integers */

    /** Mutate means getting a different feature value than the parents */
    int MutateFeature(int targetFeature, int featureRange)
    {
      int direction = Random.Range(0, 2)-1;
      int ammount = Random.Range(0, 2);
      return targetFeature + (direction * ammount % featureRange);
    }

    /** Inherit the same feature value as one of the parents or mutate */
    int InheritFeature(int featureA, int featureB, int featureRange, int mutationProbability = 0)
    {
      int probA = (100 - mutationProbability)/2;
      int probB = 100 - mutationProbability;

      result = Random.Range(0, 100);
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
        targetFeature = Random.Range(0, 2) == 0 ? featureA : featureB;
        return mutateFeature(targetFeature, featureRange);
      }
    }
}
