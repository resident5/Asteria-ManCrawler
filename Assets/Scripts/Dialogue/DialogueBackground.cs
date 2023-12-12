using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBackground : MonoBehaviour
{
    public Image cgImage;

    private void Awake()
    {
        cgImage = GetComponent<Image>();
    }
}
