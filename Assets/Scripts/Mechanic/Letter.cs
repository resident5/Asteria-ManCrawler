using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    private char letter;
    public TextMeshProUGUI letterText;

    private void Awake()
    {
        letterText = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLetter(char l)
    {
        letter = l;
        letterText.text = letter.ToString().ToUpper();
    }
}
