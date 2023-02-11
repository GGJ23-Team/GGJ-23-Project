using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** TODO: 
////  - Adapt to the drag&drop system for getting the parent IDs
  - Condition for mutate if crossover parent with child
*/

public class CrossoverButton : MonoBehaviour
{
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

    void Start() {
      parentLeft = new Creature(new List<string>() {});
      parentRight = new Creature(new List<string>() {});
    }

    public void PerformCrossover()
    { 
        parentLeft = parentLeftContainer.transform.GetChild(0).GetComponent<CreatureUI>().GetCreature();
        parentRight = parentRightContainer.transform.GetChild(0).GetComponent<CreatureUI>().GetCreature();

        if (parentLeft.id == null || parentRight.id == null) {
          Debug.Log("No parents selected");
          return;
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

        CreatureSessionLog.Instance.creatureLog.Add(childCreature);

        // TODO: Add the child to the tree : UnityEngine.Object.get_name () (at /Users/bokken/build/output/unity/unity/Runtime/Export/Scripting/UnityEngineObject.bindings.cs:194)
        // parentLeft.AddChild(childCreature);
        // parentRight.AddChild(childCreature);

        // Will use for the tree-diagram
        // parentLeft.AddPartner(parentRight);
        // parentRight.AddPartner(parentLeft);

        creaturePrefab.GetComponent<CreatureUI>().SetCreatureID(childCreature.id);

        Sprite desiredBody, desiredEye, desiredMouth;
        Color desiredColor;
        GetDesiredFeatures(childCreature, out desiredBody, out desiredEye, out desiredMouth, out desiredColor);

        InstantiateCreature(desiredBody, desiredEye, desiredMouth, desiredColor);

        CheckIfWin();
    }

    private void InstantiateCreature(Sprite desiredBody, Sprite desiredEye, Sprite desiredMouth, Color desiredColor)
    {
        creaturePrefab = CreatureSessionLog.Instance.BuildDefinitiveCreature(desiredBody, desiredEye, desiredMouth, desiredColor);
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
        // TODO: get the creature from the creatureUI or from the creatureSessionLog by ID
        int[] features = createdCreature.transform.GetChild(0).GetComponent<CreatureUI>().GetGenetics();
        Creature target = CreatureSessionLog.Instance.goalCreature;
        
        if (features[1] == target.color
            && features[2] == target.eye
            && features[0] == target.form
            && features[3] == target.mouth)
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
