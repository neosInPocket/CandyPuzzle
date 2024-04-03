using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleSettingsSystem : MonoBehaviour
{
	[SerializeField] private GameObject musicDisabledImage;
	[SerializeField] private GameObject soundsDisabledImage;
	[SerializeField] private Button musicToggle;
	[SerializeField] private Button soundsToggle;

	private PuzzleSounds puzzleSounds;

	private void Start()
	{
		InitiatePuzzleSettings();

		musicToggle.onClick.AddListener(PuzzleMusic);
		soundsToggle.onClick.AddListener(PuzzleSounds);
	}

	public void InitiatePuzzleSettings()
	{
		puzzleSounds = FindFirstObjectByType<PuzzleSounds>();
		bool musicActive = PuzzleSystem.save.music == 0;
		bool soundsActive = PuzzleSystem.save.sound == 0;

		musicDisabledImage.SetActive(musicActive);
		soundsDisabledImage.SetActive(soundsActive);
	}

	public void PuzzleMusic()
	{
		musicDisabledImage.SetActive(!musicDisabledImage.activeSelf);
		puzzleSounds.PuzzleMusicEnabled(!musicDisabledImage.activeSelf);
	}

	public void PuzzleSounds()
	{
		soundsDisabledImage.SetActive(!soundsDisabledImage.activeSelf);
		PuzzleSystem.save.sound = soundsDisabledImage.activeSelf ? 0 : 1;
		PuzzleSystem.SavePuzzles();
	}
}
