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
	Animator anim;
	public float attackRange;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
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
		bool attacking = false;
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
			if ((closest.transform.position - transform.position).sqrMagnitude < attackRange) {
				attacking = true;
				//Destroy (closest);
			}
		} else {
			nav.isStopped = true;
			anim.SetTrigger ("Die");
		}
		Attacking (attacking);
	}

	void Attacking (bool attacking) {
		anim.SetBool ("IsAttacking", attacking);
	}
}