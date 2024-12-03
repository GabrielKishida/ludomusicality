using UnityEngine;

public class CharacterVisualsController : MonoBehaviour {
	[Header("Character Render Variables")]
	[SerializeField] private Color characterOriginalColor;
	[SerializeField] private Renderer characterRenderer;
	[SerializeField] private GameObject characterModel;

	public void SetCharacterColor(Color color) {
		characterRenderer.material.color = color;
	}

	public void ResetCharacterColor() {
		characterRenderer.material.color = characterOriginalColor;
	}

	private void Start() {
		characterRenderer = characterModel.GetComponent<Renderer>();
		characterOriginalColor = characterRenderer.material.color;
	}
}
