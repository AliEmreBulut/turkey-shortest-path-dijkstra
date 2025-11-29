using System.Collections.Generic;
using System.IO;
using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    internal class Program
    {
        // Method to find the shortest path between cities
        public static Database[,] Find_shortest_path_city(Database[,] city_array)
        {
            for (int county = 0; county < 81; county++)
            {
                int[] arrived = new int[81]; // Array to track visited cities

                for (int i = 0; i < 81 - 1; i++)
                {
                    int min = int.MaxValue; // Initialize minimum distance
                    int index = 0; // Index of the current city

                    // Find the city with the minimum distance that hasn't been visited
                    for (int j = 0; j < 81; j++)
                    {
                        if (city_array[county, j].Number < min && city_array[county, j].Number != 0 && arrived[j] != 1)
                        {
                            min = city_array[county, j].Number;
                            index = j;
                        }
                    }

                    // Update distances and paths for neighboring cities
                    for (int x = 0; x < 81; x++)
                    {
                        if (city_array[index, x].Number + min < city_array[county, x].Number && city_array[index, x].Number != 0)
                        {
                            city_array[county, x].Number = min + city_array[index, x].Number;
                            city_array[county, x].Shortes_Path = city_array[county, index].Shortes_Path + "-" + city_array[index, x].Shortes_Path;
                        }
                    }
                    arrived[index] = 1; // Mark city as visited
                }
            }
            return city_array; // Return the updated city array with shortest paths
        }

        // Method to find the shortest path between counties
        public static Database_county[,] find_shortest_path(Database_county[,] county_array)
        {
            for (int county = 0; county < 30; county++)
            {
                int[] arrived = new int[30]; // Array to track visited counties

                for (int i = 0; i < 30 - 1; i++)
                {
                    double min = int.MaxValue; // Initialize minimum distance
                    int index = 0; // Index of the current county

                    // Find the county with the minimum distance that hasn't been visited
                    for (int j = 0; j < 30; j++)
                    {
                        if (county_array[county, j].Number < min && county_array[county, j].Number != 0 && arrived[j] != 1)
                        {
                            min = county_array[county, j].Number;
                            index = j;
                        }
                    }

                    // Update distances and paths for neighboring counties
                    for (int x = 0; x < 30; x++)
                    {
                        if (county_array[index, x].Number + min < county_array[county, x].Number && county_array[index, x].Number != 0)
                        {
                            county_array[county, x].Number = min + county_array[index, x].Number;
                            county_array[county, x].Shortes_Path = county_array[county, index].Shortes_Path + "-" + county_array[index, x].Shortes_Path;
                        }
                    }
                    arrived[index] = 1; // Mark county as visited
                }
            }
            return county_array; // Return the updated county array with shortest paths
        }

        // Class representing a city with distance and shortest path
        public class Database
        {
            private int number; // Distance value
            private String shortest_path; // Shortest path string

            // Constructor
            public Database(int number, string shortest_path)
            {
                this.number = number;
                this.shortest_path = shortest_path;
            }

            public int Number
            {
                get { return number; }
                set { number = value; }
            }

            public String Shortes_Path
            {
                get { return shortest_path; }
                set { shortest_path = value; }
            }
        }

        // Class representing a county with distance and shortest path
        public class Database_county
        {
            private double number; // Distance value
            private String shortest_path; // Shortest path string

            // Constructor
            public Database_county(double number, string shortest_path)
            {
                this.number = number;
                this.shortest_path = shortest_path;
            }

            public double Number
            {
                get { return this.number; }
                set { number = value; }
            }

            public String Shortes_Path
            {
                get { return shortest_path; }
                set { shortest_path = value; }
            }
        }

        // Main method
        static void Main(string[] args)
        {
            int cityCount = 0; // Counter for the number of cities processed
            Random random = new Random(); // Random number generator
            StreamReader reader; // File reader for input files
            Dictionary<int, String> citys = new Dictionary<int, String>(); // Dictionary to store city names
            int[,] All_city_array = new int[81, 81]; // Array to store distances between cities
            int[][] city_jagged_array = new int[81][]; // Jagged array for city distances
            Database[,] city_array = new Database[81, 81]; // 2D array of Database objects for cities

            int[,] komsu_listesi = new int[81, 81]; // Array to track neighboring cities

            try
            {
                int a = 0; // Counter for the number of cities read
                reader = new StreamReader("ilmesafe.txt"); // Open city distance file

                // Read the first line to initialize
                String line = reader.ReadLine();
                String[] cityValues = line.Split();
                while (reader.EndOfStream == false)
                {
                    int cityNum = int.Parse(cityValues[0]); // Get the current city number
                    citys.Add(int.Parse(cityValues[0]), cityValues[1]); // Add city to dictionary

                    // Populate city_array and All_city_array with distances
                    for (int i = 2; i < 83; i++)
                    {
                        Database city_w = new Database(int.Parse(cityValues[i]), cityValues[1]);
                        city_array[cityNum - 1, i - 2] = city_w;
                        All_city_array[cityNum - 1, i - 2] = int.Parse(cityValues[i]);
                    }

                    // Initialize jagged array for city distances
                    if (a < 81)
                    {
                        city_jagged_array[a] = new int[cityNum];
                    }

                    // Populate jagged array with distances
                    for (int i = 2; i < cityNum + 2; i++)
                    {
                        city_jagged_array[cityNum - 1][i - 2] = int.Parse(cityValues[i]);
                    }
                    a++;

                    line = reader.ReadLine(); // Read the next line
                    cityValues = line.Split(); // Split the line into values
                }
                reader.Close(); // Close the file
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("file not found....."); // Error handling for file not found
            }

            try
            {
                reader = new StreamReader("illerin_komsulari.txt"); // Open neighboring cities file
                string line;
                string[] komsular;

                // Read neighboring cities
                while (reader.EndOfStream == false)
                {
                    line = reader.ReadLine();
                    komsular = line.Split();
                    for (int i = 0; i < komsular.Length; i++)
                    {
                        komsu_listesi[int.Parse(komsular[0]), int.Parse(komsular[i])] = 1; // Mark neighbors
                    }
                }
                reader.Close();

                // Set distances to a large value for non-neighboring cities
                for (int i = 0; i < 81; i++)
                {
                    for (int j = 0; j < 81; j++)
                    {
                        if (komsu_listesi[i, j] == 0)
                        {
                            city_array[i, j].Number = 10000000; // Set distance to infinity
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("file not found.."); // Error handling for file not found
            }

            // Randomly select pairs of cities and output their distances
            while (cityCount < 10)
            {
                int x, y;

                // Ensure x and y are different
                do
                {
                    x = random.Next(1, 81);
                    y = random.Next(1, 81);
                } while (x == y);

                int aradaki_mesafe = 0; // Distance between selected cities
                string sehir1 = citys[x]; // First city name
                string sehir2 = citys[y]; // Second city name

                // Determine distance based on city indices
                if (x > y)
                {
                    aradaki_mesafe = city_jagged_array[x - 1][y - 1];
                }
                else if (x < y)
                {
                    aradaki_mesafe = city_jagged_array[y - 1][x - 1];
                }

                Console.WriteLine(x+" "+sehir1 + " - " + y + " " + sehir2 + " = " + aradaki_mesafe); // Output the distance

                cityCount++; // Increment city counter
            }

            Console.WriteLine(); // Print a new line for clarity

            city_array = Find_shortest_path_city(city_array); // Calculate shortest paths for cities
            double max = -1; // Initialize maximum distance
            double min = 10000000000; // Initialize minimum distance
            List<String> Max_list = new List<String>(); // List to store maximum distance entries
            List<String> Min_list = new List<String>(); // List to store minimum distance entries

            Console.WriteLine("Given Number -  Calculated Number          shortest path");
            for (int i = 0; i < 81; i++)
            {
                for (global::System.Int32 j = 0; j < 81; j++)
                {
                    // Check for non-neighboring cities
                    if (komsu_listesi[i, j] == 0)
                    {
                        city_array[i, j].Shortes_Path += "-" + citys[j + 1]; // Append the city name to the shortest path
                        Console.WriteLine("    "+All_city_array[i, j] + "      -      " + city_array[i, j].Number + " = " + Math.Abs(All_city_array[i, j] - city_array[i, j].Number) + "       " + city_array[i, j].Shortes_Path);

                        // Update minimum distance information
                        if (Math.Abs(All_city_array[i, j] - city_array[i, j].Number) < min)
                        {
                            min = Math.Abs(All_city_array[i, j] - city_array[i, j].Number);
                            Min_list.Clear();
                            Min_list.Add(All_city_array[i, j] + "  -  " + city_array[i, j].Number + " = " + Math.Abs(All_city_array[i, j] - city_array[i, j].Number) + "       " + city_array[i, j].Shortes_Path);
                        }
                        else if (Math.Abs(All_city_array[i, j] - city_array[i, j].Number) == min)
                        {
                            Min_list.Add(All_city_array[i, j] + "  -  " + city_array[i, j].Number + " = " + Math.Abs(All_city_array[i, j] - city_array[i, j].Number) + "       " + city_array[i, j].Shortes_Path);
                        }

                        // Update maximum distance information
                        if (Math.Abs(All_city_array[i, j] - city_array[i, j].Number) > max)
                        {
                            max = Math.Abs(All_city_array[i, j] - city_array[i, j].Number);
                            Max_list.Clear();
                            Max_list.Add(All_city_array[i, j] + "  -  " + city_array[i, j].Number + " = " + Math.Abs(All_city_array[i, j] - city_array[i, j].Number) + "       " + city_array[i, j].Shortes_Path);
                        }
                        else if (Math.Abs(All_city_array[i, j] - city_array[i, j].Number) == max)
                        {
                            Max_list.Add(All_city_array[i, j] + "  -  " + city_array[i, j].Number + " = " + Math.Abs(All_city_array[i, j] - city_array[i, j].Number) + "       " + city_array[i, j].Shortes_Path);
                        }
                    }
                }
            }

            // Output maximum distance entries
            Console.WriteLine("Max------------------------------------");
            for (int i = 0; i < Max_list.Count; i++)
            {
                Console.WriteLine(Max_list[i]);
            }
            // Output minimum distance entries
            Console.WriteLine("Min---------------------------------------");
            for (int i = 0; i < Min_list.Count; i++)
            {
                Console.WriteLine(Min_list[i]);
            }

            Console.WriteLine("*****************************************************************");

            StreamReader reader2; // File reader for county data
            int[,] count_neighboor_check = new int[30, 30]; // Array to check county neighbors
            Dictionary<int, String> countys = new Dictionary<int, String>(); // Dictionary to store county names
            double[,] array = new double[30, 30]; // Array to store distances between counties
            Database_county[,] county_array = new Database_county[30, 30]; // 2D array of Database_county objects for counties
            Database_county county; // Variable to hold county data

            // Read county distances
            try
            {
                reader2 = new StreamReader("ilce_mesafe.txt");
                String line = reader2.ReadLine();
                String[] county_values = line.Split();
                int row = 0; // Row index for counties
                int column = 0; // Column index for counties

                while (reader2.EndOfStream == false)
                {
                    // Check for specific counties (e.g., İzmir)
                    if (county_values[0] == "İZMİR" && county_values[2] == "İZMİR" && county_values[1] != "MERKEZ" && county_values[3] != "MERKEZ")
                    {
                        if (countys.ContainsKey(row) == false)
                        {
                            countys.Add(row, county_values[1]); // Add county name to dictionary
                        }
                        if (row == column)
                        {
                            array[row, column] = 0; // Distance to self is zero
                            Database_county county1 = new Database_county(0, county_values[1]);
                            county_array[row, column] = county1; // Initialize county array
                            column++;
                        }
                        array[row, column] = double.Parse(county_values[4]); // Read distance value

                        county = new Database_county(double.Parse(county_values[4]), county_values[1]);
                        county_array[row, column] = county; // Store county distance

                        column++;
                        // Move to the next row if we reach the end of the current row
                        if (column == 30)
                        {
                            row++;
                            column = 0;
                        }

                        // Ensure the last element is initialized
                        if (row == 29 && column == 29)
                        {
                            array[row, column] = 0;
                            Database_county county1 = new Database_county(0, county_values[1]);
                            county_array[row, column] = county1;
                        }
                    }
                    line = reader2.ReadLine(); // Read the next line
                    county_values = line.Split(); // Split the line into values
                }
                reader2.Close(); // Close the file
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("file not found....."); // Error handling for file not found
            }

            // Read county neighbors
            try
            {
                reader2 = new StreamReader("ilce_komsu.txt");
                string line;
                string[] county_neighboor;
                while (reader2.EndOfStream == false)
                {
                    line = reader2.ReadLine();
                    county_neighboor = line.Split();
                    for (global::System.Int32 i = 0; i < 30; i++)
                    {
                        count_neighboor_check[int.Parse(county_neighboor[0]), i] = int.Parse(county_neighboor[i + 1]); // Mark neighbors
                        if (int.Parse(county_neighboor[i + 1]) == 0)
                        {
                            county_array[int.Parse(county_neighboor[0]), i].Number = 1000000000; // Set distance to infinity for non-neighbors
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("file not found...."); // Error handling for file not found
            }

            // Calculate shortest paths for counties
            county_array = find_shortest_path(county_array);
            max = -1; // Initialize maximum distance
            min = 10000000000; // Initialize minimum distance
            Max_list = new List<String>(); // List to store maximum distance entries
            Min_list = new List<String>(); // List to store minimum distance entries

            Console.WriteLine("Given Number -  Calculated Number          shortest path");
            for (int i = 0; i < 30; i++)
            {
                for (global::System.Int32 j = 0; j < 30; j++)
                {
                    // Check for non-neighboring counties
                    if (count_neighboor_check[i, j] == 0)
                    {
                        county_array[i, j].Shortes_Path += "-" + countys[j]; // Append the county name to the shortest path
                        Console.WriteLine(array[i, j] + "  -  " + county_array[i, j].Number + " = " + Math.Abs(array[i, j] - county_array[i, j].Number) + "       " + county_array[i, j].Shortes_Path);

                        // Update minimum distance information
                        if (Math.Abs(array[i, j] - county_array[i, j].Number) < min)
                        {
                            min = Math.Abs(array[i, j] - county_array[i, j].Number);
                            Min_list.Clear();
                            Min_list.Add(array[i, j] + "  -  " + county_array[i, j].Number + " = " + Math.Abs(array[i, j] - county_array[i, j].Number) + "       "+ county_array[i, j].Shortes_Path);
                        }
                        else if (Math.Abs(array[i, j] - county_array[i, j].Number) == min)
                        {
                            Min_list.Add(array[i, j] + "  -  " + county_array[i, j].Number + " = " + Math.Abs(array[i, j] - county_array[i, j].Number) + "       " + county_array[i, j].Shortes_Path);
                        }

                        // Update maximum distance information
                        if (Math.Abs(array[i, j] - county_array[i, j].Number) > max)
                        {
                            max = Math.Abs(array[i, j] - county_array[i, j].Number);
                            Max_list.Clear();
                            Max_list.Add(array[i, j] + "  -  " + county_array[i, j].Number + " = " + Math.Abs(array[i, j] - county_array[i, j].Number) + "       " + county_array[i, j].Shortes_Path);
                        }
                        else if (Math.Abs(array[i, j] - county_array[i, j].Number) == max)
                        {
                            Max_list.Add(array[i, j] + "  -  " + county_array[i, j].Number + " = " + Math.Abs(array[i, j] - county_array[i, j].Number) + "       " + county_array[i, j].Shortes_Path);
                        }
                    }
                }
            }
            // Output maximum distance entries
            Console.WriteLine("Max------------------------------------");
            for (int i = 0; i < Max_list.Count; i++)
            {
                Console.WriteLine(Max_list[i]);
            }
            // Output minimum distance entries
            Console.WriteLine("Min------------------------------------");
            for (int i = 0; i < Min_list.Count; i++)
            {
                Console.WriteLine(Min_list[i]);
            }
        }
    }
}