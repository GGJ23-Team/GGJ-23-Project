using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FamilyTree : MonoBehaviour
{
  public CreatureSessionLog csl; //GetCreatureByID(id)
  public GameObject creatureInTreePrefab;
  public GameObject PartnerLinePrefab;
  public GameObject PartnerBetaLinePrefab;
  public GameObject DescendantLinePrefab;
  public RectTransform treePanel;
  public float HorizontalSpacing = 150;
  public float VerticalSpacing = 150;

  private List<int> creatureIDList; // Check drawn creatures
  private List<RectTransform> nodeRectList; // Check drawn creatures


  private void Start()
  {
    if (csl.creatureLog.Count > 0)
    {
      nodeRectList = new List<RectTransform>();
      DrawDiagram();
    }
    else
    {
      Debug.Log("Family tree is empty.");
    }
  }

  private void DrawDiagram()

  public void UpdateDiagram()
  {
    Debug.Log("Updating diagram...");
    foreach (Transform child in treePanel)
    {
      Destroy(child.gameObject);
    }
    DrawDiagram();
  }


  public void DrawDiagram()
  {
    creatureIDList = new List<int>();
    Debug.Log("Drawing diagram...");
    nodeRectList.Clear();
    float x = 0;
    float y = 0;
    var highestGeneration = 0;
    int highestGeneration = 0;

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
    InstantiateCreatureNodes(highestGeneration);

    // ArrangeCreatureNodes(highestGeneration);

    // DrawJoints();

    //Draw lines de descendente a padres
      for (int i = 0; i < nodeRectList.Count; i++) {
        RectTransform creatureRect = nodeRectList[i];
        Creature creature = csl.GetCreatureByID(creatureRect.GetComponentInChildren<Text>().text);

        if(creature.generation > 0) {
          List<RectTransform> parentRects = new List<RectTransform>();
          
          foreach (RectTransform node in nodeRectList)
          { 
            if( node.gameObject.GetComponentInChildren<Text>().text == creature.parentsID[0]
              || node.gameObject.GetComponentInChildren<Text>().text == creature.parentsID[1])
            {
              parentRects.Add(node);
            }
          }

            creatureText.text = csl.creatureLog[i].id;
            creatureRect.anchoredPosition = new Vector2(x, y);
          if (parentRects.Count > 0) {
            DrawLine(creatureRect, parentRects);
          }
          if (parentRects.Count > 1) {
            DrawLine(parentRects[0], new List<RectTransform>() { parentRects[1] });
          }
        }
      }
  }


  private void InstantiateCreatureNodes(int maxGen) {
    float x = 0;
    float y = 0;

            //creatureIDs.Add(csl.creatureLog[i].id);
    for (int i = 0; i < (maxGen + 1); i++) {
      
      for (int j = 0; j < csl.creatureLog.Count; j++) {
        if (csl.creatureLog[j].generation == i) {

            x += HorizontalSpacing;
          // }
            GameObject creatureNode = Instantiate(creatureInTreePrefab,treePanel);
            CreatureNode creatureData = creatureNode.GetComponent<CreatureNode>();
            Text creatureText = creatureNode.GetComponentInChildren<Text>();
            RectTransform creatureRect = creatureNode.GetComponent<RectTransform>();
            
            nodeRectList.Add(creatureRect);
            creatureData.SetNode(csl.creatureLog[j]);
            creatureText.text = csl.creatureLog[j].id;
            creatureRect.anchoredPosition = new Vector2( x, y );

            x = x + HorizontalSpacing;
        }
      }
      x = 0;
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
  // private void DrawJoints(){}

  private void DrawLine(RectTransform start, List<RectTransform> end)
  {
    if(end.Count > 1){ // Descendant Line
    var joint = Instantiate(DescendantLinePrefab, treePanel, false);
    joint.GetComponent<DescendantJoint>().SetLine(start.anchoredPosition, end[0].anchoredPosition, end[1].anchoredPosition);

    } else if (end.Count == 1) { // Partner Line
      if( start.anchoredPosition.y != end[0].anchoredPosition.y ) {
        var joint = Instantiate(PartnerBetaLinePrefab, treePanel, false);
        joint.GetComponent<PartnerBetaJoint>().SetLine(start.anchoredPosition, end[0].anchoredPosition);
      } else {
        var joint = Instantiate(PartnerLinePrefab, treePanel, false);
        joint.GetComponent<PartnerJoint>().SetLine(start.anchoredPosition, end[0].anchoredPosition);
      }

    } else { // Error
      Debug.LogError("DrawLine Error: Missing end node.");
    }
  }
}
