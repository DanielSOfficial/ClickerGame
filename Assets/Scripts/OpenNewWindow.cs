using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenNewWindow : MonoBehaviour {

	public static OpenNewWindow instance;
	public GameObject scrollView;
	public GameObject scrollView2;
	public GameObject scrollView3;
	private bool active = false;
	private bool active2 = false;
	private bool active3 = false;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		//invokeMethod ();
	}

	public static void openPanel(string name)
	{
		if (name == "OpenManagers") 
		{
			if (instance.active) 
			{
				instance.scrollView.transform.position = new Vector3 (-500, -500);
				instance.active = false;
				Globals.setWindowOpenFalse ();
			} 
			else
			{
				instance.scrollView.transform.position = new Vector3 (550, 320);
				instance.scrollView2.transform.position = new Vector3 (-500, -500);
				instance.scrollView3.transform.position = new Vector3 (-500, -500);
				instance.active = true;
				instance.active2 = false;
				instance.active3 = false;
				Globals.setWindowOpenTrue ();
			}
		} 
		else if (name == "OpenUpgrades") 
		{
			if (instance.active2) 
			{
				instance.scrollView2.transform.position = new Vector3 (-500, -500);
				instance.active2 = false;
				Globals.setWindowOpenFalse ();
			} 
			else
			{
				instance.scrollView2.transform.position = new Vector3 (550, 320);
				instance.scrollView.transform.position = new Vector3 (-500, -500);
				instance.scrollView3.transform.position = new Vector3 (-500, -500);
				instance.active2 = true;
				instance.active = false;
				instance.active3 = false;
				Globals.setWindowOpenTrue ();
			}
		}
		else if (name == "OpenInvestors") 
		{
			if (instance.active3) 
			{
				instance.scrollView3.transform.position = new Vector3 (-500, -500);
				instance.active3 = false;
				Globals.setWindowOpenFalse ();
			} 
			else
			{
				instance.scrollView3.transform.position = new Vector3 (550, 320);
				instance.scrollView.transform.position = new Vector3 (-500, -500);
				instance.scrollView2.transform.position = new Vector3 (-500, -500);
				instance.active3 = true;
				instance.active2 = false;
				instance.active = false;
				Globals.setWindowOpenTrue ();
			}
		}
	}
}
