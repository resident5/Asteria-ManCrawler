using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    #region Singleton
    private static DialogueManager instance;
    public static DialogueManager Instance { get { return instance; } }
    #endregion

    #region Dialogue Event Handler
    public enum DIALOGUEEVENT
    {
        TEXT,
        IMAGE,
        CG,
        BATTLE,
        CHOICE,
        END
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

    public GameObject restartScreen;
    public GameObject pauseScreen;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        restartScreen.SetActive(false);
        pauseScreen.SetActive(false);
        dialogueUI.left.gameObject.SetActive(false);
        dialogueUI.narration.gameObject.SetActive(false);
        dialogueUI.narration.transform.parent.gameObject.SetActive(false);
        //PlayDialogue();
    }

    public void PlayDialogue()
    {
        dialogueUI.narration.transform.parent.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(StartDialogue());
    }

    public void PlayDialogue(DialogueObject nextDialogue)
    {
        dialogueUI.narration.transform.parent.gameObject.SetActive(true);
        currentDialogue = nextDialogue;
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

            while (index != diagList.Count)
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
                            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) && dialogueUI.SkipOrContinueText());
                            break;
                        case DIALOGUEEVENT.IMAGE:
                            break;
                        case DIALOGUEEVENT.CG:
                            dialogueUI.ShowBackground(diagList[index].cgImage);
                            break;
                        case DIALOGUEEVENT.BATTLE:
                            break;
                        case DIALOGUEEVENT.CHOICE:
                            currentChoices = diagList[index].branch;
                            break;
                        case DIALOGUEEVENT.END:
                            restartScreen.SetActive(true);
                            break;
                        default:
                            break;
                    }
                }
                index++;

                yield return null;
            }


            Debug.Log("End of Dialogue");

            if (currentChoices)
            {
                choiceManager.AddBranches(currentChoices);
                currentChoices = null;

            }
        }


    }

    public void PauseGame(bool pause)
    {
        if (pause)
        {
            restartScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            restartScreen.SetActive(false);
            Time.timeScale = 1;
        }

    }
}
