using Checkers.Logic;

namespace CheckersTests
{
    public class GameFieldTests
    {
        [Fact]
        public void PlaceFiguresTest()
        {
            // Arrange
            GameField gameField = new GameField();
            List<KeyValuePair<char, int>> predpolagaemPustie = new List<KeyValuePair<char, int>>()
            {
                new KeyValuePair<char, int>('a', 5),
                new KeyValuePair<char, int>('b', 4),
                new KeyValuePair<char, int>('c', 5),
                new KeyValuePair<char, int>('d', 4),
                new KeyValuePair<char, int>('e', 5),
                new KeyValuePair<char, int>('f', 4),
                new KeyValuePair<char, int>('g', 5),
                new KeyValuePair<char, int>('h', 4)
            };

            // Act
            gameField.PlaceFigures();

            // Assert
            Assert.Equal(new HashSet<KeyValuePair<char, int>>(predpolagaemPustie), new HashSet<KeyValuePair<char, int>>(gameField.EmptyCells));
        }
    }
}