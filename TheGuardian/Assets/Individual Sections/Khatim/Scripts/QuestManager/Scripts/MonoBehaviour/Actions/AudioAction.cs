using vidioomedia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vidioomedia
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioAction : Action
    {
        private AudioSource source;
        private void Start()
        {
            source = GetComponent<AudioSource>();
        }
        public override void Begin()
        {
            base.Begin();
            if(!source) source = GetComponent<AudioSource>();

            source.Play();
            StartCoroutine(AudioActionUpdate());
        }
        private IEnumerator AudioActionUpdate()
        {
            do
            {
                yield return null;
            }
            while (source.isPlaying);
            Complete();
            
        }
    }
}