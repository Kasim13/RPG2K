using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rampa : MonoBehaviour
{
    public GameObject cube;
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (!cube)
            return;
        cube.GetComponent<Destruction>().Createcube.Invoke();

        //collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.up * 200, ForceMode.Impulse);

    }
}
