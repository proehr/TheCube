using System;

namespace Utils.Variables
{
    [Serializable]
    public class IntReference
    {
        public bool useConstant = true;
        public int constantValue;
        public IntVariable variable;

        public IntReference()
        {
        }

        public IntReference(int value)
        {
            useConstant = true;
            constantValue = value;
        }

        public int value => useConstant ? constantValue : variable.intValue;

        public static implicit operator int (IntReference reference)
        {
            return reference.value;
        }
    }
}
