using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class PoolController : Singleton<PoolController>
{
    [System.Serializable]
    private class Pool
    {
        public string id;
        public GameObject prefab;
        public int size;
    }
    
    [SerializeField] private List<Pool> pools;
    
    private Dictionary<string, Queue<GameObject>> _pooledDictionary;

    public void Start()
    {
        _pooledDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (var i = 0; i < pool.size; i++)
            {
                var gO = Instantiate(pool.prefab);
                gO.SetActive(false);
                objectPool.Enqueue(gO);
            }
            
            _pooledDictionary.Add(pool.id, objectPool);
        }
    }

    public void SpawnFromPool(string id, int amount)
    {
        if (id == string.Empty || !_pooledDictionary.ContainsKey(id))
        {
            Debug.LogWarning($"The tag {id} was not found in the dictionary.");
            return;
        }

        //if there are not enough objects in the pool, increase the pool
        if (amount > _pooledDictionary[id].Count) IncreasePool(id, amount - _pooledDictionary[id].Count);

        //set all inactive
        foreach (var o in _pooledDictionary[id])
        {
            o.SetActive(false);
        }

        for (var i = 0; i < amount; i++)
        {
            //Grab an object from the collection
            var gameObjectToSpawn = _pooledDictionary[id].Dequeue();
            
            //active where relevant
            gameObjectToSpawn.SetActive(true);

            //Get the interface connected to the "spawned" object and call the "Start" function
            gameObjectToSpawn.GetComponent<IPooledObject>()?.OnSpawn();

            //add the object back into the pool (first in, first out)
            //this allows the greatest degree of flexibility for objects not to de-spawn onscreen
            _pooledDictionary[id].Enqueue(gameObjectToSpawn);  
        }
    }

    public Queue<GameObject> GetPool(string id)
    {
        return _pooledDictionary.TryGetValue(id, out var q) ? q : null;
    }

    private void IncreasePool(string id, int amount)
    {
        for (var i = 0; i < amount; i++)
        {
            //Instantiate and queue a copy of an existing element in the queue
            var prefab = pools.First(pool => pool.id == id).prefab;
            var gO = Instantiate(prefab);
            gO.SetActive(false);
            
            _pooledDictionary[id].Enqueue(gO);
        }
    }
}
