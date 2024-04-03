using UnityEngine;

public class PuzzleSystem : MonoBehaviour
{
	[SerializeField] private bool clearPuzzleSaves;
	[SerializeField] private PuzzleSave defaultSave;
	public static PuzzleSave save;


	private void Awake()
	{
		save = new PuzzleSave();

		if (clearPuzzleSaves)
		{
			SetDefaultPuzzleData();
			SavePuzzles();
		}
		else
		{
			LoadPuzzles();
		}
	}
	public void SetDefaultPuzzleData()
	{
		PlayerPrefs.SetInt("levelProgress", defaultSave.levelProgress);
		PlayerPrefs.SetInt("firstLevelUpdate", defaultSave.firstLevelUpdate);
		PlayerPrefs.SetInt("secondLevelUpdate", defaultSave.secondLevelUpdate);
		PlayerPrefs.SetInt("crowns", defaultSave.crowns);
		PlayerPrefs.SetInt("tutorial", defaultSave.tutorial);
		PlayerPrefs.SetInt("sound", defaultSave.sound);
		PlayerPrefs.SetInt("music", defaultSave.music);

		save.levelProgress = defaultSave.levelProgress;
		save.firstLevelUpdate = defaultSave.firstLevelUpdate;
		save.secondLevelUpdate = defaultSave.secondLevelUpdate;
		save.crowns = defaultSave.crowns;
		save.tutorial = defaultSave.tutorial;
		save.sound = defaultSave.sound;
		save.music = defaultSave.music;
	}

	public static void SavePuzzles()
	{
		PlayerPrefs.SetInt("levelProgress", save.levelProgress);
		PlayerPrefs.SetInt("firstLevelUpdate", save.firstLevelUpdate);
		PlayerPrefs.SetInt("secondLevelUpdate", save.secondLevelUpdate);
		PlayerPrefs.SetInt("crowns", save.crowns);
		PlayerPrefs.SetInt("tutorial", save.tutorial);
		PlayerPrefs.SetInt("sound", save.sound);
		PlayerPrefs.SetInt("music", save.music);
	}

	public void LoadPuzzles()
	{
		save.levelProgress = PlayerPrefs.GetInt("levelProgress", defaultSave.levelProgress);
		save.secondLevelUpdate = PlayerPrefs.GetInt("secondLevelUpdate", defaultSave.secondLevelUpdate);
		save.crowns = PlayerPrefs.GetInt("crowns", defaultSave.crowns);
		save.tutorial = PlayerPrefs.GetInt("tutorial", defaultSave.tutorial);
		save.sound = PlayerPrefs.GetInt("sound", defaultSave.sound);
		save.music = PlayerPrefs.GetInt("music", defaultSave.music);
		save.firstLevelUpdate = PlayerPrefs.GetInt("firstLevelUpdate", defaultSave.firstLevelUpdate);
	}
}
