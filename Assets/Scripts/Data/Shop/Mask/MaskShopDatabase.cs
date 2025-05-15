using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "MaskShopDatabase", menuName = "Database/Mask Shop")]
public class MaskShopDatabase : ScriptableObject
{
    [SerializeField] private TextAsset _csvFile;

    public Mask[] _masks;

    [NonSerialized]
    private List<Mask> _sortedMasks;

    public List<Mask> SortedMasks => _sortedMasks ?? (_sortedMasks = GetSortedList());

    private Dictionary<int, Mask> _masksDict;

    public Dictionary<int, Mask> MasksDict => _masksDict ?? (_masksDict = InitializeDict());

    public Mask GetMaskById(int id) => 
        MasksDict[id];

    private Dictionary<int, Mask> InitializeDict() =>
        _masks.ToDictionary(mask => mask.Id);

    private List<Mask> GetSortedList() => 
        _masks.OrderBy(mask => mask.Price).ToList();

#if UNITY_EDITOR
    public void LoadFromCSV()
    {
        if (_csvFile == null)
        {
            Debug.LogError("CSV file is null.");
            return;
        }

        string[] lines = _csvFile.text.Split(new[] { '\n', '\r' }, System.StringSplitOptions.RemoveEmptyEntries);
        List<Mask> newMasks = new List<Mask>();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(',');

            if (fields.Length < 2) continue;

            Mask mask = new Mask
            {
                Id = int.TryParse(fields[0], out int id) ? id : -1,
                LocalizationId = fields[1].Trim(),
                Price = int.TryParse(fields[2], out int price) ? price : -1,
            };

            newMasks.Add(mask);
        }

        _masks = newMasks.ToArray();

        Debug.Log($"MaskDatabase updated with {_masks.Length} entries from CSV.");
    }
#endif
}
