using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{

	public static AudioManager instance;

	public AudioMixerGroup mixerGroup;

	public Sound[] sounds;


	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	private void Start()
	{
		PlayerPrefs.SetInt("MusicEnabled", 1);
		PlayerPrefs.SetInt("SoundEnabled", 1);
		Play("GameMusic");
		
	}
	public void Play(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		if( !s.source.isPlaying)
			s.source.Play();
	}

	public void PlayOnShot(string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		s.source.PlayOneShot(s.clip);
	}

	public void PlayWithFadeIn(string sound, float time)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = 0;
		StartCoroutine(FadeAudioSource.StartFade(s.source, time, s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f))));
		s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

		if (!s.source.isPlaying)
			s.source.Play();
	}

	public void PlayWithPitch(string sound , float pitch)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
		s.source.pitch = pitch;

		if (!s.source.isPlaying)
			s.source.Play();
	}

	public void Pause( string sound)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.Pause();
	}
	public void PauseWithFadeOut(string sound , float time)
	{
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}
		StartCoroutine(FadeAudioSource.StartFade( s.source, time, 0));
		StartCoroutine(PauseSound(s, time));

	}


	IEnumerator PauseSound(Sound s, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		// Now do your thing here
		s.source.Pause();
	}

}
