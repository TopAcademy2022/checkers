namespace Checkers.Logic.Interfaces
{
	public interface ICheckersInterface
	{
		public byte[,] GetGameFieldAsByteArray();

		public List<byte[]> GetPossibleMovementsAsByteArrayList(List<KeyValuePair<char, int>> positions);
	}
}
