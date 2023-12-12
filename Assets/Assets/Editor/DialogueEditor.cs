using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[System.Serializable]
[CanEditMultipleObjects]
[CustomEditor(typeof(DialogueObject))]
public class DialogueEditor : Editor
{
    public string editorText;
    SerializedProperty propertyList;
    SerializedProperty propertyActor;
    //public ReorderableList reordablelist;


    public DialogueManager.DIALOGUEEVENT diagEvent;

    public DialogueObject dialogue;
    public int tabbingIndex = 0;

    public bool foldoutSetting = false;

    private void OnEnable()
    {
        propertyList = serializedObject.FindProperty("dialogueList");
        propertyActor = serializedObject.FindProperty("actors");
        //reordablelist = new ReorderableList(propertyList.serializedObject, propertyList, true, true, true, true);
        //reordablelist.drawElementCallback = DrawElement;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        //DialogueEditorList.Show(propertyList, true);
        dialogue = target as DialogueObject;
        foldoutSetting = EditorGUILayout.Foldout(foldoutSetting, "Text List");

        if (foldoutSetting)
        {
            ContextMenuDrawer();
            DialogueField();
        }

        if (GUILayout.Button("Fix name order + actor bug"))
        {
            dialogue.SetText();
        }

        EditorGUILayout.BeginHorizontal();

        AddNarrationDialogue();
        AddActorDialogue();
        SetAllDiagEventsToText();

        //if (GUI.changed)
        //{
        //    Debug.Log("Change detected");
        //    dialogue.SetText();

        //}
        EditorGUILayout.EndHorizontal();
        DialogueEditorList.Show(propertyActor);
        serializedObject.ApplyModifiedProperties();
    }



    public void DialogueField()
    {
        for (int i = 0; i < dialogue.dialogueList.Count; i++)
        {
            EditorGUIUtility.labelWidth = 0.5f;
            EditorGUILayout.BeginHorizontal("Button");
            GUI.SetNextControlName("Text_" + tabbingIndex);
            dialogue.dialogueList[i].text = GUILayout.TextArea(dialogue.dialogueList[i].text, GUILayout.Width(400));
            GUILayout.FlexibleSpace();
            dialogue.dialogueList[i].isActor = EditorGUILayout.Toggle(dialogue.dialogueList[i].isActor);
            dialogue.dialogueList[i].actorIndex = EditorGUILayout.IntField(dialogue.dialogueList[i].actorIndex);
            EditorGUILayout.Space(20);
            EditorGUILayout.EndHorizontal();
        }

    }

    public void ContextMenuDrawer()
    {
        Event e = Event.current;
        if (e.type == EventType.ContextClick)
        {
            for (int i = 0; i < dialogue.dialogueList.Count; i++)
            {
                int index = i;

                GenericMenu menu = new GenericMenu();
                menu.AddItem(new GUIContent("Add Above"), false, AddAbove, index);
                menu.ShowAsContext();
                e.Use();
            }
        }
    }


    void AddAbove(object usedata)
    {
        int index = (int)usedata;
        Debug.Log("index = " + index);
        dialogue.dialogueList.Insert(index, new DialogueElement(false));


    }

    public void AddNarrationDialogue()
    {
        if (GUILayout.Button("Narration"))
        {
            tabbingIndex = dialogue.dialogueList.Count;
            GUI.SetNextControlName("Text_" + tabbingIndex);
            dialogue.dialogueList.Add(new DialogueElement(false));
            GUI.FocusControl("TextArea" + tabbingIndex);
            Event.current.Use();
        }
    }

    public void SetAllDiagEventsToText()
    {
        if (GUILayout.Button("DiagEventToText"))
        {
            foreach (var diag in dialogue.dialogueList)
            {
                foreach (var dEvent in diag.diagEvents)
                {
                    if (diag.diagEvents.Contains(DialogueManager.DIALOGUEEVENT.TEXT))
                    {
                        return;
                    }
                }
                diag.diagEvents.Add(DialogueManager.DIALOGUEEVENT.TEXT);

            }
        }
    }

    public void AddActorDialogue()
    {
        if (GUILayout.Button("0"))
        {
            tabbingIndex = dialogue.dialogueList.Count;
            GUI.SetNextControlName("Text_" + tabbingIndex);
            dialogue.dialogueList.Add(new DialogueElement(true, 0));
            GUI.FocusControl("TextArea" + tabbingIndex);
            Event.current.Use();
        }

        if (GUILayout.Button("1"))
        {
            tabbingIndex = dialogue.dialogueList.Count;
            GUI.SetNextControlName("Text_" + tabbingIndex);
            dialogue.dialogueList.Add(new DialogueElement(true, 1));
            GUI.FocusControl("TextArea" + tabbingIndex);
            Event.current.Use();
        }

    }

    //public void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
    //{
    //    var element = reordablelist.serializedProperty.GetArrayElementAtIndex(index);
    //    rect.y += 2;

    //    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, dialogue);
    //}
}
