using UnityEngine;

namespace _RuinTheJam.Core.Input
{
    public abstract class InputProvider : MonoBehaviour
    {
        public abstract void EnableInputs();
        public abstract void DisableInputs();
    }
}