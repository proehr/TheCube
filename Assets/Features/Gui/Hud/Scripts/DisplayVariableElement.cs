using DataStructures.Variables;
using TMPro;
using UnityEngine;

namespace Features.Gui.Hud.Scripts
{
    public class DisplayVariableElement : MonoBehaviour
    {
        [SerializeField] private IntVariable resource;
        [SerializeField] private TMP_Text amountLabel;

        private void Start()
        {
            UpdateLabel();
        }

        public void OnValueChanged()
        {
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            amountLabel.text = resource.Get().ToString();
        }
    }
}
