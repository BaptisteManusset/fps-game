using System;



  #region VARIABLES

  public static AudioManager instance;


  #endregion




  // Use this for initialization

  void Awake()
    {
      s.source.clip = s.audioClip;
      s.source.volume = s.volume;
      s.source.loop = s.isLooping;
      if (s.playOnAwake)
      {
        s.source.Play();
      }
    }
  }

  #region METHODS

  public static void PlayMusic(string name)
  {
    AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
    if (s == null)
    {
      Debug.LogError("Sound name" + name + "not found!");
      return;
    }
    else
    {
      s.source.Play();
    }
  }
  public static void StopMusic(String name)
  {
    AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
    if (s == null)
    {
      Debug.LogError("Sound name" + name + "not found!");
      return;
    }
    else
    {
      s.source.Stop();
    }
  }
  public static void PauseMusic(String name)
  {
    AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
    if (s == null)
    {
      Debug.LogError("Sound name" + name + "not found!");
      return;
    }
    else
    {
      s.source.Pause();
    }
  }
  public static void UnPauseMusic(String name)
  {
    AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
    if (s == null)
    {
      Debug.LogError("Sound name" + name + "not found!");
      return;
    }
    else
    {
      s.source.UnPause();
    }
  }
  {
    if (instance.isLowered == false)
    {
      AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
      if (s == null)
      {
        Debug.LogError("Sound name" + name + "not found!");
        return;
      }
      else
      {
        instance.tmpName = name;
        instance.tmpVol = s.volume;
        instance.timeToReset = Time.time + _duration;
        instance.timerIsSet = true;
        s.source.volume = s.source.volume / 3;
      }
      instance.isLowered = true;
    }
  }
  public static void FadeOut(String name, float duration)
  {
    instance.StartCoroutine(instance.IFadeOut(name, duration));
  }
  public static void FadeIn(String name, float targetVolume, float duration)
  {
    instance.StartCoroutine(instance.IFadeIn(name, targetVolume, duration));
  }

  //not for use
  private IEnumerator IFadeOut(String name, float duration)
  {
    AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
    if (s == null)
    {
      Debug.LogError("Sound name" + name + "not found!");
      yield return null;
    }
    else
    {
      if (fadeOut == false)
      {
        fadeOut = true;
        float startVol = s.source.volume;
        fadeOutUsedString = name;
        while (s.source.volume > 0)
        {
          s.source.volume -= startVol * Time.deltaTime / duration;
          yield return null;
        }
        s.source.Stop();
        yield return new WaitForSeconds(duration);
        fadeOut = false;
      }
      else
      {
        Debug.Log("Could not handle two fade outs at once : " + name + " , " + fadeOutUsedString + "! Stopped the music " + name);
        StopMusic(name);
      }
    }
  }
  public IEnumerator IFadeIn(string name, float targetVolume, float duration)
  {
    AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == name);
    if (s == null)
    {
      Debug.LogError("Sound name" + name + "not found!");
      yield return null;
    }
    else
    {
      if (fadeIn == false)
      {
        fadeIn = true;
        instance.fadeInUsedString = name;
        s.source.volume = 0f;
        s.source.Play();
        while (s.source.volume < targetVolume)
        {
          s.source.volume += Time.deltaTime / duration;
          yield return null;
        }
        yield return new WaitForSeconds(duration);
        fadeIn = false;
      }
      else
      {
        Debug.Log("Could not handle two fade ins at once: " + name + " , " + fadeInUsedString + "! Played the music " + name);
        StopMusic(fadeInUsedString);
        PlayMusic(name);
      }
    }
  }
  void ResetVol()
  {
    AudioFile s = Array.Find(instance.audioFiles, AudioFile => AudioFile.audioName == tmpName);
    s.source.volume = tmpVol;
    isLowered = false;
  }
  {
    if (Time.time >= timeToReset && timerIsSet)
    {
      ResetVol();
      timerIsSet = false;
    }
  #endregion
}