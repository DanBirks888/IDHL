namespace DeveloperAssessment.Web.Interfaces;

public interface IFileService
{
    public Stream GetFileStream(string fileName);
    public void WriteToFile(string path, string jsonString);
    public T DeserializeFromFile<T>(string fileName) where T : new();
}