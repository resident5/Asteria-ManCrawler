using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public string chosenWord;
    public Word word;
    public int letterIndex;

    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[]
    {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F,
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L,
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R,
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X,
        KeyCode.Y, KeyCode.Z
    };
    // Start is called before the first frame update
    void Start()
    {
        letterIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode vKey in SUPPORTED_KEYS)
        {
            if (Input.GetKeyDown(vKey) && letterIndex < word.word.Length)
            {
                if (word.word[letterIndex] == ((char)vKey))
                {
                    word.HighlightLetter(letterIndex);
                    letterIndex++;
                    Debug.Log("CORRECT LETTER?");
                }
                else if (word.word[letterIndex] != ((char)vKey) && letterIndex != word.word.Length - 1)
                {
                    word.ClearHighlight();
                    letterIndex = 0;
                    Debug.Log("Word not finished and we fucked up so restarting");
                }
            }
        }
        //for (int x = 0; x < SUPPORTED_KEYS.Length; x++)
        //{
        //    if(Input.GetKeyDown(SUPPORTED_KEYS[x]))
        //    {
        //        if(word.word[letterIndex] == SUPPORTED_KEYS[x])
        //        {

        //        }

        //        //word.
        //    }
        //}
    }
}
