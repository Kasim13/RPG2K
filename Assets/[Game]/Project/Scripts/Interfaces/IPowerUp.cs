using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPowerUp : ICollectable
{
    void Execute();
    IEnumerator ExecuteCo();
    void Initialize(PowerUpBase powerUpBase);
    void Interup();
}
