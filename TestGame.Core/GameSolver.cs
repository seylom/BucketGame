using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Core
{
    /// <summary>
    /// The solver class for the Bucket game
    /// </summary>
    public static class GameSolver
    {
        /// <summary>
        /// Solves the bucket game for the provided parameters
        /// </summary>
        /// <param name="bucket1capacity">Capacity of the first bucket</param>
        /// <param name="bucket2capacity">Capacity of the second bucket</param>
        /// <param name="target">The target value desired</param>
        /// <returns></returns>
        /// <remarks>
        /// This solution uses a graph generation and shortest path algorithm
        /// </remarks>
        public static IEnumerable<StateTransition> Solve(int bucket1capacity, int bucket2capacity, int target){

            GameState root = new GameState(0, 0);

            HashSet<StateTransition> exploredTransitions = new HashSet<StateTransition>();

            HashSet<GameState> exploredStates = new HashSet<GameState>();

            Stack<GameState> stateStack = new Stack<GameState>();

            SolutionGraph graph = new SolutionGraph();

            stateStack.Push(root);

            while (stateStack.Count > 0)
            {
                GameState state = stateStack.Pop();

                exploredStates.Add(state);

                IList<StateTransition> possibleTransitions = StateTransition.GetPossibleTransitions(state, bucket1capacity, bucket2capacity);

                foreach (StateTransition transition in possibleTransitions)
                {
                    if (!exploredStates.Contains(transition.To()))
                        stateStack.Push(transition.To());

                    if (!exploredTransitions.Contains(transition))
                        graph.AddTransition(transition); 
                }
            }

            return graph.GetSolutionTransitions(target); 
        }
    }
}
