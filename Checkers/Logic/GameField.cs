﻿using Microsoft.Extensions.Logging;

namespace Checkers.Logic
{
	public class GameField
	{
		private readonly ILogger<GameField> _logger;

		private readonly List<KeyValuePair<char, int>> _emptyCells;

		private readonly Dictionary<GameFigure, KeyValuePair<char, int>> _checkersField;

        public IReadOnlyList<KeyValuePair<char, int>> EmptyCells => this._emptyCells;

        public IReadOnlyDictionary<GameFigure, KeyValuePair<char, int>> CheckersField => this._checkersField;

		public const byte COUNT_ROWS = 8;

		public const byte COUNT_COLLUMNS = 8;

		/*! 
		* @brief Checks if the cell is empty and makes a move to the destination cell.
		* @param[in] gameFigure - The figure that will be walking.
		* @param[in] destinationCell - Destination cell.
		*/
		private void MoveFigureToCell(GameFigure gameFigure, KeyValuePair<char, int> destinationCell)
		{
			/*! 
			* @exception Exception Thrown when existing cell destination or it was not found.
			*/
			try
			{
				/*! 
				* @if Destination cell is empty cell list.
				*	Moving figure.
				* @else
				*	Create exception.
				* @endif
				*/
				if (this._emptyCells.Contains(destinationCell))
				{
					this._emptyCells.Add(this._checkersField[gameFigure]);
					this._checkersField[gameFigure] = destinationCell;
					this._emptyCells.Remove(destinationCell);
				}
				else
				{
					throw new Exception("Destination cell not found.");
				}
			}
			catch (Exception exception)
			{
				this._logger.LogError(exception.Message);
			}
		}

		private List<KeyValuePair<char, int>> GetEmptyCellInRow(byte rowNumber)
		{
			const byte START_ROW_NUMBER = 1;
			const byte FINISH_ROW_NUMBER = 8;

			List<KeyValuePair<char, int>> result = new List<KeyValuePair<char, int>>();

			try
			{
				if (rowNumber >= START_ROW_NUMBER && rowNumber <= FINISH_ROW_NUMBER)
				{
					result = this._emptyCells.Where(cell => cell.Value == rowNumber).ToList();
				}
				else
				{
					throw new IndexOutOfRangeException("Row number out of range.");
				}
			}
			catch (Exception exception)
			{
				this._logger.LogError(exception.Message);
			}

			return result;
		}

		/*!
		* @brief Getting the left diagonal of one specific figure.
		* @param[in] gameFigure - We use it to know, for which figure we calculate the diagonals.
		* @return List of every field coordinate in left diagonal.
		*/
		private List<KeyValuePair<char, int>> GetLeftDiagonal(GameFigure gameFigure)
		{
			List<KeyValuePair<char, int>> result = new List<KeyValuePair<char, int>>();

			KeyValuePair<char, int> currentPosition = this._checkersField[gameFigure];

			GameFigure.DirectionMovement directionMovement = gameFigure.GetDirectionMovement(); ///< Getting the direction of checkers movement

			byte CHAR_CODE_H = 104; ///< Char code from Ascii table
			byte CHAR_CODE_A = 97;  ///< Char code from Ascii table

			if (directionMovement == GameFigure.DirectionMovement.Up) ///< Normal figure moving down
			{
				for (byte i = 1; i <= COUNT_ROWS; i++)
				{
					if ((currentPosition.Key - i) >= CHAR_CODE_A && currentPosition.Value + i <= 8)
					{
						result.Add(new KeyValuePair<char, int>((char)(currentPosition.Key - i), currentPosition.Value + i));
					}
				}
			}
			else ///< Normal figure moving up
			{
				for (byte i = 1; i <= COUNT_ROWS; i++)
				{
					if ((currentPosition.Key + i) <= CHAR_CODE_H && currentPosition.Value - i >= 1)
					{
						result.Add(new KeyValuePair<char, int>((char)(currentPosition.Key + i), currentPosition.Value - i));
					}
				}
			}

			if (gameFigure.GetIsQueen()) ///< Checking for queen
			{
				for (byte i = 1; i < COUNT_ROWS; i++)
				{
					if ((currentPosition.Key + i) <= CHAR_CODE_H && currentPosition.Value - i >= 1 &&
						directionMovement == GameFigure.DirectionMovement.Up)
					{
						result.Add(new KeyValuePair<char, int>((char)(currentPosition.Key + i), currentPosition.Value - i));
					}
					if ((currentPosition.Key - i) >= CHAR_CODE_A && currentPosition.Value + i <= 8 &&
						directionMovement == GameFigure.DirectionMovement.Down)
					{
						result.Add(new KeyValuePair<char, int>((char)(currentPosition.Key - i), currentPosition.Value + i));
					}
				}
			}

			return result;
		}

