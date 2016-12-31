using System;
using System.Collections.Generic;
using System.Linq;
using Beerhall_bis.Models.Domain;

namespace Beerhall_bis.Models.Domain
{
    public class Brewer
    {
        #region Fields
        private string _name;
        private int? _turnover;
        #endregion

        #region Properties
        public int BrewerId { get; set; }

        public string Name
        {
            get { return _name; }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Naam is verplicht");
                if (value.Length > 50)
                    throw new ArgumentException("Naam mag niet groter dan 50 tekens zijn");
                _name = value;
            }
        }

        public string Street { get; set; }

        public int? Turnover
        {
            get { return _turnover; }
            set
            {
                if (value.GetValueOrDefault() < 0)
                    throw new ArgumentException("Omzet moet groter of gelijk aan 0 zijn");
                _turnover = value;

            }
        }

        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public DateTime? DateEstablished { get; set; }
        public int NrOfBeers => Beers.Count;

        public ICollection<Beer> Beers { get; private set; }
        public Location Location { get; set; }
        #endregion

        #region Constructors
        protected Brewer()
        {
            Beers = new HashSet<Beer>();
        }

        public Brewer(string name) : this()
        {
            Name = name;
        }

       

        public Brewer(string name, Location location, string street) : this()
        {
            Name = name;
            Location = location;
            Street = street;
        }
        #endregion

        #region Methods
        public Beer AddBeer(string name, double? alcoholByVolume = null,
           string description = null)
        {
            if (name != null && Beers.FirstOrDefault(b => b.Name == name) != null)
                throw new ArgumentException("Brouwer {Name} heeft reeds een bier met die smaak");

            Beer bier = new Beer(name)
            {
                AlcoholByVolume = alcoholByVolume,
                Description = description
            };
            Beers.Add(bier);
            return bier;
        }

        public void DeleteBier(Beer beer)
        {
            if (!Beers.Contains(beer))
                throw new ArgumentException("Brouwer {Name} heeft dat bier niet");
            Beers.Remove(beer);
        }

        public Beer GetBy(int beerId)
        {
            return Beers.FirstOrDefault(b => b.BeerId == beerId);
        }

        public Beer GetBy(string name)
        {
            return Beers.FirstOrDefault(b => b.Name == name);
        } 
        #endregion

    }
}
