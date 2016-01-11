using System;

namespace Gigobyte.Mockaroo
{
    public interface IMockarooSerializer
    {
        object Deserialize(string data, Type returnType);

        object Deserialize(Newtonsoft.Json.Linq.JObject json, Type returnType);
    }
}