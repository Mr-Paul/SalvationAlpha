using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour {

	public string resourceType;
	private int resourceCount;

	// Use this for initialization
	void Start () {
		resourceCount = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string GetResourceType() {
		return resourceType;
	}

	public int HarvestResource(int harvestQuantity) {
		int remainder = resourceCount - harvestQuantity;
		int bounty;
		if (remainder > 0) {
			bounty = harvestQuantity;
			resourceCount -= bounty;
		} else {
			bounty = resourceCount;
			resourceCount = 0;
			Destroy (gameObject);
		}
		return bounty;
	}
}
