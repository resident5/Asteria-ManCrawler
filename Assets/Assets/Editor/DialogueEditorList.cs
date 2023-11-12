using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

public static class DialogueEditorList
{
    //private static ReorderableList reordablelist;

    public static void Show(SerializedProperty list, bool showListSize = true)
    {
        //EditorGUILayout.PropertyField(list);
        EditorGUI.indentLevel += 1;

        //if (reordablelist == null)
        //{
        //    reordablelist = new ReorderableList(list.serializedObject, list, true, true, true, true);
        //    reordablelist.drawElementCallback = DrawElement;
        //}
        //reordablelist

        if (list.isExpanded)
        {
            if (showListSize)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            }
            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }

        }
        EditorGUI.indentLevel -= 1;

    }

    //public static void DrawElement(Rect rect, int index, bool isActive, bool isFocused)
    //{
    //    var element = reordablelist.serializedProperty.GetArrayElementAtIndex(index);
    //    rect.y += 2;

    //    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), element, GUIContent.none);
    //}
}
