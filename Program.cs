

class Country
{
    static void Main(string[] args)
    {
        var adjacentCountries = new Dictionary<string, List<string>>()
        {
           { "IN", new List<string> { "Pakistan", "China",} },
            {"US", new List<string> { "Canada", "Mexico" } },
            {"NZ",new List<string> {"Australia" } }
        };

        Console.WriteLine("Enter a country code ");
        string countryCode = Console.ReadLine();

        if (adjacentCountries.ContainsKey(countryCode)) 
        {
            var neightbours = adjacentCountries[countryCode];
            Console.WriteLine("Adjacent countries of this code are ");
            foreach(var neighbour in neightbours)
            {
                Console.WriteLine("Neighbour of country is "+neighbour);
            }
        }
        else
        {
            Console.WriteLine("Country was not found ");
        }

    }
}