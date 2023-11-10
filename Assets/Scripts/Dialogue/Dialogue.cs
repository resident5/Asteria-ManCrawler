using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueCreator", order = 1)]
public class Dialogue : ScriptableObject
{
    public List<DialogueElement> dialogueList = new List<DialogueElement>();
    public DialogueActor[] actors;

    public void SetText()
    {
        for (int i = 0; i < dialogueList.Count; i++)
        {
            dialogueList[i].idLabel = name + "_" + i;
        }
    }

}
