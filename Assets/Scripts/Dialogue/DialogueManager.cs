using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<DialogueManager>();
            }
            return instance;
        }
    }

    public enum DIALOGUEEVENT
    {
        TEXT,
        IMAGE,
        CG,
        BATTLE,
        CHOICE
    };
    public enum ACTOR
    {
        PLAYER,
        NONPLAYER,
        NARRATOR
    };

    public Dialogue currentDialogue;
    public DialogueUI dialogueUI;
    public DialogueChoiceManager choiceManager;

    private void Start()
    {
        StartCoroutine(StartDialogue());
    }

    IEnumerator StartDialogue()
    {
        if (currentDialogue != null)
        {
            var diagList = currentDialogue.dialogueList;
            var diagActors = currentDialogue.actors[0];

            foreach (var diag in diagList)
            {
                bool done = false;

                switch (diag.diagEvent)
                {
                    case DIALOGUEEVENT.TEXT:
                        if (!diag.isActor)
                        {
                            dialogueUI.ChangeDialogueBox(dialogueUI.narration);
                        }
                        else
                        {
                            dialogueUI.ChangeDialogueBox(dialogueUI.left);
                        }
                        dialogueUI.ChangeText(diag.text);
                        if (dialogueUI.isTypingText)
                        {
                            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && dialogueUI.TextCanContinue());
                            yield return null;
                        }
                        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && dialogueUI.TextCanContinue());


                        //yield return null;
                        //yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && dialogueUI.TextCanContinue());

                        break;
                    case DIALOGUEEVENT.IMAGE:
                        break;
                    case DIALOGUEEVENT.CG:
                        break;
                    case DIALOGUEEVENT.BATTLE:
                        break;
                    case DIALOGUEEVENT.CHOICE:
                        if (!diag.isActor)
                        {
                            dialogueUI.ChangeDialogueBox(dialogueUI.narration);
                        }
                        else
                        {
                            dialogueUI.ChangeDialogueBox(dialogueUI.left);
                        }
                        dialogueUI.ChangeText(diag.text);

                        yield return new WaitUntil(() => dialogueUI.TextCanContinue());

                        break;
                    default:
                        break;
                }
                //if (diag.diagEvent == DIALOGUEEVENT.TEXT)
                //{
                //    if (!diag.isActor)
                //    {
                //        dialogueUI.ChangeDialogueBox(dialogueUI.narration);
                //    }
                //    else
                //    {
                //        dialogueUI.ChangeDialogueBox(dialogueUI.left);
                //    }

                //    dialogueUI.ChangeText(diag.text);
                //    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
                //}

                yield return null;
            }
        }


    }

}
