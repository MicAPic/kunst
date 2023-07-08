using System.Collections;
using UnityEngine;

namespace Interactables
{
    public class Laser : IInteractable
    {
        [Header("Appearance")] 
        [SerializeField]
        private Color laserColor = Color.red;
        [Space]
        [SerializeField]
        private float distanceRay = 100;
        [SerializeField]
        private Transform laserFirePoint;
        public LineRenderer lineRenderer;

        public ParticleSystem laserEndParticles;
        
        public bool endParticlesPlaying;

        void Start()
        {
            // lineRenderer.material.color = laserColor;
        }

        public override void Update()
        {
            base.Update();
            
            if (!isEnabled) return;
            ShootLaser();
            //TODO: move the following back to Start
            lineRenderer.material.color = laserColor;
        }

        public override void Enable()
        {
            isEnabled = false;
            lineRenderer.enabled = false;
            DisableParticle();
        }

        public override void Disable()
        {
            isEnabled = true;
            lineRenderer.enabled = true;
        }

        public override IEnumerator EnableWithTimer(float time)
        {
            isEnabled = false;
            lineRenderer.enabled = false;
            DisableParticle();


            timer.gameObject.SetActive(true);
            StartCoroutine(timer.Countdown(time, this));

            yield return null;
        }

        private bool soundWasPlayed;

        private void ShootLaser()
        {
            RaycastHit2D[] hitAll = Physics2D.RaycastAll(laserFirePoint.position, transform.right);
            if (hitAll.Length > 1)
            {
                int index = 1;
                for (int i = 1; i < hitAll.Length; ++i)
                {
                    if (!hitAll[i].collider.CompareTag("Button") && !hitAll[i].collider.CompareTag("Floor"))
                    {
                        index = i;
                        break;
                    }
                }


                //Debug.Log(hitAll[index].collider);


                //if (hitAll[index].collider.CompareTag("Player"))
                //{
                //    // TODO: game over
                //}

                if (!endParticlesPlaying)
                {
                    endParticlesPlaying = true;
                    laserEndParticles.gameObject.SetActive(true);
                    laserEndParticles.Play(true);
                }
                laserEndParticles.gameObject.transform.position = hitAll[index].point;
                float distance = ((Vector2)hitAll[index].point - (Vector2)transform.position).magnitude;
                lineRenderer.SetPosition(1, new Vector3(distance, 0, 0));
                Draw2DRay(laserFirePoint.position, hitAll[index].point);
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

        private void DisableParticle()
        {
            endParticlesPlaying = !endParticlesPlaying;
            laserEndParticles.gameObject.SetActive(endParticlesPlaying);
            if (endParticlesPlaying)
                laserEndParticles.Play();
        }


    }
}
