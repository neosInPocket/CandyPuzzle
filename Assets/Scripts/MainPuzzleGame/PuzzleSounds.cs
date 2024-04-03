using System.Linq;
using UnityEngine;

public class PuzzleSounds : MonoBehaviour
{
	[SerializeField] private AudioSource source;
	public const string SceneName = "DontDestroyOnLoad";

	private void Awake()
	{
		var sounds = FindOthersPuzzleSystems();

		try
		{
			PuzzleSounds dontDestroy = sounds.First(x => x.gameObject.scene.name == SceneName);

			if (dontDestroy == this)
			{

			}
			else
			{
				Destroy(gameObject);
				return;
			}
		}
		catch
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	public PuzzleSounds[] FindOthersPuzzleSystems()
	{
		PuzzleSounds[] puzzleSounds = FindObjectsByType<PuzzleSounds>(sortMode: FindObjectsSortMode.None);
		return puzzleSounds;
	}

	private void Start()
	{
		PuzzleMusicEnabled(PuzzleSystem.save.music == 1);
	}

	public void PuzzleMusicEnabled(bool valueEnabled)
	{
		source.volume = !valueEnabled ? 0f : 1f;

		PuzzleSystem.save.music = valueEnabled ? 1 : 0;
	}
}
