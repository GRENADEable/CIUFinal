using UnityEngine;

namespace Cinemachine.Examples
{
    public class CharacterMovement : MonoBehaviour
    {
        public float turnSpeed = 10f;
        public KeyCode sprintKeyboard = KeyCode.LeftShift;

        private float turnSpeedMultiplier;
        private float speed = 0f;

        private float direction = 0f;
        private Animator anim;
        private Vector3 targetDirection;
        [SerializeField]
        private Vector2 input;
        private Quaternion freeRotation;
        private float velocity;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        void FixedUpdate()
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            // set speed to both vertical and horizontal inputs
            speed = Mathf.Abs(input.x) + input.y;

            speed = Mathf.Clamp(speed, 0f, 1f);
            speed = Mathf.SmoothDamp(anim.GetFloat("Speed"), speed, ref velocity, 0.1f);
            anim.SetFloat("Speed", speed);

            direction = input.y;

            anim.SetFloat("Direction", direction);

            // Update target direction relative to the camera view (or not if the Keep Direction option is checked)
            UpdateTargetDirection();
            if (input != Vector2.zero && targetDirection.magnitude > 0.1f)
            {
                Vector3 lookDirection = targetDirection.normalized;
                freeRotation = Quaternion.LookRotation(lookDirection, transform.up);
                var diferenceRotation = freeRotation.eulerAngles.y - transform.eulerAngles.y;
                var eulerY = transform.eulerAngles.y;

                if (diferenceRotation < 0 || diferenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
                var euler = new Vector3(0, eulerY, 0);

                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(euler), turnSpeed * turnSpeedMultiplier * Time.deltaTime);
            }
        }

        public virtual void UpdateTargetDirection()
        {
            turnSpeedMultiplier = 0.2f;
            var forward = transform.TransformDirection(Vector3.forward);
            forward.y = 0;

            //get the right-facing direction of the referenceTransform
            var right = transform.TransformDirection(Vector3.right);
            targetDirection = input.x * right + Mathf.Abs(input.y) * forward;
        }
    }
}
