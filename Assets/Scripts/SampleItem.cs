using System;
using UnityEngine;
using UnityEngine.UI;


public class SampleItem : MonoBehaviour {

	public Image itemItself;
	public Button button;
	public Text itemName;
	public Image icon;
	public Text effect;
	public Text costText;
	public double thisCost;
	public int multiplier;

		public SampleItem ()
		{
		}

	public void destroyItem()
	{
		Destroy (this);
	}

	}