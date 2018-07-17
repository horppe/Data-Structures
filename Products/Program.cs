using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products
{
    class Program
    {
        static void Main(string[] args)
        {
            HashSet<Product> set = new HashSet<Product>();
            set.Add(new Product("Indomie", "UniFoods Inc.", 5));
            set.Add(new Product("Rice", "Dangote Cooporation", 7));
            set.Add(new Product("Vegetable Oil", "Dangote Cooporation", 15));
            set.Add(new Product("Cabin Biscuit", "Cabin Inc.", 10));
            set.Add(new Product("Dangote Cement", "Dangote Inc.", 6));
            set.Add(new Product("Golden Penny Noodles", "A-Star Foods Inc.", 9));
            set.Add(new Product("Dangote Spaghetti", "Dangote Cooporation", 3));
            set.Add(new Product("Golden Penny Spaghetti", "A-Star Foods Inc.", 4));
            List<Product> result = new List<Product>();
            foreach (var product in set)
            {
                if (product.priceInDollars >= 5 && product.priceInDollars <= 10)
                {
                    result.Add(product);
                }
            }
            foreach (var product in result)
            {
                Console.WriteLine(product);
            }
            Console.ReadKey();
        }
    }

    class Product
    {
        public string name;
        public string producer;
        public int priceInDollars;
        
        public Product(string name, string producer, int priceInDollars)
        {
            this.name = name;
            this.producer = producer;
            this.priceInDollars = priceInDollars;
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() + producer.GetHashCode() + priceInDollars.GetHashCode();
        }

        public override string ToString()
        {
            return name.ToString() + ", " + producer.ToString() + " " + priceInDollars.ToString() + "$";
        }

    }
}
