using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleEnd : MonoBehaviour
{
	[SerializeField] private TMP_Text crownsReward;
	[SerializeField] private TMP_Text puzzleGameResult;
	[SerializeField] private TMP_Text puzzleButtonText;
	[SerializeField] private TMP_Text currentLevelText;

	public void PuzzleResult(PuzzleLevelInfo puzzleLevelInfo)
	{
		gameObject.SetActive(true);
		string buttonText = puzzleLevelInfo.win ? "next level" : "try again";
		string crownsString = puzzleLevelInfo.crownsAdded.ToString();
		string puzzleGameResult = puzzleLevelInfo.win ? "you win" : "you lose";

		crownsReward.text = crownsString;
		this.puzzleGameResult.text = puzzleGameResult;
		puzzleButtonText.text = buttonText;
		currentLevelText.text = $"LEVEL {puzzleLevelInfo.currentLevel}";
	}

	public void MainPuzzleScene()
	{
		SceneManager.LoadScene("MainPuzzleScene");
	}

	public void SidePuzzleScene()
	{
		SceneManager.LoadScene("SidePuzzleScene");
	}
}

public class PuzzleLevelInfo
{
	public bool win;
	public int crownsAdded;
	public int currentLevel;

	public PuzzleLevelInfo(bool isWon, int crowns, int level)
	{
		win = isWon;
		crownsAdded = crowns;
		currentLevel = level;
	}
}
