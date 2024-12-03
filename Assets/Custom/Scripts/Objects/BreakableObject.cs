using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakableObject : MonoBehaviour {
	[SerializeField] private GameObject wholeObject;
	[SerializeField] private GameObject brokenObject;
	private List<GameObject> brokenPieces;
	[SerializeField] private Hurtbox hurtbox;

	[SerializeField] private float minDamageToBreak = 2.0f;
	[SerializeField] private float despawnDuration = 2.0f;
	[SerializeField] private float timeToStartDespawn = 5.0f;

	[SerializeField] private EventScriptableObject eventWhenBroken;

	private List<GameObject> GetObjectChildren(GameObject parentObject) {
		List<GameObject> children = new List<GameObject>();
		foreach (Transform child in parentObject.transform) {
			children.Add(child.gameObject);
		}
		return children;
	}

	private void BrokenPiecesSetActive(bool active) {
		foreach (GameObject piece in brokenPieces) {
			piece.SetActive(active);
		}
	}

	private void BrokenPiecesSetScale(Vector3 scale) {
		foreach (GameObject piece in brokenPieces) {
			piece.transform.localScale = scale;
		}
	}


	void Start() {
		wholeObject.SetActive(true);
		brokenPieces = GetObjectChildren(brokenObject);
		BrokenPiecesSetActive(false);
		hurtbox.hurtboxHitEvent.AddListener(OnHurtboxHit);
	}

	private void OnHurtboxHit(float damage, Vector3 knockback) {
		if (damage >= minDamageToBreak) {
			wholeObject.SetActive(false);
			brokenObject.transform.localPosition += wholeObject.transform.localPosition;
			brokenObject.transform.localRotation = wholeObject.transform.localRotation;
			if (eventWhenBroken != null) { eventWhenBroken.Invoke(); }
			BrokenPiecesSetActive(true);
			StartCoroutine(DespawnCoroutine());
		}
	}

	private IEnumerator DespawnCoroutine() {
		Vector3 initialScale = Vector3.one;
		float elapsedTime = 0f;

		yield return new WaitForSeconds(timeToStartDespawn);

		while (elapsedTime < despawnDuration) {
			float percentageComplete = elapsedTime / despawnDuration;
			float smoothStep = Mathf.SmoothStep(0f, 1f, percentageComplete);
			Vector3 newScale = Vector3.Lerp(initialScale, Vector3.zero, smoothStep);
			BrokenPiecesSetScale(newScale);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		transform.localScale = Vector3.zero;
		Destroy(gameObject);
	}
}
