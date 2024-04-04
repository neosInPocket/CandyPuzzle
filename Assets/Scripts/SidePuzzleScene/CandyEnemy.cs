using Unity.VisualScripting;
using UnityEngine;

public class CandyEnemy : MonoBehaviour
{
	[SerializeField] private PlayerPath playerPath;
	[SerializeField] private float velocity;
	[SerializeField] private Rigidbody2D movement;
	[SerializeField] private Transform player;
	[SerializeField] private float activateDistance;
	public bool activated;
	private float currentYTarget = 0;
	private int currentPathIndex = 1;
	public Vector2 Velocity
	{
		get => movement.velocity;
		set => movement.velocity = value;
	}

	private void Update()
	{
		if (Mathf.Abs(player.transform.position.y - transform.position.y) > activateDistance)
		{
			activated = true;
		}

		if (!activated) return;

		if (transform.position.y < currentYTarget)
		{
			IncreasePathIndex();
		}

		movement.velocity = (playerPath.Path.GetPosition(currentPathIndex) - playerPath.Path.GetPosition(currentPathIndex - 1)).normalized * velocity;
	}

	public void IncreasePathIndex()
	{
		currentPathIndex++;
		currentYTarget = playerPath.Path.GetPosition(currentPathIndex).y;
	}
}
