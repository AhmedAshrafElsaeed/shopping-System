# Shopping System Console App

This is a simple console-based shopping system built with C#. The application allows users to browse different product categories, add items to a cart, view the cart, remove items, undo actions, and checkout. It demonstrates the use of object-oriented programming principles, the Singleton design pattern, and LINQ for concise code.

## Features

- **Browse Products:**  
  View available products in three categories:
  - Meats
  - Grains
  - Vegetables

- **Add/Remove Items:**  
  Add items to your shopping cart and remove them if needed.

- **Undo Actions:**  
  Reverse the last action (add or remove) with an undo feature.

- **Checkout:**  
  Display a detailed bill and reset the cart after purchase.

- **Case-Insensitive Input:**  
  Product names are compared in a case-insensitive manner for a smoother user experience.

## Project Structure

- **Program.cs:**  
  The main entry point of the application. It handles user inputs and controls the flow of the shopping system.

- **Logic.cs:**  
  Contains the core application logic such as product initialization, managing the cart, handling undo operations, and processing checkouts.

- **Helper_Functions.cs:**  
  Provides helper methods for displaying menus, printing the cart, and formatting product information.

