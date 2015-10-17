using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPSolutions.Algorithms.GA_TSP
{
    public class Tour
    {
        private List<City> tour = new List<City>();
        Random rng = new Random();
        // Cache
        private double fitness = 0;
        private int distance = 0;

        // Constructs a blank tour
        public Tour()
        {
            for (int i = 0; i < TourManager.numberOfCities(); i++)
            {
                tour.Add(null);
            }
        }

        public Tour(List<City> tour)
        {
            this.tour = tour;
        }

        // Creates a random individual
        public void generateIndividual()
        {
            // Loop through all our destination cities and add them to our tour
            for (int cityIndex = 0; cityIndex < TourManager.numberOfCities(); cityIndex++)
            {
                setCity(cityIndex, TourManager.getCity(cityIndex));
            }
            // Randomly reorder the tour
            Shuffle(tour);
            //Collections.shuffle(tour);
        }

       

        public void Shuffle(List<City> list)
        {            
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                City value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        // Gets a city from the tour
        public City getCity(int tourPosition)
        {
            return (City)tour[tourPosition];
        }

        // Sets a city in a certain position within a tour
        public void setCity(int tourPosition, City city)
        {
            tour[tourPosition] = city;
            // If the tours been altered we need to reset the fitness and distance
            fitness = 0;
            distance = 0;
        }

        // Gets the tours fitness
        public double getFitness()
        {
            if (fitness == 0)
            {
                fitness = 1 / (double)getDistance();
            }
            return fitness;
        }

        // Gets the total distance of the tour
        public int getDistance()
        {
            if (distance == 0)
            {
                int tourDistance = 0;
                // Loop through our tour's cities
                for (int cityIndex = 0; cityIndex < tourSize(); cityIndex++)
                {
                    // Get city we're travelling from
                    City fromCity = getCity(cityIndex);
                    // City we're travelling to
                    City destinationCity;
                    // Check we're not on our tour's last city, if we are set our
                    // tour's final destination city to our starting city
                    if (cityIndex + 1 < tourSize())
                    {
                        destinationCity = getCity(cityIndex + 1);
                    }
                    else
                    {
                        destinationCity = getCity(0);
                    }
                    // Get the distance between the two cities
                    tourDistance += (int)fromCity.distanceTo(destinationCity);
                }
                distance = tourDistance;
            }
            return distance;
        }

        // Get number of cities on our tour
        public int tourSize()
        {
            return tour.Count;
        }

        // Check if the tour contains a city
        public bool containsCity(City city)
        {
            return tour.Contains(city);
        }

        public String ToString()
        {
            String geneString = "|";
            for (int i = 0; i < tourSize(); i++)
            {
                geneString += getCity(i).ToString() + "|";
            }
            return geneString;
        }
    }
}
