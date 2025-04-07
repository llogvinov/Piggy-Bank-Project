using System.IO;
using UnityEngine;

namespace Core.Services.PlayerData
{
    public class LocalPlayerDataService : BasePlayerDataService, IPlayerDataService
    {
        private string _savePath = Application.dataPath + "player-data.txt";

        public override PlayerData Load()
        {
            // var playerData = BinarySerializer.Load<PlayerData>("player-data.txt");
            // return playerData;

            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                PlayerData data = JsonUtility.FromJson<PlayerData>(json);
                Debug.Log("Data loaded from " + _savePath);
                return data;
            }
            else
            {
                Debug.LogWarning("Save file not found at " + _savePath);
                PlayerData playerData = new PlayerData();
                Save(playerData);
                return null;
            }
        }

        public override void Save(PlayerData playerData)
        {
            string json = JsonUtility.ToJson(playerData, true);
            File.WriteAllText(_savePath, json);
            Debug.Log("Data saved to " + _savePath);
            // BinarySerializer.Save(playerData, "player-data.txt");
        }
    }
}