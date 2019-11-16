using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController> ();
		else
			Debug.Log ("Cannot find 'GameController' script");
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary") 
			return;

		if (other.tag == "Player") 
		{
			gameController.GameOver();
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
		}

		Instantiate (explosion, transform.position, transform.rotation);

		gameController.AddScore (scoreValue);

		Destroy(other.gameObject);
		Destroy (gameObject);
	}
}
