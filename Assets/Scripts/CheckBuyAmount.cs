using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckBuyAmount : MonoBehaviour {

	public Text amountText;

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown()
	{
		Globals.changeAmountToBuy();

	}
	
	// Update is called once per frame
	void Update () {
	
		amountText.text = "Buy\n" + Globals.getAmountToBuyText ();
	}
}
