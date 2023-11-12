using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Branch", menuName = "Create Dialogue Path Branch", order = 2)]
public class DialogueBranchObject : ScriptableObject
{
    public List<DialogueObject> dialogues;
}
