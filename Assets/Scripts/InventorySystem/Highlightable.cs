using InventorySystem.Interfaces;
using UnityEngine;

namespace InventorySystem
{
    public class Highlightable : MonoBehaviour, IHighlightable
    {
        [SerializeField] private Outline outline;

        public void EnableHighlight() => outline.enabled = true;
        public void DisableHighlight() => outline.enabled = false;
    }
}