using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Books.API.Entities;

namespace Books.API.Models.External;

public class StoryDto
{
    public string By { get; set; }
    public int Descendants { get; set; }

    public int ID { get; set; }

    public int[] Kids { get; set; }

    public int Score { get; set; }

    public double Time { get; set; }

    

    public string? Title { get; set; }

    public string? Type { get; set; }

    public string? Url { get; set; }

    

}