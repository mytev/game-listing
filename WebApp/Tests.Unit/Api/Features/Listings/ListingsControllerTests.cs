using Api;
using Api.Controllers;
using FakeItEasy;
using FluentAssertions;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Unit
{
    public class ListingsControllerTests
    {
        private readonly ListingsController _sut;
        private readonly IListingRepository _listingRepository;

        public ListingsControllerTests()
        {
            this._listingRepository = A.Fake<IListingRepository>();
            this._sut = new ListingsController(this._listingRepository);
        }

        [Fact]
        public async Task GivenCreateListingBody_WhenCreate_ThenCallCreate()
        {
            // Given
            var body = A.Fake<CreateListingBody>();

            // When
            var res = await this._sut.Create(body);

            // Then
            A.CallTo(() => this._listingRepository.Create(A<Listing>.That.Matches(x => x.Title == body.Title)))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task GivenRepositoryFailure_WhenCreate_ThenThrows()
        {
            // Given
            var body = A.Fake<CreateListingBody>();
            var exception = A.Fake<Exception>();
            A.CallTo(() => this._listingRepository.Create(A<Listing>._))
                .ThrowsAsync(exception);

            // When
            Func<Task<string>> action = () => this._sut.Create(body);

            // Then
            await action.Should().ThrowAsync<Exception>();
        }

        [Fact]
        public async Task GivenListings_WhenGet_ThenReturnsListingViewModels()
        {
            // Given
            var listings = A.Fake<List<Listing>>();
            A.CallTo(() => this._listingRepository.GetAll())
                .Returns(listings);

            // When
            var res = await this._sut.Get();

            // Then
            res.Should().BeEquivalentTo(
                listings.Select(x => new ListingViewModel
                {
                    Category = x.Category,
                    Description = x.Description,
                    Images = x.Images.Select(img => new ImageViewModel { Id = img.Id, Type = img.Type, Url = img.Url }).ToArray(),
                    Title = x.Title,
                    Subtitle = x.SubTitle,
                    Version = x.Version
                }));
        }
    }
}
