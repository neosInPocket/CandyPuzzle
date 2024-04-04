using UnityEngine;

public class LevelLimiter : MonoBehaviour
{
	[SerializeField] new public SpriteRenderer renderer;
	private Vector2 screenSize;

	private void Awake()
	{
		var screenW = Camera.main.orthographicSize * Camera.main.aspect;
		var screenH = Camera.main.orthographicSize;
		screenSize = new Vector2(screenW, screenH);
	}

	public void SetPosition(Vector2 position, float size)
	{
		Vector2 pos = Vector2.zero;
		Vector2 limiterSize = Vector2.zero;

		if (position == Vector2.up)
		{
			pos.x = 0;
			pos.y = screenSize.y + size / 2;
			limiterSize.x = 2 * screenSize.x;
			limiterSize.y = size;
		}

		if (position == Vector2.down)
		{
			pos.x = 0;
			pos.y = -screenSize.y - size / 2;
			limiterSize.x = 2 * screenSize.x;
			limiterSize.y = size;
		}

		if (position == Vector2.left)
		{
			pos.x = -screenSize.x - size / 2;
			pos.y = 0;
			limiterSize.x = size;
			limiterSize.y = 2 * screenSize.y;
		}

		if (position == Vector2.right)
		{
			pos.x = screenSize.x + size / 2;
			pos.y = 0;
			limiterSize.x = size;
			limiterSize.y = 2 * screenSize.y;
		}

		transform.position = pos;
		renderer.size = limiterSize;
	}
}
