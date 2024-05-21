using UnityEngine;

public class PauseManager : MonoBehaviour {

	public bool isPaused = false;
	[SerializeField] private GameObject pauseMenu;
	[SerializeField] private MusicController musicController;

	private void Start() {
		pauseMenu.SetActive(false);
	}
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (isPaused) {
				ResumeGame();
			}
			else {
				PauseGame();
			}
		}
	}

	public void PauseGame() {
		pauseMenu.SetActive(true);
		Time.timeScale = 0;
		isPaused = true;
		musicController.PauseMusic();
	}
	public void ResumeGame() {
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
		isPaused = false;
		musicController.ResumeMusic();
	}
	public void CloseGame() {
		Application.Quit();
	}
}
