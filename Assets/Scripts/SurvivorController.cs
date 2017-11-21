using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SurvivorController : MonoBehaviour {

	private Vector3 myPosition;
	private GameObject[] resources;
	private NavMeshAgent nav;
	private GameObject closest;
	private float distance;
	private GameController gameController;
	public int harvestQuantity;
	private float harvestDelay;
	private float harvestNext;

	void Start () {
		harvestQuantity = 10;
		harvestDelay = 1.0f;
		harvestNext = 0.5f;
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
					Harvest (closest);
				}
			}
		} else {
			nav.isStopped = true;
		}
	}

	void Harvest (GameObject resource) {
		if (Time.time >= harvestNext) {
			ResourceController resourceController = resource.GetComponent<ResourceController> ();
			string type = resourceController.GetResourceType ();
			int gathered = resourceController.HarvestResource (10);
			gameController.ResourceChange (type, gathered);
			harvestNext = Time.time + harvestDelay;
		}
	}
}