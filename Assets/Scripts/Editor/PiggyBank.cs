using System.IO;
using UnityEditor;
using UnityEngine;


public class ResetManager : MonoBehaviour
{
    private static readonly string PlayerProgressFilePath = Application.dataPath + "/player-data.txt";
        
    [MenuItem("Piggy Bank/Reset Saved Data")]
    public static void ResetSavedData()
    {
        File.Delete(PlayerProgressFilePath);
    }
}
