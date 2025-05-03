using Player;

namespace Camera
{
    using UnityEngine;

    public class CameraBobbing : MonoBehaviour
    {
        [SerializeField] private PlayerMovementWithRigidbody playerMovement;
        [SerializeField] private float bobFrequency = 6f;
        [SerializeField] private float bobAmplitude = 0.05f;

        private Vector3 _initialLocalPosition;
        private float _bobTimer;

        private void Start()
        {
            _initialLocalPosition = transform.localPosition;
        }

        private void LateUpdate()
        {
            if (playerMovement == null) return;

            if (playerMovement.CurrentMoveMagnitude > 0.1f)
            {
                _bobTimer += Time.deltaTime * bobFrequency;
                float bobOffset = Mathf.Sin(_bobTimer) * bobAmplitude;
                transform.localPosition = _initialLocalPosition + new Vector3(0, bobOffset, 0);
            }
            else
            {
                _bobTimer = 0f;
                transform.localPosition = Vector3.Lerp(transform.localPosition, _initialLocalPosition, Time.deltaTime * 5f);
            }
        }
    }

}