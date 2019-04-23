using vidioomedia;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace vidioomedia
{
    [RequireComponent(typeof(BoxCollider))]
    public class LookAction : Action
    {
        public LayerMask mask;
        private BoxCollider boxCollider;
        Transform player;

        private void Awake()
        {
            boxCollider = this.GetComponent<BoxCollider>() ;
            player = Camera.main.transform;
            //this.boxCollider.enabled = false;

        }

        public override void Begin()
        {
            base.Begin();
            StartCoroutine(LookActionUpdate());
            this.boxCollider.enabled = true;
        }

        private IEnumerator LookActionUpdate()
        {
            bool looked = false;
            do
            {
                yield return null;
                var ray = new Ray(player.transform.position, player.transform.forward);
                RaycastHit hit;
                if(Physics.Raycast(ray, out hit, mask))
                {
                    if (hit.collider == this.boxCollider)
                    {
                        looked = true;
                    }
                    
                }
                
            }
            while (!looked);
            this.boxCollider.enabled = false;
            Complete();
            
        }

        private void OnDrawGizmosSelected()
        {
            if(!boxCollider)
                boxCollider = this.GetComponent<BoxCollider>();

            var color = Color.green;
            color.a = .5f;
            Gizmos.color = color;

            Gizmos.DrawCube(this.transform.position + boxCollider.center, this.boxCollider.size);
        }

        private void OnDrawGizmos()
        {
            if (!boxCollider)
                boxCollider = this.GetComponent<BoxCollider>();

            var color = Color.red;
            color.a = .2f;
            Gizmos.color = color;

            Gizmos.DrawCube(this.transform.position + boxCollider.center,  this.boxCollider.size);
        }
    }
}