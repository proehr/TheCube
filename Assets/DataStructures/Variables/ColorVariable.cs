using UnityEngine;

namespace Utils.Variables
{
    [CreateAssetMenu(fileName = "new ColorVariable", menuName = "Utils/Variables/ColorVariable")]
    public class ColorVariable : ScriptableObject
    {
        public Color colorValue;
        [SerializeField] private Color startColor;

        public Color GetVariableValue() => colorValue;

        public void ResetVariable() => colorValue = startColor;

        public void SetValue(Color value) => colorValue = value;
        
        public void SetValue(ColorVariable value) => colorValue = value.colorValue;
    }
}
