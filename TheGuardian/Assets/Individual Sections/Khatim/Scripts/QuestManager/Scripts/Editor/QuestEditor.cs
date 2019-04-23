using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditorInternal;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    private Quest quest;
    ReorderableList objectives;

    [MenuItem("Quest System/Add Quest")]
    public static void AddQuest()
    {
        var obj = new GameObject().AddComponent<Quest>();
        obj.name = "Quest";
    }

    public void OnEnable()
    {
        quest = (Quest)target;
        objectives = new ReorderableList(serializedObject, serializedObject.FindProperty("objectives"), true, true, true, true)
        {
            onAddCallback = (ReorderableList list) =>
            {
                var obj = new GameObject().AddComponent<Objective>();
                obj.transform.parent = quest.transform;
                var index = list.serializedProperty.arraySize;
                list.serializedProperty.arraySize++;
                list.index = index;
                obj.name = list.index + "_Objective";
                obj.Quest = quest;

                var element = list.serializedProperty.GetArrayElementAtIndex(index);
                element.objectReferenceValue = obj;
                serializedObject.ApplyModifiedProperties();
            },
            onRemoveCallback = (ReorderableList list) =>
            {
                RemoveItemAt(list.index);
            },
            onReorderCallback = (ReorderableList list) =>
            {
                ReorderList(list);
            },
            drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Objectives");
            },
            drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
            {
                var element = objectives.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                EditorGUI.PropertyField(
                 new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                 element, GUIContent.none);
            }
        };
    }

    private void ReorderList(ReorderableList list)
    {
        quest.transform.DetachChildren();
        for (int i = 0; i < list.serializedProperty.arraySize; i++)
        {
            var element = list.serializedProperty.GetArrayElementAtIndex(i).objectReferenceValue as Objective;
            if (element)
            {
                //element.name = i + "_Objective";
                element.transform.parent = quest.transform;
            }
        }
    }

    public override void OnInspectorGUI()
    {
        if (!Application.isPlaying)
        {
            if (quest.currentObjective == null)
            {
                quest.Restart();
            }
        }
        // EditorGUILayout.ObjectField(serializedObject.FindProperty("reward"));
        RemoveBrokenRefreences();
        objectives.DoLayoutList();
        GUI.enabled = false;
        EditorGUILayout.ObjectField(serializedObject.FindProperty("currentObjective"));
        GUI.enabled = true;

        if (EditorApplication.isPlaying)
        {
            EditorGUILayout.BeginHorizontal();
            if (quest.Current == quest.objectives.Count - 1)
            {
                GUI.enabled = false;
            }
            if (GUILayout.Button("Next"))
            {
                quest.Next();
            }
            GUI.enabled = true;

            if (quest.Current == 0)
            {
                GUI.enabled = false;
            }
            if (GUILayout.Button("previous"))
            {
                quest.Previous();
            }
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
        }
    }

    private void RemoveBrokenRefreences()
    {
        bool itemDeleted = false;
        for (int i = objectives.serializedProperty.arraySize - 1; i >= 0; i--)
        {
            if (objectives.serializedProperty.GetArrayElementAtIndex(i).objectReferenceValue == null)
            {
                objectives.serializedProperty.DeleteArrayElementAtIndex(i);
                itemDeleted = true;
            }
        }

        serializedObject.ApplyModifiedProperties();
        if (itemDeleted)
        {
            ReorderList(objectives);
        }

    }


    private void RemoveItemAt(int index)
    {
        var element = objectives.serializedProperty.GetArrayElementAtIndex(index);
        var objective = element.objectReferenceValue as Objective;
        if (objective)
            GameObject.DestroyImmediate(objective.gameObject);
        RemoveBrokenRefreences();
    }
}