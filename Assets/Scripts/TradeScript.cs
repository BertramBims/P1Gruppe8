using UnityEngine;

public class TradeScript : MonoBehaviour
{
    public ResourceAmount[] cost;
    public ResourceAmount[] purchase;

    public void DoTrade()
    {
        if (ResourceManager.Instance.TrySpend(cost))
        {
            for (int i = 0; i < purchase.Length; i++)
            {
                ResourceManager.Instance.Add(purchase[i].type, purchase[i].amount);
            }
        }
    }
}
