using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class Item
{
	public Sprite icon;
	public string itemName;
	public string effect;
	public string costText;
	public int multiplier;
	public double thisCost;
	public Button.ButtonClickedEvent thingToDo;
}

public class FillList : MonoBehaviour {

	public GameObject sampleItem;
	public List<Item> itemList;

	public Transform contentPanel;

	// Use this for initialization
	void Start () {
		populateList ();
	}

	void populateList()
	{
		foreach (var item in itemList) 
		{
			GameObject newImage = Instantiate (sampleItem) as GameObject;
			SampleItem itemItself = newImage.GetComponent<SampleItem> ();
			itemItself.itemName.text = item.itemName;
			itemItself.icon.sprite = item.icon;
			itemItself.effect.text = item.effect;
			itemItself.costText.text = item.costText;
			itemItself.multiplier = item.multiplier;
			itemItself.thisCost = item.thisCost;
			itemItself.button.onClick = item.thingToDo;
			itemItself.transform.SetParent (contentPanel);
		}
	}

	public void updateProfitMultiplier(string name)
	{
		Item item = itemList.Where (obj => obj.itemName == name).SingleOrDefault ();

		if (Globals.getCurrentGold() - item.thisCost >= 0) 
		{
			Globals.updateProfitMultiplier (item, item.multiplier, item.thisCost);
			foreach (Transform child in contentPanel) 
			{
				GameObject.Destroy (child.gameObject);
			}
			itemList.Remove(item);
			populateList ();
		}
	}

	public void BoughtAuto(string name)
	{
		Item item = itemList.Where (obj => obj.itemName == name).SingleOrDefault ();


		if (Globals.getCurrentGold () - item.thisCost >= 0) 
		{
			Globals.setAuto (item, item.thisCost);
			if (itemList.Count != 0) 
			{
				foreach (Transform child in contentPanel) 
				{
					GameObject.Destroy (child.gameObject);
				}
				itemList.Remove(item);
				populateList ();
			}
		}
	}

}