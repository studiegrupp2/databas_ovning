using System.Security.AccessControl;

namespace databaset;


class Program
{
    private static MovieService databaseService = new DatabaseService();
    static void Main(string[] args)
    {
        //databaseService.RemoveAll();

        // List<string> actors = new List<string>();
        // actors.Add("Bamse");
        // actors.Add("Lilleskutt");

        // databaseService.Add(
        //     new Movie("film2", 4.7, 3, hej)
        // );

        //Övning 4 (insertMany)
        // List<Movie> testList = new List<Movie>();
        // testList.Add(new Movie("film3", 2.0, 3, actors));
        // testList.Add(new Movie("film4", 5.0, 3, actors));
        // databaseService.AddMany(testList);

        //Övning 9 (ändra rating på en av filmerna)
        databaseService.UpdateOne("titanic", 5.0);

        foreach (Movie movie in databaseService.GetAll())
        {
            Console.WriteLine($"Title: {movie.Title}, rating: {movie.Rating}");
        }
    }
}

