using System;
using Mono.Cecil;
using UnityEngine;

public class SandbagConstructor : MonoBehaviour
{
    public Disaster TriggerThis;
    private float ResourceAmount.amount TestPrice = 425f;
    private ResourceAmount[] PriceOfSand = ResourceType.Pesos, ResourceAmount. ; 

    public void BuildSandbags()
    {
        
        if(!ResourceManager.Instance.TrySpend(PriceOfSand))
        {
            Debug.Log("Not enough resources!");
            return;
        }

        DisasterManager.Instance.TriggerDisaster(TriggerThis);
    } 
}
