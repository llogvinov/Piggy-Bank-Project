using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllLocalizationData", menuName = "Localization/AllLocalizationData")]
public class AllLocalizationData : ScriptableObject
{ 
    public List<LocalizationData> PartDataList;
}