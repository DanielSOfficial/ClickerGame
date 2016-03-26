using UnityEngine;
using System.Collections;

public class ClickMusic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown()
	{
		switch (gameObject.name) 
		{
		case "ClickLeft":
			ControlMusic.changeLeft ();
			break;
		case "ClickRight":
			ControlMusic.changeRight ();
			break;
		case "Mute":
			ControlMusic.muteUnmute ();
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
