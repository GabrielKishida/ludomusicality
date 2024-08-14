using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour {

	public bool hasLost = false;
	[SerializeField] private GameObject loseMenu;
	[SerializeField] private MusicController musicController;
	[SerializeField] private PlayerHealthEventScriptableObject playerHealthEvent;

	private void Start() {
		loseMenu.SetActive(false);
		playerHealthEvent.deathEvent.AddListener(ShowLossScreen);
	}

	public void ShowLossScreen() {
		loseMenu.SetActive(true);
		Time.timeScale = 0;
		hasLost = true;
		musicController.PauseMusic();
	}
	public void ReplayGame() {
		Scene currentScene = SceneManager.GetActiveScene();
		musicController.Reset();
		SceneManager.LoadScene(currentScene.name);
		Time.timeScale = 1;
	}

	public void CloseGame() {
		Application.Quit();
	}
}
