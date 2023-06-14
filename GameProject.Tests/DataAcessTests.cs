using Autofac.Extras.Moq;
using GameProject.DataAccess;
using GameProject.DataAccess.Postgres;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace GameProject.Tests
{
    public class DataAccessTests
    {
        [Test]
        public void AddHeroToList_ShouldWork()
        {
            using (AutoMock mock = AutoMock.GetLoose())
            {
                // Arrange
                var heroesBefore = new List<Hero>();
                var heroesAfter = new List<Hero>();

                // Mock the database connection and repository
                var mockConnection = mock.Mock<IDatabaseConnection>();
                var mockRepository = mock.Mock<IHeroRepository>();

                // Set up the mock repository
                mockRepository.Setup(r => r.GetAll()).Returns(heroesBefore);
                mockRepository.Setup(r => r.Insert(It.IsAny<Hero>())).Returns((Hero hero) =>
                {
                    hero.Id = 4;
                    heroesAfter.Add(hero);
                    return hero.Id;
                });


                // Resolve the repository from the mock container
                IHeroRepository _heroRepository = mock.Create<IHeroRepository>();

                // Act
                Hero newHero = new Hero(4, "Rogue", 70, 60, 18, 12);
                newHero.Id = _heroRepository.Insert(newHero);

                // Assert
                Assert.AreEqual(heroesBefore.Count + 1, heroesAfter.Count);
                Assert.Contains(newHero, heroesAfter);
            }
        }
    }
}
