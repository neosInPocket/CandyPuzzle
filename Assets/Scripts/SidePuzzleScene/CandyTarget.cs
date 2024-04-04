using System.Collections;
using UnityEngine;

public class CandyTarget : MonoBehaviour
{
	[SerializeField] private SpriteRenderer Renderer;
	[SerializeField] private GameObject endEffect;
	[SerializeField] private new Collider2D collider;
	[SerializeField] private GameObject charge;

	public void Target()
	{
		collider.enabled = false;
		Renderer.enabled = false;
		charge.SetActive(false);
		endEffect.gameObject.SetActive(true);
	}

	private IEnumerator EndRoutine()
	{
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
