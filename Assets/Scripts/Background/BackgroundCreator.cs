using UnityEngine;

public class BackgroundCreator : MonoBehaviour
{
    [Header("Background elements")]
    [SerializeField] private SpriteRenderer sky;
    [SerializeField] private SpriteRenderer ground;
    [SerializeField] private SpriteRenderer trees;
    [SerializeField] private SpriteRenderer mountain;

    [Header("Spawner")]
    [SerializeField] private CloudSpawner cloudSpawner;

    private void Awake()
    {
        CreateBackground();
    }

    private void CreateBackground()
    {
        Location location = GameDataManager.GetSelectedLocation();

        AdjustLocation(location);
        AdjustClouds(location);
    }

    private void AdjustLocation(Location location)
    {
        sky.sprite = location.sky;
        ground.sprite = location.ground;
        trees.sprite = location.trees;
        mountain.sprite = location.mountain;
    }

    private void AdjustClouds(Location location)
    {
        cloudSpawner.Clouds.Clear();
        foreach (var cloud in location.clouds)
        {
            cloudSpawner.Clouds.Add(cloud);
        }
    }

}
