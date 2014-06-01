using TestGame.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic; 

namespace TestGame.Core.Tests
{
    
    
    /// <summary>
    ///This is a test class for StateTransitionTest and is intended
    ///to contain all StateTransitionTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StateTransitionTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for GetPossibleTransitions
        ///</summary>
        [TestMethod()]
        public void GetPossibleTransitionsFromState0and0returns2Transitions()
        {
            GameState fromState = new GameState(0, 0);
            int bucket1Max = 3;  
            int bucket2Max = 5;  

            IList<StateTransition> actual  = StateTransition.GetPossibleTransitions(fromState, bucket1Max, bucket2Max);

            //fill 1, fill 2
            Assert.AreEqual(actual.Count, 2);
        }

        /// <summary>
        ///A test for GetPossibleTransitions
        ///</summary>
        [TestMethod()]
        public void GetPossibleTransitionsFromState0and3returns4Transitions()
        {
            GameState fromState = new GameState(0,3);
            int bucket1Max = 3;
            int bucket2Max = 5;

            IList<StateTransition> actual = StateTransition.GetPossibleTransitions(fromState, bucket1Max, bucket2Max);

            //dump 2, fill 1, fill 2, tranfer 2 to 1
            Assert.AreEqual(actual.Count, 4);
        }

        /// <summary>
        ///A test for GetPossibleTransitions
        ///</summary>
        [TestMethod()]
        public void GetPossibleTransitionsFromState2and5returns4Transitions()
        {
            GameState fromState = new GameState(2, 5);
            int bucket1Max = 3;
            int bucket2Max = 5;

            IList<StateTransition> actual = StateTransition.GetPossibleTransitions(fromState, bucket1Max, bucket2Max);

            //dump 1, dump 2, fill 1, tranfer 2 to 1
            Assert.AreEqual(actual.Count, 4);
        }
    }
}