		/*!
		* @brief Getting the right diagonal of one specific figure.
		 * @param [in] gameFigure - We use it to know, for which figure we calculate the diagonals.
		 * @return List of every field coordinate in right diagonal.
		 */
		private List<KeyValuePair<char, int>> GetRightDiagonal(GameFigure gameFigure)
		{
			List<KeyValuePair<char, int>> result = new List<KeyValuePair<char, int>>(); ///< All possible positions
			KeyValuePair<char, int> presentPosition = this._checkersField[gameFigure]; ///< Current position

			const uint CHAR_CODE_A = 97; ///< Char code from Ascii table
			const uint CHAR_CODE_H = 104; ///< Char code from Ascii table

			GameFigure.DirectionMovement movement = gameFigure.GetDirectionMovement(); ///< Direction Movement

			if (movement == GameFigure.DirectionMovement.Up)
			{
				for (int i = 1; i < COUNT_ROWS; ++i)
				{
					if (presentPosition.Key + i <= CHAR_CODE_H && presentPosition.Key + i >= CHAR_CODE_A &&
						(presentPosition.Value + i) <= COUNT_ROWS)
					{
						result.Add(new KeyValuePair<char, int>((char)(presentPosition.Key + i), presentPosition.Value + i)); ///< Position addition
					}
				}
			}
			else if (movement == GameFigure.DirectionMovement.Down)
			{
				for (int i = 1; i < COUNT_ROWS; ++i)
				{
					if (presentPosition.Key - i <= CHAR_CODE_H && presentPosition.Key - i >= CHAR_CODE_A &&
						presentPosition.Value - i >= 1)
					{
						result.Add(new KeyValuePair<char, int>((char)(presentPosition.Key - i), presentPosition.Value - i)); ///< Position addition
					}
				}
			}

			if (gameFigure.GetIsQueen())
			{
				for (int i = 1; i < COUNT_ROWS; ++i)
				{
					if (presentPosition.Key + i <= CHAR_CODE_H && presentPosition.Key + i >= CHAR_CODE_A &&
						(presentPosition.Value + i) <= COUNT_ROWS &&
						movement != GameFigure.DirectionMovement.Up)
					{
						result.Add(new KeyValuePair<char, int>((char)(presentPosition.Key + i), presentPosition.Value + i)); ///< Position addition
					}
					if (presentPosition.Key - i <= CHAR_CODE_H && presentPosition.Key - i >= CHAR_CODE_A &&
						presentPosition.Value - i >= 1 && movement != GameFigure.DirectionMovement.Down)
					{
						result.Add(new KeyValuePair<char, int>((char)(presentPosition.Key - i), presentPosition.Value - i)); ///< Position addition
					}
				}
			}

			return result;
		}

		// TODO: rewrite logic for first placement
		public GameField()
		{
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            this._logger = factory.CreateLogger<GameField>();

            this._emptyCells = new List<KeyValuePair<char, int>>();
            this._checkersField = new Dictionary<GameFigure, KeyValuePair<char, int>>();
        }

		public void PlaceFigures()
		{
            const byte START_ROW_NUMBER_FOR_UPPER = 8;
            const byte FINISH_ROW_NUMBER_FOR_UPPER = 6;

            const byte START_ROW_NUMBER_FOR_LOWER = 3;
            const byte FINISH_ROW_NUMBER_FOR_LOWER = 1;

            char[] chars1 = { 'a', 'c', 'e', 'g' };

            foreach (char symbol in chars1)
            {
                this._emptyCells.Add(new KeyValuePair<char, int>(symbol, 1));
                this._emptyCells.Add(new KeyValuePair<char, int>(symbol, 3));
                this._emptyCells.Add(new KeyValuePair<char, int>(symbol, 7));
            }

            char[] chars2 = { 'b', 'd', 'f', 'h' };

            foreach (char symbol in chars2)
            {
                this._emptyCells.Add(new KeyValuePair<char, int>(symbol, 2));
                this._emptyCells.Add(new KeyValuePair<char, int>(symbol, 6));
                this._emptyCells.Add(new KeyValuePair<char, int>(symbol, 8));
            }

            for (byte i = START_ROW_NUMBER_FOR_UPPER; i >= FINISH_ROW_NUMBER_FOR_UPPER; i--)
            {
                foreach (KeyValuePair<char, int> emptyCell in this.GetEmptyCellInRow(i))
                {
                    GameFigure gameFigure = new GameFigure(GameFigure.GameColor.White, GameFigure.DirectionMovement.Down);
                    this._checkersField.Add(gameFigure, emptyCell);
                    this._emptyCells.Remove(emptyCell);
                }
            }

            for (byte i = START_ROW_NUMBER_FOR_LOWER; i >= FINISH_ROW_NUMBER_FOR_LOWER; i--)
            {
                foreach (KeyValuePair<char, int> emptyCell in this.GetEmptyCellInRow(i))
                {
                    GameFigure gameFigure = new GameFigure(GameFigure.GameColor.Black, GameFigure.DirectionMovement.Up);
                    this._checkersField.Add(gameFigure, emptyCell);
                    this._emptyCells.Remove(emptyCell);
                }
            }

            // Bad, rewrite
            foreach (char symbol in chars1)
            {
                this._emptyCells.Add(new KeyValuePair<char, int>(symbol, 5));
            }

            foreach (char symbol in chars2)
            {
                this._emptyCells.Add(new KeyValuePair<char, int>(symbol, 4));
            }
        }

