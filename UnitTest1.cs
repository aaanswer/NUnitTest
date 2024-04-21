using ProjectForNUnit;
using System.Xml.Linq;
using System;
using Moq;

namespace ModulTestNUnit
{
    public class Tests
    {
        private Gorilla _gorilla;
        private AlphaGorilla _alpha;
        [SetUp]
        public void Setup()
        {
            _gorilla = new Gorilla("Коко", 7, true);
            _alpha = new AlphaGorilla("Бабун", 12, true);
            
        }

        [Test]
        public void feedGorilla()
        {
            _gorilla.feed();
            Assert.That(_gorilla.isWannaEat, Is.EqualTo(false));
        }

        [Test]
        public void checkGorillaDie()
        {
            StringWriter sw = new StringWriter();
            Console.SetOut(sw);
            _gorilla.killGorilla();
            string consoleOutput = sw.ToString();
            Assert.That(consoleOutput, Is.EqualTo(_gorilla.Name + " is dead\r\n"));
        }

        [Test]
        public void checkIfAlphaWillChange()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(r => r.Next(1, 10)).Returns(3);

            _gorilla.feed();
            _alpha.feed();

            var _groupOfGorillas = new groupOfGorillas(new List<Gorilla> { _gorilla }, _alpha);

            _groupOfGorillas.SetRandom(mockRandom.Object);
            _groupOfGorillas.fightWithAlpha(_gorilla);

            Assert.That(_groupOfGorillas.AlphaGorilla.Name, Is.EqualTo(_gorilla.Name));
            Assert.That(_groupOfGorillas.Gorillas.Count, Is.EqualTo(0));
        }

        [Test]
        public void checkIfGorillaWillDie()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(r => r.Next(1, 10)).Returns(2);

            _gorilla.feed();
            _alpha.feed();

            var _groupOfGorillas = new groupOfGorillas(new List<Gorilla> { _gorilla }, _alpha);

            _groupOfGorillas.SetRandom(mockRandom.Object);
            _groupOfGorillas.fightWithAlpha(_gorilla);

            Assert.That(_groupOfGorillas.AlphaGorilla, Is.EqualTo(_alpha));
            Assert.That(_groupOfGorillas.Gorillas.Count, Is.EqualTo(0));
        }

        [Test]
        public void checkIfAlphaWannaEat()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(r => r.Next(1, 10)).Returns(2);

            _gorilla.feed();

            var _groupOfGorillas = new groupOfGorillas(new List<Gorilla> { _gorilla }, _alpha);

            _groupOfGorillas.SetRandom(mockRandom.Object);
            _groupOfGorillas.fightWithAlpha(_gorilla);

            Assert.That(_groupOfGorillas.AlphaGorilla.Name, Is.EqualTo(_gorilla.Name));
            Assert.That(_groupOfGorillas.Gorillas.Count, Is.EqualTo(0));
        }

        [Test]
        public void checkIfGorillaWannaEat()
        {
            var mockRandom = new Mock<Random>();
            mockRandom.Setup(r => r.Next(1, 10)).Returns(2);

            var _groupOfGorillas = new groupOfGorillas(new List<Gorilla> { _gorilla }, _alpha);

            _groupOfGorillas.SetRandom(mockRandom.Object);
            _groupOfGorillas.fightWithAlpha(_gorilla);

            Assert.That(_groupOfGorillas.AlphaGorilla, Is.EqualTo(_alpha));
            Assert.That(_groupOfGorillas.Gorillas.Count, Is.EqualTo(1));
        }
    }
}