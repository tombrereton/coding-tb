using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly Dictionary<Size, List<Shirt>> _shirtBySize;
        private readonly Dictionary<Color, List<Shirt>> _shirtsByColor;

        public SearchEngine(List<Shirt> shirts)
        {
            var shirtsGroupedBySize = shirts.ToLookup(shirt => shirt.Size);
            var shirtsGroupedByColor = shirts.ToLookup(shirt => shirt.Color);
            _shirtBySize = shirtsGroupedBySize.ToDictionary(group => group.Key, group => group.ToList());
            _shirtsByColor = shirtsGroupedByColor.ToDictionary(group => group.Key, group => group.ToList());
        }

        public SearchResults Search(SearchOptions options)
        {
            var shirtsBySize = GetShirtsBySize(options.Sizes);
            var shirtsByColor = GetShirtByColor(options.Colors);
            var filteredShirts = GetShirtsBySizeAndColor(shirtsBySize, shirtsByColor);

            var sizeCounts = GetSizeCounts(filteredShirts);
            var colorCounts = GetColorCounts(filteredShirts);

            return new SearchResults
            {
                Shirts = filteredShirts,
                SizeCounts = sizeCounts,
                ColorCounts = colorCounts
            };
        }

        private List<Shirt> GetShirtsBySize(IEnumerable<Size> sizes)
        {
            var shirtsBySize = new List<Shirt>();
            foreach (var size in sizes)
            {
                shirtsBySize.AddRange(_shirtBySize[size]);
            }

            return shirtsBySize;
        }

        private List<Shirt> GetShirtByColor(IEnumerable<Color> colors)
        {
            var shirtsByColor = new List<Shirt>();
            foreach (var color in colors)
            {
                shirtsByColor.AddRange(_shirtsByColor[color]);
            }

            return shirtsByColor;
        }

        private static List<Shirt> GetShirtsBySizeAndColor(List<Shirt> shirtsBySize, List<Shirt> shirtsByColor)
        {
            var shirts = shirtsBySize.Any() ? shirtsBySize : shirtsByColor;
            if (shirtsBySize.Any() && shirtsByColor.Any())
            {
                shirts = shirtsBySize.Intersect(shirtsByColor).ToList();
            }

            return shirts;
        }

        private List<SizeCount> GetSizeCounts(IEnumerable<Shirt> shirts)
        {
            var shirtsGroupedBySize = shirts.ToLookup(shirt => shirt.Size);
            var sizeCounts = Size.All.ToDictionary(size => size, size => new SizeCount {Size = size, Count = 0});
            foreach (var sizeGroup in shirtsGroupedBySize)
            {
                sizeCounts[sizeGroup.Key] = new SizeCount {Size = sizeGroup.Key, Count = sizeGroup.Count()};
            }

            return sizeCounts.Values.ToList();
        }

        private static List<ColorCount> GetColorCounts(IEnumerable<Shirt> shirts)
        {
            var shirtsGroupedByColor = shirts.ToLookup(shirt => shirt.Color);
            var colorCounts =
                Color.All.ToDictionary(color => color, color => new ColorCount {Color = color, Count = 0});
            foreach (var colorGroup in shirtsGroupedByColor)
            {
                colorCounts[colorGroup.Key] = new ColorCount {Color = colorGroup.Key, Count = colorGroup.Count()};
            }

            return colorCounts.Values.ToList();
        }
    }
}