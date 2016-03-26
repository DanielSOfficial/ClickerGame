using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldText : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () 
	{
		invokeMethod ();
	}

	void invokeMethod()
	{
		InvokeRepeating ("setText", 0.01f, 0.2f);
	}

	void setText()
	{
		double currentGoldAmount = Globals.getCurrentGold ();
		text.text = "$" + Globals.formatCurrentGold (currentGoldAmount);
	}
}
