namespace DeveloperAssessment.Web.Interfaces;

public interface IFileService
{
    public void WriteToFile(string path, string jsonString);
    public T DeserializeFromFile<T>(string fileName) where T : new();
    public string SaveFileUploadToDirectory(IFormFile file);
}