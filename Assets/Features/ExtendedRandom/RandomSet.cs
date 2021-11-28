using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Features.ExtendedRandom
{
    public abstract class RandomSet<T>
    {
        public static T PickOne(IReadOnlyList<T> choices)
        {
            return choices[Random.Range(0, choices.Count - 1)];
        }

        IList<T> availableChoices;

        protected abstract IList<T> NewSet();

        public T PickOne()
        {
            T element;
            if (this.GetAvailableChoices().Count == 1)
            {
                element = this.GetAvailableChoices()[0];

                this.availableChoices = this.NewSet();

                return element;
            }
            else
            {
                var index = Random.Range(0, this.GetAvailableChoices().Count - 1);
                element = this.GetAvailableChoices()[index];
                this.GetAvailableChoices().RemoveAt(index);
                return element;
            }
        }

        protected IList<T> GetAvailableChoices()
        {
            if (this.availableChoices == null)
            {
                this.availableChoices = this.NewSet();
            }

            return this.availableChoices;
        }

        /**
         * If this is <code>true</code>, you may not call {@link #pickOne()} at all
         *
         */
        public bool IsEmpty()
        {
            return this.GetAvailableChoices().Count == 0;
        }
    }

    public class RandomEnumSet<T> : RandomSet<T> where T : Enum
    {
        private readonly Type enumType;

        /**
         * Usage: typeof(EnumClass)
         */
        public RandomEnumSet(Type enumType)
        {
            this.enumType = enumType;
        }

        protected override IList<T> NewSet()
        {
            return new List<T>((T[]) Enum.GetValues(enumType));
        }
    }

    public class RandomIntegerSet : RandomSet<int>
    {
        private readonly int numberOfElements;

        public RandomIntegerSet(int numberOfElements)
        {
            this.numberOfElements = numberOfElements;
        }

        protected override IList<int> NewSet()
        {
            var set = new List<int>();
            for (var i = 0; i < this.numberOfElements; i++)
            {
                set.Add(i);
            }

            return set;
        }
    }


    public class RandomListSet<T> : RandomSet<T>
    {
        private readonly IList<T> elements;

        public RandomListSet(IList<T> elements)
        {
            this.elements = elements;
        }


        protected override IList<T> NewSet()
        {
            /*
             * Copy the array.
             */
            return new List<T>(this.elements);
        }
    }
}
