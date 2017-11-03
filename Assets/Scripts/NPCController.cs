using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour {

	private Vector3 myPosition;
	private GameObject[] resources;
	private NavMeshAgent nav;
	private GameObject closest;
	private float distance;
	//private Vector3 lastPosition;

	void Start () {
		nav = GetComponent<NavMeshAgent> ();
	}
	
	void Update () {
		
		resources = GameObject.FindGameObjectsWithTag ("Resource");
		if (resources.Length > 0) {
			closest = null;
			distance = Mathf.Infinity;
			foreach (GameObject resource in resources) {
				Vector3 diff = resource.transform.position - transform.position;
				float currDistance = diff.sqrMagnitude;
				if (currDistance < distance) {
					closest = resource;
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
		//lastPosition = transform.position;
	}
}