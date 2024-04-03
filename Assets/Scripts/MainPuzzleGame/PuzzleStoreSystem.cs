using UnityEngine;

public class PuzzleStoreSystem : MonoBehaviour
{
	[SerializeField] private PuzzleUpgrade firstUpgrade;
	[SerializeField] private PuzzleUpgrade secondUpgrade;
	[SerializeField] private MainPuzzleSystem mainPuzzleSystem;

	private void Start()
	{
		RefreshAllUpdates();
	}

	public void RefreshAllUpdates()
	{
		firstUpgrade.FillInfo();
		secondUpgrade.FillInfo();
		mainPuzzleSystem.UpdateCrowns();
	}
}
