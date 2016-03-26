using UnityEngine;
using System.Collections;

public class CheckPanelClicked : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown()
	{
		OpenNewWindow.openPanel (name);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
