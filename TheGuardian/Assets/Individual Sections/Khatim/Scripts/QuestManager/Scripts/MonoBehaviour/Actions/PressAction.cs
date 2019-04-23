using vidioomedia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vidioomedia
{
    [RequireComponent(typeof(BoxCollider))]
    public class PressAction : Action
    {
        private void Start()
        {
            //GetComponent<Collider>().isTrigger = true;
        }
        public override void Begin()
        {
            base.Begin();
        }
        private void OnTriggerEnter(Collider other)
        {
            Complete();
        }

    }
}