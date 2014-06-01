using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Core
{
    /// <summary>
    /// Represents a state in the bucket game
    /// </summary>
    /// <remarks>
    /// The state is modeled using the current amount of water in buckets. 
    /// </remarks>
    public class GameState
    {
        public int Bucket1 { get; private set; }
        public int Bucket2 { get; private set; } 

        /// <summary>
        /// Initializes a new instance of <see cref="GameState"/>
        /// </summary>
        /// <param name="buck1"></param>
        /// <param name="buck2"></param>
        /// <param name="parent"></param>
        public GameState(int bucket1, int bucket2)
        {
            Bucket1 = bucket1;
            Bucket2 = bucket2;
        }

        /// <summary>
        /// Overriding the tostring function just for debugging purposes.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", Bucket1, Bucket2);
        }

        /// <summary>
        /// Equality comparer
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            GameState state = obj as GameState;

            return state != null && state.Bucket1 == Bucket1 &&
                   state.Bucket2 == Bucket2;
        }

        /// <summary>
        /// Gets the hash code for <see cref="GameState"/> necessary for 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hash = 13;
            hash = hash * 23 + Bucket1.GetHashCode();
            hash = hash * 23 + Bucket2.GetHashCode();

            return hash;
        }
    }
}
