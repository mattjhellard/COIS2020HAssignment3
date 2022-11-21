/*
 * how to open this repository in VS:
 * -link VS to your github account
 * -clone this repository in VS
 */

using System;
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
        //CHeck proper address given

        
        //navigate to this directory

        //CHeck if their are conflicts

        //if no conflicts add file
        
        
        
        return false; //placeholder, replace with real code
    }

    // Removes the file at the given address
    // Returns false if the file is not found at that address or the path is undefined; true otherwise
    public bool RemoveFile(string address) 
    {
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
    }
}