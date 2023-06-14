using GameProject.DataAccess;

namespace GameProject.Tests
{
    public class Tests
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
        public void AddItem_AddItemToItemList()
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
        public void RemoveItem_RemoveItemFromItemList()
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
    }
}