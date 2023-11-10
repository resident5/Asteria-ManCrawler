using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public DialogueUIBox left;
    public DialogueUIBox narration;

    public DialogueUIBox activeBox;

    public IEnumerator TextCouroutine;
    public bool isTypingText;
    public string currentText;

    private void Start()
    {
        activeBox = narration;
        activeBox.active = true;
        isTypingText = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SkipOrContinueText();
            //if (TextCouroutine != null)
            //{
            //    isRunning = false;
            //    StopCoroutine(TextCouroutine);
            //}

            //if (TextCouroutine != null && !isRunning)
            //{
            //    StopCoroutine(TextCouroutine);
            //}
            //else if (isRunning)
            //{
            //    StopCoroutine(TextCouroutine);
            //    isRunning = false;
            //}
        }

        if(activeBox.text.text == currentText)
        {
            isTypingText = false;
        }
    }

    public void ChangeText(string text)
    {
        if (activeBox != null)
        {
            StartCoroutine(TextCouroutine = TypeOutText(text));
            currentText = text;
        }
    }

    IEnumerator TypeOutText(string text)
    {
        activeBox.text.text = "";
        isTypingText = true;
        foreach (char c in text)
        {
            activeBox.text.text += c;
            yield return null;
            yield return null;
            yield return null;
        }
    }

    public bool SkipOrContinueText()
    {
        if (activeBox.text.text != currentText)
        {
            StopCoroutine(TextCouroutine);
            activeBox.text.text = currentText;
            isTypingText = false;
            return false;
        }
        else
        {
            StopCoroutine(TextCouroutine);
            return true;
        }
    }

    public bool TextCanContinue()
    {
        if (activeBox.text.text == currentText)
            return true;
        else
            return false;

    }

    public void ChangeDialogueBox(DialogueUIBox newBox)
    {
        activeBox.gameObject.SetActive(false);
        activeBox.active = false;
        activeBox = newBox;
        activeBox.gameObject.SetActive(true);
        activeBox.active = true;
    }
}
