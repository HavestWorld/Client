using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(PlantDB))]
public class DB_EDITOR : Editor
{
    private static GUIContent TOPcreatebutton = new GUIContent("TOP작물 생성", "생성합니다."),
        MIDDLEcreatebutton = new GUIContent("MIDDLE작물 생성", "생성합니다."),
        BOTTOMcreatebutton = new GUIContent("BOTTOM작물 생성", "생성합니다."),
        deletebutton = new GUIContent("작물 삭제", "삭제합니다.");

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.LabelField("작물 세팅");
        EditorGUILayout.PropertyField(serializedObject.FindProperty("id"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("names"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("content"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("icon"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("inGamePlantSprites"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("growTime"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxHP"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("minValue"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("maxValue"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("moePercent"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("colorCode"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("bigkind"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("smallkind"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("detailkind"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("size"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("taste"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("colorType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("durability"));

        DrawSeperator(Color.gray);

        PlantDB script = target as PlantDB;

        if (GUILayout.Button(TOPcreatebutton))
        {
            script.Add(0);
        }
        if (GUILayout.Button(MIDDLEcreatebutton))
        {
            script.Add(1);
        }
        if (GUILayout.Button(BOTTOMcreatebutton))
        {
            script.Add(2);
        }

        DrawSeperator(Color.gray);

        EditorGUILayout.LabelField("TOP 작물 리스트");
        showlist(serializedObject.FindProperty("topPlantList"));

        DrawSeperator(Color.gray);

        EditorGUILayout.LabelField("MIDDLE 작물 리스트");
        showlist(serializedObject.FindProperty("middlePlantList"));

        DrawSeperator(Color.gray);

        EditorGUILayout.LabelField("BOTTOM 작물 리스트");
        showlist(serializedObject.FindProperty("bottomPlantList"));

        serializedObject.ApplyModifiedProperties();
    }

    private static void showlist(SerializedProperty list)
    {
        EditorGUILayout.PropertyField(list, false);
        EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
        EditorGUI.indentLevel += 1;
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i),true);
            if (GUILayout.Button(deletebutton))
            {
                list.DeleteArrayElementAtIndex(i);
            }
        }
        EditorGUI.indentLevel -= 1;
    }

    void DrawSeperator(Color color)
    {
        EditorGUILayout.Space();

        Texture2D tex = new Texture2D(1, 1);
        GUI.color = color;
        float y = GUILayoutUtility.GetLastRect().yMax;
        GUI.DrawTexture(new Rect(0.0f, y, Screen.width, 1.0f), tex);
        tex.hideFlags = HideFlags.DontSave;
        GUI.color = Color.white;

        EditorGUILayout.Space();
    }
}
