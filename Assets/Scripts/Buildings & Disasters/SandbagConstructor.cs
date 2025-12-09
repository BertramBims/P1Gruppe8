using System;
using Mono.Cecil;
using UnityEngine;

public class SandbagConstructor : MonoBehaviour
{
    public Disaster TriggerThis;
    public BuildingType SandbagPricing; 
 
    public void BuildSandbags()
    {
        
        if(!ResourceManager.Instance.TrySpend(SandbagPricing.buildCost))
        {
            Debug.Log("Not enough resources!");
            return;
        }

        DisasterManager.Instance.TriggerDisaster(TriggerThis);
    } 
}
