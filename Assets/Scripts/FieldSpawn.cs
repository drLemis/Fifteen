using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FieldSpawn : MonoBehaviour
{
	public GameObject fieldObject;
	public GameObject columnPrefab;
	public GameObject rowPrefab;

	public FieldHole[][] fieldHoles;

	private int mixMoves = 0;
	private FieldHole lastHoleMix;

	private void Awake()
	{
		GameManager.Instance.fieldSpawn = this;

		fieldHoles = new FieldHole[GameManager.Instance.fieldSize][];

		for (int i = 0; i < GameManager.Instance.fieldSize; i++)
		{
			fieldHoles[i] = new FieldHole[GameManager.Instance.fieldSize];
		}

		FullSpawn();
	}

	private void Update()
	{
		if (GameManager.Instance.gameState == GameManager.GameState.SPAWNMIX && !GameManager.Instance.fieldDrag.isTweening)
		{
			if (mixMoves > 0)
				FieldMix();
			else
				GameManager.Instance.gameState = GameManager.GameState.IDLE;
		}
	}

	public void ValidateField()
	{
		// empty space is always last
		if (fieldHoles[GameManager.Instance.fieldSize - 1][GameManager.Instance.fieldSize - 1].emptySpace != true)
		{
			return;
		}

		for (int i = 0; i < fieldHoles.Length; i++)
		{
			for (int j = 0; j < fieldHoles[i].Length; j++)
			{
				if (i == j && j == GameManager.Instance.fieldSize - 1)
				{
					// made it to the end, so field is correct
					GameEnd();
				}
				else if (fieldHoles[i][j].holeIndex != j * GameManager.Instance.fieldSize + 1 + i)
					return;
			}
		}
	}

	public void GameEnd()
	{

		SaveSystem.Instance.Load(GameManager.Instance.fieldSize);

		SaveSystem.Instance.minMoves = Mathf.Min(GameManager.Instance.fieldCounter.moves, SaveSystem.Instance.minMoves);
		SaveSystem.Instance.minTime = Mathf.Min((int)GameManager.Instance.fieldCounter.seconds, SaveSystem.Instance.minTime);

		SaveSystem.Instance.Save(GameManager.Instance.fieldSize);

		GameManager.Instance.gameState = GameManager.GameState.WINSCREEN;



		GameManager.Instance.fieldWinScreen.TurnOn();
	}

	public void FieldMix()
	{
		mixMoves--;

		GameManager.Instance.gameState = GameManager.GameState.SPAWNMIX;

		List<FieldHole> neighbours = GetNeighbours(GameManager.Instance.fieldDrag.currentEmpty);
		neighbours.Remove(lastHoleMix);
		lastHoleMix = GameManager.Instance.fieldDrag.currentEmpty;

		GameManager.Instance.fieldDrag.Swap(neighbours[Random.Range(0, neighbours.Count)]);
	}

	public void FullSpawn()
	{
		int fieldSize = GameManager.Instance.fieldSize;

		foreach (Transform child in fieldObject.transform)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < fieldSize; i++)
		{
			GameObject spawnedColumn = Instantiate(columnPrefab, fieldObject.transform);

			for (int j = 0; j < fieldSize; j++)
			{
				GameObject spawnedRow = Instantiate(rowPrefab, spawnedColumn.transform);
				FieldHole fieldHole = spawnedRow.GetComponent<FieldHole>();
				fieldHole.holeIndex = j * fieldSize + 1 + i;
				fieldHole.column = i;
				fieldHole.row = j;
				fieldHole.SyncVisuals();
				fieldHoles[i][j] = fieldHole;
			}
		}

		GameManager.Instance.fieldDrag.currentEmpty = GetByPos(fieldSize - 1, fieldSize - 1).GetComponent<FieldHole>();

		GameManager.Instance.fieldDrag.currentEmpty.emptySpace = true;
		GameManager.Instance.fieldDrag.currentEmpty.SyncVisuals();

		lastHoleMix = GameManager.Instance.fieldDrag.currentEmpty;
		mixMoves = GameManager.Instance.fieldSize * GameManager.Instance.fieldSize * 2;
	}

	public bool IsNeighbours(FieldHole target)
	{
		return GetNeighbours(GameManager.Instance.fieldDrag.currentEmpty).Contains(target);
	}

	public FieldHole GetByPos(int column, int row)
	{
		if (column >= 0 && column < fieldHoles.Length)
			if (row >= 0 && row < fieldHoles[column].Length)
				return fieldHoles[column][row];

		return null;
	}

	public List<FieldHole> GetNeighbours(FieldHole target)
	{
		List<FieldHole> result = new List<FieldHole>();

		// top
		if (target.row > 0)
			result.Add(fieldHoles[target.column][target.row - 1]);

		// right
		if (target.column < fieldHoles.Length - 1)
			result.Add(fieldHoles[target.column + 1][target.row]);

		// bottom
		if (target.row < fieldHoles[target.column].Length - 1)
			result.Add(fieldHoles[target.column][target.row + 1]);

		// left
		if (target.column > 0)
			result.Add(fieldHoles[target.column - 1][target.row]);

		return result;
	}
}
