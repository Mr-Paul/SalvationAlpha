﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	
	void Update () {
		transform.Rotate (new Vector3(0.0f, 45, 0.0f) * Time.deltaTime);
	}
}
