using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResetStuff : MonoBehaviour {

	public Text currentInvestors;

	void OnMouseDown()
	{
		if (Globals.getCurrentInvestors() > 0.99999999d) 
		{
			Globals.resetStuff ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		currentInvestors.text = Globals.getCurrentInvestorsFormatted ();
	}
}
