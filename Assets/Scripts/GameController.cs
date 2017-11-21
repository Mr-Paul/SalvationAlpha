using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// Resources and UI Text
	public Text woodText;
	public Text stoneText;
	public Text metalText;
	private int wood;
	private int stone;
	private int metal;

	// Use this for initialization
	void Start () {
		wood = 0;
		stone = 0;
		metal = 0;
	}
	
	// Update is called once per frame
	void Update () {
		TextUpdate ();
	}

	void TextUpdate () {
		woodText.text = "Wood: " + wood.ToString();
		stoneText.text = "Stone: " + stone.ToString ();
		metalText.text = "Metal: " + metal.ToString ();
	}

	public void ResourceChange (string type, int qty) {
		switch (type) {
		case "wood":
			wood += qty;
			break;
		case "stone":
			stone += qty;
			break;
		case "metal":
			metal += qty;
			break;
		}
	}
}
