using DataStructures.Variables;
using TMPro;
using UnityEngine;

namespace Features.Gui.Hud.Scripts
{
    public class ResourceElement : MonoBehaviour
    {
        [SerializeField] private IntVariable resource;
        [SerializeField] private TMP_Text amountLabel;

        private void Start()
        {
            UpdateLabel();
        }

        public void OnResourceAmountChanged()
        {
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            amountLabel.text = resource.Get().ToString();
        }
    }
}
