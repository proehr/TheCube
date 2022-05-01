using DataStructures.Variables;
using Features.GameController.Scripts;
using Features.GameController.Scripts.StateMachine;
using UnityEngine;

namespace Features.Planet_Integrity
{
    public class IntegrityBarBehaviour : MonoBehaviour
    {
        [SerializeField] private RectTransform integrityPanel;
        [SerializeField] private FloatVariable currentIntegrity;
        [SerializeField] private FloatVariable baseIntegrity;

        private float startingIntegrityPanelWidth;

        private void Awake()
        {
            startingIntegrityPanelWidth = integrityPanel.rect.width;
        }
        
        private void UpdateIntegrityPanel()
        {
            float width = currentIntegrity.Get() / baseIntegrity.Get() * startingIntegrityPanelWidth;
            
            integrityPanel.sizeDelta = new Vector2(width, integrityPanel.sizeDelta.y);
        }
    }
}
