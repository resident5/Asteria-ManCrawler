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
    public DialogueBranchObject currentChoices;
    public List<string> emphasisWordList = new List<string>();
    int index = 0;


    private void Start()
    {
        dialogueUI.gameObject.SetActive(false);
        //PlayDialogue();
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

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!string.IsNullOrEmpty(dialogueUI.emphasisInputField.text) && emphasisWordList.Contains(dialogueUI.emphasisInputField.text))
            {
                //Debug.Log($"Is {emphasisWordList[0]} the same as {dialogueUI.emphasisInputField.text}?");
                ChangeDialogue(dialogueUI.SubmitEmphasis(dialogueUI.emphasisInputField.text));
                PlayDialogue();
            }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            dialogueUI.emphasisInputField.Select();
        }
    }

    IEnumerator StartDialogue()
    {
        if (currentDialogue != null)
        {
            var diagList = currentDialogue.dialogueList;
            var diagActors = currentDialogue.actors;

            while(index != diagList.Count)
            {
                bool done = false;

                diagList[index].seenText = true;
                emphasisWordList.Clear();

                if (diagList[index].empahsisWords != null)
                {
                    emphasisWordList.AddRange(diagList[index].empahsisWords);
                }

                foreach (var e in diagList[index].diagEvents)
                {
                    switch (e)
                    {
                        case DIALOGUEEVENT.TEXT:
                            //Either hide the actor index or null ref check
                            dialogueUI.ChangeDialogueBox(diagList[index].isActor, diagActors[diagList[index].actorIndex]);
                            dialogueUI.ChangeText(diagList[index].text);
                            yield return new WaitUntil(() => !dialogueUI.emphasisInputField.isFocused && Input.GetKeyDown(KeyCode.Space) && dialogueUI.SkipOrContinueText());
                            break;
                        case DIALOGUEEVENT.IMAGE:
                            break;
                        case DIALOGUEEVENT.CG:
                            break;
                        case DIALOGUEEVENT.BATTLE:
                            break;
                        case DIALOGUEEVENT.CHOICE:
                            currentChoices = diagList[index].branch;
                            break;
                        default:
                            break;
                    }
                }
                index++;

                yield return null;
            }
            Debug.Log("End of Dialogue");

            //foreach (var diag in diagList)
            //{
            //    bool done = false;

            //    diag.seenText = true;
            //    emphasisWordList.Clear();

            //    if (diag.empahsisWords != null)
            //    {
            //        emphasisWordList.AddRange(diag.empahsisWords);
            //    }

            //    foreach (var e in diag.diagEvents)
            //    {
            //        switch (e)
            //        {
            //            case DIALOGUEEVENT.TEXT:
            //                //Either hide the actor index or null ref check
            //                dialogueUI.ChangeDialogueBox(diag.isActor, diagActors[diag.actorIndex]);
            //                dialogueUI.ChangeText(diag.text);
            //                yield return new WaitUntil(() => !dialogueUI.emphasisInputField.isFocused && Input.GetKeyDown(KeyCode.Space) && dialogueUI.SkipOrContinueText());
            //                break;
            //            case DIALOGUEEVENT.IMAGE:
            //                break;
            //            case DIALOGUEEVENT.CG:
            //                break;
            //            case DIALOGUEEVENT.BATTLE:
            //                break;
            //            case DIALOGUEEVENT.CHOICE:
            //                currentChoices = diag.branch;
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //    Debug.Log("End of Dialogue");

            //    yield return null;
            //}

            if (currentChoices)
            {
                choiceManager.AddBranches(currentChoices);
                currentChoices = null;

            }
        }


    }

}
