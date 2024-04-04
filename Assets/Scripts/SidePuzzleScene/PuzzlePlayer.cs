using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using Finger = UnityEngine.InputSystem.EnhancedTouch.Finger;
using System;

public class PuzzlePlayer : MonoBehaviour
{
	[SerializeField] private float[] spinSpeedUpgrades;
	[SerializeField] private float[] sizes;
	[SerializeField] private Rigidbody2D rigidEngine;
	[SerializeField] private new SpriteRenderer renderer;
	[SerializeField] private GameObject endParticles;
	[SerializeField] private GameObject glow;

	[SerializeField] private float crawlSpeed;
	private float spinSpeed;
	private Vector3 currentSpin;
	public Action TargetCaptured { get; set; }
	public Action EndLevel { get; set; }
	public float CurrentSpinValue
	{
		get => currentSpin.z;
		set
		{
			currentSpin.z = value;
			transform.eulerAngles = currentSpin;
		}
	}
	public bool Activate
	{
		get => activate;
		set
		{
			activate = value;

			if (!value)
			{
				Touch.onFingerDown -= PuzzleFingerDown;
				Touch.onFingerUp -= PuzzleFingerUp;
				rigidEngine.velocity = Vector2.zero;
			}
			else
			{
				Touch.onFingerDown += PuzzleFingerDown;
				Touch.onFingerUp += PuzzleFingerUp;
			}
		}
	}
	private bool activate;
	private bool spinActive = true;
	public bool Ready;

	private void Start()
	{
		spinSpeed = spinSpeedUpgrades[PuzzleSystem.save.firstLevelUpdate];
		transform.localScale *= sizes[PuzzleSystem.save.secondLevelUpdate];

		CurrentSpinValue = 0;
	}

	private void Update()
	{
		if (transform.position.y > 1) Collide();

		if (!Activate) return;
		if (!spinActive) return;

		CurrentSpinValue += spinSpeed * Time.deltaTime;
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (!Ready) return;

		if (collider.TryGetComponent<CandyTarget>(out CandyTarget candyTarget))
		{
			candyTarget.Target();
			TargetCaptured?.Invoke();
			return;
		}

		if (collider.TryGetComponent<CandyEnemy>(out CandyEnemy enemy))
		{
			Collide();
			return;
		}

		if (collider.TryGetComponent<LevelLimiter>(out LevelLimiter limiter))
		{
			Collide();
			return;
		}

		Collide();
	}

	public void Collide()
	{
		Activate = false;
		glow.SetActive(false);
		renderer.enabled = false;
		endParticles.gameObject.SetActive(true);
		rigidEngine.velocity = Vector2.zero;
		EndLevel?.Invoke();
	}

	public void PuzzleFingerDown(Finger finger)
	{
		spinActive = false;
		rigidEngine.velocity = -transform.up * crawlSpeed;
	}

	public void PuzzleFingerUp(Finger finger)
	{
		spinActive = true;
		rigidEngine.velocity = Vector2.zero;
	}

	private void OnDestroy()
	{
		Activate = false;
	}
}
