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
            //variable holding parentNode
            QuestionTree tree = new QuestionTree();

            //if there alr exists a file w data, deserialize
            if (File.Exists("data.txt"))
            {
                using (StreamReader reader = new StreamReader("data.txt"))
                {
                    string fileData = reader.ReadToEnd();
                    nodes = JsonSerializer.Deserialize<List<TreeNode>>(fileData);
                }


                //while loop -> run until user input is defined end case
                while (!gameOver)
                {
                    //while the response value isn't predefined yes or no case
                    TreeNode currentQuestion = nodes.FirstOrDefault();
                    Boolean correctGuess = false;
                    string answer = null;

                    while (!currentQuestion.IsLeaf() && !correctGuess)
                    {
                        //ask the question
                        Console.WriteLine(currentQuestion);

                        //get user input
                        response = Console.ReadLine();
                        while (!(response == "yes" || response == "no"))
                        {
                            Console.Write("Please enter 'yes' or 'no': ");
                        }

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
                    //if win
                    //you win
                    if (correctGuess)
                    {
                        Console.WriteLine("You win!");
                    }
                    //if fail
                    //what question should be added + answer
                    //write that to file
                    else
                    {
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

                        if (answer.ToLower() == "no")
                        {
                            currentQuestion.NoNode = newQ;
                        }
                        else if (answer.ToLower() == "yes")
                        {
                            currentQuestion.YesNode = newYes;
                        }


                        newQ.YesNode = newYes;
                        newQ.NoNode = newNo;

                        nodes.Add(newQ);
                        nodes.Add(newYes);
                        nodes.Add(newNo);    //not sure if these need to be added
                                             //add new node instance to List (.Add())
                    }


                    //Play again?
                    Console.Write("Play again? ");
                    response = Console.ReadLine().ToLower();

                    //Break game loop if response is "no"
                    if (response == "no")
                    {
                        //serialize list to add to file
                        var options = new JsonSerializerOptions() { WriteIndented = true };
                        //var jsonString = JsonSerializer.Serialize(/*parentNode variable*/, options);
                        //streamWriter to write to/update file
                        using (StreamWriter writer = new StreamWriter("data.txt", false))
                        {
                            //writer.Write(jsonString);
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
            TreeNode Root { get; set; }
        }
    }
}

