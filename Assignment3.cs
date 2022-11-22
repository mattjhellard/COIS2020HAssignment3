/*
 * how to open this repository in VS:
 * -link VS to your github account
 * -clone this repository in VS
 */

using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public class FileSystem
{
    private class Node
    {
        public class List <T>
        {

        }

        public string directory;
        public List<string> file;
        public Node leftMostChild;
        public Node rightSibling;
    }

    private Node root;

    // Creates a file system with a root directory where the name of the root directory is “/”.
    public FileSystem() {
        root = new Node
        {
            //Initialize values
            directory = "/",
            leftMostChild = null,
            rightSibling = null,
            file = null
        };
    }

    //Check if the filename contains invalid characters
    string CheckInvalidCharacters(string stringToCheck, char[] invalidCharacters)
    {
        string invalidMsg = " ";
        char[] invalidFilenameChars = Path.GetInvalidFileNameChars();
        string filename = Path.GetFileName(stringToCheck);

        foreach (char c in filename)
        {
            foreach (char invalidChar in invalidFilenameChars)
            {
                if (c == invalidChar)
                {
                    //Invalid character detected
                    invalidMsg = "Invalid characters found: " + invalidChar;
                    Console.WriteLine(invalidMsg);
                    break;
                }
            }
            if (invalidMsg != " ")
            {
                break;
            }
        }
        return invalidMsg;
    }

    // Adds a file at the given address
    // Returns false if the file already exists at that address or the path is undefined; true otherwise
    public bool AddFile(string address) 
    {
        //Check proper address given
        //Format: "/DirectoryName/DirectoryName/.../Filename"

        //Check if the path is null
        if (address != null)
        {
            //Valid
        }
        else
        {
            //null string
            Console.WriteLine("String can not be null.");
            return false;
        }

        //Check if the path contains invalid characters
        //below but just the path
         


        //Check if the filename contains invalid characters
        char[] invalidFilenameChars = Path.GetInvalidFileNameChars();

        //Issue
        string filename = Path.GetFileName(address);        //Use this as the check try to do this else fail for filename and directory
        string invalidMsg = CheckInvalidCharacters(filename, invalidFilenameChars);

        if (invalidMsg == " ")
        {
            //Valid
        }
        else
        {
            //No extension
            Console.WriteLine(invalidMsg);
            return false;
        }

        //Check if the path starts at the root (a full path)
        if (Path.IsPathRooted(address))
        {
            //Valid
        }
        else
        {
            //Invalid file path
            Console.WriteLine("Please enter a valid file path in the form: /Directory/.../Filename");
            return false;
        }

        //Check if the path has a valid extension eg .txt
        if (Path.HasExtension(address))
        {
            //Valid
        }
        else
        {
            //No extension
            Console.WriteLine("Please add a file extension. Example: .txt or .exe");
            return false;
        }

        //navigate to this directory

        //CHeck if their are conflicts

        //if no conflicts add file



        return false; //placeholder, replace with real code
    }

    // Removes the file at the given address
    // Returns false if the file is not found at that address or the path is undefined; true otherwise
    public bool RemoveFile(string address) 
    {
        //CHeck proper address given


        //navigate to this directory

        //CHeck if their are conflicts

        //if no conflicts remove file



        return false; //placeholder, replace with real code
    }

    // Adds a directory at the given address
    // Returns false if the directory already exists or the path is undefined; true otherwise
    public bool AddDirectory(string address) 
    {
        return false; //placeholder, replace with real code
    }

    // Removes the directory (and its subdirectories) at the given address
    // Returns false if the directory is not found or the path is undefined; true otherwise
    public bool RemoveDirectory(string address) 
    {
        return false; //placeholder, replace with real code
    }

    // Returns the number of files in the file system (Do not add a count as a data member)
    public int NumberFiles() 
    {
        return 0; //placeholder, replace with real code
    }

    // Prints the directories in a pre-order fashion along with their files
    public void PrintFileSystem() {
        Console.WriteLine(this.root.directory);

        /*Node traversalNode = this.root;
        while (traversalNode.leftMostChild != null) { 
            
        }*/
    }
}

public class Demo
{
    public static void Main()
    {
        //testing space

        //Test fileSystem constructor
        FileSystem testFileSystem = new FileSystem();
        testFileSystem.PrintFileSystem();

        //Test file paths
        string[]  testStrings  = {" ", "/", "A", "/A", "/A/FileA", "/A/FileA.txt", "/File>A.txt", "/FileA.txt"};
        foreach (string item in testStrings)
        {
            Console.WriteLine(item);
            testFileSystem.AddFile(item);
            Console.WriteLine(" ");
        }
    }
}