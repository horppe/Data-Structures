using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Search_Engine
{
    class DataBasePreparationCode
    {
        //Random rand = new Random(0);
        //List<string> colours = new List<string>() { "Red", "Blue", "Pink", "Black", "White" };
        //StreamWriter writer = new StreamWriter("Database.txt");
        //    using (writer)
        //    {
        //        for (int i = 0; i<lines.Count; i++)
        //        {
        //            StringBuilder str = new StringBuilder();
        //            str.Append(lines[i]);
        //            str.Append(string.Format("\t{0}", colours[rand.Next(colours.Count)]));
        //            writer.WriteLine(str.ToString());
        //        }
        //    }
    }

    class Program
    {
        static SearchEngine searchEngine;
        static void Main(string[] args)
        {
            searchEngine = new SearchEngine();
            FillEngine();


            Console.ReadKey();
        }

        static void FillEngine()
        {
            List<string> lines = new List<string>();
            StreamReader reader = new StreamReader("Database.txt");
            using (reader)
            {
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine().Trim());
                }
            }
            foreach (var line in lines)
            {
                string[] columns = line.Split('\t');
                string price = columns[3].TrimStart('$');
                string[] tempPrice = price.Split(',');
                price = tempPrice[0] + tempPrice[1];
                searchEngine.AddCar(columns[0], columns[1], columns[4], int.Parse(columns[2]), int.Parse(price));
            }
        }
    }

    class SearchEngine
    {

        class YearComparer: IComparer<Car>
        {
            public int Compare(Car carOne, Car carTwo)
            {
                return carOne.year.CompareTo(carTwo.year);
            }
        }
        class PriceComparer: IComparer<Car>
        {
            public int Compare(Car carOne, Car carTwo)
            {
                return carOne.price.CompareTo(carTwo.price);
            }
        }


        public class Car
        {
            public string brand;
            public string model;
            public string color;
            public int year;
            public int price;
            public Car(string brand, string model, string color, int year, int price)
            {
                this.brand = brand;
                this.model = model;
                this.color = color;
                this.year = year;
                this.price = price;
            }

            public override int GetHashCode()
            {
                return brand.GetHashCode() + model.GetHashCode() + color.GetHashCode() +
                    year.GetHashCode() + price.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                Car otherCar = (Car)obj;
                int state = 0;
                if (brand == otherCar.brand)
                {
                    state++;
                }
                if (model == otherCar.model)
                {
                    state++;
                }
                if (color == otherCar.color)
                {
                    state++;
                }
                if (year == otherCar.year)
                {
                    state++;
                }
                if (price == otherCar.price)
                {
                    state++;
                }

                if (state == 5)
                {
                    return true;
                }

                return false;
            }            
        }

        Dictionary<string, HashSet<Car>> carBrands;
        Dictionary<string, HashSet<Car>> carModels;
        Dictionary<string, HashSet<Car>> carColors;
        List<Car> carYears;
        List<Car> carPrices;
        
        public SearchEngine()
        {
            carBrands = new Dictionary<string, HashSet<Car>>();
            carModels = new Dictionary<string, HashSet<Car>>();
            carColors = new Dictionary<string, HashSet<Car>>();
            carYears = new List<Car>();
            carPrices = new List<Car>();
        }

        public void AddCar(string brand, string model, string color, int year, int price)
        {
            Car car = new Car(brand, model, color, year, price);
            HashSet<Car> set = new HashSet<Car>();
            set.Add(car);
            HashSet<Car> tempSet;
            if (!carBrands.TryGetValue(car.brand, out tempSet))
            {
                carBrands.Add(car.brand, set);
            }
            else
            {
                tempSet.Add(car);
            }
            if (!carModels.TryGetValue(car.model, out tempSet))
            {
                carModels.Add(car.model, set);
            }
            else
            {
                tempSet.Add(car);
            }
            if (!carColors.TryGetValue(car.color, out tempSet))
            {
                carColors.Add(car.color, set);
            }
            else
            {
                tempSet.Add(car);
            }
            carYears.Add(car);
            carPrices.Add(car);
            
        }

        public List<Car> Search(string brand)
        {
            return Search(brand, null, null, 0, 0);
        }
        public List<Car> Search(string brand, string model)
        {
            return Search(brand, model, null, 0, 0);
        }
        public List<Car> Search(string brand, string model, string color)
        {
            return Search(brand, model, color, 0, 0);
        }
        public List<Car> Search(string brand, string model, string color, int year)
        {
            return Search(brand, model, color, year, 0);
        }
        public List<Car> Search(string brand, string model, string color, int year, int price)
        {
            List<HashSet<Car>> cars = new List<HashSet<Car>>();
            if (brand != null)
            {
                cars.Add(SearchBrand(brand));
            }
            if (model != null)
            {
                cars.Add(SearchModel(model));
            }
            if (color != null)
            {
                cars.Add(SearchColor(color));
            }
            if (year != 0)
            {
                cars.Add(SearchYear(year));
            }
            if (price != 0)
            {
                cars.Add(SearchPrice(price));
            }

            HashSet<Car> intersect = new HashSet<Car>(cars[0]);
            for (int i = 1; i < cars.Count; i++)
            {
                intersect.IntersectWith(cars[i]);
            }
            return intersect.ToList();
        }

        public HashSet<Car> SearchBrand(string brand)
        {
            return carBrands[brand];
        }
        public HashSet<Car> SearchModel(string model)
        {
            return carModels[model];
        }
        public HashSet<Car> SearchColor(string color)
        {
            return carColors[color];
        }

        public HashSet<Car> SearchYear(int year)
        {
            List<Car> tempCars = new List<Car>(carYears);
            tempCars.Sort(new YearComparer());
            int p = tempCars.Count;
            HashSet<Car> set = new HashSet<Car>();
            for (int i = 0; i < p; i++)
            {
                int mid = (i + p) / 2;
                if (tempCars[mid].year < year)
                {
                    i = mid + 1;
                }
                else if (tempCars[mid].year > year)
                {
                    p = mid - 1;
                }

                if (tempCars[mid].year == year)
                {
                    set.Add(tempCars[mid]);
                }
            }
            return set;

            //tempCars.BinarySearch(new Car("", "", "", year, 0), new YearComparer());
        }
        public List<Car> SearchYear(int startYear, int endYear)
        {
            List<Car> tempCars = new List<Car>(carYears);
            tempCars.Sort(new YearComparer());
            int p = tempCars.Count;
            List<Car> result = new List<Car>();
            for (int i = 0; i < p; i++)
            {
                int mid = (i + p) / 2;
                if (tempCars[mid].year < startYear)
                {
                    i = mid + 1;
                }

                if (tempCars[mid].year > startYear)
                {
                    p = mid - 1;
                }

                if (tempCars[mid].year >= startYear && tempCars[mid].year <= endYear)
                {
                    result.Add(tempCars[mid]);
                }
            }
            return result;
        }
            
        public HashSet<Car> SearchPrice(int price)
        {
            List<Car> tempCars = new List<Car>(carPrices);
            tempCars.Sort(new PriceComparer());
            int p = tempCars.Count;
            HashSet<Car> set = new HashSet<Car>();
            for (int i = 0; i < p; i++)
            {
                int mid = (i + p) / 2;
                if (tempCars[mid].price < price)
                {
                    i = mid + 1;
                }
                else if (tempCars[mid].price > price)
                {
                    p = mid - 1;
                }

                if (tempCars[mid].price >= price)
                {
                    set.Add(tempCars[mid]);
                }
            }
            return set;
        }
        public List<Car> SearchPrice(int startPrice, int endPrice)
        {
            List<Car> tempCars = new List<Car>(carPrices);
            tempCars.Sort(new PriceComparer());
            int p = tempCars.Count;
            List<Car> result = new List<Car>();
            for (int i = 0; i < p; i++)
            {
                int mid = (i + p) / 2;
                if (tempCars[mid].price < startPrice)
                {
                    i = mid + 1;
                }

                if (tempCars[mid].price > startPrice)
                {
                    p = mid - 1;
                }

                if (tempCars[mid].price >= startPrice && tempCars[mid].price <= endPrice)
                {
                    result.Add(tempCars[mid]);
                }
            }
            return result;
        }

    }
}
