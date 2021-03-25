using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    
    //this could be made private, but I want access to the Queue to avoid a FindObjectsOfType<FindNearestNeighbour>() call.
    public Dictionary<string, Queue<GameObject>> PooledDictionary;
    
    //Show in UI
    [SerializeField] private TextMeshProUGUI uiCounter;

    public void Start()
    {
        PooledDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                var gO = Instantiate(pool.prefab);
                gO.SetActive(false);
                objectPool.Enqueue(gO);
            }
            
            PooledDictionary.Add(pool.id, objectPool);
        }

        //Grab the cube instance counter and display in the UI
        uiCounter.text = $"0 / {PooledDictionary["Cube"].Count}";
    }

    public void SpawnFromPool(string id, int amount)
    {
        if (id == string.Empty || !PooledDictionary.ContainsKey(id))
        {
            Debug.LogWarning($"The tag {id} was not found in the dictionary.");
            return;
        }

        for (var i = 0; i < amount; i++)
        {
            //Grab an object from the collection
            var gameObjectToSpawn = PooledDictionary[id].Dequeue();
        
            //activate the object
            gameObjectToSpawn.SetActive(true);

            //Get the interface connected to the "spawned" object and call the "Start" function
            gameObjectToSpawn.GetComponent<IPooledObject>()?.OnSpawn();

            //add the object back into the pool (FI-FO principle)
            PooledDictionary[id].Enqueue(gameObjectToSpawn);  
        }

        //update GUI
        if (uiCounter && PooledDictionary.ContainsKey("Cube"))
        {
            uiCounter.text = $"{PooledDictionary["Cube"].Count(o => o.activeSelf)} / {PooledDictionary["Cube"].Count}";   
        }
    }
}
