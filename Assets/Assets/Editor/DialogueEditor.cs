using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
[CustomEditor(typeof(Dialogue))]
public class DialogueEditor : Editor
{
    public string editorText;
    SerializedProperty property;

    public DialogueManager.DIALOGUEEVENT diagEvent;

    public Dialogue dialogue;
    public int tabbingIndex = 0;

    public bool foldoutSetting = false;

    private void OnEnable()
    {
        property = serializedObject.FindProperty("dialogueList");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        dialogue = target as Dialogue;
        foldoutSetting = EditorGUILayout.Foldout(foldoutSetting, "Text List");

        if(foldoutSetting)
        {
            DialogueField();
        }

        if (GUILayout.Button("Fix name order"))
        {
            dialogue.SetText();
        }

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Narration"))
        {
            AddNarrationDialogue();
        }

        if (GUILayout.Button("Actor"))
        {
            AddActorDialogue();
        }
        
        EditorGUILayout.EndHorizontal();


        for (int i = 0; i < dialogue.dialogueList.Count; i++)
        {
            EditorGUILayout.Space(20);

            Event e = Event.current;
            if(e.keyCode == KeyCode.Tab && e.type == EventType.KeyDown)
            {
                if(e.shift)
                {
                    tabbingIndex = (tabbingIndex - 1 + dialogue.dialogueList.Count) % dialogue.dialogueList.Count;
                }
                else
                {
                    tabbingIndex = (tabbingIndex + 1) % dialogue.dialogueList.Count;
                    Debug.Log("TABB!" + tabbingIndex);
                }
            }
        }


        serializedObject.ApplyModifiedProperties();
    }

    public void DialogueField()
    {
        for(int i = 0; i < dialogue.dialogueList.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            GUI.SetNextControlName("Text_" + tabbingIndex);
            dialogue.dialogueList[i].text = GUILayout.TextArea(dialogue.dialogueList[i].text);
            EditorGUILayout.Space(20);
            EditorGUILayout.EndHorizontal();

        }

    }

    public void AddNarrationDialogue()
    {
        tabbingIndex = dialogue.dialogueList.Count;
        GUI.SetNextControlName("Text_" + tabbingIndex);
        dialogue.dialogueList.Add(new DialogueElement(false));
        GUI.FocusControl("TextArea" + tabbingIndex);
        Event.current.Use();
    }

    public void AddActorDialogue()
    {
        tabbingIndex = dialogue.dialogueList.Count;
        GUI.SetNextControlName("Text_" + tabbingIndex);
        dialogue.dialogueList.Add(new DialogueElement(true));
        GUI.FocusControl("TextArea" + tabbingIndex);
        Event.current.Use();

    }
}
