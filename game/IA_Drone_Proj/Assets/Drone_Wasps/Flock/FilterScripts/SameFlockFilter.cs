using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Flock/Filter/SameFlock")]
public class SameFlockFilter : FlockContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original){
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original){
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();
            if(itemAgent != null && (itemAgent.FlockGroup == agent.FlockGroup)){
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
