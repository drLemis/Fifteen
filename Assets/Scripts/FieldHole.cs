using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using DG.Tweening;

public class FieldHole : MonoBehaviour
{
	public GameObject holeObject;
	public TextMeshProUGUI textIndex;
	public int holeIndex = 0;
	public bool emptySpace = false;

	public int column = 0;
	public int row = 0;

	// public void DropPositions()
	// {
	// 	if (dropPositions == -1)
	// 	{
	// 		GameManager.Instance.fieldSpawn.DotSpawn(this);

	// 		// droppableObject.position = transform.position + new Vector3(0f, 10f, 0f);
	// 		// droppableObject.DOLocalMoveY(0, 0.75f).SetEase(Ease.OutBounce);
	// 		dropPositions = 0;
	// 	}
	// 	else if (dropPositions > 0)
	// 	{
	// 		FieldHole target = GameManager.Instance.fieldSpawn.GetByPos(column, row - dropPositions);

	// 		if (target == null)
	// 		{
	// 			dropPositions = -1;
	// 			DropPositions();
	// 		}
	// 		else
	// 		{
	// 			dotType = target.dotType;
	// 			dotSprite.color = target.dotSprite.color;

	// 			// droppableObject.position = target.transform.position;
	// 			// droppableObject.DOLocalMoveY(0, 0.25f * dropPositions).SetEase(Ease.OutBounce);
	// 			dropPositions = 0;
	// 		}
	// 	}
	// }

	public void SyncVisuals()
	{
		holeObject.SetActive(!emptySpace);
		textIndex.text = holeIndex.ToString();
	}

	public void OnPointerDown()
	{
		if (GameManager.Instance.fieldSpawn.IsNeighbours(this))
		{
			GameManager.Instance.fieldDrag.Swap(this);
		}
	}

	public void OnPointerEnter()
	{
	}

	public void OnPointerExit()
	{
	}

	public void OnPointerUp()
	{
	}

}
