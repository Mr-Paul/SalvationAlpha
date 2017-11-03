using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// Resources and UI Text
	public Text woodText;
	private int wood;

	// Use this for initialization
	void Start () {
		wood = 0;
	}
	
	// Update is called once per frame
	void Update () {
		TextUpdate ();
	}

	void TextUpdate () {
		woodText.text = "Wood: " + wood.ToString();
	}

	public void ResourceChange (string type, int qty) {
		switch (type) {
		case "wood":
			wood += qty;
			break;
		case "stone":
			break;
		}
	}
}
