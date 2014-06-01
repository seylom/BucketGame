using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Core
{
    /// <summary>
    /// This class represents the change of state for buckets after applying an Operation such as Dump, Fill or Transfer
    /// </summary>
    /// <remarks>
    /// A transition has the same meaning an edge holds in a directed graph, a directed link between two states (vertices).
    /// In our case, vertices are starting and ending point for a combination of buckets and <see cref="StateTransition"/> 
    /// represent the transition
    /// </remarks>
    public class StateTransition
    {
        public GameState StartState {get; private set;}
        public GameState EndState {get; private set;}
        public string Details { get { return Operation.ToDetails(); } }
        public Operation Operation {get; private set;}

        /// <summary>
        /// Gets the starting state for buckets
        /// </summary>
        /// <returns></returns>
        public GameState From()
        {
            return StartState;
        }

        /// <summary>
        /// Gets the end state for buckets after applying the operation
        /// </summary>
        /// <returns></returns>
        public GameState To()
        {
            return EndState;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="StateTransition"/>
        /// </summary>
        /// <param name="startState"></param>
        /// <param name="endState"></param>
        /// <param name="operation"></param>
        public StateTransition(GameState startState, GameState endState, Operation operation)
        {
            StartState = startState;
            EndState = endState;
            Operation = operation;
        }

        /// <summary>
        /// A helper function for state creation
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static StateTransition CreateTransition(GameState start, GameState end, Operation op)
        {
            return new StateTransition(start, new GameState(0, end.Bucket2), op);
        }

        /// <summary>
        /// Retrieves all possible transitions using one of the end node provided and maximum capacity of
        /// containers.
        /// </summary>
        /// <param name="fromState"></param>
        /// <param name="bucket1Max"></param>
        /// <param name="bucket2Max"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1 - Fill bucket1 if not already full
        /// 2 - Fill bucket2 if not already full
        /// 3 - transfer bucket1 to bucket2, if possible
        /// 4 - transfer bucket2 to bucket1, if possible
        /// 5 - dump bucket1 if not already empty
        /// 6 - dump bucket2 if not already empty
        /// </remarks>
        public static IList<StateTransition> GetPossibleTransitions(GameState fromState, int bucket1Max, int bucket2Max)
        {
            IList<StateTransition> transitions = new List<StateTransition>();

            if (fromState.Bucket1 < bucket1Max)
                transitions.Add(new StateTransition(fromState, new GameState(bucket1Max, fromState.Bucket2), Operation.FillBucket1));

            if (fromState.Bucket2 < bucket2Max)
                transitions.Add(new StateTransition(fromState, new GameState(fromState.Bucket1, bucket2Max), Operation.FillBucket2));

            if (fromState.Bucket1 > 0 && fromState.Bucket2 < bucket2Max)
            {
                int transferVal = Math.Min(bucket2Max - fromState.Bucket2, fromState.Bucket1);
                transitions.Add(new StateTransition(fromState, new GameState(fromState.Bucket1 - transferVal, fromState.Bucket2 + transferVal), 
                    Operation.TransferBucket1ToBucket2));
            }

            if (fromState.Bucket2 > 0 && fromState.Bucket1 < bucket1Max)
            {
                int transferVal = Math.Min(bucket1Max - fromState.Bucket1, fromState.Bucket2);
                transitions.Add(new StateTransition(fromState, new GameState(fromState.Bucket1 + transferVal, fromState.Bucket2 - transferVal), 
                    Operation.TransferBucket2ToBucket1));
            }

            if (fromState.Bucket1 > 0)
                transitions.Add(new StateTransition(fromState, new GameState(0, fromState.Bucket2), Operation.DumpBucket1));

            if (fromState.Bucket2 > 0)
                transitions.Add(new StateTransition(fromState, new GameState(fromState.Bucket1, 0), Operation.DumpBucket2));

            return transitions;
        }

        /// <summary>
        /// Display string for debugging purposes
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0} - {1}", EndState.Bucket1, EndState.Bucket2);
        }

        /// <summary>
        /// Equality comparison
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            StateTransition transition = obj as StateTransition;

            return transition != null && transition.StartState.Equals(StartState) &&
                   transition.EndState.Equals(EndState) &&
                   transition.Operation ==  Operation;
        }

        /// <summary>
        /// Gets the hashcode used for hashing purposes in hashtables and dictionaries
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override int GetHashCode()
        {
            int hash = 13;
            hash = hash * 23 + StartState.GetHashCode();
            hash = hash * 23 + EndState.GetHashCode();
            hash = hash * 23 + Operation.GetHashCode();

            return hash;
        } 
    }
}