        /*!
		* @brief Possible figure movement.
		* @param[in] gameFigure - to determine the course.
		* @return List of every field coordinate.
		*/
        public List<KeyValuePair<char, int>> GetPossibleFigureMovement(GameFigure gameFigure)
		{
			List<KeyValuePair<char, int>> result = new List<KeyValuePair<char, int>>();

			List<KeyValuePair<char, int>> leftDiagonal = this.GetLeftDiagonal(gameFigure);
			List<KeyValuePair<char, int>> ridhtDiagonal = this.GetRightDiagonal(gameFigure);

			switch (gameFigure.GetIsQueen()) ///< Queen check
			{
				case false:

					if (this._emptyCells.Contains(leftDiagonal.FirstOrDefault())) ///< Checking an empty cell
					{
						result.Add(leftDiagonal.FirstOrDefault()); ///< Add the result to our List
					}
					else
					{
						if (this._checkersField.Where(el => el.Value.Key == leftDiagonal.FirstOrDefault().Key && el.Value.Value == leftDiagonal.FirstOrDefault().Value).First().Key.GetColor() != gameFigure.GetColor())
						{
							for (int i = 1; i < leftDiagonal.Count; i += 2)
							{
								if (this._emptyCells.Contains(leftDiagonal[i])) ///< Checking an empty cell
								{
									result.Add(leftDiagonal[i]); ///< Add the result to our List
								}
								else
								{
									break;
								}
							}
						}
					}

					if (this._emptyCells.Contains(ridhtDiagonal.FirstOrDefault())) ///< Checking an empty cell
					{
						result.Add(ridhtDiagonal.FirstOrDefault()); ///< Add the result to our List
					}
					else
					{
						if (this._checkersField.Where(el => el.Value.Key == ridhtDiagonal.FirstOrDefault().Key && el.Value.Value == ridhtDiagonal.FirstOrDefault().Value).First().Key.GetColor() != gameFigure.GetColor())
						{
							for (int i = 1; i < ridhtDiagonal.Count; i += 2)
							{
								if (this._emptyCells.Contains(ridhtDiagonal[i])) ///< Checking an empty cell
								{
									result.Add(ridhtDiagonal[i]); ///< Add the result to our List
								}
								else
								{
									break;
								}
							}
						}
					}

					break;

				case true:

					foreach (KeyValuePair<char, int> left in leftDiagonal)
					{
						if (this._emptyCells.Contains(left)) ///< Checking an empty cell
						{
							result.Add(left); ///< Add the result to our List
						}
					}
					foreach (KeyValuePair<char, int> ridht in ridhtDiagonal)
					{
						if (this._emptyCells.Contains(ridht)) ///< Checking an empty cell
						{
							result.Add(ridht); ///< Add the result to our List
						}
					}

					break;
			}

			return result;
		}

		/*!
		 * @brief Gets a list of free cells and move figure.
		 * @param[in] gameFigure - Game piece that will move.
		 * @param[in] destinationCell - Destination cell game piece.
		 * @return True - on a successful move; False - relocation failed.
		 */
		public bool MoveFigure(GameFigure gameFigure, KeyValuePair<char, int> destinationCell)
		{
			List<KeyValuePair<char, int>> possiblePositions = this.GetPossibleFigureMovement(gameFigure); ///< Positions possible.

			/*!
			 * @if Checking destination сell in possible cells.
			 *		Figure movement.
			 * @endif
			 */
			if (possiblePositions.Contains(destinationCell))
			{
				this.MoveFigureToCell(gameFigure, destinationCell);
				return true;
			}

			return false;
		}

		public byte CheckWinner()
		{
			byte draw = 0;
			byte whiteWin = 1;
			byte blackWin = 2;

			bool isWhiteWin = false;
			bool isBlackWin = false;

			foreach (KeyValuePair<GameFigure, KeyValuePair<char, int>> keyValue in this._checkersField)
			{
				if (keyValue.Key.GetColor() == GameFigure.GameColor.White)
				{
					isWhiteWin = true;
				}
				else if (keyValue.Key.GetColor() == GameFigure.GameColor.Black)
				{
					isBlackWin = true;
				}
				if (isWhiteWin && !isBlackWin)
				{
					return whiteWin;
				}
				else if (!isWhiteWin && isBlackWin)
				{
					return blackWin;
				}
			}

			return draw;
		}
	}
}
