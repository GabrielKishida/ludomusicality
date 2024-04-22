using UnityEngine;

public class MusicControlller : MonoBehaviour {
	public AudioClip musicClip;

	[SerializeField] private AudioSource musicSource;
	[SerializeField] private float musicTime;

	[SerializeField] private MusicEventScriptableObject enemyEvent;
	[SerializeField] private MusicEventScriptableObject playerEvent;

	void Start() {
		musicSource = GetComponent<AudioSource>();
		musicSource.clip = musicClip;
		musicSource.loop = true;
		musicSource.Play();
	}

	private void Update() {
		musicTime = (float)musicSource.timeSamples / musicSource.clip.frequency;
		enemyEvent.UpdateMusicTime(musicTime);
		playerEvent.UpdateMusicTime(musicTime);
	}
}
