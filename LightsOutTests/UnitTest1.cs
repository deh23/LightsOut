using System;
using LightsOut;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace LightsOutTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AllLightsTurnedOff()
        {
            // Generate new game
            var testForm = new Form1();

            // Set all lights to be off
            for (int i = 0; i < testForm.Lights.GetLength(1); i++)
            {
                for (int j = 0; j < testForm.Lights.GetLength(0); j++)
                {
                    testForm.Lights[i, j] = false;
                }
            }

            // Get actual result
            bool actual = testForm.GameStatus();

            // Assert they are the same
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [Repeat(2500)]
        public void GameDoesNotRandomlyWinOnStart()
        {
            // Generate new game
            var testForm = new Form1();

            // Get actual result
            bool actual = testForm.GameStatus();

            // Assert they are the same
            Assert.AreEqual(false, actual);
        }

        [TestMethod]
        public void ButtonChangesLightStatus()
        {
            // Generate new game
            var testForm = new Form1();

            var currentLightStatus = testForm.Lights[1, 1];
            var currentButtonStatus = testForm.ButtonLights[1, 1];

            testForm.HandleLightChanges(currentButtonStatus, 1, 1);

            // Get actual result
            bool actual = testForm.Lights[1, 1];

            // Assert they are the same
            Assert.AreEqual(!currentLightStatus, actual);
        }
    }
}
