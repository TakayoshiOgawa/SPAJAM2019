using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OG.Extensions;

namespace OG.Audio
{
    public class SePlayer : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> seList;
        [SerializeField] private List<AudioClip> audioList;

        /// <summary>
        /// 指定のプレイヤーで再生
        /// </summary>
        public void Play(int index, AudioClip clip)
        {
            if (index >= seList.Count) return;
            seList[index].PlayOneShot(clip);
        }

        /// <summary>
        /// オーディオリストから取得
        /// </summary>
        public AudioClip GetAudio(string se)
        {
            return audioList.Where(audio => audio.name.Equals(se)).FirstOrDefault();
        }
    }
}
