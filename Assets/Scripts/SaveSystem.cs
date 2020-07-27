using UnityEngine;

public class SaveSystem
{
	private static SaveSystem instance;

	public static SaveSystem Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new SaveSystem();
				instance.Load(4);
			}
			return instance;
		}
	}

	protected SaveSystem() { }

	// ########
	public int minTime = int.MaxValue;
	public int minMoves = int.MaxValue;

	void OnApplicationQuit()
	{
		PlayerPrefs.Save();
	}

	public void Save(int fieldSize)
	{
		PlayerPrefs.SetInt(fieldSize + "MinTime", minTime);
		PlayerPrefs.SetInt(fieldSize + "MinMoves", minMoves);
		PlayerPrefs.Save();
	}

	public void Load(int fieldSize)
	{
		minTime = PlayerPrefs.GetInt(fieldSize + "MinTime", int.MaxValue);
		minMoves = PlayerPrefs.GetInt(fieldSize + "MinMoves", int.MaxValue);
	}
}
