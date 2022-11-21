/*
 * how to open this repository in VS:
 * -link VS to your github account
 * -clone this repository in VS
 */

using System;
using System.IO;
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
            Console.WriteLine("valid");
        }
        else
        {
            //Invalid file path
            Console.WriteLine("String can not be null.");
        }

        //Check if the path starts at the root
        if (Path.IsPathRooted(address))
        {
            //Valid
            Console.WriteLine("valid");
        }
        else {
            //Invalid file path
            Console.WriteLine("Please enter a valid file path in the form: /Directory/.../Filename");
        }

        //Check if the path has a valid extension eg .txt
        if (Path.HasExtension(address))
        {
            //Valid
            Console.WriteLine("valid2");
        }
        else
        {
            //No extension
            Console.WriteLine("Please add a file extension. Example: .txt or .exe");
        }

        //Check if the path contains invalid characters
        if (1 == 2)
        {
            //Valid
            Console.WriteLine("valid2");
        }
        else
        {
            //No extension
            Console.WriteLine("Please add a file extension. Example: .txt or .exe");
        }

        //CHeck if its an absolute path



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
        FileSystem testFileSystem = new FileSystem();

        testFileSystem.PrintFileSystem();
        testFileSystem.AddFile(null);
    }
}