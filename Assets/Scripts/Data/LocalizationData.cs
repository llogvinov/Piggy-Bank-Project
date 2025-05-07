using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "Localization/LocalizationData")]
public class LocalizationData : ScriptableObject
{
    [SerializeField] private TextAsset CsvFile;
    public List<SingleLocalizationData> Data;

#if UNITY_EDITOR
    public void LoadFromCSV()
    {
        if (CsvFile == null)
        {
            Debug.LogError("CSV file is not assigned.");
            return;
        }

        string[] lines = CsvFile.text.Split('\n');
        if (lines.Length < 2)
        {
            Debug.LogError("CSV file is empty or has no data rows.");
            return;
        }

        Data = new List<SingleLocalizationData>();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] values = line.Split(',');
            if (values.Length >= 3)
            {
                SingleLocalizationData entry = new SingleLocalizationData
                {
                    Id = values[0].Trim(),
                    RU = values[1].Trim(),
                    EN = values[2].Trim()
                };
                Data.Add(entry);
            }
            else
            {
                Debug.LogWarning($"Line {i + 1} is malformed: {line}");
            }
        }

        Debug.Log("Localization data loaded from assigned CSV.");
    }
#endif
}