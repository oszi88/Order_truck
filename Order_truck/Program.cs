using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Order_truck
{
    class Program
    {
        struct Truck
        {
            public int id;
            public string jobTypeList;
        }

        struct Order
        {
            public int id;
            public string type;
        }

        static Truck[] TrucksToArrays(string path) //Read the file to a Truck type array.
        {
            string[] orderTruckFile = File.ReadAllLines(path);
            Truck[] trucks = new Truck[Convert.ToInt32(orderTruckFile[0]) + 1];
            for (int i = 1; i < trucks.Length; i++)
            {
                string[] temp = orderTruckFile[i].Split(' ');
                trucks[i].id = Convert.ToInt32(temp[0]);
                for (int j = 1; j < temp.Length; j++)
                {
                    trucks[i].jobTypeList += temp[j];
                }
            }
            return trucks;
        }

        static Order[] OrdersToArray(string path) //Read the file to a Order type array.
        {
            string[] orderTruckFile = File.ReadAllLines(path);
            Order[] orders = new Order[Convert.ToInt32(orderTruckFile[Convert.ToInt32(orderTruckFile[0]) + 1])];
            for (int i = 0; i < orders.Length; i++)
            {
                string[] temp = orderTruckFile[i + orders.Length + 2].Split(' ');
                orders[i].id = Convert.ToInt32(temp[0]);
                for (int j = 0; j < temp.Length; j++)
                {
                    orders[i].type = temp[j];
                }
            }
            return orders;
        }

        static void WritetoFile(string path, List<string> finalList) //Write the final List to a specified place.
        {
            File.WriteAllLines(path, finalList);
        }

        static void Main(string[] args)
        {
            List<Truck> trucks = TrucksToArrays("order_truck.txt").OfType<Truck>().ToList(); // Load the array to a Truck list.
            List<Order> orders = OrdersToArray("order_truck.txt").OfType<Order>().ToList(); // Load the array to an Order list.

            List<string> finalList = new List<string>();

            int i = 0, j;
            bool match;

            while (i < orders.Count)
            {
                match = false;
                j = 1;
                while (match != true)
                {
                    foreach (char item in trucks[j].jobTypeList)
                    {
                        if (item == char.Parse(orders[i].type))
                        {
                            match = true;
                            finalList.Add(trucks[j].id + " " + orders[i].id);
                            trucks.Remove(trucks[j]);
                            orders.Remove(orders[i]);
                        }
                       
                    }
                    j++;
                    if (j == trucks.Count)
                    {
                        match = true;
                        finalList.Add("---" + " " + orders[i].id);
                        orders.Remove(orders[i]);
                    }
                }
            }

            WritetoFile("result.txt", finalList);
        }
    }
}
