using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour
{
    public Text[] texts;

    void Start()
    {
        int randomIndex = Random.Range(0, texts.Length);

        for (int i = 0; i < texts.Length; i++)
        {
            if (i == randomIndex)
            {
                texts[i].enabled = true;
            }
            else
            {
                texts[i].enabled = false;
            }
        }
    }
}
