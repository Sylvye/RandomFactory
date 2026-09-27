using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CustomEditor(typeof(MaterialObjectLootpool))]
public class MaterialObjectLootpoolEditor : Editor
{
    private SerializedProperty _entries;
    private ReorderableList _list;

    private void OnEnable()
    {
        _entries = serializedObject.FindProperty("entries");
        _list = new ReorderableList(serializedObject, _entries, true, true, true, true);
        _list.drawHeaderCallback = rect =>
        {
            EditorGUI.LabelField(new Rect(rect.x, rect.y, rect.width - 162f, rect.height), "Blueprint");
            EditorGUI.LabelField(new Rect(rect.xMax - 157f, rect.y, 90f, rect.height), "Weight");
            EditorGUI.LabelField(new Rect(rect.xMax - 62f, rect.y, 62f, rect.height), "Chance");
        };
        _list.elementHeight = EditorGUIUtility.singleLineHeight + 6f;
        _list.drawElementCallback = DrawEntry;
        _list.onAddCallback = list =>
        {
            int index = _entries.arraySize;
            _entries.InsertArrayElementAtIndex(index);
            SerializedProperty entry = _entries.GetArrayElementAtIndex(index);
            entry.FindPropertyRelative("blueprint").objectReferenceValue = null;
            entry.FindPropertyRelative("weight").floatValue = 1f;
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        _list.DoLayoutList();

        double totalWeight = GetTotalWeight();
        if (totalWeight <= 0d)
            EditorGUILayout.HelpBox("No selectable blueprints. Add a blueprint with a positive weight.", MessageType.Warning);
        else
            EditorGUILayout.LabelField("Total weight", totalWeight.ToString("G"));

        serializedObject.ApplyModifiedProperties();
    }

    private void DrawEntry(Rect rect, int index, bool active, bool focused)
    {
        SerializedProperty entry = _entries.GetArrayElementAtIndex(index);
        SerializedProperty blueprint = entry.FindPropertyRelative("blueprint");
        SerializedProperty weight = entry.FindPropertyRelative("weight");
        rect.y += 3f;

        float chanceWidth = 62f;
        float weightWidth = 90f;
        float gap = 5f;
        Rect blueprintRect = new(rect.x, rect.y, rect.width - weightWidth - chanceWidth - 2f * gap, EditorGUIUtility.singleLineHeight);
        Rect weightRect = new(blueprintRect.xMax + gap, rect.y, weightWidth, EditorGUIUtility.singleLineHeight);
        Rect chanceRect = new(weightRect.xMax + gap, rect.y, chanceWidth, EditorGUIUtility.singleLineHeight);

        EditorGUI.PropertyField(blueprintRect, blueprint, GUIContent.none);
        EditorGUI.BeginChangeCheck();
        float newWeight = EditorGUI.FloatField(weightRect, weight.floatValue);
        if (EditorGUI.EndChangeCheck())
            weight.floatValue = float.IsNaN(newWeight) || float.IsInfinity(newWeight) ? 0f : Mathf.Max(0f, newWeight);

        double total = GetTotalWeight();
        double chance = blueprint.objectReferenceValue != null && IsValidWeight(weight.floatValue) && total > 0d
            ? weight.floatValue / total * 100d
            : 0d;
        EditorGUI.LabelField(chanceRect, $"{chance:0.#}%");
    }

    private double GetTotalWeight()
    {
        double total = 0d;
        for (int i = 0; i < _entries.arraySize; i++)
        {
            SerializedProperty entry = _entries.GetArrayElementAtIndex(i);
            SerializedProperty blueprint = entry.FindPropertyRelative("blueprint");
            float weight = entry.FindPropertyRelative("weight").floatValue;
            if (blueprint.objectReferenceValue != null && IsValidWeight(weight))
                total += weight;
        }
        return total;
    }

    private static bool IsValidWeight(float weight) => weight > 0f && !float.IsNaN(weight) && !float.IsInfinity(weight);
}
