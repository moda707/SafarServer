using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SafarCore.GenFunctions;

namespace SafarCore.DbClasses
{
    public class DbConnection
    {
        private static IMongoClient _client;
        private static IMongoDatabase _database;

        public DbConnection()
        {
            Connect("SafarDB");
        }

        /* Connect to the Database */
        public FuncResult Connect(string dbname)
        {

            var connectionString = DbConfig.GetConnectionString();
            //Check the Connection
            try
            {
                _client = new MongoClient(connectionString);

                try
                {
                    _database = _client.GetDatabase(dbname);
                    return new FuncResult(ResultEnum.Successfull);
                }
                catch (Exception e)
                {
                    var logger = new Logger(ObjectId.Empty, "DbConnection", "Connect to db", e.Message);
                    logger.AddLog();
                    return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
                }
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "Connect to client", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }


        public FuncResult Connect()
        {
            return Connect("SafarDB");
        }

        public Task<List<T>> GetFilteredListAsync<T>(CollectionNames collectionname, List<FieldFilter> filterPairs)
        {
            try
            {
                FilterDefinition<T> filter = new BsonDocument();

                if (filterPairs != null && filterPairs.Count > 0)
                {
                    filter = FilterBuilder<T>(filterPairs);
                }

                var curser = _database.GetCollection<T>(Collections.GetCollectionName(collectionname))
                    .Find(filter).ToListAsync();
                
                return curser;
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "GetFilteredList", e.Message);
                logger.AddLog();
                return null;
            }
        }

        public async Task<List<T>> GetFilteredListAsync<T>(CollectionNames collectionname, List<FieldFilter> filterPairs
            , SortFilter sortFilter, int count)
        {
            // count = -1 means to return all
            try
            {
                FilterDefinition<T> filter = new BsonDocument();

                if (filterPairs != null && filterPairs.Count > 0)
                {
                    filter = FilterBuilder<T>(filterPairs);
                }

                var sort = SortBuilder<T>(sortFilter);
                var collection = _database.GetCollection<T>(Collections.GetCollectionName(collectionname));
                var curser = collection.FindAsync(filter);

                var result = await curser.Result.MoveNextAsync();
                return result ? curser.Result.Current.ToList() : null;
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "GetFilteredList", e.Message);
                logger.AddLog();
                return null;
            }
        }

