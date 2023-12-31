using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class DialogueUI : MonoBehaviour
{
    public DialogueUIBox left;
    public DialogueUIBox narration;
    public DialogueUIBox activeBox;

    public DialogueBackgroundManager backgroundManager;
    public DialogueCharacterManager characterManager;


    public IEnumerator TextCouroutine;
    public bool isTypingText;
    public string currentText;

    //public TMP_InputField emphasisInputField;

    private void Start()
    {
        activeBox = narration;
        isTypingText = false;
    }

    private void Update()
    {
    }

    public void ChangeText(string text)
    {
        if (activeBox != null)
        {
            StopAllCoroutines();
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
        isTypingText = false;
    }

    public bool SkipOrContinueText()
    {
        if (activeBox.text.text != currentText)
        {
            isTypingText = false;
            StopCoroutine(TextCouroutine);
            activeBox.text.text = currentText;
            return false;
        }
        else
        {
            StopCoroutine(TextCouroutine);
            return true;
        }
    }

    public void ChangeDialogueBox(bool isActorBox, DialogueActorObject actor)
    {
        activeBox.gameObject.SetActive(false);
        activeBox.active = false;

        if (isActorBox)
        {
            activeBox = left;
            activeBox.namePlateText.text = actor.name;
            activeBox.image.sprite = actor.actorTextBoxImage;
        }
        else
        {
            activeBox = narration;
        }
        activeBox.gameObject.SetActive(true);
        activeBox.active = true;
    }

    public void ShowBackground(Sprite sprite)
    {
        backgroundManager.cgBackground.cgImage.sprite = sprite;
        backgroundManager.canvasG.alpha = 1;
    }

    public void SwapBackground(Sprite sprite)
    {
        backgroundManager.cgBackground.cgImage.sprite = sprite;
    }
}
