using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HatShopDatabase))]
public class HatShopDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        HatShopDatabase db = (HatShopDatabase)target;

        if (GUILayout.Button("Load From CSV File"))
        {
            db.LoadFromCSV();
            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();
        }
    }
}
