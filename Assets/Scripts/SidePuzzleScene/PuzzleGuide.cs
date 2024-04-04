using System;
using TMPro;
using UnityEngine;
using PlayerTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using PlayerFinger = UnityEngine.InputSystem.EnhancedTouch.Finger;

public class PuzzleGuide : MonoBehaviour
{
	[SerializeField] private TMP_Text popupText;
	[SerializeField] private GameObject objectsPointer;
	public Action<bool> PuzzleEnd { get; private set; }
	private Animator objectsAnimator;

	private void Start()
	{
		objectsAnimator = objectsPointer.GetComponent<Animator>();
	}

	public void PuzzleAction(Action<bool> puzzleEndBoolAction)
	{
		PuzzleEnd = puzzleEndBoolAction;

		PlayerTouch.onFingerDown += Lollipop;
		popupText.text = "WELCOME to puzzle candies, wanderer!";

		gameObject.SetActive(true);
	}

	private void Lollipop(PlayerFinger finger)
	{
		SubscribeNewPhase(Lollipop, TapTheScreen);
		popupText.text = "This is your lollipop, control it by holding the screen!";
		objectsAnimator.SetTrigger("change");
	}

	private void TapTheScreen(PlayerFinger finger)
	{
		SubscribeNewPhase(TapTheScreen, Avoid);
		popupText.text = "hold the screen to stop its spin and give him speed towards where its stick is pointing!";
		objectsAnimator.SetTrigger("change");
	}

	private void Avoid(PlayerFinger finger)
	{
		SubscribeNewPhase(Avoid, FindOut);
		popupText.text = "your task is to follow the green trail without touching the red walls and avoiding the red spike that will chase you";
		objectsAnimator.SetTrigger("change");
	}

	private void FindOut(PlayerFinger finger)
	{
		SubscribeNewPhase(FindOut, Release);
		popupText.text = "collect all trophies to complete the level! good luck!";
		objectsAnimator.SetTrigger("change");
	}

	private void Release(PlayerFinger finger)
	{
		PlayerTouch.onFingerDown -= Release;
		PuzzleEnd(true);

		if (gameObject != null)
		{
			gameObject.SetActive(false);
		}
	}

	public void SubscribeNewPhase(Action<PlayerFinger> from, Action<PlayerFinger> to)
	{
		PlayerTouch.onFingerDown -= from;
		PlayerTouch.onFingerDown += to;
	}
}
