using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUpgrade : MonoBehaviour
{
	[SerializeField] private string upgradeName;
	[SerializeField] private int purchaseCost;
	[SerializeField] private TMP_Text purchaseStatus;
	[SerializeField] private Button purchaseButton;
	[SerializeField] private TMP_Text purchaseButtonText;
	[SerializeField] private Image purchaseStatusFill;
	[SerializeField] private TMP_Text purchaseCostText;
	[SerializeField] private TMP_Text upgradeNameText;
	[SerializeField] private int updateIndex;
	[SerializeField] private PuzzleStoreSystem puzzleStoreSystem;

	private void Start()
	{
		purchaseButton.onClick.AddListener(PurchasePuzzleUpdate);
	}

	public void FillInfo()
	{
		int purchaseAmount = updateIndex == 0 ? PuzzleSystem.save.firstLevelUpdate : PuzzleSystem.save.secondLevelUpdate;
		int currentCrowns = PuzzleSystem.save.crowns;

		purchaseStatus.text = $"{(float)purchaseAmount / 4f * 100f}% purchased";
		purchaseStatusFill.fillAmount = (float)purchaseAmount / 4f;

		upgradeNameText.text = upgradeName;
		purchaseCostText.text = purchaseCost.ToString();

		bool interactable = false;
		string buttonText = null;
		Color textColor;

		if (purchaseAmount == 4)
		{
			interactable = false;
			buttonText = "purchased";
			textColor = Color.green;
		}
		else
		{
			if (currentCrowns < purchaseCost)
			{
				interactable = false;
				buttonText = "no crowns";
				textColor = Color.red;
			}
			else
			{
				interactable = true;
				buttonText = "purchase";
				textColor = Color.white;
			}
		}

		purchaseButton.interactable = interactable;
		purchaseButtonText.text = buttonText;
		purchaseButtonText.color = textColor;
	}

	public void PurchasePuzzleUpdate()
	{
		PuzzleSystem.save.crowns -= purchaseCost;

		if (updateIndex == 0)
		{
			PuzzleSystem.save.firstLevelUpdate++;
		}
		else
		{
			PuzzleSystem.save.secondLevelUpdate++;
		}

		PuzzleSystem.SavePuzzles();
		puzzleStoreSystem.RefreshAllUpdates();
	}
}
