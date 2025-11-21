using System.Text.Json;
using System.Xml.Linq;
//import system io

namespace _20_questions
{
    internal class Program
    {
        //making the category animals for simplicity

        //someone handling the file reader stuff -> kaylah
        //someone handling the node class and converting the file line into a node -> sam
        //someone making the outer repeat game stuff -> max
        //someone do the inner run game stuff -> vicky
        //someone handling adding the new question to the file -> meghan

        //first node
        //stream reader -> get the questions our
        //stream write -> "learning" portion 
        static void Main(string[] args)
        {
            // Initialize variables
            bool gameOver = false;
            string line;
            string response;
            List</**/> nodes = new List</**/>(); //empty, will eventually hold node instances
            //variable holding parentNode


            //if there alr exists a file w data, deserialize
            if (File.Exists("data.txt"))
            {
                using (StreamReader reader = new StreamReader("data.txt"))
                {
                    string fileData = reader.ReadToEnd();
                    nodes = JsonSerializer.Deserialize<List</*node class name*/>>(fileData);
                }
            }
            
            // Welcome message
            Console.WriteLine("Welcome to 20 Questions!");
            Console.WriteLine("Think of an animal, and I will try to guess it.");

            //while loop -> run until user input is defined end case
            while (!gameOver)
            {
                //instatiate current response + line number
                //while the response value isn't predefined yes or no case
                //ask the question
                //get user input
                //if win
                //you win
                //if fail
                //what question should be added + answer
                //write that to file
                Console.WriteLine("Add a question!");
                string newQuestion = Console.ReadLine();
                Console.WriteLine("Add an answer!");
                string newAnswer = Console.ReadLine();
                //^^ from here, add these vals to a new node instance

                //add new node instance to List (.Add())

           

                //Play again?
                Console.Write("Play again? ");
                response = Console.ReadLine().ToLower();
                if (response == "no")
                {
                    //serialize list to add to file
                    var options = new JsonSerializerOptions() { WriteIndented = true };
                    var jsonString = JsonSerializer.Serialize(/*parentNode variable*/, options);
                    //streamWriter to write to/update file
                    using(StreamWriter writer = new StreamWriter("data.txt", false))
                        {
                        writer.Write(jsonString);
                        }



                        gameOver = true;

                }
            }
        }
    }

    //TreeNode class

    //data -> question / answer text
    //yes path node
    //no path node

    //in the file
    //each node is its own line
    //question @ yes line number @ no line number
    
    //pre define a string that goes for the yes and no of a final guess
    //on the predetermined no thing -> trigger new question input stuff
}

