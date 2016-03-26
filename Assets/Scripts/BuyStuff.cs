using UnityEngine;
using System.Collections;

public class BuyStuff : MonoBehaviour {

	public SpriteRenderer rend;
	public SpriteRenderer rend2;
	private int boxNumber = 0;
	private double cost = 0;

	void Start()
	{
		rend = gameObject.GetComponent<SpriteRenderer> ();
		rend2.color = new Color (0, 0, 0, 1);

		switch (gameObject.name) 
		{
		case "AppleBox":
			rend.color = new Color32 (255, 215, 0, 255);
			rend2.color = new Color (1, 1, 1, 1);
			boxNumber = 0;
			Globals.updateBought (0);
			break;
		case "NewsBox":
			boxNumber = 1;
			cost = 55f;
			break;
		case "PhoneBox":
			boxNumber = 2;
			cost = 600f;
			break;
		case "BandBox":
			boxNumber = 3;
			cost = 7200f;
			break;
		case "BookBox":
			boxNumber = 4;
			cost = 86400f;
			break;
		case "MineBox":
			boxNumber = 5;
			cost = 1036800f;
			break;
		case "LabBox":
			boxNumber = 6;
			cost = 12441600f;
			break;
		case "FootballBox":
			boxNumber = 7;
			cost = 149299200f;
			break;
		case "PlaneBox":
			boxNumber = 8;
			cost = 1791590400f;
			break;
		}
	}

	void OnMouseDown()
	{
		if (!Globals.getBought(boxNumber) && Globals.getCurrentGold() - cost >= 0 && Globals.getWindowOpen() == false) 
		{
			Globals.changeBoughtSomething (cost, boxNumber);
			Globals.updateBought (boxNumber);
			rend2.color = new Color32 (255, 255, 255, 255);
		} 
		else
		{
			Globals.fillIt (boxNumber);
		}
	}
}