using System;

namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// Apply forward force to instantiated prefab
    /// </summary>
    public class LaunchProjectile : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The projectile that's created")]
        GameObject m_ProjectilePrefab = null;

        [SerializeField]
        [Tooltip("The point that the project is created")]
        Transform m_StartPoint = null;

        [SerializeField]
        [Tooltip("The speed at which the projectile is launched")]
        float m_LaunchSpeed = 1.0f;

        public float shotCooldown = 0.7f;
        private float lastFired;
        private AudioClipController _controller;

        private void Awake()
        {
            _controller = GetComponent<AudioClipController>();
        }
        
        public void OnSelected()
        {
            GameManager.instance.OnPickUpLaunchers();
        }

        public void Fire()
        {
            if (Time.time < lastFired + shotCooldown)
                return;
            lastFired = Time.time;
            _controller.PlayClip();
            GameObject newObject = Instantiate(m_ProjectilePrefab, m_StartPoint.position, m_StartPoint.rotation, null);

            if (newObject.TryGetComponent(out Rigidbody rigidBody))
                ApplyForce(rigidBody);
        }

        void ApplyForce(Rigidbody rigidBody)
        {
            Vector3 force = m_StartPoint.forward * m_LaunchSpeed;
            rigidBody.AddForce(force);
        }
    }
}
