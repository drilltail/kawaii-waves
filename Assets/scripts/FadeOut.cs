using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut {

  public static IEnumerator fadeOut(AudioSource source, float fadeTime) {
    float startVolume = source.volume;

    while (source.volume > 0) {
      source.volume -= startVolume * Time.deltaTime / fadeTime;

      yield return null;
    }

    source.Stop();
    source.volume = startVolume;
  }
}
