using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeableLight : LightSource, IRechargeable
{
    public void Recharge(float rechargeValue)
    {
        ChangeCurrentSize(rechargeValue);
    }
}
