using System;
using System.Collections.Generic;
using System.Linq;

namespace ConstructionLine.CodingChallenge
{
    public class SearchEngine
    {
        private readonly List<Shirt> _shirts;
        private readonly Dictionary<Size,List<Shirt>> _shirtBySize;
        private readonly Dictionary<Color,List<Shirt>> _shirtsByColor;

        public SearchEngine(List<Shirt> shirts)
        {
            _shirts = shirts;
            _shirtBySize = new Dictionary<Size, List<Shirt>>();
            _shirtsByColor = new Dictionary<Color, List<Shirt>>();
            
            foreach (var shirt in shirts)
            {
                if (!_shirtBySize.TryAdd(shirt.Size, new List<Shirt> {shirt}))
                {
                    var existingList = _shirtBySize[shirt.Size];
                    existingList.Add(shirt);
                    _shirtBySize[shirt.Size] = existingList;
                }
                if (!_shirtsByColor.TryAdd(shirt.Color, new List<Shirt> {shirt}))
                {
                    var existingList = _shirtsByColor[shirt.Color];
                    existingList.Add(shirt);
                    _shirtsByColor[shirt.Color] = existingList;
                }
            }
        }


        public SearchResults Search(SearchOptions options)
        {
            // shirts
            var shirtsBySize = new List<Shirt>();
            var shirtsByColor = new List<Shirt>();
            foreach (var size in options.Sizes)
            {
                shirtsBySize.AddRange(_shirtBySize[size]);
            }
            foreach (var color in options.Colors)
            {
                shirtsByColor.AddRange(_shirtsByColor[color]);
            }

            var shirts = shirtsBySize;
            if (!shirtsBySize.Any())
            {
                shirts = shirtsByColor;
            }
            if (shirtsBySize.Any() && shirtsByColor.Any())
            {
                shirts = shirtsBySize.Intersect(shirtsByColor).ToList();
            }
            
            // size counts
            var sizeCounts = new List<SizeCount>();
            foreach (var size in options.Sizes)
            {
                var shirtsByOneSize  = _shirtBySize[size];
                var filteredShirtsByOneSize = shirtsByOneSize.Intersect(shirtsByColor);
                var sizeCount = new SizeCount{Count = filteredShirtsByOneSize.Count(), Size = size};
                sizeCounts.Add(sizeCount);
            }
            
            // color counts
            var colorCounts = new List<ColorCount>();
            foreach (var color in options.Colors)
            {
                var shirtsByOneColor = _shirtsByColor[color];
                var filteredShirtsByOneColor = shirtsByOneColor.Intersect(shirtsBySize);
                var colorCount = new ColorCount{Count = filteredShirtsByOneColor.Count(), Color = color};
                colorCounts.Add(colorCount);
            }
            
            return new SearchResults
            {
               Shirts =  shirts,
               SizeCounts = sizeCounts,
               ColorCounts = colorCounts
            };
        }
    }
}