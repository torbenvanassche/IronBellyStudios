using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    //Potential for editor script to select string from existing keys in dictionary
    [SerializeField] private string id;

    [SerializeField] private int initialAmount = 0;

    //Could error if the PoolController Start has not been called (fix with script execution order in unity)
    //Alternatively WaitUntil and using a coroutine
    private void Start()
    {
        PoolController.Instance.SpawnFromPool(id, initialAmount);
    }
    
    public void Spawn(TMP_InputField input)
    {
        //Guaranteed as integer because of the settings on the input field, if needed can be changed to TryParse
        PoolController.Instance.SpawnFromPool(id, int.Parse(input.text));
    }
}
