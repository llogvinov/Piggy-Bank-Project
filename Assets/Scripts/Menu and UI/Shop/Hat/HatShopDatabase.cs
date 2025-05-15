using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "HatShopDatabase", menuName = "Database/Hats Shop")]
public class HatShopDatabase : ScriptableObject
{
    [SerializeField] private TextAsset _csvFile;

    public Hat[] hats;

    [NonSerialized]
    private List<Hat> _sortedHats;

    public List<Hat> SortedHats => _sortedHats ?? (_sortedHats = GetSortedList());

    private Dictionary<int, Hat> _hatsDict;

    public Dictionary<int, Hat> HatsDict => _hatsDict ?? (_hatsDict = InitializeDict());

    public Hat GetHatById(int id) => 
        HatsDict[id];

    private Dictionary<int, Hat> InitializeDict() =>
        hats.ToDictionary(hat => hat.Id);

    private List<Hat> GetSortedList() => 
        hats.OrderBy(hat => hat.Price).ToList();

#if UNITY_EDITOR
    public void LoadFromCSV()
    {
        if (_csvFile == null)
        {
            Debug.LogError("CSV file is null.");
            return;
        }

        string[] lines = _csvFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        List<Hat> newHats = new List<Hat>();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(',');

            if (fields.Length < 2) continue;

            Hat hat = new Hat
            {
                Id = int.TryParse(fields[0], out int id) ? id : -1,
                LocalizationId = fields[1].Trim(),
                Price = int.TryParse(fields[2], out int price) ? price : -1,
            };

            newHats.Add(hat);
        }

        hats = newHats.ToArray();

        Debug.Log($"HatDatabase updated with {hats.Length} entries from CSV.");
    }
#endif
}
