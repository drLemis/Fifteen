using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using DG.Tweening;


public class FieldCounter : MonoBehaviour
{
	public TextMeshProUGUI textMoves;
	public TextMeshProUGUI textTimer;

	public float seconds = 0f;
	public int moves = 0;

	public bool blockTimer = false;

	private void Awake()
	{
		blockTimer = true;
		GameManager.Instance.fieldCounter = this;
		Initialize();
	}

	private void Update()
	{
		if (!blockTimer &&
			GameManager.Instance.gameMode == GameManager.GameMode.FIFTEEN &&
			GameManager.Instance.gameState != GameManager.GameState.WINSCREEN)
		{
			seconds += Time.deltaTime;
			textTimer.text = FormatSecondsToMinute((int)seconds);
		}
	}

	public void Initialize()
	{
		textTimer.text = "0:00";
		textMoves.text = "Moves: 0";
	}

	public string FormatSecondsToMinute(int seconds)
	{
		int resultMinutes = seconds / 60;
		int resultSeconds = seconds - resultMinutes * 60;

		return resultMinutes.ToString() + ":" + resultSeconds.ToString("00");
	}

	public void UseMove()
	{
		if (GameManager.Instance.gameState != GameManager.GameState.IDLE)
			return;

		blockTimer = false;

		moves++;
		textMoves.text = "Moves: " + (moves).ToString();
	}
}
