using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueButtonControl : MonoBehaviour
{
    public void RestartScene()
    {
        GameManager.Instance.dialogueManager.PauseGame(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void ReturnToMainMenu()
    {
        GameManager.Instance.dialogueManager.PauseGame(false);
        SceneManager.LoadScene(0);
    }
}
