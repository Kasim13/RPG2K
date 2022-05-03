using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destruction : MonoBehaviour
{
	public UnityEvent Createcube = new UnityEvent();

	public GameObject mesh;
	float cubeWidth;
	float cubeHeight;
	float cubeDepth;

	public float cubeScale = 0.3f;
	public float cubez = 0.3f;

    private void OnEnable()
    {
		Createcube.AddListener(CreateCube);
    }
    private void OnDisable()
    {
		Createcube.RemoveListener(CreateCube);
    }

    void Start()
	{
		cubeWidth = transform.localScale.z;
		cubeHeight = transform.localScale.y;
		cubeDepth = transform.localScale.x;
		
		mesh.gameObject.GetComponent<Transform>().localScale = new Vector3(cubeScale, cubeScale, cubez);
	}

 //   private void OnCollisionEnter(UnityEngine.Collision collision)
 //   {
	//	Createcube.Invoke();
	//}
    void CreateCube()
	{		
		this.gameObject.SetActive(false);
		//gameObject.GetComponent<MeshRenderer>().enabled = false;
		//this.gameObject.GetComponent<BoxCollider>().enabled = false;
		//this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

		if (gameObject.CompareTag("Destruction"))
		{
			for (float x = (-cubeWidth / 2); x < (cubeWidth/2); x += cubeScale)
			{
				for (float y = (-cubeHeight / 2); y < (cubeHeight/2); y += cubeScale)
				{
					for (float z = (-cubeDepth / 2); z < (cubeDepth/2); z += cubeScale)
					{
						Vector3 vec = transform.position;

						GameObject cubes = (GameObject)Instantiate(mesh, vec + new Vector3(x, y, z), Quaternion.identity);
						cubes.transform.parent = this.gameObject.transform.root;
						//cubes.AddComponent<DestroyCube>();
						//cubes.gameObject.GetComponent<MeshRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
					}
				}
			}
		}	
	}
}
