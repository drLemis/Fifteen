using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
// using DG.Tweening;

public class Menu : MonoBehaviour
{
	public void StartGame(int fieldSize)
	{
		GameManager.Instance.gameMode = GameManager.GameMode.FIFTEEN;
		GameManager.Instance.gameState = GameManager.GameState.SPAWNMIX;
		GameManager.Instance.fieldSize = fieldSize;
		GameManager.Instance.ChangeScene("Game");
	}
}
