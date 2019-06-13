using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OG.Extensions
{
    public static class AudioSourceExtension
    {
        public static bool Play(this AudioSource self, AudioClip clip = null, float volume = 1F)
        {
            if (clip == null || volume < 0F) return false;

            self.clip = clip;
            self.volume = volume;
            self.Play();

            return true;
        }

        public static IEnumerator PlayWithFadeIn(this AudioSource self, AudioClip clip, float fadetime = 0.1F)
        {
            self.Play(clip, 0F);

            while (self.volume < 1F)
            {
                self.volume = Mathf.Clamp01(self.volume + (Time.deltaTime / fadetime));
                yield return null;
            }
        }

        public static IEnumerator StopWithFadeOut(this AudioSource self, float fadetime = 0.1F)
        {
            while (self.volume > 0F)
            {
                self.volume = Mathf.Clamp01(self.volume - (Time.deltaTime / fadetime));
                yield return null;
            }

            self.Stop();
        }
    }
}
