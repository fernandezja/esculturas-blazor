namespace Esculturas.Core.Configuration
{
    public interface ICurrentConfiguration
    {
        string DataFilePath { get; set; }
        string DataFilePathComplete { get; set; }
    }
}