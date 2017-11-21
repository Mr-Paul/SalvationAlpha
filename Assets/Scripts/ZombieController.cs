using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour {

	GameObject[] survivors;
	NavMeshAgent nav;
	GameObject closest;
	float distance;
	GameController gameController;

	// Use this for initialization
	void Start () {
		nav = GetComponent<NavMeshAgent> ();
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	// Update is called once per frame
	void Update () {
		survivors = GameObject.FindGameObjectsWithTag ("Survivor");
		if (survivors.Length > 0) {
			closest = null;
			distance = Mathf.Infinity;
			foreach (GameObject survivor in survivors) {
				Vector3 diff = survivor.transform.position - transform.position;
				float currDistance = diff.sqrMagnitude;
				if (currDistance < distance) {
					closest = survivor;
					distance = currDistance;
				}
			}
			nav.isStopped = false;
			nav.SetDestination (closest.transform.position);
			if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending) {
				if (nav.velocity.sqrMagnitude == 0.0f) {
					Destroy (closest);
				}
			}
		} else {
			nav.isStopped = true;
		}
	}
}