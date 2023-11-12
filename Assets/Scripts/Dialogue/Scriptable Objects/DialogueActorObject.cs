using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Create Dialogue Actor", order = 1)]
public class DialogueActorObject : ScriptableObject
{
    public string actorName;
    public Sprite actorFaceImage;
    public Sprite actorTextBoxImage;
    public Font actorFont;


}
