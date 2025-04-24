using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LocalizationData", menuName = "LocalizationData")]
public class LocalizationData : ScriptableObject
{
    public List<SingleLocalizationData> Data;
}

[System.Serializable]
public class SingleLocalizationData
{
    public string Id;
    public string RU;
    public string EN;
}