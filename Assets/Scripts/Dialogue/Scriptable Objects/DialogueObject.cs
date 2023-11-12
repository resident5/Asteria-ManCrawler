using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;


[CreateAssetMenu(fileName = "Dialogue", menuName = "Create Dialogue", order = 1)]
public class DialogueObject : ScriptableObject
{
    [SerializeField]
    public List<DialogueElement> dialogueList = new List<DialogueElement>();
    public DialogueActorObject[] actors;
    public DialogueBranchObject nextDialogue;

    public void SetText()
    {
        for (int i = 0; i < dialogueList.Count; i++)
        {
            dialogueList[i].idLabel = name + "_" + i;
        }
    }

}
