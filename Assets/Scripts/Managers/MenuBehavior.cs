using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour, IDataPersistence
{
    public Image level1Image;

    private bool seenLevel1 = false;

    public void Start()
    {

    }

    private void Update()
    {
        if (seenLevel1)
        {
            level1Image.color = Color.white;
        }
        else
        {
            level1Image.color = Color.black;
        }
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlayerLevel(int level)
    {
        seenLevel1 = true;
        SceneManager.LoadScene(level);
    }

    public void LoadData(GameData data)
    {
        seenLevel1 = data.seenLevel1;
    }

    public void SaveData(ref GameData data)
    {
        data.seenLevel1 = this.seenLevel1;
    }
}
