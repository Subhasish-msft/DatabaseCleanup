﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CleanUpCore
{
    //public static class DocumentDBRepository
    //{
    //    private static readonly string DatabaseId = ConfigurationManager.AppSettings["database"];
    //    private static readonly string CollectionId = ConfigurationManager.AppSettings["collection"];
    //    private static DocumentClient client;

    //    public static async Task<T> GetItemAsync<T>(string id, string partitionKey) where T : class
    //    {
    //        try
    //        {
    //            Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id),
    //                new RequestOptions { PartitionKey = new PartitionKey(partitionKey) });

    //            return (T)(dynamic)document;
    //        }
    //        catch (DocumentClientException e)
    //        {
    //            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
    //            {
    //                return null;
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //    }

    //    public static async Task<IEnumerable<T>> GetItemsAsync<T>(Expression<Func<T, bool>> predicate) where T : class
    //    {
    //        IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
    //            UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
    //            new FeedOptions { MaxItemCount = -1 })
    //            .Where(predicate)
    //            .AsDocumentQuery();

    //        List<T> results = new List<T>();
    //        while (query.HasMoreResults)
    //        {
    //            results.AddRange(await query.ExecuteNextAsync<T>());
    //        }

    //        return results;
    //    }

    //    public static async Task<Document> CreateItemAsync<T>(T item)
    //    {
    //        return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item);
    //    }

    //    public static async Task<Document> UpdateItemAsync<T>(string id, T item)
    //    {
    //        return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id), item);
    //    }

    //    public static async Task DeleteItemAsync(string id)
    //    {
    //        try
    //        {
    //            var res = client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
    //            await res;

    //        }
    //        catch (Exception ex)
    //        {

    //            throw;
    //        }
    //    }

    //    public static void Initialize()
    //    {
    //        client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"], new ConnectionPolicy { EnableEndpointDiscovery = false });
    //        CreateDatabaseIfNotExistsAsync().Wait();
    //        CreateCollectionIfNotExistsAsync().Wait();
    //    }

    //    private static async Task CreateDatabaseIfNotExistsAsync()
    //    {
    //        try
    //        {
    //            await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
    //        }
    //        catch (DocumentClientException e)
    //        {
    //            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
    //            {
    //                await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //    }

    //    private static async Task CreateCollectionIfNotExistsAsync()
    //    {
    //        try
    //        {
    //            await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
    //        }
    //        catch (DocumentClientException e)
    //        {
    //            if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
    //            {
    //                await client.CreateDocumentCollectionAsync(
    //                    UriFactory.CreateDatabaseUri(DatabaseId),
    //                    new DocumentCollection { Id = CollectionId },
    //                    new RequestOptions { OfferThroughput = 1000 });
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //    }
    //}
}
