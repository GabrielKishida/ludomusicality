using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

	public Color hpColor;
	public Color damagedColor;
	public Image[] hpCounter;

	[SerializeField] private PlayerHealthEventScriptableObject playerHealth;

	private void UpdateImage(float currentHp) {
		for (int i = 0; i < hpCounter.Length; i++) {
			hpCounter[i].color = (currentHp > i) ? hpColor : damagedColor;
		}
	}

	private void Start() {
		playerHealth.healthEvent.AddListener(UpdateImage);
	}
}
