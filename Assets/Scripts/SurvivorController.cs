using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SurvivorController : MonoBehaviour {

	private Vector3 myPosition;
	private GameObject[] resources;
	GameObject[] dropOffPoints;
	private NavMeshAgent nav;
	private GameObject target;
	private GameController gameController;
	int capacity;
	int maxCapacity;
	public int harvestQuantity;
	private float harvestDelay;
	private float harvestNext;

	void Start () {
		
		harvestQuantity = 10;
		harvestDelay = 1.0f;
		harvestNext = 0.5f;

		capacity = 0;
		maxCapacity = 85;

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

		dropOffPoints = GameObject.FindGameObjectsWithTag ("Drop Off");
		resources = GameObject.FindGameObjectsWithTag ("Resource");
		if (resources.Length > 0 && capacity < maxCapacity) {
			target = GetTarget (resources);
			nav.isStopped = false;
			nav.SetDestination (target.transform.position);
			if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending) {
				if (nav.velocity.sqrMagnitude == 0.0f) {
					Harvest (target);
				}
			}
		} else if (dropOffPoints.Length > 0) {
			target = GetTarget (dropOffPoints);
			nav.isStopped = false;
			nav.SetDestination (target.transform.position);
			if (nav.remainingDistance <= nav.stoppingDistance && !nav.pathPending) {
				if (nav.velocity.sqrMagnitude == 0.0f) {
					DropOff ();
				}
			}
		} else {
			nav.isStopped = true;
		}
	}

	void Harvest (GameObject resource) {
		if (Time.time >= harvestNext) {
			int thisHarvest;
			if (capacity + harvestQuantity < maxCapacity) {
				thisHarvest = harvestQuantity;
			} else {
				thisHarvest = maxCapacity - capacity;
			}
			ResourceController resourceController = resource.GetComponent<ResourceController> ();
			string type = resourceController.GetResourceType ();
			int gathered = resourceController.HarvestResource (thisHarvest);
			capacity += gathered;
			harvestNext = Time.time + harvestDelay;
		}
	}

	void DropOff() {
		gameController.ResourceChange ("wood", capacity);
		capacity = 0;
	}

	GameObject GetTarget (GameObject[] targets) {
		GameObject closest = null;
		float distance = Mathf.Infinity;
		foreach (GameObject target in targets) {
			Vector3 diff = target.transform.position - transform.position;
			float currDistance = diff.sqrMagnitude;
			if (currDistance < distance) {
				closest = target;
				distance = currDistance;
			}
		}
		return closest;
	}
}