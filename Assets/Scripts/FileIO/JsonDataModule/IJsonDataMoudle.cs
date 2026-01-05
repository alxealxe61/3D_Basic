using Newtonsoft.Json.Linq;

namespace Study.FileIO.JsonDataModule
{
    public interface IJsonDataModule
    {
        string Key { get; }

        void OnLoad(JToken dataSegment);
        JToken OnSave();
    }
}