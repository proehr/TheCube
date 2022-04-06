using DataStructures.Variables;
using UnityEngine;
namespace Features.Planet_Integrity
{
    public class IntegrityBarBehaviour : MonoBehaviour
    {
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
            UpdateIntegrityPanel();
        }

        public void UpdateIntegrityPanel()
        {
            integrityPanel.sizeDelta = new Vector2(
                startingIntegrityPanelWidth - currentIntegrity.Get() / integrityThreshold.Get() * startingIntegrityPanelWidth, integrityPanel.sizeDelta.y);
        }
    }
}
