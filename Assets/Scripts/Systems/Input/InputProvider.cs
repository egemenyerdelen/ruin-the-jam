using UnityEngine;

namespace Systems.Input
{
    public abstract class InputProvider : MonoBehaviour
    {
        public abstract void EnableInputs();
        public abstract void DisableInputs();
    }
}