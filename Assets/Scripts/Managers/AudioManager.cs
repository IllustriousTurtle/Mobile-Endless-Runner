using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

//Plays audio queues in Animation Events and through other scripts.
public class AudioManager : MonoBehaviour
{
	private static AudioManager instance = null;
	private static readonly object padlock = new object();

	AudioManager() { }

	public static AudioManager Instance
	{
		get
		{
			lock (padlock)
			{
				if (instance == null)
				{
					instance = FindObjectOfType<AudioManager>();

					if (instance == null)
					{
						instance = new GameObject().AddComponent<AudioManager>();
					}
				}
				return instance;
			}
		}
	}

	[SerializeField]
	AudioClip playerJump;
	[SerializeField]
	AudioClip playerDeath;

	[SerializeField]
	AudioClip pickupCollectable;

	[SerializeField]
	AudioClip backgroundAudio;

	AudioSource audioSrc;

	//Starts background music
	private void Start()
	{
		if (backgroundAudio != null)
		{
			audioSrc = GetComponent<AudioSource>();
			audioSrc.clip = backgroundAudio;
			audioSrc.loop = true;
			audioSrc.Play();
		}
	}

	public void JumpAudioEffect()
	{
		audioSrc.PlayOneShot(playerJump);
	}

	public void PlayerDeath()
	{
		audioSrc.PlayOneShot(playerDeath);
	}

	public void CollectablePickup()
	{
		audioSrc.PlayOneShot(pickupCollectable);
	}
}
