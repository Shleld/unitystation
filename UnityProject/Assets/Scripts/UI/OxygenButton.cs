﻿using UnityEngine;
using UnityEngine.UI;

public class OxygenButton : MonoBehaviour
{
	private Image image;
	public Sprite[] stateSprites;
	public bool IsInternalsEnabled;

	// Use this for initialization
	private void Start()
	{
		image = GetComponent<Image>();
		IsInternalsEnabled = false;
		EventManager.AddHandler(EVENT.EnableInternals, OnEnableInternals);
		EventManager.AddHandler(EVENT.DisableInternals, OnDisableInternals);
	}

	private void OnDestroy()
	{
		EventManager.RemoveHandler(EVENT.EnableInternals, OnEnableInternals);
		EventManager.RemoveHandler(EVENT.DisableInternals, OnDisableInternals);
	}

	/// <summary>
	/// toggle the button state and play any sounds
	/// </summary>
	public void OxygenSelect()
	{
		if (PlayerManager.LocalPlayer == null)
		{
			return;
		}

		if (PlayerManager.LocalPlayerScript.playerHealth.IsCrit)
		{
			return;
		}
		
		SoundManager.Play("Click01");

		if (IsInternalsEnabled)
		{
			EventManager.Broadcast(EVENT.DisableInternals);
		}
		else
		{
			EventManager.Broadcast(EVENT.EnableInternals);
		}
	}

	public void OnEnableInternals()
	{
		image.sprite = stateSprites[1];
		IsInternalsEnabled = true;
	}

	public void OnDisableInternals()
	{
		image.sprite = stateSprites[0];
		IsInternalsEnabled = false;
	}
}