using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FamilyTree : MonoBehaviour
{
  public CreatureSessionLog csl; //GetCreatureByID(id)
  public GameObject creatureInTreePrefab;
  public float HorizontalSpacing = 150;
  public float VerticalSpacing = 150;

  private List<int> creatureIDList; // Check drawn creatures

  private void Start()
  {
    if (csl.creatureLog.Count > 0)
    {
      DrawDiagram();
    }
    else
    {
      Debug.Log("Family tree is empty.");
    }
  }

  private void DrawDiagram()
  {
    creatureIDList = new List<int>();
    float x = 0;
    float y = 0;
    var highestGeneration = 0;

    // Find highest generation level creature
    for (int i = 0; i < csl.creatureLog.Count; i++)
    {
      if (csl.creatureLog[i].generation > highestGeneration)
      {
        highestGeneration = csl.creatureLog[i].generation;
      }
    }

    // Loop through creatureLog in generation order
    for (var i = 0; i < highestGeneration; i++)
    {
      for (int j = 0; j < csl.creatureLog.Count; j++)
      {
        if (csl.creatureLog[i].generation == i)
        {
          // if (!creatureIDs.Contains(csl.creatureLog[i].id))
          // {
            var creatureNode = Instantiate(creatureInTreePrefab);
            var creatureRect = creatureNode.GetComponent<RectTransform>();
            var creatureText = creatureNode.GetComponentInChildren<Text>();

            creatureText.text = csl.creatureLog[i].id;
            creatureRect.anchoredPosition = new Vector2(x, y);

            //creatureIDs.Add(csl.creatureLog[i].id);

            x += HorizontalSpacing;
          // }
        }
      }
      y -= VerticalSpacing;
    }
  }

  // private void DrawLine(RectTransform start, RectTransform end)
  // {
  //   var line = new GameObject("Line", typeof(Image));
  //   line.transform.SetParent(transform, false);
  //   line.GetComponent<Image>().color = Color.black;
  //   line.GetComponent<RectTransform>().anchoredPosition = start.anchoredPosition + (end.anchoredPosition - start.anchoredPosition) / 2;
  //   line.GetComponent<RectTransform>().sizeDelta = new Vector2(Vector2.Distance(start.anchoredPosition, end.anchoredPosition), 2f);
  //   line.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0.5f);
  //   line.GetComponent<RectTransform>().anchorMax = new Vector2(0, 0.5f);
  //   line.GetComponent<RectTransform>().pivot = new Vector2(0, 0.5f);
  //   line.GetComponent<RectTransform>().rotation = Quaternion.FromToRotation(Vector3.right, end.anchoredPosition - start.anchoredPosition);
  // }
}
