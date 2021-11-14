namespace Api
{
    public class ListingViewModel
    {
        public string Category { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Description { get; set; }

        public ImageViewModel[] Images { get; set; }

        public string Version { get; set; }
    }
}
