using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Game_Launcher;
using System.Collections.Generic;

namespace UnitTest_Game_Launcher
{
    [TestClass]
    public class UnitTest1
    {
        const string name = "Name";
        const string pfad = "Pfad";

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void VerwaltungHinzufuegen_NegativTest_Null()
        {
            //arrange
            Controller.Spiele.Clear();

            //act
            Controller.ProgrammHinzufügen(null);

            //assert
            Assert.AreEqual(0, Controller.Spiele.Count);
        }

        [TestMethod]
        public void VerwaltungHinzufuegen_PositivTest()
        {
            //arrange
            Controller.Spiele.Clear();
            Programm neuesSpiel = new Programm("windows xp", "C//");

            //act
            Controller.ProgrammHinzufügen(neuesSpiel);

            //assert
            Assert.AreEqual(neuesSpiel, Controller.Spiele[0]);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void VerwaltungEntfernen_NegativTest_Null()
        {
            //arrange
            Controller.Spiele.Clear();
            Controller.Spiele.Add(new Programm("windows xp", "C//"));

            //act
            Controller.ProgrammLöschen(null);

            //assert
            Assert.AreEqual(1, Controller.Spiele.Count);

        }

        [TestMethod]
        public void VerwaltungEntfernen_PositivTest()
        {
            //arrange
            Controller.Spiele.Clear();
            Programm entfernenSpiel = new Programm("windows xp", "C//");
            Controller.Spiele.Add(new Programm("windows xp", "C//"));

            //act
            Controller.ProgrammLöschen(entfernenSpiel.Name);

            //assert
            Assert.AreEqual(0, Controller.Spiele.Count);
        }
    }
}
