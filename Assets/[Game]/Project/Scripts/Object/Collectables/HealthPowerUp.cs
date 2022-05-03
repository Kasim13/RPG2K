using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerUp : PowerUpBase
{
    public override void Collect()
    {
        Use();
    }

    public override void Use()
    {
        CharacterManager.Instance.Player.GetComponent<IHealable>().Heal();
        Dispose();
    }

    public override IEnumerator ExecuteCo()
    {
        throw new System.NotImplementedException();
    }
}
