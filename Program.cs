using System.Text.Json;
using System.Text.Json.Serialization;
public class program 

{

    public static void Main(string[]  args)
    {

      Menu.ShowMenu();
      var UserAnswer =  Menu.WantToOrder();
      var ListOfOrders = Menu.ListOfDishes(UserAnswer);
      Menu.OrderedValue(ListOfOrders);

    }


}

public class FullMenu
{
    public static string RawMenu = File.ReadAllText("C:/Dev/MenuKaartCSharp/MenuKaartCSharp/menu.json");
    public static List<Dish> myDishes = JsonSerializer.Deserialize<List<Dish>>(RawMenu);
}

public class Dish
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("Dish")] public string Name { get; set; }

    [JsonPropertyName("Price")] public decimal Price { get; set; }
}

public class Menu
{


    public static void ShowMenu()
    {
        
        

        foreach (var dish in FullMenu.myDishes)
        {
        Console.WriteLine($"{dish.Id} {dish.Name}   {dish.Price}");
        }

     
        
    }


    public static Boolean WantToOrder()
    {
        string WantsToOrder="fsjdgksdfjg";

        while (WantsToOrder != "no" && WantsToOrder != "yes")
        {
            Console.WriteLine(" Would you like to order something ? Enter 'yes' or 'no' ");
            WantsToOrder = Console.ReadLine();
        }
        if (WantsToOrder == "yes")
        {
            return true;

        }
        else
        {
            return false;
        }
        
    }

    public static Array ListOfDishes(Boolean UserAnswer)
    {
        List<string> OrderedDishesList = new List<string>();
        int numericValue;
        var DishId = "";
        var SatisfiedOrder = "no";
            
            if (UserAnswer)
            {
            
                    OrderedDishesList.Clear();
                    Console.WriteLine("type the id if the dish you want. If done ordering press 'end'");
                    DishId = Console.ReadLine();
                    var Userinput = int.TryParse(DishId, out numericValue);

                    while (DishId != "end")
                    {

                        while (SatisfiedOrder != "yes")
                        {

                            if (Userinput && (numericValue > -1 && numericValue < 11))
                            {
                                OrderedDishesList.Add(DishId);
                                Console.WriteLine("type the id if the dish you want. If done ordering press 'end'");
                                DishId = Console.ReadLine();
                                Userinput = int.TryParse(DishId, out numericValue);
                            }
                            else if (DishId != "end" || (numericValue < 0 || numericValue > 10))
                            {
                                Console.WriteLine("Wrong input, type the Id of the dish. If done ordering press 'end'");
                                DishId = Console.ReadLine();
                                Userinput = int.TryParse(DishId, out numericValue);
                            }

                            else if (DishId == "end")
                            {
                                var DishIndex = 0;
                                Console.WriteLine("You have have the following dishes in your shopping cart: ");

                                for (var i = 0; i < OrderedDishesList.Count; i++)
                                {
                                    var IsNumber = Int32.TryParse(OrderedDishesList[i], out DishIndex);
                                    Console.WriteLine(FullMenu.myDishes[DishIndex].Name);
                                }

                                Console.WriteLine("Type 'yes' if you this is the correct order. Give another input if you are not satisfied ");
                                var CorrectOrder = Console.ReadLine();
                                if ( CorrectOrder == "yes")
                                {
                                    SatisfiedOrder = "yes";

                                } else
                                {
                                    OrderedDishesList.Clear();
                                    Console.WriteLine("We emptied your shopping cart");
                                    Console.WriteLine("type the id if the dish you want. If done ordering press 'end'.");
                                    DishId = Console.ReadLine();
                                    Userinput = int.TryParse(DishId, out numericValue);

                                }

                            }                  

                        }

                     } 

                
            }



       
        string[] OrderDishesArray = OrderedDishesList.ToArray();
        return OrderDishesArray;

    }

    public static void OrderedValue( Array ListOfOrders)
    {

        var AllOrders = ListOfOrders.OfType<string>().ToList();
        decimal TotalValue = 0m;
        var DishIndex = 0;

        for (var i = 0; i < ListOfOrders.Length; i++)
        {
            var IsNumber = Int32.TryParse(AllOrders[i], out DishIndex );
            TotalValue = TotalValue+ (FullMenu.myDishes[DishIndex].Price);
            
        }

        if (TotalValue > 0)
        {
            Console.WriteLine("You have ordered for a total amount of: " + TotalValue + " Euro's ");
        }
        else
        {
            Console.WriteLine("You have canceled the order. Thank you for our visit ");
        }

    }


}



