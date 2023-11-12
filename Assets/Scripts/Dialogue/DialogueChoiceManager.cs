using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueChoiceManager : MonoBehaviour
{
    public GameObject choicePrefab;
    public List<Button> buttonChoices;

    public bool selectedText = false;

    public void AddChoices(List<DialogueObject> selectChoices)
    {
        ClearButtonChoices();
        foreach (var choice in selectChoices)
        {
            GameObject choiceObj = Instantiate(choicePrefab);
            Button choiceButton = choiceObj.GetComponent<Button>();
            choiceButton.onClick.AddListener(() => choice.SetText());
        }
    }

    public void AddBranches(DialogueBranchObject branch)
    {
        gameObject.SetActive(true);
        foreach(var diag in branch.dialogues)
        {
            GameObject choiceObj = Instantiate(choicePrefab, gameObject.transform);
            Button choiceButton = choiceObj.GetComponent<Button>();
            DialogueChoice choser = choiceObj.GetComponent<DialogueChoice>();
            choser.choiceObject = diag;
            choiceButton.onClick.AddListener(() => gameObject.SetActive(false));
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
