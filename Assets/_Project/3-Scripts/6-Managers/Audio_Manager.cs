using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// BGM Functions
/// PlayBackgroundAudio(), StopBackgroundAudio(), PauseBackgroundAudio(), ChangeBackgroundAudio(AudioClip audioClip)
/// TransitionBackgroundAudio(AudioClip audioClip, float duration) Transition between current BGM and newly supplied BGM. Fade in and out by duration amount.
/// StartFade(float duration, float targetVolume) Fade out or fade in current audio by duration amount. Target volume between 0(muted) and 1(loudest).
/// 
/// SFX Functions
/// PlaySFX(AudioClip audioClip) Plays supplied audioClip
/// </summary>
///
[Serializable]
public class LevelToBGM
{
	public string levelName;
	public AudioClip levelBGM;
}
public class Audio_Manager : MonoBehaviour
{
	public static Audio_Manager current;

	[Tooltip("Audio Mixer Object")]
	[SerializeField] private AudioMixer audioMixer;

	[Tooltip("Volume to change string in audio mixer")]
	[SerializeField] private string exposedParamString;
	[SerializeField] private string exposedSFXString;

	[Tooltip("AudioSource component for background music")]
    [SerializeField] private AudioSource musicAudioSource;

	[Tooltip("AudioSource component for sound effects")]
	[SerializeField] private AudioSource SFXAudioSource;

	private List<GameObject> _longSFXList = new List<GameObject>();

	public List<LevelToBGM> levelToBgmsList;
	public List<AudioClip> randomBGM;

	private void Awake()
	{
		if (current != this && current != null)
		{
			Destroy(this);
		}
		else
		{
			current = this;
			musicAudioSource = GetComponent<AudioSource>();

			DontDestroyOnLoad(gameObject);
			SceneManager.sceneLoaded += OnSceneLoaded;
			//PlayBGMFromLevel();
		}
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		//if (scene.name != "Player_TestScene") return;
		
		PlayBGMFromLevel();
	}

	private void PlayBGMFromLevel()
	{
		bool soundFound = false;
		for (int ii = 0; ii < SceneManager.sceneCount; ii++)
		{
			foreach (LevelToBGM obj in levelToBgmsList)
			{
				if (obj.levelName == SceneManager.GetSceneAt(ii).name)
				{
					ChangeBackgroundAudio(obj.levelBGM);
					soundFound = true;
					break;
				}
			}
			if (soundFound) break;
		}

		if (!soundFound)
		{
			ChangeBackgroundAudio(randomBGM[Random.Range(0, randomBGM.Count)]);
		}
		
		if(SFXAudioSource.isPlaying) SFXAudioSource.Stop();
	}
	
	#region Background Audio Functions
	public void PlayBackgroundAudio()
	{
		musicAudioSource.Play();
	}

	public void StopBackgroundAudio()
	{
		musicAudioSource.Stop();
	}

	public void PauseBackgroundAudio()
	{
		musicAudioSource.Pause();
	}

	public void ChangeBackgroundAudio(AudioClip audioClip)
	{
		if (musicAudioSource.clip != null && audioClip.name == musicAudioSource.clip.name) return;
		StopBackgroundAudio();
		musicAudioSource.clip = audioClip;
		PlayBackgroundAudio();
	}

	public void TransitionBackgroundAudio(AudioClip audioClip, float duration)
	{
		if (audioClip.name == musicAudioSource.clip.name) return;
		StartCoroutine(TransitionBackground_Coroutine(audioClip, duration));
	}

	private IEnumerator TransitionBackground_Coroutine(AudioClip audioClip, float duration)
	{
		StartFade(duration, 0f);
		yield return new WaitForSeconds(duration);
		ChangeBackgroundAudio(audioClip);
		StartFade(duration, 1f);
	}
	#endregion

	#region Audio Fade Functions
	public void StartFade(float duration, float targetVolume)
	{
		StartCoroutine(StartFade_Coroutine(duration, targetVolume));
	}

	private IEnumerator StartFade_Coroutine(float duration, float targetVolume)
	{
		float currentTime = 0;
		float currentVol;

		audioMixer.GetFloat(exposedParamString, out currentVol);
		currentVol = Mathf.Pow(10, currentVol / 20);
		float targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1f);
		while (currentTime < duration)
		{
			currentTime += Time.deltaTime;
			float newVol = Mathf.Lerp(currentVol, targetValue, currentTime / duration);
			audioMixer.SetFloat(exposedParamString, Mathf.Log10(newVol) * 20);
			yield return null;
		}
		yield break;
	}

	public void SetSFXVolume(float value)
	{
		audioMixer.SetFloat(exposedSFXString, Mathf.Clamp(value, 0, 20));
	}

	public void SetBGMVolume(float value)
	{
		audioMixer.SetFloat(exposedParamString, value);
	}
	#endregion

	#region SFX Functions
	public void PlaySFX(AudioClip audioClip)
	{
		SFXAudioSource.PlayOneShot(audioClip);
	}

	public GameObject PlaySFXAtPoint(AudioClip audioClip, Vector3 position)
	{
		GameObject spawnedObject = new GameObject(name);
		AudioSource source = spawnedObject.AddComponent<AudioSource>();

		spawnedObject.transform.position = position;
		source.clip = audioClip;
		source.outputAudioMixerGroup = SFXAudioSource.outputAudioMixerGroup;
		source.rolloffMode = AudioRolloffMode.Linear;
		source.maxDistance = 30f;
		source.spatialBlend = 1f;
		source.Play();
		
		Destroy(spawnedObject, 1f);
		return spawnedObject;
	}
	
	public void PlayLongSFX(string name, AudioClip audioClip)
	{
		GameObject spawnedObject = new GameObject(name);
		spawnedObject.AddComponent<AudioSource>();
		spawnedObject.GetComponent<AudioSource>().clip = audioClip;
		spawnedObject.GetComponent<AudioSource>().Play();
		_longSFXList.Add(spawnedObject);
	}

	public void StopLongSFX(string name)
	{
		GameObject tempGO = null;
		for (int ii = _longSFXList.Count - 1; ii >= 0; ii--)
		{
			if (_longSFXList[ii] == null) _longSFXList.RemoveAt(ii);
		}
		foreach (GameObject sfx in _longSFXList)
		{
			if (sfx != null && sfx.name == name)
			{
				tempGO = sfx;
				break;
			}
		}
		Destroy(tempGO);
		_longSFXList.Remove(tempGO);
	}

	#endregion
}
