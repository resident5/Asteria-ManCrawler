using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueBackgroundManager : MonoBehaviour
{
    public DialogueBackground cgBackground;
    public CanvasGroup canvasG;

    private void Awake()
    {
        canvasG = GetComponent<CanvasGroup>();
        canvasG.alpha = 0;
    }
}
