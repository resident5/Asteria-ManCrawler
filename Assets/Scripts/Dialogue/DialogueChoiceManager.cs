using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueChoiceManager : MonoBehaviour
{
    public GameObject choicePrefab;
    public List<Button> buttonChoices;

    public bool selectedText = false;

    public void AddChoices(List<Dialogue> selectChoices)
    {
        ClearButtonChoices();
        foreach (var choice in selectChoices)
        {
            GameObject choiceObj = Instantiate(choicePrefab);
            Button choiceButton = choiceObj.GetComponent<Button>();
            choiceButton.onClick.AddListener(() => choice.SetText());
        }
    }

    public void ClearButtonChoices()
    {
        foreach (var b in buttonChoices)
        {
            b.onClick.RemoveAllListeners();
        }
        buttonChoices.Clear();

    }
}
