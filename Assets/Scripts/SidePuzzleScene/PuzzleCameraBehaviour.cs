using UnityEngine;

public class PuzzleCameraBehaviour : MonoBehaviour
{
	[SerializeField] private Transform puzzlePlayer;
	[SerializeField] private float cameraOffset;
	private Vector3 currentPosition;

	private void Start()
	{
		currentPosition = transform.position;
	}

	private void Update()
	{
		currentPosition.y = puzzlePlayer.position.y - cameraOffset;
		transform.position = currentPosition;
	}
}
