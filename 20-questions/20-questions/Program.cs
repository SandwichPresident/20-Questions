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
            string response;
            List<TreeNode> nodes = new List<TreeNode>();
            QuestionTree tree = new QuestionTree();
            string path = "data.txt";

            //if there alr exists a file w data, deserialize
            if (File.Exists("data.txt"))
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string fileData = reader.ReadToEnd();
                    nodes = JsonSerializer.Deserialize<List<TreeNode>>(fileData);
                }
            }
            //Set up questions if file does not exist
            else
            {

                tree.Root = new TreeNode("Does it have wings?");
                tree.Root.YesNode = new TreeNode("Can it fly?");
                tree.Root.NoNode = new TreeNode("Is it under a foot tall?");

                tree.Root.YesNode.YesNode = new TreeNode("Does it honk?");
                tree.Root.YesNode.YesNode.YesNode = new TreeNode("Is it a goose?");
                tree.Root.YesNode.YesNode.NoNode = new TreeNode("Is it a pigeon?");

                tree.Root.YesNode.NoNode = new TreeNode("Does it live on a farm?");
                tree.Root.YesNode.NoNode.YesNode = new TreeNode("Is it a chicken?");
                tree.Root.YesNode.NoNode.NoNode = new TreeNode("Is it a penguin?");

                tree.Root.NoNode.YesNode = new TreeNode("Can it camoflauge?");
                tree.Root.NoNode.YesNode.YesNode = new TreeNode("Is it a chameleon?");
                tree.Root.NoNode.YesNode.NoNode = new TreeNode("Is it a mouse?");

                tree.Root.NoNode.NoNode = new TreeNode("Does it have large ears?");
                tree.Root.NoNode.NoNode.YesNode = new TreeNode("Is it an elephant?");
                tree.Root.NoNode.NoNode.NoNode = new TreeNode("Is it a deer?");
            }

            //variable holding parentNode
            TreeNode root = tree.Root;

            //while loop -> run until user input is defined end case
            while (!gameOver)
            {
                //while the response value isn't predefined yes or no case
                TreeNode currentQuestion = tree.Root;
                bool correctGuess = false;
                string answer = null;

                while (!correctGuess/*!currentQuestion.IsLeaf() && !correctGuess*/)
                {
                    //ask the question
                    Console.WriteLine(currentQuestion.Data);


                    //get user input
                    response = Console.ReadLine();
                    while (!(response == "yes" || response == "no"))
                    {
                        Console.Write("Please enter 'yes' or 'no': ");
                        response = Console.ReadLine();
                    }


                    if (currentQuestion.Data.Contains("Is it a")) //we have reached the "is it a(n) __?" question
                    {
                        if (response == "yes")
                        {
                            //if win
                            //you win
                            Console.WriteLine("You win!");
                            //correctGuess = true;
                        }
                        else if (response == "no")
                        {
                            //if fail
                            //what question should be added + answer
                            //write that to file
                            Console.WriteLine("Add a question!");
                            string newQuestion = Console.ReadLine();
                            Console.WriteLine("Add a guess if the answer was yes!");
                            string newYesAnswer = Console.ReadLine();
                            Console.WriteLine("Add a guess if the answer was no!");
                            string newNoAnswer = Console.ReadLine();
                            //^^ from here, add these vals to a new node instance
                            TreeNode newQ = new TreeNode(newQuestion);
                            TreeNode newYes = new TreeNode(newYesAnswer);
                            TreeNode newNo = new TreeNode(newNoAnswer);
                            currentQuestion.NoNode = newQ;
                            currentQuestion.YesNode = newYes;

                            newQ.YesNode = newYes;
                            newQ.NoNode = newNo;

                            //not sure if these need to be added
                            //add new node instance to List (.Add())
                            nodes.Add(newQ);
                            nodes.Add(newYes);
                            nodes.Add(newNo);
                             
                        }

                        //Play again?
                        Console.Write("Play again? ");
                        response = Console.ReadLine().ToLower();
                        correctGuess = true;

                        //Break game loop if response is "no"
                        if (response == "no")
                        {
                            //serialize list to add to file
                            var options = new JsonSerializerOptions() { WriteIndented = true };
                            var jsonString = JsonSerializer.Serialize(tree.Root, options);
                            //streamWriter to write to/update file
                            using (StreamWriter writer = new StreamWriter(path, false))
                            {
                                writer.Write(jsonString);
                            }

                            gameOver = true;
                        }

                    }
                    else
                    {
                        if (response == "yes")
                        {
                            //lead to next "yes" node
                            currentQuestion = currentQuestion.YesNode;
                        }
                        else
                        {
                            //lead to next "no" node
                            currentQuestion = currentQuestion.NoNode;
                        }
                    }
                }
            }
        }

        //TreeNode class

        //data -> question / answer text
        //yes path node
        //no path node

        //pre define a string that goes for the yes and no of a final guess
        //on the predetermined no thing -> trigger new question input stuff


        public class TreeNode
        {
            // Question or answer text
            public string Data { get; set; }

            // Child references (linked after loading)
            public TreeNode YesNode { get; set; }
            public TreeNode NoNode { get; set; }

            // Main constructor for file-loaded nodes
            public TreeNode(string data)
            {
                Data = data;

                YesNode = null;
                NoNode = null;
            }

            // A final guess (leaf) is indicated by having no children
            public bool IsLeaf()
            {
                return YesNode == null && NoNode == null;
            }
        }

        public class QuestionTree
        {
            public TreeNode Root { get; set; }
        }
    }
}