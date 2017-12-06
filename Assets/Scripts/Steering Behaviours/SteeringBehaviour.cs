using UnityEngine;
using System.Collections;

namespace AI_OOP_Assignment
{
    [RequireComponent(typeof(AIAgent))]
    public class SteeringBehaviour : MonoBehaviour
    {
        public float weighting = 7.5f;
        [HideInInspector]
        public AIAgent owner;

        protected virtual void Awake()
        {
            owner = GetComponent<AIAgent>();
        }

        public virtual Vector3 GetForce()
        {
            return Vector3.zero;
        }
    }
}