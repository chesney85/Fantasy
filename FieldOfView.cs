using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TauntGames
{

    public class FieldOfView : MonoBehaviour
    {
        public float viewRadius;
        [Range(0, 360)] public float viewAngle;
        [Range(0.1f, 2f)] public float obstacleCheckInterval;
        public LayerMask targetMask;
        public LayerMask obstacleMask;
        public List<Collider> visibleTargets = new List<Collider>();
        Interaction interaction;



        private void Start()
        {
            interaction = GetComponent<Interaction>();
            StartCoroutine(nameof(FindTargetWithDelay), obstacleCheckInterval);
        }

        //kind of update loop
        IEnumerator FindTargetWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindVisibleTarget();
            }
        }

        void FindVisibleTarget()
        {
            ClearTargets();
            Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

            //check if the target are inside the view radius of the players fov
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Collider target = targetsInViewRadius[i];
                Vector3 dirToTarget = (target.gameObject.transform.position - transform.position).normalized;

                //asking if the raycast doesnt hit the obstacles then the ray reached the object and its in view
                //so add target to visible target list
                if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.gameObject.transform.position);
                    if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                    {
                        visibleTargets.Add(target);
                    }
                }
            }

            SortVisibleTargets();
        }

        void SortVisibleTargets()
        {
            if (visibleTargets.Count == 0)
            {
                return;
            }
            //call something here to do with all the targets you found
            //find closest target
            Collider closest = visibleTargets[0];
            float closestDistance = 3;
            for (int i = 0; i < visibleTargets.Count; i++)
            {
                float DistanceFromTarget =
                    Vector3.Distance(transform.position, visibleTargets[i].gameObject.transform.position);

                if (visibleTargets.Count > 0)
                {
                    if (DistanceFromTarget < closestDistance)
                    {
                        closestDistance =
                            Vector3.Distance(transform.position, visibleTargets[i].gameObject.transform.position);
                        closest = visibleTargets[i];
                    }
                }
            }

            if (closest && closest.gameObject.transform != interaction.interactable)
            {
                interaction.interactable = closest;
                Interactable inter = closest.gameObject.GetComponent<Interactable>();
                interaction.SetInteractable(inter);
                inter.OnBeginFocus();
            }
        }


        public void ClearTargets()
        {
            if (interaction.interactable)
            {
                if (Vector3.Distance(transform.position, interaction.interactable.gameObject.transform.position) > 3f ||
                    !visibleTargets.Contains(interaction.interactable))
                {
                    interaction.interactable.GetComponent<Interactable>().OnEndFocus();
                    interaction.interactable = null;
                }
            }

            visibleTargets.Clear();
        }

        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }

}
