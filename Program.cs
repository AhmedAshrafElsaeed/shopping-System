using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shopping_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Look at Logic class 👉👉👉
            Logic logic = Logic.Instance;
            Logic.InitializeProducts();
            Helper_Functions.Welcome();

            bool flag = true;
            while (flag)
            {
                Helper_Functions.DisplayServices();
                int choice = Helper_Functions.GetChoice();
                switch (choice)
                {
                    case 1:
                        Logic.Serve(Logic.Meats);
                        break;
                    case 2:
                        Logic.Serve(Logic.Grains);
                        break;
                    case 3:
                        Logic.Serve(Logic.Vegetables);
                        break;
                    case 4:
                        Logic.ViewMyCart();
                        break;
                    case 5:
                        Logic.UndoLastAction();
                        break;
                    case 6:
                        Logic.CheckOut();
                        break;
                    case 7:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please choose a valid option.");
                        break;
                }
            }
        }
    }
}
