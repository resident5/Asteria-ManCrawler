using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class DialogueElement
{
    [HideInInspector]
    public string idLabel;

    public List<DialogueManager.DIALOGUEEVENT> diagEvents = new List<DialogueManager.DIALOGUEEVENT>();
    public string[] empahsisWords;

    public DialogueBranchObject branch;
    public List<int> variables;

    public bool seenText = false;
    public bool isActor;
    public int actorIndex = 0;

    public string text;

    public DialogueElement()
    {
        text = "";
        seenText = false;
        variables = new List<int>();
        diagEvents.Add(DialogueManager.DIALOGUEEVENT.TEXT);
        
    }

    public DialogueElement(bool actor)
    {
        text = "";
        seenText = false;
        isActor = actor;
        variables = new List<int>();
        diagEvents.Add(DialogueManager.DIALOGUEEVENT.TEXT);
    }
}
