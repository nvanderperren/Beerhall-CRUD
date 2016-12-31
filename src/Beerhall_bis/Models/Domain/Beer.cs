using System;

namespace Beerhall_bis.Models.Domain
{
    public class Beer
    {
        private string _name;
        private double? _volume;
        private decimal _price;

        public int BeerId { get; set; }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException
                        ("Bier moet een naam hebben");
                _name = value;
            }
        }

        public string Description { get; set; }

        public double? AlcoholByVolume
        {
            get { return _volume; }
            set
            {
                if (value.GetValueOrDefault() < 0)
                    throw new ArgumentException("volume kan niet lager dan 0 zijn");
                _volume = value;
            }
        }

        public bool AlcoholKnown => AlcoholByVolume.HasValue;

        public decimal Price
        {
            get { return _price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("prijs kan niet lager dan 0 zijn");
                _price = value;
            }
        }

        protected Beer() { }

        public Beer(string name) : this()
        {
            Name = name;
        }
    }
}
