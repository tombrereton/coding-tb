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
                _shirtBySize.TryAdd(shirt.Size, new List<Shirt> {shirt});
                _shirtsByColor.TryAdd(shirt.Color, new List<Shirt> {shirt});
            }
        }


        public SearchResults Search(SearchOptions options)
        {
            var shirtsBySize = new List<Shirt>();
            var shirtsByColor = new List<Shirt>();
            var shirts = _shirtsByColor[options.Colors.First()];
            
            
            return new SearchResults
            {
               Shirts =  shirts
            };
        }
    }
}