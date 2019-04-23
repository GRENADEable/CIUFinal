using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditorInternal;
using System.Text;

[CustomEditor(typeof(Objective))]
public class ObjectiveEditor : Editor
{
    private Objective objective;
    ReorderableList actions;

    public void OnEnable()
    {
        objective = (Objective)target;
        actions = new ReorderableList(serializedObject, serializedObject.FindProperty("actions"), false, true, true, true)
        {
            onAddDropdownCallback = (Rect buttonRect, ReorderableList list) =>
            {
                var menu = new GenericMenu();
                var types = GetAllTypes();
                foreach (var type in types)
                {
                    var name = AddSpacesToSentence(type.name);
                    menu.AddItem(new GUIContent(name, type.description), false, (objType) =>
                    {
                        var o = (ActionType)objType;
                        var action = GameObject.Instantiate<Action>(o.prefab);

                        action.objective = objective;
                        action.transform.parent = objective.transform;
                        var index = list.serializedProperty.arraySize;
                        list.serializedProperty.arraySize++;
                        list.index = index;
                        action.name = list.index + "_" + name;
                        var element = list.serializedProperty.GetArrayElementAtIndex(index);
                        element.objectReferenceValue = action;
                        serializedObject.ApplyModifiedProperties();
                    }, type);

                }
                menu.ShowAsContext();
            },
            onRemoveCallback = (ReorderableList list) =>
            {
                RemoveItemAt(list.index);
            },
            drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Actions");
            },
            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = actions.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                EditorGUI.PropertyField(
                 new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                 element, GUIContent.none);
            }
        };
    }
    ActionType[] GetAllTypes()
    {
        var guids = AssetDatabase.FindAssets("t:ActionType");
        var types = new ActionType[guids.Length];
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            types[i] = AssetDatabase.LoadAssetAtPath<ActionType>(path);
        }
        Debug.Log(types.Length);
        return types;
    }

    private string AddSpacesToSentence(string text)
    {
        if (text == null)
            return "";
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }
    private void ReorderList(ReorderableList list)
    {
        for (int i = 0; i < list.serializedProperty.arraySize; i++)
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(i).objectReferenceValue as Objective;
            if (element)
            {
                element.transform.parent = objective.transform;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        actions.DoLayoutList();
    }

    private void RemoveBrokenRefreences()
    {
        bool itemDeleted = false;
        for (int i = actions.serializedProperty.arraySize - 1; i >= 0; i--)
        {
            if (actions.serializedProperty.GetArrayElementAtIndex(i).objectReferenceValue == null)
            {
                actions.serializedProperty.DeleteArrayElementAtIndex(i);
                itemDeleted = true;
            }
        }

        serializedObject.ApplyModifiedProperties();
        if (itemDeleted)
        {
            ReorderList(actions);
        }
    }

    private void RemoveItemAt(int index)
    {
        var element = actions.serializedProperty.GetArrayElementAtIndex(index);
        var objective = element.objectReferenceValue as Action;
        if (objective)
        {
            GameObject.DestroyImmediate(objective.gameObject);
        }
        actions.serializedProperty.DeleteArrayElementAtIndex(index);
        RemoveBrokenRefreences();
    }
}