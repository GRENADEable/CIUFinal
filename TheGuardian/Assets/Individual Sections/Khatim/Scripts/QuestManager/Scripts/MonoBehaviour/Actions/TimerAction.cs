using vidioomedia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vidioomedia
{
    public class TimerAction : Action
    {
        public float time;

        public override void Begin()
        {
            base.Begin();
            StartCoroutine(TimerActionUpdate());
        }
        private IEnumerator TimerActionUpdate()
        {
            float elapsedTime = 0;

            while (elapsedTime < time)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            Complete();
            
        }

    }
}