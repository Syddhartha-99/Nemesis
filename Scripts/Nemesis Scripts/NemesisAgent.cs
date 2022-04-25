using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;


namespace SG
{

    public class NemesisAgent : Agent
    {
        [SerializeField]
        float positiveReward = 1;
        [SerializeField]
        float negativeReward = -1;

        [SerializeField]
        private Rigidbody rb;
        [SerializeField]
        private Transform targetTransform;


        private NemesisController nemesisController;

        public override void Initialize()
        {
            nemesisController = GetComponent<NemesisController>();
        }

        public override void OnEpisodeBegin()
        {
            rb.velocity = Vector3.zero;
            transform.rotation = Quaternion.Euler(Vector3.up * Random.Range(0f, 360f));

            transform.localPosition = new Vector3(Random.Range(9f, -9f), 0, Random.Range(-9f, 9f));
            targetTransform.localPosition = new Vector3(Random.Range(9f, -9f), 0, Random.Range(-9f, 9f));
        }

        public override void OnActionReceived(ActionBuffers actions)
        {
            float moveInput = actions.DiscreteActions[0] <= 1 ? actions.DiscreteActions[0] : 0;
            float rotateInput = actions.DiscreteActions[1] <= 1 ? actions.DiscreteActions[1] : -1;


            nemesisController.moveInput = moveInput;
            nemesisController.rotateInput = rotateInput;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Goal>(out Goal goal))
            {
                SetReward(positiveReward);
                EndEpisode();
            }
            if (other.TryGetComponent<Wall>(out Wall Wall))
            {
                SetReward(negativeReward);
                EndEpisode();
            }
        }
    }
}