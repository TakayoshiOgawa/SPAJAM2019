using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace OG.Audio
{
    public class MixerController : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;

        private readonly string MASTER = "MASTER";
        private readonly string BGM = "BGM";
        private readonly string SE = "SE";

        private float GetVolumeByLiner(string name)
        {
            float decibel;
            mixer.GetFloat(name, out decibel);
            return Mathf.Pow(10F, decibel / 20F);
        }

        private void SetVolumeByLiner(string name, float volume)
        {
            float decibel = 20F * Mathf.Log10(volume);
            if (float.IsNegativeInfinity(decibel)) decibel = -96F;
            mixer.SetFloat(name, decibel);
        }

        public float GetMasterVolumeByLiner()
        {
            return GetVolumeByLiner(MASTER);
        }

        public void SetMasterVolumeByLiner(float volume)
        {
            SetVolumeByLiner(MASTER, volume);
        }

        public float GetBgmVolumeByLiner()
        {
            return GetVolumeByLiner(BGM);
        }

        public void SetBgmVolumeByLiner(float volume)
        {
            SetVolumeByLiner(BGM, volume);
        }

        public float GetSeVolumeByLiner()
        {
            return GetVolumeByLiner(SE);
        }

        public void SetSeVolumeByLiner(float volume)
        {
            SetVolumeByLiner(SE, volume);
        }
    }
}
