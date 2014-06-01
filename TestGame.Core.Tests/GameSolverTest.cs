using TestGame.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestGame.Core.Tests
{
    /// <summary>
    ///This is a test class for GameSolverTest and is intended
    ///to contain all GameSolverTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GameSolverTest
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
        ///A test for Solve
        ///</summary>
        [TestMethod()]
        public void SolveCapaCity3and5WithTarget4Returns6Steps()
        {
            int bucket1capacity = 3; 
            int bucket2capacity = 5; 
            int target = 4;  

            IEnumerable<StateTransition> result =  GameSolver.Solve(bucket1capacity, bucket2capacity, target);

            Assert.AreEqual(result.ToList().Count, 6);
        }

        /// <summary>
        ///A test for Solve
        ///</summary>
        [TestMethod()]
        public void SolveCapaCity2and18WithTarget16Returns2Steps()
        {
            int bucket1capacity = 2;
            int bucket2capacity = 18;
            int target = 16;

            IEnumerable<StateTransition> result = GameSolver.Solve(bucket1capacity, bucket2capacity, target);

            Assert.AreEqual(result.ToList().Count, 2);
        }

        /// <summary>
        ///A test for Solve
        ///</summary>
        [TestMethod()]
        public void SolveCapaCity2and18WithTarget4Returns4Steps()
        {
            int bucket1capacity = 2;
            int bucket2capacity = 18;
            int target = 4;

            IEnumerable<StateTransition> result = GameSolver.Solve(bucket1capacity, bucket2capacity, target);

            Assert.AreEqual(result.ToList().Count, 4);
        }

        /// <summary>
        ///A test for Solve
        ///</summary>
        [TestMethod()]
        public void SolveCapaCity2and18WithTarget5Returns0Steps()
        {
            int bucket1capacity = 2;
            int bucket2capacity = 18;
            int target = 5;

            IEnumerable<StateTransition> result = GameSolver.Solve(bucket1capacity, bucket2capacity, target);

            Assert.AreEqual(result.ToList().Count, 0);
        }
    }
}
