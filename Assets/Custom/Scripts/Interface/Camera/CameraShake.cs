
using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraShake : MonoBehaviour {
	[SerializeField] float shakeFalloffTime = 0.3f;
	Coroutine currentCoroutine;

	private static CameraShake _instance;

	public static CameraShake Instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<CameraShake>();

				if (_instance == null) {
					GameObject singletonObject = new GameObject();
					_instance = singletonObject.AddComponent<CameraShake>();
					singletonObject.name = typeof(CameraShake).ToString() + " (Singleton)";
					DontDestroyOnLoad(singletonObject);
				}
			}
			return _instance;
		}
	}

	private void InitCamera() {
		CinemachineVirtualCamera activeCamera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as CinemachineVirtualCamera;
		CinemachineBasicMultiChannelPerlin noise = activeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		noise.m_AmplitudeGain = 0;
	}

	public void Start() {
		InitCamera();
	}

	public void OnEnable() {
		InitCamera();
	}

	public void ApplyCameraShake(float amplitude) {
		if (currentCoroutine != null) {
			StopCoroutine(currentCoroutine);
		}
		currentCoroutine = StartCoroutine(OnCameraShakeCoroutine(amplitude));
	}

	IEnumerator OnCameraShakeCoroutine(float amplitude) {
		CinemachineVirtualCamera activeCamera = CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera as CinemachineVirtualCamera;
		CinemachineBasicMultiChannelPerlin noise = activeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
		if (noise != null) {
			float elapsedTime = 0f;
			while (elapsedTime < shakeFalloffTime) {
				float percentageComplete = elapsedTime / shakeFalloffTime;
				noise.m_AmplitudeGain = Mathf.SmoothStep(amplitude, 0, percentageComplete);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			noise.m_AmplitudeGain = 0;
		}
		yield return null;
	}
}
