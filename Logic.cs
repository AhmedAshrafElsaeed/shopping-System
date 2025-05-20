using System;
using System.Collections.Generic;
using System.Linq;

namespace shopping_System
{
    public class Logic
    {


        private static readonly Lazy<Logic> _instance = new Lazy<Logic>(() => new Logic());
        public static Dictionary<string,string> Meats{ get; private set;}
        public static Dictionary<string,string> Grains{ get; private set;}
        public static Dictionary<string,string>Vegetables{ get; private set;}
        public static Dictionary<string,int>AllProductsWithAmount{ get; private set;}
        public static Stack<Tuple<string,string,double>>LastAction{ get; private set;}
        public static List<Tuple<string, double>> Cart;
        public static double MyBill = 0;

        private Logic()
        {
            Meats = new Dictionary<string,string>();
            Grains = new Dictionary<string,string>();
            Vegetables = new Dictionary<string,string>();
            AllProductsWithAmount = new Dictionary<string,int>();
            Cart = new List<Tuple<string,double>>();
            LastAction = new Stack<Tuple<string,string,double>>();
        }

        public static Logic Instance => _instance.Value;
        public static void InitializeProducts()
        {
            string[] meats = new string[] { "chickenbreast", "groundbeef", "salmonfillets", "turkeythighs", "lambchops", "beefsteak", "bacon", "sausages", "duckbreast" };
            string[] grains = new string[] { "whiterice", "brownrice", "jasminerice", "basmatirice", "lentils", "beans", "oats", "barley", "habt elbarakah", "tean shokhy" };
            string[] vegetables = new string[] { "arugula", "tomato", "potatoes", "molokhia", "lettuce", "cucumber", "onion", "garlic", "banger", "gargear" };

            string[] pricesMeats = { "$4.99", "$5.49", "$6.99", "$9.99", "$7.49", "$12.99", "$8.99", "$3.99", "$5.99" };
            string[] pricesGrains = { "$2.49", "$3.99", "$1.79", "$2.99", "$1.99", "$3.49", "$2.29", "$4.99", "$3.79", "$2.89" };
            string[] pricesVegetables = { "$1.99", "$0.89", "$2.49", "$1.49", "$2.99", "$2.79", "$1.99", "$0.79", "$1.59", "$1.29" };

            for (int i = 0; i < meats.Length; i++)
            {
                Meats.Add(meats[i], pricesMeats[i]);
                AllProductsWithAmount.Add(meats[i], 5);
            }
            for (int i = 0; i < grains.Length; i++)
            {
                Grains.Add(grains[i], pricesGrains[i]);
                AllProductsWithAmount.Add(grains[i], 5);
            }
            for (int i = 0; i < vegetables.Length; i++)
            {
                Vegetables.Add(vegetables[i], pricesVegetables[i]);
                AllProductsWithAmount.Add(vegetables[i], 5);
            }
        }
        public static void CheckOut()
        {
            double totalPrice = 0;
            Console.WriteLine("---------- Checkout ----------");
            foreach (var item in Cart)
            {
                Console.WriteLine($"{item.Item1} - Price: ${item.Item2:F2}");
                totalPrice += item.Item2;
            }
            Console.WriteLine("------------------------------");
            Console.WriteLine($"Total Bill: ${totalPrice:F2}");

            LastAction.Clear();
            Cart.Clear();
            MyBill = 0;
            Console.WriteLine("Thank you for your purchase!");
            Console.WriteLine("------------------------------");
        }

        public static void UndoLastAction()
        {
            if (LastAction.Count == 0)
            {
                Console.WriteLine("No actions to undo.");
                return;
            }
            var lastAction = LastAction.Pop();
            string actionType = lastAction.Item1;
            string product = lastAction.Item2;
            double price = lastAction.Item3;

            switch (actionType)
            {
                case "Add":
                    Remove(product, price, recordAction : false);
                    Console.WriteLine($"Undo Add: Removed {product} from the cart.");
                    break;
                case "Remove":
                    Add(product, price, recordAction: false);
                    Console.WriteLine($"Undo Remove: Added {product} back to the cart.");
                    break;
                default:
                    Console.WriteLine("Unknown action.");
                    break;
            }
            Console.WriteLine("------------------------------");
        }

        public static void Serve(Dictionary<string,string>productsDictionary)
        {
            while (true)
            {
                Console.WriteLine("-----------------");
                Console.WriteLine("Available Products:");
                Console.WriteLine("-----------------");
                Helper_Functions.DisplayProducts(productsDictionary);
                Console.WriteLine("Enter the product name to add or type 'back' to return to the main menu:");
                string input = Console.ReadLine()?.Trim().ToLower();
                if (input == "back")
                    break;

                if (!productsDictionary.ContainsKey(input))
                {
                    Console.WriteLine("Invalid product name. Please try again.");
                    continue;
                }

                if (AllProductsWithAmount[input] <= 0)
                {
                    Console.WriteLine("Sorry, this product is out of stock.");
                    continue;
                }
                if (double.TryParse(productsDictionary[input].Replace("$", ""), out double price))
                {
                    Add(input, price);
                    LastAction.Push(new Tuple<string, string, double>("Add", input, price));
                    Console.WriteLine($"{input} added successfully to your cart.");
                }
            }
        }

        public static void ViewMyCart()
        {
            bool isEmpty = Helper_Functions.PrintCart(Cart);
            if (!isEmpty)
            {
                Console.WriteLine("Enter the name of the item you wish to remove (or type 'back' to cancel):");
                string product = Console.ReadLine()?.Trim().ToLower();
                if (product == "back")
                    return;

                bool found = Cart.Any(item => item.Item1.Equals(product, StringComparison.OrdinalIgnoreCase));
                if (found)
                {
                    double price = Cart.First(item => item.Item1.Equals(product, StringComparison.OrdinalIgnoreCase)).Item2;
                    Remove(product, price);
                    LastAction.Push(new Tuple<string, string, double>("Remove", product, price));
                    Console.WriteLine($"{product} removed successfully from your cart.");
                }
                else
                {
                    Console.WriteLine("Product not found in your cart.");
                }
            }
        }
        public static void Add(string product, double price, bool recordAction = true)
        {
            if (AllProductsWithAmount.ContainsKey(product))
            {
                AllProductsWithAmount[product]--;
            }
            Cart.Add(new Tuple<string,double>(product, price));
            MyBill += price;

            // Tricky (by me) to not add this action to the stack 
            if (recordAction)
            {
                LastAction.Push(new Tuple<string,string,double>("Add", product, price));
            }
        }
        public static void Remove(string product, double price, bool recordAction = true)
        {
            if (AllProductsWithAmount.ContainsKey(product))
            {
                AllProductsWithAmount[product]++;
            }
            var itemToRemove = Cart.FirstOrDefault(item => item.Item1.Equals(product, StringComparison.OrdinalIgnoreCase));
            if (itemToRemove != null)
            {
                Cart.Remove(itemToRemove);
                MyBill -= price;
                if (recordAction)
                {
                    LastAction.Push(new Tuple<string, string, double>("Remove", product, price));
                }
            }
        }
    }
}
