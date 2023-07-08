using System.Collections;
using UnityEngine;

namespace Interactables
{
    public class Laser : IInteractable
    {
        [SerializeField]
        private float distanceRay = 100;
        [SerializeField]
        private Transform laserFirePoint;
        public LineRenderer lineRenderer;

        public ParticleSystem laserEndParticles;
        
        private bool endParticlesPlaying;

        void Start()
        {
            lineRenderer.material.color = Color.red;
        }

        public override void Update()
        {
            base.Update();
            
            if (!isEnabled) return;
            ShootLaser();
        }

        public override void Enable()
        {
            isEnabled = true;
            lineRenderer.enabled = true;
        }

        public override void Disable()
        {
            isEnabled = false;
            lineRenderer.enabled = false;
            DisableParticle();
        }

        public override IEnumerator EnableWithTimer(float time)
        {
            throw new System.NotImplementedException();
        }

        private bool soundWasPlayed;

        private void ShootLaser()
        {
            if (Physics2D.Raycast(transform.position, transform.right))
            {
                RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, transform.right);
                if (hit)
                {
                    if (hit.collider is CapsuleCollider2D)
                    {
                        //hit something
                        Debug.Log(hit.collider);
                    }

                    if (!endParticlesPlaying)
                    {
                        endParticlesPlaying = true;
                        laserEndParticles.Play(true);
                    }
                    laserEndParticles.gameObject.transform.position = hit.point;
                    float distance = ((Vector2)hit.point - (Vector2)transform.position).magnitude;
                    lineRenderer.SetPosition(1, new Vector3(distance, 0, 0));
                }
                Draw2DRay(laserFirePoint.position, hit.point);
            }
            else
            {
                Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * distanceRay);
            }
        }

        private void Draw2DRay(Vector2 startPos, Vector2 endPos)
        {
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.SetPosition(1, endPos);
        }

        public void DisableParticle()
        {
            laserEndParticles.Pause();
        }
    }
}
