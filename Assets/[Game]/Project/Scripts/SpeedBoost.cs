using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PowerUpBase
{
    public override IEnumerator ExecuteCo()
    {
        GameManager.Instance.GameData.IsBoostSpeed = 12.0f;
        yield return new WaitForSeconds(GameManager.Instance.GameData.BoostSpeedTimer);
        GameManager.Instance.GameData.IsBoostSpeed = 0.0f;
        Destroy(this);

        yield return null;
    }

    public override void Collect()
    {
        base.Collect();
    }

    public override void Use()
    {
        SpeedBoost speedBoost = CharacterManager.Instance.Player.gameObject.AddComponent<SpeedBoost>();
        speedBoost.Initialize(this);
        speedBoost.Execute();
        Dispose();
    }

    public override void Interup()
    {
        base.Interup();
        GameManager.Instance.GameData.IsBoostSpeed = 0.0f;
    }
}