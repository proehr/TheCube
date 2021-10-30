using System;

namespace Utils.Variables
{
    [Serializable]
    public class FloatReference : AbstractReference<float>
    {
        public FloatReference(float value) : base(value)
        {
        }
    }
}
