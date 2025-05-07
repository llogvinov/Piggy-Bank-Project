using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocalizationData))]
public class LocalizationDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LocalizationData data = (LocalizationData)target;

        if (GUILayout.Button("Load From CSV File"))
        {
            data.LoadFromCSV();
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
        }
    }
}
