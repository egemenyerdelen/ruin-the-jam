using _RuinTheJam.Art.External_Assets.QuickOutline.Scripts;
using _RuinTheJam.Systems.InteractionSystem.Interfaces;
using UnityEngine;

namespace _RuinTheJam.Systems.InventorySystem
{
    public class Highlightable : MonoBehaviour, IHighlightable
    {
        [SerializeField] private Outline outline;

        public void EnableHighlight() => outline.enabled = true;
        public void DisableHighlight() => outline.enabled = false;
    }
}