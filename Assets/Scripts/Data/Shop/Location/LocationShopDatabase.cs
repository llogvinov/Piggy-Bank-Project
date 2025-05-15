using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LocationsShopDatabase", menuName = "Database/Locations Shop")]
public class LocationShopDatabase : ScriptableObject
{
    [SerializeField] private TextAsset _csvFile;

    public Location[] _locations;

    [NonSerialized]
    private List<Location> _sortedLocations;

    public List<Location> SortedLocations => _sortedLocations ?? (_sortedLocations = GetSortedList());

    private Dictionary<int, Location> _locationsDict;

    public Dictionary<int, Location> LocationsDict => _locationsDict ?? (_locationsDict = InitializeDict());

    public Location GetLocationById(int id) => 
        LocationsDict[id];

    private Dictionary<int, Location> InitializeDict() =>
        _locations.ToDictionary(location => location.Id);

    private List<Location> GetSortedList() => 
        _locations.OrderBy(location => location.Price).ToList();

#if UNITY_EDITOR
    public void LoadFromCSV()
    {
        if (_csvFile == null)
        {
            Debug.LogError("CSV file is null.");
            return;
        }

        string[] lines = _csvFile.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        List<Location> newLocations = new List<Location>();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] fields = line.Split(',');

            if (fields.Length < 2) continue;

            Location location = new Location
            {
                Id = int.TryParse(fields[0], out int id) ? id : -1,
                LocalizationId = fields[1].Trim(),
                Price = int.TryParse(fields[2], out int price) ? price : -1,
            };

            newLocations.Add(location);
        }

        _locations = newLocations.ToArray();

        Debug.Log($"LocationDatabase updated with {_locations.Length} entries from CSV.");
    }
#endif
}