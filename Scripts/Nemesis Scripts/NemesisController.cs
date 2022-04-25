using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class NemesisController : MonoBehaviour
    {
        public float moveSpeed = 1f;
        public float rotateSpeed = 300f;

        public float moveInput;
        public float rotateInput;
        public bool checkGrounded;
        public bool checkInteracting;

        private Rigidbody rb;
        private CapsuleCollider col;
        private Animator anim;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            col = GetComponent<CapsuleCollider>();
            anim = GetComponentInChildren<Animator>();
        }

        private void Update()
        {
            checkInteracting = anim.GetBool("checkInteracting");
        }

        private void FixedUpdate()
        {
            isGrounded();
            HandleMovement();
            HandleRotation();
        }

        private void isGrounded()
        {
            float slopeLimit = 45f;

            checkGrounded = false;
            float capsuleHeight = Mathf.Max(col.radius * 2f, col.height);
            Vector3 capsuleBottom = transform.TransformPoint(col.center - Vector3.up * capsuleHeight / 2f);
            float radius = transform.TransformVector(col.radius, 0f, 0f).magnitude;

            Ray ray = new Ray(capsuleBottom + transform.up * .01f, -transform.up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, radius * 5f))
            {
                float normalAngle = Vector3.Angle(hit.normal, transform.up);
                if (normalAngle < slopeLimit)
                {
                    float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .02f;
                    if (hit.distance < maxDist)
                        checkGrounded = true;
                }
            }
        }

        private void HandleMovement()
        {
            if (checkInteracting == true)
                return;

            if (checkGrounded)
            {
                GetComponent<Rigidbody>().velocity = Vector3.zero;
                rb.velocity += transform.forward * Mathf.Clamp(moveInput, -1f, 1f) * moveSpeed;

            }
        }


        private void HandleRotation()
        {
            if (rotateInput != 0f)
            {
                if (checkInteracting == true)
                    return;
                float angle = Mathf.Clamp(rotateInput, -1f, 1f) * rotateSpeed;
                transform.Rotate(Vector3.up, Time.fixedDeltaTime * angle);
            }
        }

    }
}