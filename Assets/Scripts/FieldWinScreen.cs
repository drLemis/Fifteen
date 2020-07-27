using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using DG.Tweening;

public class FieldWinScreen : MonoBehaviour
{
	public GameObject winScreenObject;
	public TextMeshProUGUI textCurrentTime;
	public TextMeshProUGUI textMinTime;
	public TextMeshProUGUI textCurrentMoves;
	public TextMeshProUGUI textMinMoves;

	private void Awake()
	{
		GameManager.Instance.fieldWinScreen = this;
	}

	public void SyncText()
	{
		textCurrentTime.text = GameManager.Instance.fieldCounter.FormatSecondsToMinute((int)GameManager.Instance.fieldCounter.seconds);
		textMinTime.text = GameManager.Instance.fieldCounter.FormatSecondsToMinute(SaveSystem.Instance.minTime);

		textCurrentMoves.text = GameManager.Instance.fieldCounter.moves.ToString();
		textMinMoves.text = SaveSystem.Instance.minMoves.ToString();
	}

	public void TurnOn()
	{
		StartCoroutine(DelayedTurnOn());
	}

	IEnumerator DelayedTurnOn()
	{
		yield return new WaitForSeconds(1f);

		winScreenObject.SetActive(true);

		SyncText();
		SaveSystem.Instance.Save(GameManager.Instance.fieldSize);
	}

	public void EndGame()
	{
		GameManager.Instance.ChangeScene("Menu");
	}
}
