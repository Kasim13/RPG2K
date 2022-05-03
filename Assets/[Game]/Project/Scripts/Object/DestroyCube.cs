using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCube : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SelfDestruct());
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.8f);
        this.gameObject.GetComponent<Collider>().isTrigger = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
