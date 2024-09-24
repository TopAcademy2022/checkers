namespace Checkers.Logic
{
    public class GameField
    {
        private readonly byte[,] _checkersField;

        public const byte COUNT_ROWS = 8;

		public const byte COUNT_COLLUMNS = 8;

        public GameField()
        {
            this._checkersField = new byte[COUNT_ROWS, COUNT_COLLUMNS];

            for (int i = 0; i < COUNT_ROWS; i++)
            {
                for (int k = 0; k < COUNT_COLLUMNS; k++)
                {
                    // Set empty cells
                    this._checkersField[i, k] = 0;
                }
            }

            // 0 - индекс строки, БЕЛЫЕ
            this._checkersField[0, 1] = 1;
            this._checkersField[0, 3] = 1;
            this._checkersField[0, 5] = 1;
            this._checkersField[0, 7] = 1;

            // 1 - индекс строки, БЕЛЫЕ
            this._checkersField[1, 0] = 1;
            this._checkersField[1, 2] = 1;
            this._checkersField[1, 4] = 1;
            this._checkersField[1, 6] = 1;

            // 2 - индекс строки, БЕЛЫЕ
            this._checkersField[2, 1] = 1;
            this._checkersField[2, 3] = 1;
            this._checkersField[2, 5] = 1;
            this._checkersField[2, 7] = 1;



            // 5 - индекс строки, ЧЁРНЫЕ
            this._checkersField[5, 0] = 2;
            this._checkersField[5, 2] = 2;
            this._checkersField[5, 4] = 2;
            this._checkersField[5, 6] = 2;

            // 6 - индекс строки, ЧЁРНЫЕ
            this._checkersField[6, 1] = 2;
            this._checkersField[6, 3] = 2;
            this._checkersField[6, 5] = 2;
            this._checkersField[6, 7] = 2;

            // 7 - индекс строки, ЧЁРНЫЕ
            this._checkersField[7, 0] = 2;
            this._checkersField[7, 2] = 2;
            this._checkersField[7, 4] = 2;
            this._checkersField[7, 6] = 2;
        }

		public byte[,] GetCheckersField()
		{
			return this._checkersField;

		}
	}
}