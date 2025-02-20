using UnityEngine;

namespace Main.Background
{
    public class BackgroundCreator : MonoBehaviour
    {
        [Header("Background elements")]
        [SerializeField] private SpriteRenderer _sky;
        [SerializeField] private SpriteRenderer _ground;
        [SerializeField] private SpriteRenderer _trees;
        [SerializeField] private SpriteRenderer _mountain;

        [Header("Spawner")]
        [SerializeField] private CloudSpawner _cloudSpawner;

        private void Start()
        {
            SetLocation();
        }

        public void SetLocation()
        {
            var location = GameDataManager.GetSelectedLocation();

            SetBackground(location);
            SetClouds(location);
        }

        private void SetBackground(Location location)
        {
            _sky.sprite = location.sky;
            _ground.sprite = location.ground;
            _trees.sprite = location.trees;
            _mountain.sprite = location.mountain;
        }

        private void SetClouds(Location location)
        {
            _cloudSpawner.Clouds.Clear();
            foreach (var cloud in location.clouds)
            {
                _cloudSpawner.Clouds.Add(cloud);
            }
        }
    }
}