namespace Books.API.Entities
{
    public class Story
    {
        public string Title { get; }
        public string Uri { get; }

        public string PostedBy { get; }

        public DateTime Time { get; }

        public int Score { get; set; }

        public int CommentCount { get; set; }

        public Story(string title, string uri, string postedBy, double utxTime, int score, int commentCount)
        {
            Title = title;
            Uri = uri;
            PostedBy = postedBy;

            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(utxTime).ToLocalTime();
            Time = dateTime;

            Score = score;
            CommentCount = commentCount;
        }
    }
}
