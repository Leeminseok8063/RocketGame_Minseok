using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public enum SOUNDFILE
{
    BGM,
    SFX01
}

namespace Assets.Scripts.Challenge
{
    internal class SoundManger : Singleton<SoundManger>
    {
        private AudioSource audioSource; 
        [SerializeField]List<AudioClip> soundFiles = new List<AudioClip>();

        public void Init()
        {
            audioSource = this.AddComponent<AudioSource>();
            soundFiles.Add(Resources.Load<AudioClip>("Sounds/BGM"));           
        }

        private void Start()
        {
            audioSource.clip = soundFiles[(int)SOUNDFILE.BGM];
            audioSource.Play();
        }

    }
}
