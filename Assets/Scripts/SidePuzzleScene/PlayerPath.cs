using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPath : MonoBehaviour
{
	[SerializeField] private LineRenderer path;
	[SerializeField] private PuzzlePlayer puzzlePlayer;
	[SerializeField] private float generateOffset;
	[SerializeField] private Vector2 sectorLengthRange;
	[SerializeField] private float edgeColliderOffset;
	[SerializeField] private EdgeCollider2D leftCollider;
	[SerializeField] private EdgeCollider2D rightCollider;
	[SerializeField] private Vector2 startPosition;
	[SerializeField] private LineRenderer leftEdge;
	[SerializeField] private LineRenderer rightEdge;
	[SerializeField] private GameObject redWall;
	[SerializeField] private float xGenerateOffset;
	[SerializeField] private CandyTarget candyTarget;
	[SerializeField] private float candyTargetSpawnChance;
	[SerializeField] private Vector2 enemySpawnPosition;
	[SerializeField] private CandyEnemy candyEnemy;
	public LineRenderer Path => path;
	private Vector2 screenSize;
	private float lastYPosition;

	private void Start()
	{
		var screenW = Camera.main.orthographicSize * Camera.main.aspect;
		var screenH = Camera.main.orthographicSize;
		screenSize = new Vector2(screenW, screenH);

		path.positionCount = 2;
		path.SetPosition(1, startPosition);
		path.SetPosition(0, enemySpawnPosition);
		puzzlePlayer.transform.position = startPosition;
		lastYPosition = startPosition.y;
		candyEnemy.transform.position = enemySpawnPosition;

	}

	private void Update()
	{
		if (puzzlePlayer.transform.position.y - generateOffset < lastYPosition)
		{
			GenerateNextPoint();
		}
	}

	public void GenerateNextPoint()
	{
		float length = Random.Range(sectorLengthRange.x, sectorLengthRange.y);
		float xPosition = Random.Range(-screenSize.x + xGenerateOffset, screenSize.x - xGenerateOffset);
		Vector2 prevPosition = path.GetPosition(path.positionCount - 1);
		prevPosition.y -= length;
		prevPosition.x = xPosition;

		path.positionCount++;
		path.SetPosition(path.positionCount - 1, prevPosition);
		lastYPosition = prevPosition.y;

		if (Random.Range(0, 1f) < candyTargetSpawnChance)
		{
			SpawnTarget();
		}

		SetColliderPoints();
	}

	public void SpawnTarget()
	{
		Vector2 position = path.GetPosition(path.positionCount - 1);
		Instantiate(candyTarget, position, Quaternion.identity, transform);
	}

	public void SetColliderPoints()
	{
		Vector3[] positions = new Vector3[path.positionCount];
		path.GetPositions(positions);

		Vector2[] leftPoints = new Vector2[path.positionCount];
		Vector2[] rightPoints = new Vector2[path.positionCount];
		rightEdge.positionCount = rightPoints.Length;
		leftEdge.positionCount = leftPoints.Length;
		Vector3 prevNormal = Vector2.zero;
		Vector3 nextNormal = Vector2.zero;
		Vector3 nextDir = Vector2.zero;
		Vector3 prevDir = Vector2.zero;
		Vector3 normal = Vector2.zero;
		leftPoints[0] = new Vector2(edgeColliderOffset, 0);
		rightPoints[0] = new Vector2(-edgeColliderOffset, 0);
		leftPoints[path.positionCount - 1] = new Vector2(positions[path.positionCount - 1].x + edgeColliderOffset, positions[path.positionCount - 1].y);
		rightPoints[path.positionCount - 1] = new Vector2(positions[path.positionCount - 1].x - edgeColliderOffset, positions[path.positionCount - 1].y);
		leftEdge.SetPosition(0, leftPoints[0]);
		rightEdge.SetPosition(0, rightPoints[0]);
		leftEdge.SetPosition(path.positionCount - 1, leftPoints[path.positionCount - 1]);
		rightEdge.SetPosition(path.positionCount - 1, rightPoints[path.positionCount - 1]);


		for (int i = 1; i < positions.Length - 1; i++)
		{
			prevDir = (positions[i] - positions[i - 1]).normalized;
			nextDir = (positions[i + 1] - positions[i]).normalized;
			prevNormal.x = -prevDir.y;
			prevNormal.y = prevDir.x;

			nextNormal.x = -nextDir.y;
			nextNormal.y = nextDir.x;
			normal = (prevNormal + nextNormal).normalized;

			leftPoints[i] = positions[i] + normal * edgeColliderOffset;
			rightPoints[i] = positions[i] - normal * edgeColliderOffset;

			leftEdge.SetPosition(i, leftPoints[i]);
			rightEdge.SetPosition(i, rightPoints[i]);
		}

		leftCollider.points = leftPoints;
		rightCollider.points = rightPoints;
		SetWallsLeft(leftEdge, true);
		SetWallsLeft(rightEdge, false);
	}

	public void SetWallsLeft(LineRenderer renderer, bool left)
	{
		Vector2 lastPos = renderer.GetPosition(renderer.positionCount - 1);
		Vector2 prevPos = renderer.GetPosition(renderer.positionCount - 2);
		Vector2 middlePosition = new Vector2((prevPos.x + lastPos.x) / 2, (prevPos.y + lastPos.y) / 2);
		Vector2 direction = (lastPos - prevPos).normalized;
		float distance = Vector2.Distance(prevPos, lastPos);

		Vector2 normal;
		if (left)
		{
			normal = new Vector2(-direction.y, direction.x);
		}
		else
		{
			normal = new Vector2(direction.y, -direction.x);
		}

		var leftWall = Instantiate(redWall, middlePosition, Quaternion.identity, transform);
		leftWall.transform.forward = normal;
		var euler = leftWall.transform.eulerAngles;
		euler.z = 90;
		leftWall.transform.eulerAngles = euler;
		var scale = leftWall.transform.localScale;
		scale.x = distance;
		leftWall.transform.localScale = scale;
	}
}
