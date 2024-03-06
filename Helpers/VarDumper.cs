using System;
using Newtonsoft.Json;

public static class VarDumper
{
    public static void Dump<T>(T data)
    {
        var json = JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
        Console.WriteLine(json);
    }
}