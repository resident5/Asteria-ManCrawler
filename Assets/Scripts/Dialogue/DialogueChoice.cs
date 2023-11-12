using System.Collections;
using UnityEngine;


public class DialogueChoice : MonoBehaviour
{
    public DialogueObject choiceObject;

    public void StartChoice()
    {
        if(choiceObject != null)
        {
            DialogueManager.Instance.ChangeDialogue(choiceObject);
            DialogueManager.Instance.PlayDialogue();
        }
    }
}
