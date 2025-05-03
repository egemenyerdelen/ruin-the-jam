using System;
using System.Collections;
using Core;

namespace Helpers
{
    using UnityEngine;
    using TMPro;

    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextBreatheEffect : MonoBehaviour
    {
        [Header("Breathing Alpha Range")]
        [Range(0f, 1f)] public float minBreatheAlpha = 0.2f;
        [Range(0f, 1f)] public float maxBreatheAlpha = 0.5f;
        public float breatheSpeed = 1f;

        private TextMeshProUGUI _text;
        private float _timer;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        
        private void Update()
        {
            _timer += Time.unscaledDeltaTime * breatheSpeed;

            var alpha = Mathf.Lerp(minBreatheAlpha, maxBreatheAlpha, (Mathf.Sin(_timer) + 1f) / 2f);
            var color = _text.color;
            color.a = alpha;
            _text.color = color;
        }

        public void SetBreathAlphaRange(float min, float max)
        {
            minBreatheAlpha = min;
            maxBreatheAlpha = max;
        }
    }

}