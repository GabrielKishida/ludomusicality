using UnityEngine;

public class PlayerVisualsController : MonoBehaviour {


	[Header("Player Render Variables")]
	[SerializeField] private Color playerOriginalColor;
	[SerializeField] private Renderer playerRenderer;
	[SerializeField] private GameObject playerModel;

	public void SetPlayerColor(Color color) {
		playerRenderer.material.color = color;
	}

	public void ResetPlayerColor() {
		playerRenderer.material.color = playerOriginalColor;
	}

	private void Start() {
		playerRenderer = playerModel.GetComponent<Renderer>();
		playerOriginalColor = playerRenderer.material.color;
	}
}
