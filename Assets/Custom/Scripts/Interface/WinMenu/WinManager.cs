using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour {

	public bool hasWon = false;
	[SerializeField] private GameObject winMenu;
	[SerializeField] private MusicController musicController;
	[SerializeField] private EventScriptableObject winEvent;

	private void Start() {
		winMenu.SetActive(false);
		winEvent.AddListener(ShowWinScreen);
	}
	public void ShowWinScreen() {
		winMenu.SetActive(true);
		Time.timeScale = 0;
		hasWon = true;
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
