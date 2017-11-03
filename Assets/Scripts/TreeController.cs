using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour {

	private bool indicatorActive;
	private float proximity;
	private GameObject player;
	public GameObject indicator;
	private GameObject thisIndicator;

	void Start () {
		indicatorActive = false;
		proximity = 3;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	void Update () {
		if ((Vector3.Distance (transform.position, player.transform.position) <= proximity)) {
			if (!indicatorActive) {
				thisIndicator = Instantiate (indicator, transform);
				indicatorActive = true;
			}
			if (Input.GetKey(KeyCode.E)) {
				DestroyObject(gameObject);
			}
		} else {
			if (indicatorActive) {
				Destroy(thisIndicator);
				indicatorActive = false;
			}
		}
	}
}