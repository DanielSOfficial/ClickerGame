using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuyMore : MonoBehaviour {

	public Text newItemAmount;
	public Text newGoldCost;
	public Text itemAmount;
	private int itemNumber;

	// Use this for initialization
	void Start () 
	{
		switch (gameObject.name) 
		{
		case "AppleMore":
			itemNumber = 0;
			break;
		case "NewsMore":
			itemNumber = 1;
			break;
		case "PhoneMore":
			itemNumber = 2;
			break;
		case "BandMore":
			itemNumber = 3;
			break;
		case "BookMore":
			itemNumber = 4;
			break;
		case "MineMore":
			itemNumber = 5;
			break;
		case "LabMore":
			itemNumber = 6;
			break;
		case "FootballMore":
			itemNumber = 7;
			break;
		case "PlaneMore":
			itemNumber = 8;
			break;
		}
		newGoldCost.text = Globals.formatNewItemCost(itemNumber);
	}

	void OnMouseDown()
	{
		double gold = Globals.getCurrentGold ();
		if (gold - Globals.getGoldCostForNewItem(itemNumber) >= 0 && Globals.getSlideEnabled(itemNumber)) 
		{
			Globals.incrementItemAmount (itemNumber);
		}
	}

	void Update () 
	{
		itemAmount.text = Globals.getItemAmount (itemNumber);
		newItemAmount.text = Globals.getBuyableAmount (itemNumber);
		newGoldCost.text = Globals.formatNewItemCost(itemNumber);
	}
}