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
            //while loop -> run until user input is defined end case

            //instatiate current response + line number
                //while the response value isn't predefined yes or no case
                    //ask the question
                    //get user input
                //if win
                    //you win
                //if fail
                    //what question should be added + anaswers
                    //write that to file

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

    public class TreeNode
    {
        // Special constant meaning "no child → leaf node"
        public const int noChild = 0;

        // Question or answer text
        public string Data { get; set; }

        // Child references (linked after loading)
        public TreeNode YesNode { get; set; }
        public TreeNode NoNode { get; set; }

        // File line numbers used for linking
        public int YesLineNumber { get; set; }
        public int NoLineNumber { get; set; }

        // Main constructor for file-loaded nodes
        public TreeNode(string data, int yesLine, int noLine)
        {
            Data = data;
            YesLineNumber = yesLine;
            NoLineNumber = noLine;

            YesNode = null;
            NoNode = null;
        }

        // Constructor for new nodes created during the game
        // New nodes start out as leaves (0 = no child)
        public TreeNode(string data)
            : this(data, noChild, noChild)
        {
        }

        // A final guess (leaf) is indicated by having no children
        public bool IsLeaf()
        {
            return YesLineNumber == noChild && NoLineNumber == noChild;
        }
    }

}

