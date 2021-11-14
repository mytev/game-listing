
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListingsController : ControllerBase
    {
        private const string DefaultImgUrl = "https://unity3d.com/files/images/ogimg.jpg";
        private readonly IListingRepository listingRepository;

        public ListingsController(IListingRepository listingRepository)
        {
            this.listingRepository = listingRepository;
        }

        /// <summary>
        /// Get all listings
        /// </summary>
        /// <returns>List of all listings</returns>
        /// <response code="200">Returns the listings</response>
        [HttpGet]
        public async Task<IEnumerable<ListingViewModel>> Get()
        {
            var listings = await this.listingRepository.GetAll();

            return listings.Select(MapToListingViewModel);
        }

        /// <summary>
        /// Creates a Game tile.
        /// </summary>
        /// <returns>A newly created listing</returns>
        /// <response code="200">Returns the newly created item</response>
        [HttpPost]
        public async Task<string> Create([FromBody] CreateListingBody body)
        {
            var listing = MapToListing(body);
            await this.listingRepository.Create(listing);

            return "Created";
        }

        /// <summary>
        /// Delete existing listings and repopulate test data
        /// </summary>
        /// <returns>A newly created listing</returns>
        /// <response code="200">Returns success string</response>
        [HttpGet("reset-data")]
        public async Task<string> ResetTestData()
        {
            await DeleteExistingListings();
            await PopulateTestData();

            return "Data reset completed";
        }

        private static ListingViewModel MapToListingViewModel(Listing listing)
        {
            return new ListingViewModel
            {
                Category = listing.Category,
                Description = listing.Description,
                Images = listing.Images.Select(MapToImageViewModel).ToArray(),
                Subtitle = listing.SubTitle,
                Title = listing.Title,
                Version = listing.Version
            };
        }

        private static ImageViewModel MapToImageViewModel(Image img)
        {
            return new ImageViewModel { Id = img.Id, Type = img.Type, Url = img.Url };
        }

        private static Listing MapToListing(CreateListingBody body)
        {
            var imageUrl = string.IsNullOrWhiteSpace(body.ImageUrl) ? DefaultImgUrl : body.ImageUrl;
            var image = new Image
            {
                Type = ImageType.Background,
                Url = imageUrl
            };
            var listing = new Listing
            {
                Title = body.Title,
                SubTitle = body.SubTitle,
                Description = body.Description,
                Category = body.Category,
                Images = new List<Image> { image },
                Author = string.Empty,
                Duration = 0,
                IsDownloadable = false,
                IsStreamable = true,
                ReplayBundleUrlJson = string.Empty,
                Tags = new string[] { },
                Type = ListingType.Standard,
                Version = "1.0"
            };
            return listing;
        }

        private async Task DeleteExistingListings()
        {
            var listings = await this.listingRepository.GetAll();
            var deletionTasks = listings.Select(x => x.Id).Select(this.listingRepository.Remove);
            await Task.WhenAll(deletionTasks);
        }

        private async Task PopulateTestData()
        {
            var testDataFilePath = $"{Directory.GetCurrentDirectory()}/Features/Listings/UITest.json";
            var fileStream = new FileStream(testDataFilePath, FileMode.Open, FileAccess.Read);
            var testData = await JsonSerializer.DeserializeAsync<TestData>(
                fileStream,
                new JsonSerializerOptions {
                    PropertyNameCaseInsensitive = true
                }
            );
            var creationTasks = testData.Listings.Select(this.listingRepository.Create);
            await Task.WhenAll(creationTasks);
        }
    }
}
