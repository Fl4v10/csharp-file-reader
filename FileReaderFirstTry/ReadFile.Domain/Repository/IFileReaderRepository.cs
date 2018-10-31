namespace ReadFile.Domain.Repository
{
	public interface IFileReaderRepository
	{
		void GetOccurrences(string filePath, out string outputFilePath, out bool done);
	}
}
