using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OG.Extensions;

namespace OG.Audio
{
    public class BgmPlayer : MonoBehaviour
    {
        [SerializeField] private List<AudioSource> bgmList;
        [SerializeField] private List<AudioClip> audioList;

        /// <summary>
        /// フェード処理をしながら再生
        /// </summary>
        public void PlayWithFade(AudioClip clip, float fadetime = 0.1F)
        {
            // 空のプレイヤーと再生中のプレイヤーを取得
            var empty = bgmList.FirstOrDefault(asb => asb.isPlaying.Equals(false));
            var playing = bgmList.FirstOrDefault(asb => asb.isPlaying.Equals(true));

            // 再生中のをフェードアウト、空のをフェードイン
            if (playing != null)
            {
                StartCoroutine(playing.StopWithFadeOut(fadetime));
            }
            StartCoroutine(empty.PlayWithFadeIn(clip, fadetime));
        }

        /// <summary>
        /// オーディオリストから取得
        /// </summary>
        public AudioClip GetAudio(string bgm)
        {
            return audioList.Where(audio => audio.name.Equals(bgm)).FirstOrDefault();
        }
    }
}
