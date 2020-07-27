using UnityEngine.SceneManagement;

public class GameManager
{
	private static readonly GameManager instance = new GameManager();

	public static GameManager Instance
	{
		get { return instance; }
	}

	protected GameManager() { }

	// ########
	public FieldDrag fieldDrag;
	public FieldSpawn fieldSpawn;
	public FieldCounter fieldCounter;
	public FieldWinScreen fieldWinScreen;

	public int fieldSize = 4;


	public enum GameMode
	{
		MENU,
		FIFTEEN
	}

	public GameMode gameMode = GameMode.FIFTEEN;

	public enum GameState
	{
		IDLE,
		SPAWNMIX,
		WINSCREEN
	}

	public GameState gameState = GameState.IDLE;

	public void ChangeScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
}
