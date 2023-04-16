using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightBank : LightSource
{
    [Header("Light bank configuration")]
    [SerializeField] float lightRegeneration;
    [SerializeField] float regenerationPeriod;

    List<IRechargeable> rechargeables;

    Dictionary<IRechargeable, float> regenerableEntitiesActive;

    private void Start()
    {
        regenerableEntitiesActive = new Dictionary<IRechargeable, float>();
        rechargeables = new List<IRechargeable>();
    }


    protected override void Update()
    {
        base.Update();
        rechargeables.Clear();
        List<Collider> hits = Physics.OverlapSphere(transform.position, currentSize).ToList().FindAll(x => x.GetComponent<IRechargeable>() is not null);

        foreach (Collider hit in hits)
        {
            IRechargeable recargeable = hit.GetComponent<IRechargeable>();
            rechargeables.Add(recargeable);
            if (!regenerableEntitiesActive.ContainsKey(recargeable))
            {
                regenerableEntitiesActive.Add(recargeable, regenerationPeriod);
            }
        }

        Dictionary<IRechargeable, float> copyEntities= new Dictionary<IRechargeable, float>(regenerableEntitiesActive);

        foreach (KeyValuePair<IRechargeable, float> kvp in copyEntities)
        {
            if (rechargeables.Contains(kvp.Key))
            {
                regenerableEntitiesActive[kvp.Key] = kvp.Value - Time.deltaTime;
                if (regenerableEntitiesActive[kvp.Key] <= 0)
                {
                    kvp.Key.Recharge(lightRegeneration);
                    regenerableEntitiesActive[kvp.Key] = regenerationPeriod;
                }
            }
            else
            {
                regenerableEntitiesActive.Remove(kvp.Key);
            }
        }
    }
}
