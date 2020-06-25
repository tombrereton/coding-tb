using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace ConstructionLine.CodingChallenge.Tests
{
    [TestFixture]
    public class SearchCountTests : SearchEngineTestsBase
    {
        [Test]
        public void ShouldReturnCorrectSmallCount()
        {
            var smallRedShirt = new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red);
            var shirts = new List<Shirt>
            {
                smallRedShirt,
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Black),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions {Sizes = new List<Size> {Size.Small}};

            var results = searchEngine.Search(searchOptions);

            var expectedSizeCount = new SizeCount {Count = 1, Size = Size.Small};
            results.SizeCounts.Single().Should().Be(expectedSizeCount);
        }

        [Test]
        public void ShouldHaveSameSizeCounts()
        {
            var expectedSizeCount = new SizeCount {Count = 1, Size = Size.Small};
            var expectedSizeCount2 = new SizeCount {Count = 1, Size = Size.Small};

            expectedSizeCount.Should().Be(expectedSizeCount2);
        }
        
        [Test]
        public void ShouldHaveDifferentSizeCounts()
        {
            var expectedSizeCount = new SizeCount {Count = 1, Size = Size.Small};
            var expectedSizeCount2 = new SizeCount {Count = 1, Size = Size.Medium};

            expectedSizeCount.Should().NotBe(expectedSizeCount2);
        }
        
        [Test]
        public void ShouldHaveSameColorCounts()
        {
            var expectedColorCount = new ColorCount {Count = 1, Color = Color.Red};
            var expectedColorCount2 = new ColorCount {Count = 1, Color = Color.Red};

            expectedColorCount.Should().Be(expectedColorCount2);
        }
        
        [Test]
        public void ShouldHaveDifferentColorCounts()
        {
            var expectedColorCount = new ColorCount {Count = 1, Color = Color.Red};
            var expectedColorCount2 = new ColorCount {Count = 1, Color = Color.Black};

            expectedColorCount.Should().NotBe(expectedColorCount2);
        }
    }
}