using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _RuinTheJam.UI
{
    [Serializable]
    public class ChangeColorWithTime : MonoBehaviour
    {
        [SerializeField] public Component visualComponent;
        public ChangeColorWithTimeData activeData = new();
        [SerializeReference] public IColorChangeEffect colorChangeEffect = new ChangeAlpha();
        
        public IColorTarget ColorTarget
        {
            get
            {
                colorTarget ??= InitializeColorTarget();
                return colorTarget;
            }
        }
        
        [NonSerialized] public IColorTarget colorTarget;

        public bool IsEffectInProgress
        {
            get => _isEffectInProgress;
            set
            {
                if (_isEffectInProgress == value) return;
                
                _isEffectInProgress = value;
                if (value)
                    OnEffectStarted?.Invoke();
                
                else
                    OnEffectEnded?.Invoke();
            }
        }
        private bool _isEffectInProgress;

        public event Action OnEffectStarted;
        public event Action OnEffectEnded;
        
        private float _time;
        private int _completedLoops;
        private bool _isReversing;

        private void Start()
        {
            ActivateEffect();
        }

        private void OnDisable()
        {
            if (activeData.setStartingColor && ColorTarget != null)
                ColorTarget.Color = activeData.startingColor;
        }
        
        private IColorTarget InitializeColorTarget()
        {
            switch (visualComponent)
            {
                case SpriteRenderer sr:
                    colorTarget = new SpriteRendererColorTarget(sr);
                    break;

                case Image img:
                    colorTarget = new ImageColorTarget(img);
                    break;

                default:
                    Debug.LogError($"Unsupported component type for color change. Type: {visualComponent.GetType()}");
                    break;
            }

            return colorTarget;
        }

        public void ActivateEffect()
        {
            if (activeData.setStartingColor)
                ColorTarget.Color = activeData.startingColor;
            
            if (activeData.startDelay != 0f)
                StartCoroutine(DelayActivation());
            else
                StartEffect();
        }

        private IEnumerator DelayActivation()
        {
            yield return new WaitForSeconds(activeData.startDelay);
            StartEffect();
        }

        private void StartEffect()
        {
            IsEffectInProgress = true;
            _time = Mathf.Lerp(0f, activeData.effectTime, activeData.startCurveAt);
            _completedLoops = 0;
            _isReversing = false;
        }

        private void Update()
        {
            if (!IsEffectInProgress) return;

            var delta = Time.deltaTime;
            _time += _isReversing ? -delta : delta;
            _time = Mathf.Clamp(_time, 0f, activeData.effectTime);

            var percentage = _time / activeData.effectTime;
            var lerpPoint = activeData.effectCurve.Evaluate(percentage);
            colorChangeEffect.ApplyColorChangeEffect(ColorTarget, lerpPoint);

            var isAtEnd = Mathf.Approximately(_time, activeData.effectTime);
            var isAtStart = Mathf.Approximately(_time, 0f);

            switch (activeData.playMode)
            {
                case PlayMode.PlayOnce when isAtEnd:
                case PlayMode.ReverseOnce when isAtStart && _isReversing:
                    IsEffectInProgress = false;
                    break;

                case PlayMode.Loop when isAtEnd:
                    _time = 0f;
                    break;

                case PlayMode.LoopNTimes when isAtEnd:
                    _completedLoops++;
                    if (_completedLoops < activeData.loopCount)
                        _time = 0f;
                    else
                        IsEffectInProgress = false;
                    break;

                case PlayMode.PingPong:
                    if (isAtEnd || isAtStart)
                        _isReversing = !_isReversing;
                    break;

                case PlayMode.ReverseOnce when isAtEnd:
                    _isReversing = true;
                    break;
            }
        }
    }

    public interface IColorChangeEffect
    {
        void ApplyColorChangeEffect(IColorTarget target, float lerpPoint);
    }

    [Serializable]
    public class ChangeColorWithTimeData
    {
        public bool setStartingColor;
        [ShowIf(nameof(setStartingColor))] public Color startingColor;

        public float effectTime = 0.2f;
        public float startDelay;
        [Range(0f, 1f)] public float startCurveAt;
        public AnimationCurve effectCurve;
        public PlayMode playMode = PlayMode.PlayOnce;
        [ShowIf(nameof(ShowLoopCount))] public int loopCount = 1;

        private bool ShowLoopCount()
        {
            return playMode == PlayMode.LoopNTimes;
        }
    }

    [Serializable]
    public class ChangeColor : IColorChangeEffect
    {
        public Color from = Color.white;
        public Color to = Color.white;

        public void ApplyColorChangeEffect(IColorTarget target, float lerpPoint)
        {
            target.Color = Color.Lerp(from, to, lerpPoint);
        }
    }

    [Serializable]
    public class ChangeAlpha : IColorChangeEffect
    {
        public float from;
        public float to = 1f;

        public void ApplyColorChangeEffect(IColorTarget target, float lerpPoint)
        {
            var color = target.Color;
            color.a = Mathf.Lerp(from, to, lerpPoint);
            target.Color = color;
        }
    }

    public enum PlayMode
    {
        PlayOnce,
        Loop,
        PingPong,
        ReverseOnce,
        LoopNTimes
    }
    
    public interface IColorTarget
    {
        Color Color { get; set; }
    }
    
    public class SpriteRendererColorTarget : IColorTarget
    {
        private readonly SpriteRenderer _renderer;

        public SpriteRendererColorTarget(SpriteRenderer renderer) => _renderer = renderer;

        public Color Color
        {
            get => _renderer.color;
            set => _renderer.color = value;
        }
    }

    public class ImageColorTarget : IColorTarget
    {
        private readonly Image _image;

        public ImageColorTarget(Image image) => _image = image;

        public Color Color
        {
            get => _image.color;
            set => _image.color = value;
        }
    }



}