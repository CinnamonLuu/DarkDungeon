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

    protected override void Start()
    {
        base.Start();
        regenerableEntitiesActive = new Dictionary<IRechargeable, float>();
        rechargeables = new List<IRechargeable>();
    }


    protected override void Update()
    {
        base.Update();
        rechargeables.Clear();
        List<Collider2D> hits = Physics2D.OverlapCircleAll(transform.position, currentSize / 2f).ToList().FindAll(x => x.GetComponentInChildren<IRechargeable>() is not null);
        foreach (Collider2D hit in hits)
        {
            IRechargeable recargeable = hit.GetComponentInChildren<IRechargeable>();
            rechargeables.Add(recargeable);
            if (!regenerableEntitiesActive.ContainsKey(recargeable))
            {
                regenerableEntitiesActive.Add(recargeable, regenerationPeriod);
            }
        }

        Dictionary<IRechargeable, float> copyEntities = new Dictionary<IRechargeable, float>(regenerableEntitiesActive);

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
