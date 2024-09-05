using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Custom.Scripts.Objects {
	public class ObjectLerp : MonoBehaviour {
		[SerializeField] private float duration;

		[SerializeField] private Transform transformA;
		[SerializeField] private Transform transformB;

		[SerializeField] public EventScriptableObject moveToAEvent;
		[SerializeField] public EventScriptableObject moveToBEvent;


		private bool movingToA = false;
		private bool movingToB = false;

		private void Start() {
			moveToAEvent.AddListener(MoveToA);
			moveToBEvent.AddListener(MoveToB);
		}

		private IEnumerator MoveToACoroutine() {
			float elapsedTime = 0f;
			Vector3 startingPosition = transform.position;
			movingToA = true;
			movingToB = false;
			while (elapsedTime < duration) {
				if (movingToB) { break; }

				float percentageComplete = elapsedTime / duration;
				float smoothStep = Mathf.SmoothStep(0f, 1f, percentageComplete);
				transform.position = Vector3.Lerp(startingPosition, transformA.position, smoothStep);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			movingToA = false;
			transform.position = transformA.position;
		}

		private IEnumerator MoveToBCoroutine() {
			float elapsedTime = 0f;
			Vector3 startingPosition = transform.position;
			movingToB = true;
			movingToA = false;
			while (elapsedTime < duration) {
				if (movingToA) { break; }

				float percentageComplete = elapsedTime / duration;
				float smoothStep = Mathf.SmoothStep(0f, 1f, percentageComplete);
				transform.position = Vector3.Lerp(startingPosition, transformB.position, smoothStep);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			movingToB = false;
			transform.position = transformB.position;
		}

		private void MoveToA() {
			StartCoroutine(MoveToACoroutine());
		}

		private void MoveToB() {
			StartCoroutine(MoveToBCoroutine());
		}
	}
}
