using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MaskShopDatabase))]
public class MaskShopDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MaskShopDatabase db = (MaskShopDatabase)target;

        if (GUILayout.Button("Load From CSV File"))
        {
            db.LoadFromCSV();
            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();
        }
    }
}
