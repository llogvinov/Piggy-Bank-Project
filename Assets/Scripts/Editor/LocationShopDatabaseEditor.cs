using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LocationShopDatabase))]
public class LocationShopDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        LocationShopDatabase db = (LocationShopDatabase)target;

        if (GUILayout.Button("Load From CSV File"))
        {
            db.LoadFromCSV();
            EditorUtility.SetDirty(db);
            AssetDatabase.SaveAssets();
        }
    }
}
