using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "DialogueActor", order = 1)]
public class DialogueActor : ScriptableObject
{
    public string actorName;
    public Sprite actorFaceImage;
    public Sprite actorTextBoxImage;
    public Font actorFont;


}
