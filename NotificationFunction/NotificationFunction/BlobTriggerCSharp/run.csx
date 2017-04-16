#r "Microsoft.WindowsAzure.Storage"
using System;
using Microsoft.WindowsAzure.Storage.Blob;

public static void Run(ICloudBlob myBlob, string name, TraceWriter log)
{
    log.Info($"C# Blob trigger function processed: {name} {myBlob.Name} {myBlob.Properties.ContentType}");

    myBlob.Properties.ContentType = "image/jpeg";
    myBlob.SetProperties();
}