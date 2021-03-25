using UnityEngine;

public class RandomPosition : MonoBehaviour, IPooledObject
{
    [SerializeField] private Vector2 xRange;
    [SerializeField] private Vector2 yRange;
    [SerializeField] private Vector2 zRange;

    //Custom start function
    public void OnSpawn()
    {
        var x = Random.Range(xRange.x, xRange.y);
        var y = Random.Range(yRange.x, yRange.y);
        var z = Random.Range(zRange.x, zRange.y);

        transform.position = new Vector3(x, y, z);
    }
}
