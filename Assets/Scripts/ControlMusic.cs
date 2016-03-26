using UnityEngine;
using System.Collections;

public class ControlMusic : MonoBehaviour {

	private static AudioSource source;
	public AudioClip clip1;
	public AudioClip clip2;
	public AudioClip clip3;
	private static ControlMusic instance;
	private bool muted = false;
	public Sprite unmutedSprite;
	public Sprite mutedSprite;
	private SpriteRenderer gameObjectSpriteRender;

	void Awake()
	{
		instance = this;
	}

	// Use this for initialization
	void Start () {
		source = gameObject.AddComponent<AudioSource>();
		source.loop = true;
		source.clip = clip1;
		source.volume = 0.08f;
		source.Play ();
		gameObjectSpriteRender = GetComponent<SpriteRenderer> ();
	}

	public static void changeRight()
	{
		if (source.clip == instance.clip1)
		{
			source.clip = instance.clip2;
			source.Play ();
		}
		else if (source.clip == instance.clip2)
		{
			source.clip = instance.clip3;
			source.Play ();
		}
		else if (source.clip == instance.clip3)
		{
			source.clip = instance.clip1;
			source.Play ();
		}
	}

	public static void changeLeft()
	{
		if (source.clip == instance.clip1)
		{
			source.clip = instance.clip3;
			source.Play ();
		}
		else if (source.clip == instance.clip2)
		{
			source.clip = instance.clip1;
			source.Play ();
		}
		else if (source.clip == instance.clip3)
		{
			source.clip = instance.clip2;
			source.Play ();
		}
	}

	public static void muteUnmute()
	{
		if (instance.muted == false) 
		{
			source.volume = 0.0f;
			instance.muted = true;
			instance.gameObjectSpriteRender.sprite = instance.mutedSprite;
		}
		else
		{
			source.volume = 0.08f;
			instance.muted = false;
			instance.gameObjectSpriteRender.sprite = instance.unmutedSprite;
		}
	}
}
