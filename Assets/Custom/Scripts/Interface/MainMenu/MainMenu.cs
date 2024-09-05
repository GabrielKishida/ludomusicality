using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public void StartGame() {
		SceneManager.LoadScene("Map1");
	}
	public void CloseGame() {
		Application.Quit();
	}
}
