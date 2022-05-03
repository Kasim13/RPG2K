using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUpBase : CollectableBase , IPowerUp
{
    public override void Collect()
    {
        var powerUpInUse = CharacterManager.Instance.Player.GetComponent<IPowerUp>();
        if (powerUpInUse != null)
            powerUpInUse.Interup();

        Use();
    }

    public virtual void Interup()
    {
        if (CharacterManager.Instance.Player.gameObject.GetComponent<SpeedBoost>() != null)
            Destroy(CharacterManager.Instance.Player.gameObject.GetComponent<SpeedBoost>());
        StopAllCoroutines();
        Destroy(this);
    }

    public void Initialize(PowerUpBase powerUpBase) { }


    public void Execute()
    {
        //EventManager.OnLevelFinish.RemoveListener(Dispose);
        StartCoroutine(ExecuteCo());
    }
    public abstract IEnumerator ExecuteCo();
}