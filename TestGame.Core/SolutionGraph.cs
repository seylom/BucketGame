using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestGame.Core
{
    /// <summary>
    /// A class representing a Graph of all state possibilities with their transitions
    /// </summary>
    public class SolutionGraph
    {
        private Dictionary<GameState, HashSet<StateTransition>> _transitionMap = new Dictionary<GameState, HashSet<StateTransition>>();
        private Dictionary<GameState, int> _numberOfStepsTo = new Dictionary<GameState, int>();
        private Dictionary<GameState, StateTransition> _pathTo = new Dictionary<GameState, StateTransition>();

        /// <summary>
        /// Adds a new edge to the graph
        /// </summary>
        /// <param name="transition"></param>
        public void AddTransition(StateTransition transition)
        {
            if (!_transitionMap.ContainsKey(transition.From()))
                _transitionMap.Add(transition.From(), new HashSet<StateTransition>());

            if (!_transitionMap[transition.From()].Contains(transition))
                _transitionMap[transition.From()].Add(transition);
        }

        /// <summary>
        /// Displays path to the provided target bucket state.
        /// </summary>
        /// <param name="target"></param>
        public IEnumerable<StateTransition> GetSolutionTransitions(int targetBucketValue)
        {
            ComputeSteps();

            Stack<StateTransition> transitionPath = new Stack<StateTransition>();

            IList<GameState> gameStates = _numberOfStepsTo.Keys.Where(p => p.Bucket1 == targetBucketValue ||
                                                                 p.Bucket2 == targetBucketValue).ToList();

            if (gameStates == null || !gameStates.Any())
            {
                Console.WriteLine("No path found");
            }
            else
            {
                GameState bestExpectedState = gameStates.First();
                foreach (var state in gameStates)
                {
                    if (_numberOfStepsTo[state] < _numberOfStepsTo[bestExpectedState])
                        bestExpectedState = state;
                }

                GameState parentState = bestExpectedState;
                Stack<string> steps = new Stack<string>();

                while (_pathTo[parentState] != null)
                {
                    transitionPath.Push(_pathTo[parentState]);
                    parentState = _pathTo[parentState].From();
                }
            }

            return transitionPath.ToList();
        }

        /// <summary>
        /// Gets the list of edges in the solution graph
        /// </summary>
        /// <returns></returns>
        private IList<StateTransition> GetTransitions()
        {
            List<StateTransition> edges = new List<StateTransition>();
            foreach (GameState node in _transitionMap.Keys)
            {
                foreach (StateTransition edge in _transitionMap[node])
                {
                    edges.Add(edge);
                }
            }

            return edges;
        }

        /// <summary>
        /// Computes and stores all path to a particular game state as well as the cost to reach such state
        /// </summary>
        /// <param name="target"></param>
        /// <remarks>
        /// This method compute shortest path for all our gamestates. The "relaxation" process which consist of updating
        /// the cost of reaching a game state does not use Djikstra algorithm but could be modified if necessary.
        /// </remarks>
        private void ComputeSteps()
        {
            if (_transitionMap.Count == 0)
                return;

            foreach (GameState gameState in _transitionMap.Keys)
                _numberOfStepsTo.Add(gameState, 99999); //setting the number of steps to an arbitrarily large value.

            int transitionCost = 1;

            //Set the starting state when both buckets are empty
            GameState gameStartingState = _numberOfStepsTo.Keys.First(p => p.Bucket1 == 0 && p.Bucket2 == 0);
            _numberOfStepsTo[gameStartingState] = 0;
            _pathTo[gameStartingState] = null;

            HashSet<GameState> visitedStates = new HashSet<GameState>();
            Queue<GameState> states = new Queue<GameState>();
            states.Enqueue(gameStartingState);

            while (states.Count > 0)
            {
                GameState state = states.Dequeue();

                foreach (StateTransition transition in _transitionMap[state])
                {
                    GameState toState = transition.To();

                    if (_numberOfStepsTo[toState] > _numberOfStepsTo[state] + transitionCost)
                    {
                        _numberOfStepsTo[toState] = _numberOfStepsTo[state] + transitionCost;
                        _pathTo[toState] = transition;
                    }

                    if (!visitedStates.Contains(toState))
                    {
                        visitedStates.Add(toState);
                        states.Enqueue(toState);
                    }
                } 
            }
        }
    }
}
