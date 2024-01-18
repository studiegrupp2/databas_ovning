namespace databaset;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Core.Events;

public abstract class MovieService
{
    public abstract void Add(Movie movie);
    public abstract void AddMany(List<Movie> addNewMovie);
    public abstract Movie? Find(string Title);
    public abstract List<Movie> GetAll();
    public abstract void RemoveAll();

    public abstract void UpdateOne(string Title, double newRating);
}
public class Movie
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id { get; set; }
    public string Title { get; set; }
    public double Rating { get; set; }
    public int Length { get; set; }
    public List<string> Actors { get; set; }

    public Movie(string Title, double Rating, int Length, List<string> actors)
    {
        this.Title = Title;
        this.Rating = Rating;
        this.Length = Length;
        this.Actors = actors;
    }
}

public class LocalMovieService : MovieService
{
    public List<Movie> movies = new List<Movie>();

    public override void Add(Movie movie)
    {
        movies.Add(movie);
    }

    public override void AddMany(List<Movie> addNewMovie)
    {
        throw new NotImplementedException();
    }

    public override Movie? Find(string Title)
    {
        throw new NotImplementedException();
    }

    public override List<Movie> GetAll()
    {
        return this.movies;
    }

    public override void RemoveAll()
    {
        throw new NotImplementedException();
    }

    public override void UpdateOne(string Title, double newRating)
    {
        throw new NotImplementedException();
    }
}

public class DatabaseService : MovieService
{
    private MongoClient mongoClient;
    private IMongoDatabase database;
    private IMongoCollection<Movie> collection;

    public DatabaseService()
    {
        this.mongoClient = new MongoClient("mongodb://localhost:27017/databaset");
        this.database = this.mongoClient.GetDatabase("databaset");
        this.collection = this.database.GetCollection<Movie>("movies");
    }

    public override void Add(Movie movie)
    {
        this.collection.InsertOne(movie);
    }

    public override void AddMany(List<Movie> addNewMovies)
    {
        this.collection.InsertMany(addNewMovies);
    }

    public override Movie Find(string Title)
    {
        throw new NotImplementedException();
    }

    public override List<Movie> GetAll()
    {
        var filter = Builders<Movie>.Filter.Empty;
        return this.collection.Find(filter).ToList();
    }

    public override void RemoveAll()
    {
        var filter = Builders<Movie>.Filter.Empty;
        Movie movie = this.collection.Find(filter).First();

        this.collection.DeleteMany(filter);
    }

    public override void UpdateOne(string Title, double newRating)
    {
        var filter = Builders<Movie>.Filter.Eq(e => e.Title, Title);
        var update = Builders<Movie>.Update.Set(e => e.Rating, newRating);
        this.collection.UpdateOne(filter, update);
    }
}
