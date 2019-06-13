using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OG.Audio;
using OG.Extensions;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [Header("AudioPlayer")]
    [SerializeField] private BgmPlayer bgm;
    [SerializeField] private SePlayer se;
    [SerializeField] private MixerController mixer;

    public BgmPlayer BGM { get { return bgm; } }
    public SePlayer SE { get { return se; } }
    public MixerController Mixer { get { return mixer; } }

    protected override void Awake()
    {
        base.Awake();

        if (bgm == null)
        {
            var bgmPlayer = new GameObject("BgmPlayer");
            bgmPlayer.transform.SetParent(transform);
            bgm = bgmPlayer.AddComponent<BgmPlayer>();
        }

        if (se == null)
        {
            var sePlayer = new GameObject("SePlayer");
            sePlayer.transform.SetParent(transform);
            se = sePlayer.AddComponent<SePlayer>();
        }

        if (mixer == null)
        {
            var audiomixer = new GameObject("MixerController");
            audiomixer.transform.SetParent(transform);
            mixer = audiomixer.AddComponent<MixerController>();
        }
    }
}
