using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogueCharacterManager : MonoBehaviour
{
    public Image leftCharacter;
    public Image rightCharacter;

    public void SetImage(Sprite image, bool right)
    {
        if (right)
        {
            rightCharacter.sprite = image;
        }
        else
        {
            leftCharacter.sprite = image;
        }
    }

}
