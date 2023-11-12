using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
[CustomEditor(typeof(DialogueElement))]
public class DialogueElementEditor : Editor
{
    SerializedProperty property;
    SerializedProperty actorProperty;
    public DialogueElement element;


    private void OnEnable()
    {
        property = serializedObject.FindProperty("text");
        actorProperty = serializedObject.FindProperty("isActor");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(property, true);

        

        serializedObject.ApplyModifiedProperties();

    }
}
