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
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
                {Sizes = new List<Size> {Size.Small}, Colors = new List<Color> {Color.Red}};

            var results = searchEngine.Search(searchOptions);

            var sizeCounts = new List<SizeCount>()
            {
                new SizeCount {Count = 1, Size = Size.Small},
                new SizeCount {Count = 0, Size = Size.Medium},
                new SizeCount {Count = 0, Size = Size.Large}
            };
            results.SizeCounts.Should().BeEquivalentTo(sizeCounts);
        }

        [Test]
        public void ShouldReturnCorrectMediumCount()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions
                {Sizes = new List<Size> {Size.Medium}, Colors = new List<Color> {Color.Red}};

            var results = searchEngine.Search(searchOptions);
            var sizeCounts = new List<SizeCount>()
            {
                new SizeCount {Count = 0, Size = Size.Small},
                new SizeCount {Count = 1, Size = Size.Medium},
                new SizeCount {Count = 0, Size = Size.Large}
            };
            results.SizeCounts.Should().BeEquivalentTo(sizeCounts);
        }

        [Test]
        public void ShouldReturnCorrectSizeCountsWithOnlyColorOption()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Red - Medium", Size.Medium, Color.Red),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions {Colors = new List<Color> {Color.Red}};

            var results = searchEngine.Search(searchOptions);
            var sizeCounts = new List<SizeCount>()
            {
                new SizeCount {Count = 1, Size = Size.Small},
                new SizeCount {Count = 1, Size = Size.Medium},
                new SizeCount {Count = 0, Size = Size.Large}
            };
            results.SizeCounts.Should().BeEquivalentTo(sizeCounts);
        }
        
        [Test]
        public void ShouldReturnCorrectColorCountsWithOnlySizeOption()
        {
            var shirts = new List<Shirt>
            {
                new Shirt(Guid.NewGuid(), "Red - Small", Size.Small, Color.Red),
                new Shirt(Guid.NewGuid(), "Black - Small", Size.Small, Color.Black),
            };
            var searchEngine = new SearchEngine(shirts);
            var searchOptions = new SearchOptions {Sizes = new List<Size> {Size.Small}};

            var results = searchEngine.Search(searchOptions);
            var colorCounts = new List<ColorCount>()
            {
                new ColorCount {Count = 1, Color = Color.Black},
                new ColorCount {Count = 1, Color = Color.Red},
                new ColorCount {Count = 0, Color = Color.Blue},
                new ColorCount {Count = 0, Color = Color.White},
                new ColorCount {Count = 0, Color = Color.Yellow},
            };
            results.ColorCounts.Should().BeEquivalentTo(colorCounts);
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