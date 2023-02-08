using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** TODO: 
  - Adapt to the drag&drop system for getting the parent IDs
  - Condition for mutate if crossover parent with child
*/

public class CrossoverButton : MonoBehaviour
{
    public List<Creature> creatureList; // Shoud be a singleton
    public CreatureSessionLog creatureSessionLog; // Shoud be a singleton
    public GameObject creaturePrefab;

    [SerializeField] private GameObject parentLeftContainer;
    [SerializeField] private GameObject parentRightContainer;
    [SerializeField] private GameObject createdCreature;
    [SerializeField] private GameObject desiredCreature;
    [SerializeField] private GameObject parentGO;
    [SerializeField] private GameObject creatureSpritePool;

    // - - - - - - - - - - - - - -
    private Creature parentLeft;
    private Creature parentRight;

    public void PerformCrossover()
    {
        var leftParentID = parentLeftContainer.transform.GetChild(0).GetComponent<Creature>().id;
        var rightParentID = parentRightContainer.transform.GetChild(0).GetComponent<Creature>().id;

        parentLeft = new Creature(new List<string>() { "", "" });
        parentRight = new Creature(new List<string>() { "", "" });
        for (int i = 0; i < creatureSessionLog.creatureLog.Count; i++)
        {
            if (creatureSessionLog.creatureLog[i].id == leftParentID)
            {
                parentLeft = creatureSessionLog.creatureLog[i];
            }
            if (creatureSessionLog.creatureLog[i].id == rightParentID)
            {
                parentRight = creatureSessionLog.creatureLog[i];
            }
        }

        int childColor = InheritFeature(parentLeft.color, parentRight.color, 3);
        int childForm = InheritFeature(parentLeft.form, parentRight.form, 3);
        int childEyes = InheritFeature(parentLeft.eye, parentRight.eye, 3);
        int childMouth = InheritFeature(parentLeft.mouth, parentRight.mouth, 3);

        Creature childCreature = new Creature(
                                      new List<string> { parentLeft.id, parentRight.id },
                                      childColor,
                                      childForm,
                                      childEyes,
                                      childMouth);

        creatureSessionLog.creatureLog.Add(childCreature);

        creaturePrefab.GetComponent<Creature>().id = childCreature.id;
        creaturePrefab.GetComponent<Creature>().form = childCreature.form;
        creaturePrefab.GetComponent<Creature>().color = childCreature.color;
        creaturePrefab.GetComponent<Creature>().eye = childCreature.eye;
        creaturePrefab.GetComponent<Creature>().mouth = childCreature.mouth;

        Sprite desiredBody, desiredEye, desiredMouth;
        Color desiredColor;
        GetDesiredFeatures(childCreature, out desiredBody, out desiredEye, out desiredMouth, out desiredColor);

        InstantiateCreature(desiredBody, desiredEye, desiredMouth, desiredColor);

        CheckIfWin();
    }

    private void InstantiateCreature(Sprite desiredBody, Sprite desiredEye, Sprite desiredMouth, Color desiredColor)
    {
        creaturePrefab = creatureSessionLog.BuildDefinitiveCreature(desiredBody, desiredEye, desiredMouth, desiredColor);
        Instantiate(creaturePrefab, createdCreature.transform);
        creaturePrefab.transform.localScale = new Vector3(100, 100);
    }

    private void GetDesiredFeatures(Creature childCreature, out Sprite desiredBody, out Sprite desiredEye, out Sprite desiredMouth, out Color desiredColor)
    {
        desiredBody = creatureSpritePool.GetComponent<CreatureSpritePool>().bodySprites[childCreature.form];
        desiredEye = creatureSpritePool.GetComponent<CreatureSpritePool>().eyesSprites[childCreature.eye];
        desiredMouth = creatureSpritePool.GetComponent<CreatureSpritePool>().mouthSprites[childCreature.mouth];
        desiredColor = creatureSpritePool.GetComponent<CreatureSpritePool>().color[childCreature.color];
    }

    private void CheckIfWin()
    {
        var target = desiredCreature.transform.GetChild(0).GetComponent<Creature>();

        if (createdCreature.transform.GetChild(0).GetComponent<Creature>().color == target.color
            && createdCreature.transform.GetChild(0).GetComponent<Creature>().eye == target.eye
            && createdCreature.transform.GetChild(0).GetComponent<Creature>().form == target.form
            && createdCreature.transform.GetChild(0).GetComponent<Creature>().mouth == target.mouth)
        {
            StartCoroutine(WaitForSceneLoad());
        }
    }
    private IEnumerator WaitForSceneLoad()
    {
        yield return new WaitForSeconds(1.0f);
        new LoadScene().LoadSceneByPath("Assets/Scenes/You Win.unity");
    }
    /** Mutate: get a feature neart to one of the parents' */
    public int MutateFeature(int targetFeature, int featureRange)
    {
        int direction = Random.Range(0, 2) - 1;
        int ammount = Random.Range(0, 2);
        return targetFeature + (direction * ammount % featureRange);
    }

    /** Inherit the same feature value as one of the parents or mutate */
    public int InheritFeature(int featureA, int featureB, int featureRange, int mutationProbability = 0)
    {
        int probA = (100 - mutationProbability) / 2;
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
