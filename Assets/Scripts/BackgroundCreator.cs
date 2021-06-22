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

    public void CreateBackground()
    {
        Location location = GameDataManager.GetSelectedLocation();

        AdjustLocation(location);
        AdjustClouds(location);
    }

    //Adjust selected location to the scene
    private void AdjustLocation(Location location)
    {
        
        sky.sprite = location.sky;
        ground.sprite = location.ground;
        trees.sprite = location.trees;
        mountain.sprite = location.mountain;
    }

    //Adjust selected set of clouds to the scene
    private void AdjustClouds(Location location)
    {
        cloudSpawner.clouds.Clear();
        for (int i = 0; i < location.clouds.Length; i++)
        {
            cloudSpawner.clouds.Add(location.clouds[i]);
        }
    }

}
