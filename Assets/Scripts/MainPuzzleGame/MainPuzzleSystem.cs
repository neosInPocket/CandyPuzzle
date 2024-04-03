using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainPuzzleSystem : MonoBehaviour
{
	[SerializeField] private List<TMP_Text> crownTexts;
	[SerializeField] private Animator animator;

	private void Start()
	{
		UpdateCrowns();
	}

	public void UpdateCrowns()
	{
		string text = PuzzleSystem.save.crowns.ToString();
		crownTexts.ForEach(x => x.text = text);
	}

	public void SetNextPuzzleScene()
	{
		SceneManager.LoadScene("SidePuzzleScene");
	}

	public void PuzzleSettingsAnimation()
	{
		animator.SetTrigger("puzzleSettings");
	}

	public void PuzzleStoreAnimation()
	{
		animator.SetTrigger("puzzleStore");
	}

	public void MainPuzzleAnimation()
	{
		animator.SetTrigger("puzzleMain");
	}

	public void ReturnAnimation()
	{
		animator.SetTrigger("return");
	}
}
