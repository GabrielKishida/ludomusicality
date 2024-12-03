using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour {

	public bool hasLost = false;
	[SerializeField] private GameObject loseMenu;
	[SerializeField] private MusicController musicController;
	[SerializeField] private PlayerHealthEventScriptableObject playerHealthEvent;
	[SerializeField] EventScriptableObject reachedCheckpointEvent;
	[SerializeField] EventScriptableObject reloadCheckpointEvent;

	[SerializeField] private bool checkpointReached = false;

	private void Start() {
		loseMenu.SetActive(false);
		checkpointReached = false;
		reachedCheckpointEvent.AddListener(ReachCheckpoint);
		playerHealthEvent.deathEvent.AddListener(ShowLossScreen);
	}
	private void ReachCheckpoint() {
		checkpointReached = true;
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
		if (checkpointReached) {
			Debug.Log("Reloading checkpoint");
			SceneManager.LoadScene(currentScene.name + " Checkpoint");
		}
		else {
			Debug.Log("Loading Scene");
			SceneManager.LoadScene(currentScene.name);
		}
		Time.timeScale = 1;
	}

	public void CloseGame() {
		Application.Quit();
	}
}
