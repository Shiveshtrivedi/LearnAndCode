using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

class Program
{
    static string GetUserInput()
    {
        return Console.ReadLine();
    }

    static (int startPost,int endPost) ParseRange(string range)
    {
        var rangeParts = range.Split('-');
        int startPost = int.Parse(rangeParts[0])-1;
        int endPost = int.Parse(rangeParts[1]) - startPost + 1;
        return (startPost,endPost);
    }

    static string BuildApiUrl(string blogName,int startPost,int endPost)
    {
        string apiUrl = $"https://{blogName}.tumblr.com/api/read/json?type=photo&start={startPost}&num={endPost}";
        return apiUrl;
    }

    static async Task<string> GetApiResponse(string apiUrl)
    {
        var client = new HttpClient();
        return await client.GetStringAsync(apiUrl);
    }

    static string CleanApiResponse(string response)
    {
        const string prefix = "var tumblr_api_read = ";
        if (response.StartsWith(prefix))
        {
            response = response.Substring(prefix.Length);
        }
        else
        {
            Console.WriteLine("Unexpected response format: " + response);
            return null;
        }

        response = response.TrimEnd(';').Trim();

        return response;
    }

    static JObject ParseJsonResponse(string response)
    {
        try
        {
            return JObject.Parse(response);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Error Parsing Json " + exception.Message);
            return null;
        }
    }

    static (string title,string description,string name,int numberOfPosts) GetBlogInformation(JObject jsonResponse)
    {

        var blog = jsonResponse["response"]["blog"];
        string title = blog["title"].ToString();
        string description = blog["description"].ToString();
        string name = blog["name"].ToString();
        int numberOfPosts = int.Parse(blog["posts"].ToString());

        return (title,description,name,numberOfPosts);
    }

    static void DisplayBlogInformation(string title, string description, string name, int numOfPosts)
    {
        Console.WriteLine("\nBlog Information:");
        Console.WriteLine($"Title: {title}");
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Description: {description}");
        Console.WriteLine($"Number of posts: {numOfPosts}");
    }

    static void DisplayImageUrls(JObject jsonResponse)
    {
        var posts = jsonResponse["response"]["posts"];
        int postIndex = 1;

        foreach (var post in posts)
        {
            if (post["type"].ToString() == "photo")
            {
                var photos = post["photos"];

                foreach (var photo in photos)
                {
                    string imageUrl = photo["alt_sizes"][3]["url"].ToString(); 
                    Console.WriteLine($"{postIndex}. {imageUrl}");
                }
            }
            postIndex++;
        }
    }
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter the tumble blog name ");
        string blogName = GetUserInput();
        Console.Write("Enter the post range (ex 1-10): ");
        string startPostToEndPost = GetUserInput();
        var (startPost, endPost) = ParseRange(startPostToEndPost);

        string apiUrl = BuildApiUrl(blogName,startPost,endPost);
        
        var response = await GetApiResponse(apiUrl);

        response = CleanApiResponse(response);


        if (response == null) 
            return;

       JObject jsonResponse = ParseJsonResponse(response);
       if (jsonResponse == null) 
             return;

       var (title, description, name, numOfPosts) = GetBlogInformation(jsonResponse);

        DisplayBlogInformation(title, description, name, numOfPosts);

        DisplayImageUrls(jsonResponse);
    }
}
