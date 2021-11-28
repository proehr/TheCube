using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Features.Commands.Scripts.Excavation
{
    public class TestExcavationTicks
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestExcavationTicksSimplePasses()
        {
            // Use the Assert class to test conditions
            ExcavationCommand.Test();
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestExcavationTicksWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
