using System;
using System.Collections.Generic;

namespace shopping_System
{
    public class Helper_Functions
    {
        public static bool PrintCart(List<Tuple<string,double>>cart)
        {
            if (cart.Count > 0)
            {
                Console.WriteLine("----- Your Cart -----");
                foreach (var item in cart)
                {
                    Console.WriteLine($"{item.Item1} - Price: ${item.Item2:F2}");
                }
                Console.WriteLine("---------------------");
                return false;
            }
            else
            {
                Console.WriteLine("Your cart is empty.");
                return true;
            }
        }

        public static void DisplayProducts(Dictionary<string,string>productsDictionary)
        {
            foreach (var element in productsDictionary)
            {
                int availableQuantity = Logic.AllProductsWithAmount[element.Key];
                if (availableQuantity > 0)
                {
                    Console.WriteLine($"{element.Key} : {element.Value} (Available: {availableQuantity})");
                }
            }
            Console.WriteLine("----------------------------");
        }

        public static void Welcome()
        {
            Console.WriteLine("Welcome to our Market. Have a nice day!");
        }

        public static void DisplayServices()
        {
            Console.WriteLine("Select a service:");
            Console.WriteLine("1. Meats Category");
            Console.WriteLine("2. Grains Category");
            Console.WriteLine("3. Vegetables Category");
            Console.WriteLine("4. View Cart");
            Console.WriteLine("5. Undo Last Action");
            Console.WriteLine("6. Checkout");
            Console.WriteLine("7. Exit");
        }

        public static int GetChoice()
        {
            Console.Write("Enter your choice: ");
            if (int.TryParse(Console.ReadLine(), out int num))
            {
                return num;
            }
            Console.WriteLine("Invalid input. Please enter a number.");
            return 0;
        }
    }
}
