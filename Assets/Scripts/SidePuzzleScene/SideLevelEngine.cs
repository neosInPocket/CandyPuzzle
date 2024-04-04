using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class SideLevelEngine : MonoBehaviour
{
	[SerializeField] private List<LevelLimiter> limiters;
	[SerializeField] private PuzzlePlayer puzzlePlayer;
	[SerializeField] private Image percenter;
	[SerializeField] private TMP_Text percenterText;
	[SerializeField] private PuzzleGuide puzzleGuide;
	[SerializeField] private PuzzleEnd puzzleEnd;
	[SerializeField] private CandyEnemy candyEnemy;
	[SerializeField] private Animator countAnimator;
	[SerializeField] private TMP_Text rewardText;
	private int currentCrownCount = 0;
	private int crownsTarget => (int)(4 * Mathf.Log(PuzzleSystem.save.levelProgress + 3));
	private int crownsReward => (int)(20 * Mathf.Sqrt(PuzzleSystem.save.levelProgress));

	private void Awake()
	{
		EnhancedTouchSupport.Enable();
		TouchSimulation.Enable();
	}

	private void Start()
	{
		SetLimitersPositions();
		rewardText.text = crownsReward.ToString();

		if (PuzzleSystem.save.tutorial == 0)
		{

			PuzzleGuideEnd(PuzzleSystem.save.tutorial != 0);
		}
		else
		{
			PuzzleSystem.save.tutorial = 0;
			PuzzleSystem.SavePuzzles();
			puzzleGuide.PuzzleAction(PuzzleGuideEnd);
		}

		PuzzleProgress();
	}

	public void PuzzleProgress()
	{
		percenter.fillAmount = (float)currentCrownCount / (float)crownsTarget;
		percenterText.text = $"{(int)(percenter.fillAmount * 100)}%";
	}

	public void PuzzleGuideEnd(bool tutorialCallback)
	{
		countAnimator.SetTrigger("count");
	}

	public void PuzzleCountEnd()
	{
		puzzlePlayer.EndLevel += EndLevel;
		puzzlePlayer.TargetCaptured += TargetCapture;
		puzzlePlayer.Activate = true;
		puzzlePlayer.Ready = true;
	}

	public void EndLevel()
	{
		EndPuzzleLevel(false);
	}

	public void EndPuzzleLevel(bool win)
	{
		DisableAllSubscribers();

		if (win)
		{
			puzzleEnd.PuzzleResult(new PuzzleLevelInfo(true, crownsReward, PuzzleSystem.save.levelProgress));
			PuzzleSystem.save.levelProgress++;
			PuzzleSystem.save.crowns += crownsReward;
			PuzzleSystem.SavePuzzles();
		}
		else
		{
			puzzleEnd.PuzzleResult(new PuzzleLevelInfo(false, 0, PuzzleSystem.save.levelProgress));
		}
	}

	public void DisableAllSubscribers()
	{
		puzzlePlayer.EndLevel -= EndLevel;
		puzzlePlayer.TargetCaptured -= TargetCapture;
		puzzlePlayer.Activate = false;
		puzzlePlayer.Ready = false;
		candyEnemy.Velocity = Vector2.zero;
	}

	public void TargetCapture()
	{
		currentCrownCount++;

		if (currentCrownCount == crownsTarget)
		{
			EndPuzzleLevel(true);
		}

		PuzzleProgress();
	}

	public void SetLimitersPositions()
	{
		limiters[0].SetPosition(Vector2.up, 1.02f);
		limiters[1].SetPosition(Vector2.down, 1.02f);
		limiters[2].SetPosition(Vector2.left, 1.02f);
		limiters[3].SetPosition(Vector2.right, 1.02f);
	}
}
