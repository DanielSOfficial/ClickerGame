using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Globals : MonoBehaviour {

	private static double currentGold;
	private static bool firstTime = true;
	private static double totalGold;
	public Slider[] sliderz;
	public static Globals instance;
	private bool[] slidezActivated;
	private float[] secToFinish = {1f, 4f, 16f, 64f, 300f, 1350f, 5400f, 21600f, 86399f};
	private float[] pseudoTimers = { 1f, 4f, 16f, 64f, 300f, 1350f, 5400f, 21600f, 86399f};
	private bool[] slideAuto;
	public Text[] timeTexts;
	public Text[] moneyTexts;
	private int[] itemAmount;
	private double[] baseMoneyPerItem = {3.00f, 55f, 450f, 3600f, 43200f, 518400f, 6220800f, 74649600f, 895795200f};
	public Text[] priceTexts;
	private float[] baseMultipliers = {1.065f, 1.133f, 1.123f, 1.113f, 1.103f, 1.093f, 1.083f, 1.073f, 1.065f};
	private double[] baseItemCost = {4.5d, 55d, 600d, 7200d, 86400d, 1036800d, 12441600d, 149299200d, 1791590400d};
	private double[] costForNewItem = {4.5d, 55d, 600d, 7200d, 86400d, 1036800d, 12441600d, 149299200d, 1791590400d};
	private double currentInvestors = 0d;
	private double totalInvestors = 0d;
	private bool[] bought;
	public SpriteRenderer[] rends;
	private double[] profitMultipliers;
	private bool[] itemAmountConditionMet;
	private bool[] totalAmountConditionMet;
	private bool windowOpen = false;
	private int amountToBuy = 1;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		if (firstTime) 
		{
			currentGold = 0d;
			totalGold = currentGold;
		} 
		else
		{
			//Implement way to get gold if not first
			//time playing the game.
		}

		slidezActivated = new bool[9];
		slideAuto = new bool[9];
		itemAmount = new int[9];
		bought = new bool[9];
		profitMultipliers = new double[9];
		itemAmountConditionMet = new bool[90];
		totalAmountConditionMet = new bool[10];
		resetArrays ();
	}

	public static void changeAmountToBuy()
	{
		if (instance.amountToBuy == 1) 
		{
			instance.amountToBuy = 10;
		} 
		else if (instance.amountToBuy == 10) 
		{
			instance.amountToBuy = 50;
		}
		else if (instance.amountToBuy == 50) 
		{
			instance.amountToBuy = 100;
		}
		else if (instance.amountToBuy == 100) 
		{
			instance.amountToBuy = 42;
		}
		else if (instance.amountToBuy == 42) 
		{
			instance.amountToBuy = 1;
		}
	}

	public static string getAmountToBuyText()
	{
		if (instance.amountToBuy == 42) 
		{
			return "Max";
		} 
		else 
		{
			return instance.amountToBuy.ToString ();
		}
	}
		
	void Update () 
	{
		for (int i = 0; i < instance.sliderz.Length; i++) 
		{
			if (instance.slidezActivated [i] == true || instance.slideAuto [i] == true)
			{
				instance.sliderz [i].value += (float) ((1 / secToFinish [i]) * Time.deltaTime);
				instance.timeTexts [i].text = System.TimeSpan.FromSeconds (Mathf.Round (pseudoTimers[i] -= Time.deltaTime)).ToString();

				if (instance.sliderz [i].value == 1) 
				{
					instance.sliderz [i].value = 0;
					instance.setPseudoTimer (i);
					updateGold (i);
					instance.slidezActivated [i] = false;
				}
			}
			instance.moneyTexts [i].text = formatTotalAmount(i);
		}
		currentInvestors = (totalGold / 100000000000000000);
	}

	public static void setWindowOpenTrue()
	{
		instance.windowOpen = true;
	}

	public static void setWindowOpenFalse()
	{
		instance.windowOpen = false;
	}

	public static bool getWindowOpen()
	{
		return instance.windowOpen;
	}

	public static void updateBought(int i)
	{
			if (i == 0) { /*Do nothing, as it should always be activated*/
			} 
			else if (instance.bought [i] == true) 
			{
				instance.bought [i] = false;
				instance.priceTexts [i].enabled = true;
			} 
			else 
			{
				instance.bought [i] = true;
				instance.priceTexts [i].enabled = false;
			}
	}

	public static bool getBought(int i)
	{
		return instance.bought [i];
	}

	public static void resetStuff()
	{
		instance.totalInvestors += instance.currentInvestors;
		instance.currentInvestors = 0d;
		instance.sliderz [0].value = 0f;
		currentGold = 0f;
		totalGold = 0f;

		instance.resetArrays ();
	}

	public static string getCurrentInvestorsFormatted()
	{
		return instance.textFormat (instance.currentInvestors);
	}

	public static void updateProfitMultiplier(Item item, int multiplier, double cost)
	{
		int itemNumber = 0;
		switch (item.itemName) 
		{
		case "Apple Pies":
			itemNumber = 0;
			break;
		case "Gadget section":
			itemNumber = 1;
			break;
		case "Smartphone repair":
			itemNumber = 2;
			break;
		case "Get roadies":
			itemNumber = 3;
			break;
		case "Paper thin paper":
			itemNumber = 4;
			break;
		case "Mine iron":
			itemNumber = 5;
			break;
		case "Steffen Eagling":
			itemNumber = 6;
			break;
		case "1. Division":
			itemNumber = 7;
			break;
		case "Expand routes":
			itemNumber = 8;
			break;
		}
		currentGold -= cost;
		instance.profitMultipliers [itemNumber] *= multiplier;
	}

	public static double getCurrentInvestors()
	{
		return instance.currentInvestors;
	}

	public static bool getSlideEnabled(int i)
	{
		return instance.sliderz [i].enabled;
	}

	public static double getGoldCostForNewItem(int i)
	{
		return instance.getNewItemCost(i);
	}

	public static string getItemAmount(int i)
	{
		return "" + instance.itemAmount [i];
	}

	public static string getBuyableAmount(int i)
	{
		if (instance.amountToBuy == 42) 
		{
			return  "Buy\nx" + instance.getMaxItemBuyable(i);
		}
		else 
		{
			return "Buy\nx" + instance.amountToBuy;
		}
	}

	public static string formatNewItemCost(int i)
	{
		double totalAmount = instance.getNewItemCost(i);

		return instance.textFormat (totalAmount);
	}

	public static void incrementItemAmount(int i)
	{
		if (instance.amountToBuy == 42) 
		{
			instance.itemAmount [i] += instance.getMaxItemBuyable (i);
		} 
		else 
		{
			instance.itemAmount [i] += instance.amountToBuy;
		}

		currentGold -= instance.getNewItemCost(i);
		instance.costForNewItem [i] = instance.baseItemCost [i] * Mathf.Pow (instance.baseMultipliers[i], instance.itemAmount [i]);

		instance.checkForBonus (i);

	}

	public static string formatCurrentGold(double currentGoldAmount)
	{
		return instance.textFormat (currentGoldAmount);
	}

	public static double getCurrentGold()
	{
		return currentGold;
	}

	public static void updateGold(int i)
	{
		double investorBonus = 1 + (instance.totalInvestors / 100);

		currentGold += instance.baseMoneyPerItem[i] * instance.itemAmount[i] * investorBonus * instance.profitMultipliers[i];
		totalGold += instance.baseMoneyPerItem[i] * instance.itemAmount[i] * investorBonus * instance.profitMultipliers[i];
	}

	public static void fillIt(int slideToFill)
	{
		instance.slidezActivated [slideToFill] = true;
	}

	public static void changeBoughtSomething(double goldAmount, int i)
	{
			instance.sliderz [i].enabled = true;
			instance.sliderz [i].gameObject.SetActive (true);
			instance.timeTexts [i].color = new Color (1, 1, 1, 1);
			instance.moneyTexts [i].color = new Color (0, 0, 0, 1);
			instance.itemAmount [i] += 1;

		currentGold -= goldAmount;
		instance.rends[i].color = new Color32 (255, 215, 0, 255);
	}

	public static void setAuto(Item item, double cost)
	{
		int itemNumber = 0;
		switch (item.itemName) 
		{
		case "Apple stand Manager":
			itemNumber = 0;
			break;
		case "News website Manager":
			itemNumber = 1;
			break;
		case "Phone repair Manager":
			itemNumber = 2;
			break;
		case "Band Manager":
			itemNumber = 3;
			break;
		case "Publisher":
			itemNumber = 4;
			break;
		case "Mining Manager":
			itemNumber = 5;
			break;
		case "Lab Manager":
			itemNumber = 6;
			break;
		case "Football Manager":
			itemNumber = 7;
			break;
		case "Airliner Manager":
			itemNumber = 8;
			break;
		}
		currentGold -= cost;
		instance.slideAuto [itemNumber] = true;
	}



