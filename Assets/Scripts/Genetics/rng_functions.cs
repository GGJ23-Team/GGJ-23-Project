using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rng_functions : MonoBehaviour
{ 
    /** We asume that features are designed as a range of integers */

    /** Mutate means getting a different feature value than the parents */
    int mutateFeature(int targetFeature, int featureRange)
    {
      direction = Random.Range(0, 2)-1;
      ammount = Random.Range(0, 2);
      return targetFeature + (direction * ammount % featureRange);
    }

    /** Inherit the same feature value as one of the parents or mutate */
    int inheritFeature(int featureA, int featureB, int featureRange, int mutationProvability = 0)
    {
      provA = (100 - mutationProvability)/2;
      provB = 100 - mutationProvability;

      result = Random.Range(0, 100);
      if (result < provA)
      {
        return featureA;
      }
      else if (result < provB)
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
