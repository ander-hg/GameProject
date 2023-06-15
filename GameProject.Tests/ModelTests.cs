using Moq;

namespace GameProject.Tests
{
    public class ModelTests
    {
        private HeroInstance _heroInstance;

        [SetUp]
        public void SetUp()
        {
            // Configuração inicial para cada teste
            _heroInstance = new HeroInstance();
            _heroInstance.Items = new List<Item>();
        }

        [TestCase(0, 10, 10)]
        [TestCase(100, -20, 80)]
        [TestCase(int.MaxValue, 5, int.MaxValue)]
        [TestCase(2, -5, 0)]
        public void GainGold_IncreaseGoldByAmount(int initialGold, int amount, int expectedGold)
        {
            // Arrange
            _heroInstance.Gold = initialGold;

            // Act
            _heroInstance.GainGold(amount);

            // Assert
            Assert.AreEqual(expectedGold, _heroInstance.Gold);
        }


        [Test]
        public void AddItem_ShouldAddItemToHeroInstanceItemList()
        {
            // Arrange
            Item item = new Item() { Name = "Sword", Value = 10 };
            int initialItemCount = _heroInstance.Items.Count;

            // Act
            _heroInstance.AddItem(item);

            // Assert
            int expectedItemCount = initialItemCount + 1;
            Assert.AreEqual(expectedItemCount, _heroInstance.Items.Count);
            Assert.Contains(item, _heroInstance.Items);
        }

        [Test]
        public void RemoveItem_ShouldRemoveItemFromHeroInstanceItemList()
        {
            // Arrange
            Item item = new Item() { Name = "Shield", Value = 5 };
            _heroInstance.Items.Add(item);
            int initialItemCount = _heroInstance.Items.Count;

            // Act
            _heroInstance.RemoveItem(item);

            // Assert
            int expectedItemCount = initialItemCount - 1;
            Assert.AreEqual(expectedItemCount, _heroInstance.Items.Count);
            Assert.IsFalse(_heroInstance.Items.Contains(item));
        }

        [Test]
        public void GainExperience_ShouldIncreaseCurrentExperience()
        {
            // Arrange
            int initialExperience = _heroInstance.CurrentExperience;
            int experienceGain = 100;

            // Act
            _heroInstance.GainExperience(experienceGain);

            // Assert
            int expectedExperience = initialExperience + experienceGain;
            Assert.AreEqual(expectedExperience, _heroInstance.CurrentExperience);
        }


        [TestCase("Undead", 200, 30, 50, 100)]
        [TestCase("Ghost", 10, 400, 0, 400)]
        [TestCase("Warlock", 180, 100, -20, 35)]
        public void UpdateHero_ShouldUpdateHero(string name, int health, int mana, int attack, int defense)
        {
            //Arrange
            Hero newHero = new Hero(4, "Rogue", 70, 60, 18, 12);

            // Act
            newHero.updateHero(new Hero(0, name, health, mana, attack, defense));

            // Assert
            Assert.AreEqual(newHero.Name, name);
            Assert.AreEqual(newHero.Health, health);
            Assert.AreEqual(newHero.Mana, mana);
            Assert.AreEqual(newHero.Attack, attack);
            Assert.AreEqual(newHero.Defense, defense);
        }

        [Test]
        public void LevelUp_ShouldIncreaseLevelByOne()
        {
            // Arrange
            int initialLevel = _heroInstance.CurrentLevel;

            // Act
            _heroInstance.LevelUp();

            // Assert
            int expectedLevel = initialLevel + 1;
            Assert.AreEqual(expectedLevel, _heroInstance.CurrentLevel);
        }

        [Test]
        public void UpdateHeroInstance_ShouldUpdateHeroInstance()
        {
            // Arrange
            HeroInstance newHeroInstance = new HeroInstance()
            {
                Hero = new Hero(4, "Rogue", 70, 60, 18, 12),
                CurrentLevel = 10,
                CurrentExperience = 100,
                Items = new List<Item>(),
                Gold = 500,
                Targetable = true,
                Attackable = true
            };

            // Act
            _heroInstance.updateHeroInstance(newHeroInstance);

            // Assert
            Assert.AreEqual(newHeroInstance.Hero, _heroInstance.Hero);
            Assert.AreEqual(newHeroInstance.CurrentLevel, _heroInstance.CurrentLevel);
            Assert.AreEqual(newHeroInstance.CurrentExperience, _heroInstance.CurrentExperience);
            Assert.AreEqual(newHeroInstance.Items, _heroInstance.Items);
            Assert.AreEqual(newHeroInstance.Gold, _heroInstance.Gold);
            Assert.AreEqual(newHeroInstance.Targetable, _heroInstance.Targetable);
            Assert.AreEqual(newHeroInstance.Attackable, _heroInstance.Attackable);
        }


    }
}