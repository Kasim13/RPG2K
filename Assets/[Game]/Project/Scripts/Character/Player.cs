using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;

public class Player : MonoBehaviour
{
    private Rigidbody rigidbody;
    public Rigidbody Rigidbody { get { return (rigidbody == null) ? rigidbody = GetComponent<Rigidbody>() : rigidbody; } }

    protected float speedLimit = 8.0f;
    protected float MoveSpeed = 15.0f;

    private void FixedUpdate()
    {
        Debug.Log(GameManager.Instance.GameData.IsBoostSpeed);
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //Rigidbody.velocity = input * MoveSpeed * Time.fixedDeltaTime;
        Rigidbody.AddForce(input * (MoveSpeed + GameManager.Instance.GameData.IsBoostSpeed) * Time.fixedDeltaTime, ForceMode.Impulse);

        if (Rigidbody.velocity.magnitude > (speedLimit - 3))
        {
            Rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, (speedLimit + GameManager.Instance.GameData.IsBoostSpeed));
        }
    }
}

