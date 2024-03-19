using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour {

	public Color hpColor;
	public Color damagedColor;
	public Image[] hpCounter;

	private float maxHp;
	private float currentHp;

	void Start() {
		maxHp = hpCounter.Length;
		currentHp = maxHp;
	}

	private void UpdateImage() {
		for (int i = 0; i < maxHp; i++) {
			hpCounter[i].color = (currentHp > i) ? hpColor : damagedColor;
		}
	}

	public void TakeDamage(float damage) {
		currentHp -= damage;
		UpdateImage();
	}
}
