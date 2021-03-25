using TMPro;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    //Potential for editor script to select string from existing keys in dictionary
    public string id;

    [SerializeField] private int initialAmount = 0;
    [SerializeField] private TextMeshProUGUI uiCounter;

    //Could error if the PoolController Start has not been called (fix with script execution order in unity)
    //Alternatively WaitUntil and using a coroutine on the Start method
    private void Start()
    {
        PoolController.Instance.SpawnFromPool(id, initialAmount);
        
        //Initialize UI
        uiCounter.text = $"{initialAmount} / {PoolController.Instance.GetPool(id).Count}";
    }
    
    public void Spawn(TMP_InputField input)
    {
        //Guaranteed as integer because of the settings on the input field, if needed can be changed to TryParse
        PoolController.Instance.SpawnFromPool(id, int.Parse(input.text));
        
        //Update UI (Can also be done in inspector if preferred)
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (uiCounter)
        {
            var pool = PoolController.Instance.GetPool(id);
            uiCounter.text = $"{pool.Count(o => o.activeSelf)} / {pool.Count}";   
        }
    }
}
