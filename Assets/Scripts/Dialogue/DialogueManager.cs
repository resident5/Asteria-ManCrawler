using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
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
    #endregion

    #region Dialogue Event Handler
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
    #endregion

    public DialogueObject currentDialogue;
    public DialogueUI dialogueUI;
    public DialogueChoiceManager choiceManager;
    public List<int> variables;

    public DialogueBranchObject currentChoices;

    private void Start()
    {
        PlayDialogue();
    }

    public void PlayDialogue()
    {
        StopAllCoroutines();
        StartCoroutine(StartDialogue());
    }

    public void ChangeDialogue(DialogueObject nextDialogue)
    {
        currentDialogue = nextDialogue;
    }

    IEnumerator StartDialogue()
    {
        if (currentDialogue != null)
        {
            var diagList = currentDialogue.dialogueList;
            var diagActors = currentDialogue.actors;

            foreach (var diag in diagList)
            {
                bool done = false;

                foreach (var e in diag.diagEvents)
                {
                    switch (e)
                    {
                        case DIALOGUEEVENT.TEXT:
                            //Either hide the actor index or null ref check
                            dialogueUI.ChangeDialogueBox(diag.isActor, diagActors[diag.actorIndex]);
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
                            currentChoices = diag.branch;
                            break;
                        default:
                            break;
                    }
                }



                yield return null;
            }

            if(currentChoices)
            {
                choiceManager.AddBranches(currentChoices);
                currentChoices = null;

            }
            Debug.Log("End of Dialogue");
        }


    }

}
