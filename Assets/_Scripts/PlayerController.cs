using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shoot;
	public Transform shootSpaw;
	public float fireRate = 0.5f;

	float nextFire = 0.0f;

	public bool isFisica = false;


	void Update()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire ) 
		{ 
			nextFire = Time.time+fireRate;
			Instantiate (shoot, shootSpaw.position, shoot.transform.rotation);
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 moviment = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		if(isFisica)
			GetComponent<Rigidbody> ().velocity = moviment * speed;
		else
			//transform.position += moviment * speed * Time.deltaTime;
			GetComponent<Rigidbody> ().position += moviment * speed * Time.deltaTime;

		//Limitar o player aos limites do jogo
		GetComponent<Rigidbody> ().position = new Vector3
		(
			Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);

		GetComponent<Rigidbody> ().rotation = Quaternion.Euler (GetComponent<Rigidbody> ().velocity.z * (tilt/4), 0.0f, GetComponent<Rigidbody> ().velocity.x * -tilt);
	}
}