/*
 * 
 * 
 *
 *
 *
 *
 *
 * ------------ Private, fugly, functions --------------
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */

	private double getNewItemCost(int i)
	{

		float sum = 0;
		int buyableAmount = 0;

		if (instance.amountToBuy == 42) 
		{
			buyableAmount = instance.getMaxItemBuyable (i);
		}
		else 
		{
			buyableAmount = instance.amountToBuy;
		}

		for(float j = 0f; j < buyableAmount; j++)
		{
			sum += (float) instance.costForNewItem[i] * Mathf.Pow(1f + (instance.baseMultipliers[i]-1f), j);
		}

		return (double) sum;
	}

	private int getMaxItemBuyable(int i)
	{
		float sum = 0;
		for(float j = 0f; j < 100000f; j++)
		{
			sum += (float) instance.costForNewItem[i] * Mathf.Pow(1f + (instance.baseMultipliers[i]-1f), j);

			if (currentGold - sum <= 0 && j != 0f) 
			{
				return (int) j;
			}
		}

		return 1;
	}

	private void checkForBonus(int i)
	{
		if(instance.itemAmount[i] >= 500 && instance.itemAmountConditionMet[i+54] == false)
		{
			instance.checkTotalBonus ();
			instance.pseudoTimers[i] /= 2;
			instance.secToFinish [i] /= 2;
			instance.itemAmountConditionMet [i+54] = true;
		}

		if(instance.itemAmount[i] >= 400 && instance.itemAmountConditionMet[i+45] == false)
		{
			instance.checkTotalBonus ();
			instance.pseudoTimers[i] /= 2;
			instance.secToFinish [i] /= 2;
			instance.itemAmountConditionMet [i+45] = true;
		}

		if(instance.itemAmount[i] >= 300 && instance.itemAmountConditionMet[i+36] == false)
		{
			instance.checkTotalBonus ();
			instance.pseudoTimers[i] /= 2;
			instance.secToFinish [i] /= 2;
			instance.itemAmountConditionMet [i+36] = true;
		}

		if(instance.itemAmount[i] >= 200 && instance.itemAmountConditionMet[i+27] == false)
		{
			instance.checkTotalBonus ();
			instance.pseudoTimers[i] /= 2;
			instance.secToFinish [i] /= 2;
			instance.itemAmountConditionMet [i+27] = true;
		}

		if(instance.itemAmount[i] >= 100 && instance.itemAmountConditionMet[i+18] == false)
		{
			instance.checkTotalBonus ();
			instance.pseudoTimers[i] /= 2;
			instance.secToFinish [i] /= 2;
			instance.itemAmountConditionMet [i+18] = true;
		}

		if(instance.itemAmount[i] >= 50 && instance.itemAmountConditionMet[i+9] == false)
		{
			instance.checkTotalBonus ();
			instance.pseudoTimers[i] /= 2;
			instance.secToFinish [i] /= 2;
			instance.itemAmountConditionMet [i+9] = true;
		}

		if(instance.itemAmount[i] >= 25 && instance.itemAmountConditionMet[i] == false)
		{
			instance.checkTotalBonus ();
			instance.pseudoTimers[i] /= 2;
			instance.secToFinish [i] /= 2;
			instance.itemAmountConditionMet [i] = true;
		}
	}

	private void checkTotalBonus()
	{
		int lowestAmount = 100000;
		for (int i = 0; i < 9; i++) 
		{
			if (instance.itemAmount [i] < lowestAmount) 
			{
				lowestAmount = instance.itemAmount [i];
			}
		}


		if (lowestAmount >= 500 && instance.totalAmountConditionMet[7] != true) 
		{
			updateAllMultipliers ();
			instance.totalAmountConditionMet [7] = true;
		}

		if (lowestAmount >= 400 && instance.totalAmountConditionMet[6] != true) 
		{
			updateAllMultipliers ();
			instance.totalAmountConditionMet [6] = true;
		}

		if (lowestAmount >= 300 && instance.totalAmountConditionMet[5] != true) 
		{
			updateAllMultipliers ();
			instance.totalAmountConditionMet [5] = true;
		}

		if (lowestAmount >= 200 && instance.totalAmountConditionMet[4] != true) 
		{
			updateAllMultipliers ();
			instance.totalAmountConditionMet [4] = true;
		}

		if (lowestAmount >= 100 && instance.totalAmountConditionMet[3] != true) 
		{
			updateAllMultipliers ();
			instance.totalAmountConditionMet [3] = true;
		}

		if (lowestAmount >= 50 && instance.totalAmountConditionMet[2] != true) 
		{
			updateAllMultipliers ();
			instance.totalAmountConditionMet [2] = true;
		}

		if (lowestAmount >= 25 && instance.totalAmountConditionMet[1] != true) 
		{
			updateAllMultipliers ();
			instance.totalAmountConditionMet [1] = true;
		}

		if (lowestAmount >= 1 && instance.totalAmountConditionMet[0] != true) 
		{
			updateAllMultipliers ();
			instance.totalAmountConditionMet [0] = true;
		}
	}

	private void updateAllMultipliers()
	{
		for (int i = 0; i < 9; i++) 
		{
			instance.profitMultipliers [i] *= 2;
		}
	}

	private void setPseudoTimer(int i)
	{
		pseudoTimers [i] = secToFinish [i];
	}

	private string formatTotalAmount(int i)
	{
		double investorBonus = 1 + (instance.totalInvestors / 100);
		double totalAmount = (double) itemAmount[i] * baseMoneyPerItem[i] * investorBonus * profitMultipliers[i];

		return textFormat (totalAmount);
	}

	private void resetArrays()
	{
		for (int i = 0; i < instance.sliderz.Length; i++) 
		{
			instance.bought [i] = false;
			instance.sliderz [i].gameObject.SetActive (false);
			instance.sliderz [i].enabled = false;
			instance.slidezActivated [i] = false;
			instance.slideAuto [i] = false;
			instance.timeTexts [i].color = new Color (1, 1, 1, 0);
			instance.moneyTexts [i].color = new Color (1, 1, 1, 0);
			instance.costForNewItem[i] = instance.baseItemCost[i];
			instance.itemAmount [i] = 0;
			instance.sliderz [i].value = 0.0f;
			instance.rends [i].color = new Color (1, 1, 1, 1);
			instance.profitMultipliers [i] = 1;
		}

		for (int i = 0; i < 90; i++)
		{
			if (i < 10) 
			{
				totalAmountConditionMet [i] = false;
			}
			itemAmountConditionMet [i] = false;
		}

		instance.rends [0].color = new Color32 (255, 215, 0, 255);
		instance.bought [0] = true;
		instance.itemAmount [0] = 1;
		instance.sliderz [0].enabled = true;
		instance.sliderz [0].gameObject.SetActive (true);
		instance.timeTexts [0].color = new Color (1, 1, 1, 1);
		instance.moneyTexts [0].color = new Color (0, 0, 0, 1);
	}

	private string textFormat(double totalAmount)
	{
		if (totalAmount < 100000) 
		{
			return totalAmount.ToString ("F2");
		}
		else if (totalAmount > 999999999999999999999999999999999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000000000000000000000000000000000d).ToString ("F3") + " Vigintillion";
		}
		else if (totalAmount > 999999999999999999999999999999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000000000000000000000000000000d).ToString ("F3") + " Novendecillion";
		}
		else if (totalAmount > 999999999999999999999999999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000000000000000000000000000d).ToString ("F3") + " Octodecillion";
		}
		else if (totalAmount > 999999999999999999999999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000000000000000000000000d).ToString ("F3") + " Septendecillion";
		}
		else if (totalAmount > 999999999999999999999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000000000000000000000d).ToString ("F3") + " Sedecillion";
		}
		else if (totalAmount > 999999999999999999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000000000000000000d).ToString ("F3") + " Quindecillion";
		}
		else if (totalAmount > 999999999999999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000000000000000d).ToString ("F3") + " Quattuordecillion";
		}
		else if (totalAmount > 999999999999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000000000000d).ToString ("F3") + " Tredecillion";
		}
		else if (totalAmount > 999999999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000000000d).ToString ("F3") + " Duodecillion";
		}
		else if (totalAmount > 999999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000000d).ToString ("F3") + " Undecillion";
		}
		else if (totalAmount > 999999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000000d).ToString ("F3") + " Decillion";
		}
		else if (totalAmount > 999999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000000d).ToString ("F3") + " Nonillion";
		}
		else if (totalAmount > 999999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000000d).ToString ("F3") + " Octillion";
		}
		else if (totalAmount > 999999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000000d).ToString ("F3") + " Septillion";
		}
		else if (totalAmount > 999999999999999999999d) 
		{
			return (totalAmount / 1000000000000000000000d).ToString ("F3") + " Sextillion";
		}
		else if (totalAmount > 999999999999999999) 
		{
			return (totalAmount / 1000000000000000000).ToString ("F3") + " Quintillion";
		}
		else if (totalAmount > 999999999999999) 
		{
			return (totalAmount / 1000000000000000).ToString ("F3") + " Quadrillion";
		}
		else if (totalAmount > 999999999999) 
		{
			return (totalAmount / 1000000000000).ToString ("F3") + " Trillion";
		}
		else if (totalAmount > 999999999) 
		{
			return (totalAmount / 1000000000).ToString ("F3") + " Billion";
		}
		else if (totalAmount > 999999) 
		{
			return (totalAmount / 1000000).ToString ("F3") + " Million";
		} 
		else if (totalAmount > 99999) 
		{
			return  (totalAmount / 1000).ToString ("F2") + " Thousand";
		} 
		else
			return "";
	}
}