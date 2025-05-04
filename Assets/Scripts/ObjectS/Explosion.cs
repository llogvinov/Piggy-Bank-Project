using Spawners;

public class Explosion : PooledObject
{
    public void DestroyExplosion() => 
        Release();
}