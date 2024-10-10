namespace Checkers.Logic.Interfaces
{
	public interface ICheckersInterface
	{
		public byte[,] GetGameFieldAsByteArray();

		public List<byte[]> GetPossibleMovementsAsByteArrayList(byte[] figurePosition);
	}
}