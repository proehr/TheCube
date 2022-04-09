using DataStructures.Variables;
using Features.GameController.Scripts;
using Features.GameController.Scripts.StateMachine;
using UnityEngine;
namespace Features.Planet_Integrity
{
    public class IntegrityBarBehaviour : MonoBehaviour
    {
        [SerializeField] private GameState_SO gameState;
        [SerializeField] private RectTransform integrityPanel;
        [SerializeField] private FloatVariable currentIntegrity;
        [SerializeField] private FloatVariable integrityThreshold;

        private float startingIntegrityPanelWidth;

        private void Awake()
        {
            startingIntegrityPanelWidth = integrityPanel.rect.width;
        }
        
        private void Update()
        {
            if (gameState.Get() is GameState.GAMEPLAY)
            {
                UpdateIntegrityPanel();
            }
        }

        private void UpdateIntegrityPanel()
        {
            integrityPanel.sizeDelta = new Vector2(
                startingIntegrityPanelWidth - currentIntegrity.Get() / integrityThreshold.Get() * startingIntegrityPanelWidth, integrityPanel.sizeDelta.y);
        }
    }
}
