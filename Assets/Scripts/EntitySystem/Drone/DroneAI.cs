using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace EntitySystem.Drone
{
    public class DroneAI
    {
        public bool IsDroneLanded;
        
        private DroneController DroneController { get; set; }
        private readonly Transform[] _landingTransforms;
        private readonly Vector3[] _landingCoordinates;

        public event Action OnDroneLanded;

        public DroneAI(DroneController droneController, List<Transform> droneLandingTransforms)
        {
            DroneController = droneController;
            _landingTransforms = droneLandingTransforms.ToArray();
            
            _landingCoordinates = new Vector3[_landingTransforms.Length];
            for (var i = 0; i < _landingTransforms.Length; i++)
            {
                _landingCoordinates[i] = _landingTransforms[i].position;
            }
            
        }
        
        public void GoHome()
        {
            // Let the drone drive home and InitializeLandingSequence() by itself. It should find its way
            // and it should avoid obstacles on its way. It should fly safely
        }

        public void InitializeLandingSequence()
        {
            // Move and rotate with DOTween, and call method when both are finished
            Sequence landingSequence = DOTween.Sequence();

            landingSequence.Append(DroneController.transform.DOPath(_landingCoordinates, 10f, PathType.CatmullRom));
            // landingSequence.Append(DroneController.transform.DOMove(_landingTransforms.position, 10f).SetEase(Ease.OutCubic));
            landingSequence.Join(DroneController.transform.DORotateQuaternion(_landingTransforms[^1].rotation, 10f).SetEase(Ease.InQuad));
            landingSequence.OnComplete(() =>
            {
                OnLandingSequenceComplete();
            });
        }

        private void OnLandingSequenceComplete()
        {
            IsDroneLanded = true;
            OnDroneLanded?.Invoke();
        }
    }
}