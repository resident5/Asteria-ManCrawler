using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Word : MonoBehaviour
{
    public GameObject letterPrefab;

    public string word;
    public List<GameObject> letterList;

    public Color correctColor;

    public OrcBoss orcBoss;

    private void Awake()
    {
        orcBoss = GameObject.Find("Boss Enemy").GetComponent<OrcBoss>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CreateLetters()
    {
        letterList = new List<GameObject>();

        word = word.ToLower();
        foreach (char l in word)
        {
            var letterObj = Instantiate(letterPrefab, transform);
            Letter letter = letterObj.GetComponent<Letter>();
            letter.letterText.font = orcBoss.fontAsset;
            letter.letterText.fontSize = orcBoss.fontSize;
            letterList.Add(letterObj);

            letter.SetLetter(l);
        }
    }

    public void HighlightLetter(int index)
    {
        letterList[index].GetComponent<Image>().color = correctColor;
    }

    public void ClearHighlight()
    {
        foreach(var letterObj in letterList)
        {
            letterObj.GetComponent<Image>().color = Color.white;
        }
    }

    
}
