using System;

namespace ConstructionLine.CodingChallenge
{
    public class Shirt
    {
        public Guid Id { get; }

        public string Name { get; }

        public Size Size { get; set; }

        public Color Color { get; set; }

        public Shirt(Guid id, string name, Size size, Color color)
        {
            Id = id;
            Name = name;
            Size = size;
            Color = color;
        }

        protected bool Equals(Shirt other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Shirt) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(Name, StringComparer.InvariantCulture);
            hashCode.Add(Size);
            hashCode.Add(Color);
            return hashCode.ToHashCode();
        }
    }
}