        public List<T> GetFilteredList<T>(CollectionNames collectionname, List<FieldFilter> filterPairs
            , SortFilter sortFilter, int count)
        { 
            // count = -1 means to return all
            try
            {
                List<T> result;

                if (filterPairs == null)
                {
                    result = IAsyncCursorSourceExtensions.ToList(_database.GetCollection<T>(Collections.GetCollectionName(collectionname)).AsQueryable());
                    return result;
                }

                if (filterPairs.Count == 0)
                {
                    result = IAsyncCursorSourceExtensions.ToList(_database.GetCollection<T>(Collections.GetCollectionName(collectionname)).AsQueryable());
                    return result;
                }

                var filter = FilterBuilder<T>(filterPairs);
                var sort = SortBuilder<T>(sortFilter);

                result = _database.GetCollection<T>(Collections.GetCollectionName(collectionname)).Find(filter)
                    .Sort(sort).ToList();

                if (count > 0)
                {
                    result = _database.GetCollection<T>(Collections.GetCollectionName(collectionname)).Find(filter)
                        .Sort(sort).Limit(count).ToList();
                }
                else
                {
                    result = _database.GetCollection<T>(Collections.GetCollectionName(collectionname)).Find(filter)
                        .Sort(sort).ToList();
                }

                return result;
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "GetFilteredList", e.Message);
                logger.AddLog();
                return null;
            }
        }
        public List<T> GetORFilteredList<T>(CollectionNames collectionname, List<FieldFilter> filterPairs)
        {
            try
            {
                List<T> result;

                if (filterPairs == null)
                {
                    result = IAsyncCursorSourceExtensions.ToList(_database.GetCollection<T>(Collections.GetCollectionName(collectionname)).AsQueryable());
                    return result;
                }

                if (filterPairs.Count == 0)
                {
                    result = IAsyncCursorSourceExtensions.ToList(_database.GetCollection<T>(Collections.GetCollectionName(collectionname)).AsQueryable());
                    return result;
                }

                var filter = FilterBuilderOR<T>(filterPairs);

                result = _database.GetCollection<T>(Collections.GetCollectionName(collectionname)).Find(filter).ToList();

                return result;
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "GetORFilteredList", e.Message);
                logger.AddLog();
                return null;
            }
        }

        public List<T> GetFilteredList<T>(CollectionNames collectionname, List<FieldFilter> filterPairs)
        {
            try
            {
                FilterDefinition<T> filter = new BsonDocument();

                if (filterPairs != null && filterPairs.Count > 0)
                {
                    filter = FilterBuilder<T>(filterPairs);
                }

                var result = _database.GetCollection<T>(Collections.GetCollectionName(collectionname))
                    .Find(filter).ToList();

                
                return result;
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "GetFilteredList", e.Message);
                logger.AddLog();
                return null;
            }
        }

        public List<T> GetFilteredList<T>(string collectionname, List<FieldFilter> filterPairs)
        {
            try
            {
                List<T> result;

                if (filterPairs == null)
                {
                    result = IAsyncCursorSourceExtensions.ToList(_database.GetCollection<T>(collectionname).AsQueryable());
                    return result;
                }

                if (filterPairs.Count == 0)
                {
                    result = IAsyncCursorSourceExtensions.ToList(_database.GetCollection<T>(collectionname).AsQueryable());
                    return result;
                }

                var filter = FilterBuilder<T>(filterPairs);

                result = _database.GetCollection<T>(collectionname).Find(filter).ToList();

                return result;
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "GetFilteredList", e.Message);
                logger.AddLog();
                return null;
            }
        }
        
        public IMongoCollection<BsonDocument> GetMongoCollection(CollectionNames collectionname)
        {
            return _database.GetCollection<BsonDocument>(Collections.GetCollectionName(collectionname));
        }

        public IMongoCollection<BsonDocument> GetMongoCollection(string collectionname)
        {
            return _database.GetCollection<BsonDocument>(collectionname);
        }

        public FuncResult AddorUpdate(BsonDocument document, IMongoCollection<BsonDocument> collection,
            List<string> keyList = null)
        {

            try
            {
                if (keyList == null) //Just Add
                {
                    collection.InsertOneAsync(document);
                    return new FuncResult(ResultEnum.Successfull);
                }
                else //Check for existency
                {
                    var builder = Builders<BsonDocument>.Filter;
                    var filter = builder.Eq(keyList[0], document[keyList[0]]);

                    for (var i = 1; i < keyList.Count; i++)
                    {
                        filter &= builder.Eq(keyList[i], document[keyList[i]]);
                    }

                    var c = collection.Find(filter).ToList();

                    if (c.Count > 0)
                    {
                        //Update
                        collection.ReplaceOneAsync(filter, document);

                    }
                    else
                    {
                        //Add new
                        collection.InsertOneAsync(document);
                    }
                }
                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "AddorUpdate", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }

        }

        public async Task<FuncResult> AddorUpdateAsync(BsonDocument document, IMongoCollection<BsonDocument> collection,
            List<string> keyList = null)
        {
            try
            {
                if (keyList == null) //Just Add
                {
                    await collection.InsertOneAsync(document);
                }
                else //Check for existency
                {
                    var builder = Builders<BsonDocument>.Filter;
                    var filter = builder.Eq(keyList[0], document[keyList[0]]);

                    for (var i = 1; i < keyList.Count; i++)
                    {
                        filter &= builder.Eq(keyList[i], document[keyList[i]]);
                    }

                    var c = collection.Find(filter).ToList();

                    if (c.Count > 0)
                    {
                        //Update
                        await collection.ReplaceOneAsync(filter, document);

                    }
                    else
                    {
                        //Add new
                        await collection.InsertOneAsync(document);
                    }
                }
                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "AddorUpdate", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }

        }

        public FuncResult AddorUpdateMany(List<BsonDocument> documents, IMongoCollection<BsonDocument> collection,
            List<string> keyList = null)
        {

            try
            {
                if (keyList == null) //Just Add
                {
                    collection.InsertManyAsync(documents);
                    return new FuncResult(ResultEnum.Successfull);
                }

                foreach (var document in documents)
                {
                    var builder = Builders<BsonDocument>.Filter;
                    var filter = builder.Eq(keyList[0], document[keyList[0]]);

                    for (var i = 1; i < keyList.Count; i++)
                    {
                        filter &= builder.Eq(keyList[i], document[keyList[i]]);
                    }

                    var c = collection.Find(filter).ToList();

                    if (c.Count > 0)
                    {
                        //Update
                        collection.ReplaceOneAsync(filter, document);

                    }
                    else
                    {
                        //Add new
                        collection.InsertOneAsync(document);
                    }
                }
                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "AddorUpdateMany", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }

        }

        public async Task<FuncResult> AddorUpdateManyAsync(List<BsonDocument> documents, IMongoCollection<BsonDocument> collection,
            List<string> keyList = null)
        {
            try
            {
                if (keyList == null) //Just Add
                {
                    await collection.InsertManyAsync(documents);
                }

                foreach (var document in documents)
                {
                    var builder = Builders<BsonDocument>.Filter;
                    var filter = builder.Eq(keyList[0], document[keyList[0]]);

                    for (var i = 1; i < keyList.Count; i++)
                    {
                        filter &= builder.Eq(keyList[i], document[keyList[i]]);
                    }

                    var c = collection.Find(filter).ToList();

                    if (c.Count > 0)
                    {
                        //Update
                        await collection.ReplaceOneAsync(filter, document);

                    }
                    else
                    {
                        //Add new
                        await collection.InsertOneAsync(document);
                    }
                }
                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "AddorUpdateMany", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }

        }

        public FuncResult AddorReplaceMany(List<BsonDocument> documents, IMongoCollection<BsonDocument> collection, ObjectId userId)
        {
            var passed = "";

            try
            {
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("UserId", userId);

                var delres = collection.DeleteMany(filter);
                passed += delres.DeletedCount + " item removed | ";

                collection.InsertMany(documents);
                passed += documents.Count + " items added | ";


                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "AddorReplaceMany", passed + e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }

        }

        public async Task<FuncResult> AddorReplaceManyAsync(List<BsonDocument> documents, IMongoCollection<BsonDocument> collection, ObjectId userId)
        {
            var passed = "";

            try
            {
                var builder = Builders<BsonDocument>.Filter;
                var filter = builder.Eq("UserId", userId);

                var delres = collection.DeleteMany(filter);
                passed += delres.DeletedCount + " item removed | ";

                await collection.InsertManyAsync(documents);
                passed += documents.Count + " items added | ";

                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "AddorReplaceMany", passed + e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }

        }

        public FuncResult UpdateField(CollectionNames collectionName, List<FieldFilter> filterPairs,
            List<FieldUpdate> fieldsPairs)
        {
            try
            {
                var filtered = FilterBuilder<BsonDocument>(filterPairs);


                var dbConnection = new DbConnection();
                
                var collection = dbConnection.GetMongoCollection(collectionName);

                UpdateDefinition<BsonDocument> updated = new BsonDocument();
                //var updatebuilder = Builders<BsonDocument>.Update;

                foreach (var t in fieldsPairs)
                {
                    switch (t.FieldType)
                    {
                        case FieldType.String:
                            updated.Set(t.FieldName, (string)t.FieldValue);
                            break;
                        case FieldType.Double:
                            updated.Set(t.FieldName, (double)t.FieldValue);
                            break;
                        case FieldType.Bool:
                            updated.Set(t.FieldName, (bool)t.FieldValue);
                            break;
                        case FieldType.Date:
                            updated.Set(t.FieldName, (DateTime)t.FieldValue);
                            break;
                        case FieldType.ObjectId:
                            updated.Set(t.FieldName, (ObjectId)t.FieldValue);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                }


                collection.UpdateMany(filtered, updated);


                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "UpdateField", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }

        public async Task<FuncResult> UpdateFieldAsync(CollectionNames collectionName, List<FieldFilter> filterPairs,
            List<FieldUpdate> fieldsPairs)
        {
            try
            {
                var filtered = FilterBuilder<BsonDocument>(filterPairs);


                var dbConnection = new DbConnection();
                
                var collection = dbConnection.GetMongoCollection(collectionName);

                UpdateDefinition<BsonDocument> updated = new BsonDocument();
                //var updatebuilder = Builders<BsonDocument>.Update;

                foreach (var t in fieldsPairs)
                {
                    switch (t.FieldType)
                    {
                        case FieldType.String:
                            updated.Set(t.FieldName, (string)t.FieldValue);
                            break;
                        case FieldType.Double:
                            updated.Set(t.FieldName, (double)t.FieldValue);
                            break;
                        case FieldType.Bool:
                            updated.Set(t.FieldName, (bool)t.FieldValue);
                            break;
                        case FieldType.Date:
                            updated.Set(t.FieldName, (DateTime)t.FieldValue);
                            break;
                        case FieldType.ObjectId:
                            updated.Set(t.FieldName, (ObjectId)t.FieldValue);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                }
                
                await collection.UpdateManyAsync(filtered, updated);

                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "UpdateField", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }

        public FuncResult InsertMany(List<BsonDocument> documents, IMongoCollection<BsonDocument> collection,
            string fieldName = null, bool update = false)
        {
            try
            {
                var newlist = new List<BsonDocument>();
                if (fieldName == null)
                {
                    newlist = documents;
                }
                else
                {
                    //find those documents which there is no similar of them in DB
                    foreach (var document in documents)
                    {
                        var builder = Builders<BsonDocument>.Filter;
                        var filter = builder.Eq(fieldName, document[fieldName]);

                        if (collection.CountDocuments(filter) == 0)
                        {
                            newlist.Add(document);
                        }
                    }
                }

                collection.InsertMany(newlist);
                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "InsertMany", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }

        public async Task<FuncResult> InsertManyAsync(List<BsonDocument> documents, IMongoCollection<BsonDocument> collection,
            string fieldName = null, bool update = false)
        {
            try
            {
                var newlist = new List<BsonDocument>();
                if (fieldName == null)
                {
                    newlist = documents;
                }
                else
                {
                    //find those documents which there is no similar of them in DB
                    foreach (var document in documents)
                    {
                        var builder = Builders<BsonDocument>.Filter;
                        var filter = builder.Eq(fieldName, document[fieldName]);

                        if (collection.CountDocuments(filter) == 0)
                        {
                            newlist.Add(document);
                        }
                    }
                }

                await collection.InsertManyAsync(newlist);
                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "InsertMany", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }

        public FuncResult Delete(BsonDocument document, IMongoCollection<BsonDocument> collection)
        {
            try
            {
                collection.DeleteOne(document);
                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "Delete", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }

        public async Task<FuncResult> DeleteAsync(BsonDocument document, IMongoCollection<BsonDocument> collection)
        {
            try
            {
                await collection.DeleteOneAsync(document);
                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "Delete", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }

        public async Task<FuncResult> DeleteManyAsync(CollectionNames collectionName, List<FieldFilter> fieldFilters)
        {
            try
            {
                var collection = GetMongoCollection(collectionName);
                var filter = FilterBuilder<BsonDocument>(fieldFilters);
                await collection.DeleteManyAsync(filter);
                return new FuncResult(ResultEnum.Successfull);
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "Delete", e.Message);
                logger.AddLog();
                return new FuncResult(ResultEnum.Unsuccessfull, e.Message);
            }
        }

        public FilterDefinition<T> FilterBuilder<T>(List<FieldFilter> fieldFilters)
        {
            try
            {
                if (fieldFilters == null)
                {
                    return new BsonDocumentFilterDefinition<T>(new BsonDocument());
                }

                if (fieldFilters.Count == 0) return null;

                var builder = Builders<T>.Filter;
                FilterDefinition<T> filter = new BsonDocumentFilterDefinition<T>(new BsonDocument());
                

                foreach (var fieldFilter in fieldFilters)
                {
                    switch (fieldFilter.FieldType)
                    {
                        case FieldType.String:
                            var val = DynamicCast<string>(fieldFilter.FieldValue);
                            switch (fieldFilter.CompareType)
                            {
                                case CompareType.Equal:
                                    filter &= builder.Eq(fieldFilter.FieldName, val);
                                    break;
                                case CompareType.GT:
                                    filter &= builder.Gt(fieldFilter.FieldName, val);
                                    break;
                                case CompareType.GTE:
                                    filter &= builder.Gte(fieldFilter.FieldName, val);
                                    break;
                                case CompareType.LT:
                                    filter &= builder.Lt(fieldFilter.FieldName, val);
                                    break;
                                case CompareType.LTE:
                                    filter &= builder.Lte(fieldFilter.FieldName, val);
                                    break;
                                default:
                                    return null;
                            }
                            break;
                        case FieldType.Bool:
                            var val2 = DynamicCast<bool>(fieldFilter.FieldValue);
                            switch (fieldFilter.CompareType)
                            {
                                case CompareType.Equal:
                                    filter &= builder.Eq(fieldFilter.FieldName, val2);
                                    break;
                                case CompareType.GT:
                                    filter &= builder.Gt(fieldFilter.FieldName, val2);
                                    break;
                                case CompareType.GTE:
                                    filter &= builder.Gte(fieldFilter.FieldName, val2);
                                    break;
                                case CompareType.LT:
                                    filter &= builder.Lt(fieldFilter.FieldName, val2);
                                    break;
                                case CompareType.LTE:
                                    filter &= builder.Lte(fieldFilter.FieldName, val2);
                                    break;
                                default:
                                    return null;
                            }
                            break;
                        case FieldType.Double:
                            var val3 = DynamicCast<double>(fieldFilter.FieldValue);
                            switch (fieldFilter.CompareType)
                            {
                                case CompareType.Equal:
                                    filter &= builder.Eq(fieldFilter.FieldName, val3);
                                    break;
                                case CompareType.GT:
                                    filter &= builder.Gt(fieldFilter.FieldName, val3);
                                    break;
                                case CompareType.GTE:
                                    filter &= builder.Gte(fieldFilter.FieldName, val3);
                                    break;
                                case CompareType.LT:
                                    filter &= builder.Lt(fieldFilter.FieldName, val3);
                                    break;
                                case CompareType.LTE:
                                    filter &= builder.Lte(fieldFilter.FieldName, val3);
                                    break;
                                default:
                                    return null;
                            }
                            break;
                        case FieldType.Date:
                            var val4 = DynamicCast<DateTime>(fieldFilter.FieldValue);
                            switch (fieldFilter.CompareType)
                            {
                                case CompareType.Equal:
                                    filter &= builder.Eq(fieldFilter.FieldName, val4);
                                    break;
                                case CompareType.GT:
                                    filter &= builder.Gt(fieldFilter.FieldName, val4);
                                    break;
                                case CompareType.GTE:
                                    filter &= builder.Gte(fieldFilter.FieldName, val4);
                                    break;
                                case CompareType.LT:
                                    filter &= builder.Lt(fieldFilter.FieldName, val4);
                                    break;
                                case CompareType.LTE:
                                    filter &= builder.Lte(fieldFilter.FieldName, val4);
                                    break;
                                default:
                                    return null;
                            }
                            break;
                        case FieldType.ObjectId:

                            var val5 = ObjectId.Parse(fieldFilter.FieldValue.ToString());
                            switch (fieldFilter.CompareType)
                            {
                                case CompareType.Equal:
                                    filter &= builder.Eq(fieldFilter.FieldName, val5);
                                    break;
                                case CompareType.GT:
                                    filter &= builder.Gt(fieldFilter.FieldName, val5);
                                    break;
                                case CompareType.GTE:
                                    filter &= builder.Gte(fieldFilter.FieldName, val5);
                                    break;
                                case CompareType.LT:
                                    filter &= builder.Lt(fieldFilter.FieldName, val5);
                                    break;
                                case CompareType.LTE:
                                    filter &= builder.Lte(fieldFilter.FieldName, val5);
                                    break;
                                default:
                                    return null;
                            }
                            break;
                        case FieldType.ListObjectId:
                            var val6 = (List<ObjectId>) fieldFilter.FieldValue;
                            filter &= builder.In(fieldFilter.FieldName, val6);
                            break;
                        default:
                            return null;

                    }
                }

                return filter;
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "FilterBuilder", e.Message);
                logger.AddLog();
                return null;
            }
        }

        public SortDefinition<T> SortBuilder<T>(SortFilter sortFilter)
        {
            try
            {
                if (sortFilter == null)
                {
                    return new BsonDocumentSortDefinition<T>(new BsonDocument());
                }

                
                var builder = Builders<T>.Sort;

                var sort = sortFilter.SortType == SortType.Ascending
                    ? builder.Ascending(sortFilter.FieldName)
                    : builder.Descending(sortFilter.FieldName);
                
                return sort;
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "SortBuilder", e.Message);
                logger.AddLog();
                return null;
            }
        }

        public FilterDefinition<T> FilterBuilderOR<T>(List<FieldFilter> fieldFilters)
        {
            try
            {
                var builder = Builders<T>.Filter;
                FilterDefinition<T> filter = builder.Eq(fieldFilters[0].FieldName, (string)fieldFilters[0].FieldValue);

                for (var i = 1; i < fieldFilters.Count; i++)
                {
                    filter |= builder.Eq(fieldFilters[i].FieldName, (string)fieldFilters[i].FieldValue);
                }


                return filter;
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "FilterBuilderOR", e.Message);
                logger.AddLog();
                return null;
            }
        }
        
        private static T DynamicCast<T>(object value)
        {
            return (T)value;
        }
        
        public string DeleteMany(List<FieldFilter> filterkeys, IMongoCollection<BsonDocument> collection)
        {
            try
            {
                var filter = FilterBuilder<BsonDocument>(filterkeys);

                var delres = collection.DeleteManyAsync(filter);
                return delres.Result.DeletedCount.ToString();
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "DeleteMany", e.Message);
                logger.AddLog();
                return "-1";
            }
        }

        public static Task<FuncResult> DeleteMany(List<FieldFilter> filterkeys, CollectionNames collectionName)
        {
            try
            {
                var dbConnection = new DbConnection();
                var collection = dbConnection.GetMongoCollection(collectionName);

                var filter = dbConnection.FilterBuilder<BsonDocument>(filterkeys);

                var delres = collection.DeleteManyAsync(filter);
                return new Task<FuncResult>(() => new FuncResult(ResultEnum.Successfull));
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "DeleteMany", e.Message);
                logger.AddLog();
                return new Task<FuncResult>(() => new FuncResult(ResultEnum.Unsuccessfull, e.Message));
            }
        }

        public static Task<FuncResult> FastAddorUpdate(object odocument, CollectionNames collectionName,
            List<string> keyList = null)
        {
            try
            {
                var dbConnection = new DbConnection();
                

                var collection = dbConnection.GetMongoCollection(collectionName);
                var document = odocument.ToBsonDocument();

                var res = dbConnection.AddorUpdateAsync(document, collection, keyList);
                return res;
            }
            catch (Exception e)
            {
                var logger = new Logger(ObjectId.Empty, "DbConnection", "AddorUpdate", e.Message);
                logger.AddLog();
                return new Task<FuncResult>(() => new FuncResult(ResultEnum.Unsuccessfull, e.Message));
            }
        }
    }
    

    public enum CompareType
    {
        Equal = 0,
        GT = 1,
        GTE = 2,
        LT = 3,
        LTE = 4,
        IN = 5
    }

    public class FieldFilter
    {
        public string FieldName { get; set; }
        public object FieldValue { get; set; }
        public FieldType FieldType { get; set; }
        public CompareType CompareType { get; set; }

        public FieldFilter(string fieldName, object fieldValue, FieldType fieldType, CompareType compareType)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;
            FieldType = fieldType;
            CompareType = compareType;
        }
    }

    public class SortFilter
    {
        public string FieldName { get; set; }
        public SortType SortType { get; set; }

        public SortFilter(string fieldName, SortType sortType)
        {
            FieldName = fieldName;
            SortType = sortType;
        }
    }

    public class FieldUpdate
    {
        public string FieldName { get; set; }
        public object FieldValue { get; set; }
        public FieldType FieldType { get; set; }

        public FieldUpdate(string fieldName, object fieldValue, FieldType fieldType)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;
            FieldType = fieldType;
        }
    }

    public enum FieldType
    {
        String = 0,
        Double = 1,
        Bool = 2,
        Date = 3,
        ObjectId = 4,
        Integer = 5,
        ListObjectId = 6
    }

    public enum SortType
    {
        Ascending = 1,
        Descending = -1
    }
}
