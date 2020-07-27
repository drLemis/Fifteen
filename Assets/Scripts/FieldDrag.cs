using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class FieldDrag : MonoBehaviour
{
	public FieldHole currentEmpty;

	public bool isTweening = false;

	public float moveTime = 0.2f;

	private void Awake()
	{
		GameManager.Instance.fieldDrag = this;
	}

	private void Update()
	{
	}

	public void Swap(FieldHole target)
	{
		if (isTweening)
			return;

		float moveTimeCorrected = moveTime;
		if (GameManager.Instance.gameState == GameManager.GameState.SPAWNMIX)
			moveTimeCorrected /= 5f;

		isTweening = true;

		target.emptySpace = true;
		currentEmpty.emptySpace = false;

		currentEmpty.holeIndex = target.holeIndex;

		currentEmpty.holeObject.transform.position = target.holeObject.transform.position;
		currentEmpty.holeObject.transform.DOLocalMove(Vector3.zero, moveTimeCorrected).SetEase(Ease.InCubic)
		.OnComplete(() =>
		{
			isTweening = false;
		});

		target.SyncVisuals();
		currentEmpty.SyncVisuals();

		currentEmpty = target;

		GameManager.Instance.fieldCounter.UseMove();

		if (GameManager.Instance.gameState == GameManager.GameState.IDLE)
			GameManager.Instance.fieldSpawn.ValidateField();
	}
}
