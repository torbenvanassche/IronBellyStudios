using System.Linq;
using UnityEngine;

public class FindNearestNeighbour : MonoBehaviour
{
    private Transform Find()
    {
        /* var pool = PoolController.Instance.GetPool("Cube");
        
        GameObject closest = null;
        var distance = Mathf.Infinity;
        
        foreach (var o in pool)
        {
            //Could also use TryGetComponent, but as the component isn't used here there is no need
            if (o.GetComponent<FindNearestNeighbour>() && o.activeSelf && o != gameObject)
            {
                var currDist = (o.transform.position - transform.position).sqrMagnitude;
                if (currDist < distance)
                {
                    closest = o;
                    distance = currDist;
                }
            }
        }

        return closest.transform;*/
        
        //The difference is that this sorts everything by distance, while more expensive it is very robust
        return PoolController.Instance.GetPool("Cube")
            .Where(o => o.GetComponent<FindNearestNeighbour>() && o.activeSelf)
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
