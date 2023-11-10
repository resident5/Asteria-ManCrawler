using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class DialogueElement
{
    [HideInInspector]
    public string idLabel;

    public DialogueManager.DIALOGUEEVENT diagEvent;
    public List<Dialogue> choices = new List<Dialogue>();

    public bool seenText = false;
    public bool isActor;

    public string text;


    public DialogueElement(bool actor)
    {
        text = "";
        seenText = false;
        isActor = actor;
    }

    public bool FinishedText()
    {
        return true;
    }
}
