using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;


namespace Assignment
{
    public class PathFollowing : SteeringBehaviour
    {
        public Transform target; // Get to the target!
        public float nodeRadius = .1f; // How big each node is for the agent to seek to
        public float targetRadius = 3f; // Separate from the nodes that the agent follows
        public bool isAtTarget = false; // Has the agent reached the target node?
        public int currentNode = 0; // Keep track of the current node the agent is following

        private NavMeshAgent nav; // Reference to the agent component
        private NavMeshPath path; // Stores the calculated path in this variable

        void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            path = new NavMeshPath();
        }

        // Modified seek function
        Vector3 Seek(Vector3 target)
        {
            Vector3 force = Vector3.zero;
            // Get the distance to target
            float distanceToTarget = Vector3.Distance(target, transform.position);
            // Calculate distance - Ternary Operator 
            // If condition is true, return statement a else statement b
            // return value = <condition> ? <statement a> : <statement b>
            float radius = isAtTarget ? targetRadius : nodeRadius;
            // Is the magnitude greater than distance?
            if (distanceToTarget > radius)
            {
                // Apply weighting to force
                Vector3 direction = (target - transform.position).normalized * weighting;
                // Apply desired force to force (removing current owner's velocity)
                force = direction - owner.velocity;
            }
            // Return force
            return force;
        }

        // Draw out the path calculated by the agent
        void Update()
        {
            // Is the path calculated?    
            if (path != null)
            {
                // Corners refers to the nodes that Unity generated through A*
                Vector3[] corners = path.corners;
                // Has generated corners for the path?
                if (corners.Length > 0)
                {
                    // Store the last corner into target pos
                    Vector3 targetPos = corners[corners.Length - 1];             
                    // Calculate distance from agent to target
                    float distance = Vector3.Distance(transform.position, targetPos);
                    // Is the distance greater than target radius?
                    if (distance >= targetRadius)
                    {
                        for (int i = 0; i < corners.Length - 1; i++)
                        {
                            Vector3 nodeA = corners[i];
                            Vector3 nodeB = corners[i + 1];
                            Gizmos.color = Color.red;
                        }
                    }
                }
            }
        }

        public override Vector3 GetForce()
        {
            Vector3 force = Vector3.zero;

            // Is there not a target?
            if (!target)
                return force;

            // Calculate path using the nav agent
            if (nav.CalculatePath(target.position, path))
            {
                // Is the path finished calculating?
                if (path.status == NavMeshPathStatus.PathComplete)
                {
                    Vector3[] corners = path.corners;
                    // Are there any corners in the path?
                    if (corners.Length > 0)
                    {
                        int lastIndex = corners.Length - 1;
                        // Is currentNode at the end of the list?
                        if (currentNode >= corners.Length)
                        {
                            // Cap currentNode to lastIndex
                            currentNode = lastIndex;
                        }
                        // Get the current corner position
                        Vector3 currentPos = corners[currentNode];
                        // Get the distance to current pos
                        float distance = Vector3.Distance(transform.position, currentPos);
                        // Is the distance within the nodeRadius
                        if (distance <= nodeRadius)
                        {
                            // Move to the next node
                            currentNode++;
                        }
                        // Is the agent at the target?
                        float distanceToTarget = Vector3.Distance(transform.position, target.position);
                        // If distanceToTarget is less than or equal to targetRadius then isAtTarget is true
                        isAtTarget = distanceToTarget <= targetRadius;
                        // Seek towards current node's position
                        force = Seek(currentPos);
                    }
                }
            }

            return force;
        }

        #region NOTES
        int SumOf(params int[] values)
        {
            int result = 0;
            foreach (var n in values)
            {
                result += n;
            }
            return result;
        }
        #endregion
    }
}