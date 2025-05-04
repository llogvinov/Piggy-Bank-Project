using UnityEngine;

namespace Spawners
{
    public class PooledObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        
        public Rigidbody2D Rigidbody => _rigidbody;
        
        private ObjectPool _pool;

        public ObjectPool Pool
        {
            get => _pool;
            set => _pool = value;
        }

        public void Release() => 
            _pool.ReturnObjectToPool(this);
    }
}