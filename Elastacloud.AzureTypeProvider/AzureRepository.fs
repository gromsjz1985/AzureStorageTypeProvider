﻿module internal Elastacloud.FSharp.AzureTypeProvider.AzureRepository

open Microsoft.WindowsAzure.Storage
open Microsoft.WindowsAzure.Storage.Blob
open System

type LightweightContainer = 
    { Name : string
      Files : seq<string> }

/// Generates a set a lightweight container lists for a blob storage account
let getBlobStorageAccountManifest connection = 
    CloudStorageAccount.Parse(connection).CreateCloudBlobClient().ListContainers()
    |> Seq.toList
    |> List.map (fun c -> 
           { Name = c.Name
             Files = c.ListBlobs(useFlatBlobListing = true)
                     |> Seq.map (fun b -> (b :?> CloudBlockBlob).Name)
                     |> Seq.toList })
