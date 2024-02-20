using UnityEngine;

namespace BlockSort.GameUI
{
    public class UIParticle_Confetti : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem particle;

        // Start is called before the first frame update
        private void Awake()
        {
            particle.Stop();
        }

        public void PlayAnimation()
        {
            particle.Play();
        }

        public void StopAnimation()
        {
            particle.Stop();
        }
    }
}
