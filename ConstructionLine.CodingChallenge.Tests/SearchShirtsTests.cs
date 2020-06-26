using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchShirtsTests : SearchEngineTestsBase
    {
        [Test]
        public void ShouldReturnSmallRedShirt()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Black),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions {Colors = new List<Color> {Color.Red}};

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
        }

        [Test]
        public void ShouldReturnAllRedShirts()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions {Colors = new List<Color> {Color.Red}};

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
        }

        [Test]
        public void ShouldReturnAllRedAndBlackShirts()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions {Colors = new List<Color> {Color.Red, Color.Black}};

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
        }

        [Test]
        public void ShouldReturnAllSmallShirts()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red)
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions {Sizes = new List<Size> {Size.Small}};

            var results = searchEngine.Search(searchOptions);

            AssertResults(results.Shirts, searchOptions);
        }
        
        [Test]
        public void ShouldThrowArgumentExceptionForNullSearchOption()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
            };
            
            var searchEngine = new SearchEngine(shirts);
            SearchOptions searchOptions = null;

            var exception = Assert.Throws<ArgumentException>(() => searchEngine.Search(searchOptions));
            Assert.That(exception.Message, Is.EqualTo("SearchOptions cannot be null"));
        }
        
        [Test]
        public void ShouldThrowArgumentExceptionForColorOption()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
            };
            
            var searchEngine = new SearchEngine(shirts);
            SearchOptions searchOptions = new SearchOptions{Colors = null};

            var exception = Assert.Throws<ArgumentException>(() => searchEngine.Search(searchOptions));
            Assert.That(exception.Message, Is.EqualTo("Colors cannot be null, omit or provide a value"));
        }
        
        [Test]
        public void ShouldThrowArgumentExceptionForSizeOption()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
            };
            
            var searchEngine = new SearchEngine(shirts);
            SearchOptions searchOptions = new SearchOptions{Sizes = null};

            var exception = Assert.Throws<ArgumentException>(() => searchEngine.Search(searchOptions));
            Assert.That(exception.Message, Is.EqualTo("Sizes cannot be null, omit or provide a value"));
        }

        [Test]
        public void ShouldBeEqualShirts()
        {
            var newGuid = Guid.NewGuid();
            var smallRedShirt = new Shirt(newGuid, "Red - Small", Size.Small, Color.Red);
            var smallRedShirt2 = new Shirt(newGuid, "Red - Small", Size.Small, Color.Red);

            smallRedShirt.Should().Be(smallRedShirt2);
        }

        [Test]
        public void ShouldNotBeEqualShirts()
        {
            var smallRedShirt = new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red);
            var smallRedShirtDifferent = new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red);

            smallRedShirt.Should().NotBe(smallRedShirtDifferent);
        }
    }
}