using System;
using DG.Tweening;
using UnityEngine;

namespace EntitySystem.Drone
{
    public class DroneAI
    {
        public DroneController DroneController { get; private set; }

        private Transform _landingTransform;

        public DroneAI(DroneController droneController, Transform droneLandingTransform)
        {
            DroneController = droneController;
            _landingTransform = droneLandingTransform;
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

            landingSequence.Append(DroneController.transform.DOMove(_landingTransform.position, 10f).SetEase(Ease.OutCubic));
            landingSequence.Join(DroneController.transform.DORotateQuaternion(_landingTransform.rotation, 10f).SetEase(Ease.InQuad));
            landingSequence.OnComplete(() =>
            {
                OnLandingSequenceComplete();
            });
        }

        private void OnLandingSequenceComplete()
        {
            
        }
    }
}