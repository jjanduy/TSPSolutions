using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSPSolutions.Algorithms.GA_TSP
{
    public class City
    {
        int id;
        int x;
        int y;
        int[] distacias;
                
        // Constructs a city at chosen x, y location
        public City(int id, int x, int y, int[] distacias)
        {
            this.x = x;
            this.y = y;
            this.id = id;
            this.distacias = distacias;
        }

        // Gets city's x coordinate
        public int getX()
        {
            return this.x;
        }

        // Gets city's y coordinate
        public int getY()
        {
            return this.y;
        }

        public int getId()
        {
            return this.id;
        }

        // Gets the distance to given city
        public double distanceTo(City city)
        {
            //int xDistance = getX() - city.getX();
            //int yDistance = getY() - city.getY();
            //double distance = Math.Sqrt((xDistance * xDistance) + (yDistance * yDistance));
            
            return distacias[city.id];
        }

        public String ToString()
        {
            return getId() + " (" + getX() + ", " + getY() + ")";
        }
    }
}
