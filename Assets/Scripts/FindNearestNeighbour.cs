using System.Linq;
using UnityEngine;

public class FindNearestNeighbour : MonoBehaviour
{
    private Transform Find()
    {
        return PoolController.Instance.PooledDictionary["Cube"]
            .Where(o => o.GetComponent<FindNearestNeighbour>())
            .OrderBy(t => (t.transform.position - transform.position).sqrMagnitude)
            .First(neighbour => neighbour.transform != transform).transform;
    }
    
    private void OnDrawGizmos()
    {
        //Prevent errors when this runs while not in play mode (while inspecting prefab or manually adding to scene)
        if (!Application.isPlaying) return;

        var other = Find();
        if(other) Gizmos.DrawLine(transform.position, other.position);
    }
}
