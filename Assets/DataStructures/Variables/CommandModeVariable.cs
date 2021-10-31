using UnityEngine;

namespace DataStructures.Variables
{
    [CreateAssetMenu(fileName = "NewCommandModeVariable", menuName = "Utils/Variables/CommandModeVariable")]
    public class CommandModeVariable : AbstractVariable<CommandMode>
    {
        public void ApplyChange(CommandMode value) => runtimeValue = value;

        public void ApplyChange(CommandModeVariable value) => runtimeValue = value.runtimeValue;
    }
    
    public enum CommandMode
    {
        Excavate,
        TransportLine
    }
}
