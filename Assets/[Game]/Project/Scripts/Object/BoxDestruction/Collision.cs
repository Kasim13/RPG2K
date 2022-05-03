using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
	public float blastRadius;
	public float explosionPower;
	public LayerMask explosionLayers;

	private Collider[] hitColliders;

	//public GameObject Particles;

	private void OnCollisionEnter(UnityEngine.Collision col)
	{
        if (col.gameObject.transform.CompareTag("Destruction")) { Destroy(col.contacts[0].point); }		
		//Particles.gameObject.GetComponent<Renderer>().material = col.gameObject.GetComponent<MeshRenderer>().material;
		//Instantiate(Particles, col.transform.position, Quaternion.identity);
	}

	void Destroy(Vector3 explosionPoint)
	{
		hitColliders = Physics.OverlapSphere(explosionPoint, blastRadius, explosionLayers);

		foreach (Collider hitCol in hitColliders)
		{
			if (hitCol.GetComponent<Rigidbody>() == null)
			{
				hitCol.gameObject.AddComponent<DestroyCube>();

				hitCol.GetComponent<MeshRenderer>().enabled = true;
				hitCol.gameObject.AddComponent<Rigidbody>();


				hitCol.GetComponent<Rigidbody>().mass = 200;
				hitCol.GetComponent<Rigidbody>().isKinematic = false;

				hitCol.GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * 20;
				hitCol.GetComponent<Rigidbody>().AddExplosionForce(explosionPower, explosionPoint, blastRadius, 1, ForceMode.Impulse);
			}
		}
	}
}
