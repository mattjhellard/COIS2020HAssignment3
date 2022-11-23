/*
 * Authors (alphabetical): Cole Miller, Jesse Laframboise, Matthew Hellard
 * 
 * how to open this repository in VS:
 * -link VS to your github account
 * -clone this repository in VS
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

public class FileSystem
{
    private class Node
    {
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
            file = new List<string>()
        };
    }

    //Check proper address given
    //Format: "/DirectoryName/DirectoryName/.../Filename"
    private bool CheckFilePath(string address) {
        //Check if the path is null
        if (address != null)
        {
            //Valid
        }
        else
        {
            Console.WriteLine("String can not be null.");
            return false;
        }

        //Check if the path or file name contains invalid characters
        try
        {
            string filename = Path.GetFileName(address);
        }
        catch
        {
            Console.WriteLine("Invalid characters found");
            return false;
        }

        //Check if the path starts at the root (a full path)
        if (Path.IsPathRooted(address))
        {
            //Valid
        }
        else
        {
            Console.WriteLine("Please enter a valid file path in the form: /Directory/.../Filename.extension");
            return false;
        }

        //Check if the path has a valid extension eg .txt
        if (Path.HasExtension(address))
        {
            //Valid
        }
        else
        {
            Console.WriteLine("Please add a file extension. Example: .txt or .exe");
            return false;
        }

        //All previous tests passed
        return true;
    }

    //Takes a full path address and navigates to the directory where the file or directory should be inserted into
    private Node NavigateToDirectory(Node navigationNode, string address)
    {
        //Splits the address into a list of directories
        string[] directoryArray = address.Split('/');

        //Split() takes everything before the '/' so it doesn't capture the root name, so I add it back in
        if (directoryArray[0] == "")
        {
            directoryArray[0] = "/";
        }

        string filename = Path.GetFileName(address);
       
        //Move through each directory in the array
        foreach (var dir in directoryArray)
        {
            if (dir == filename)
            {
                //Correct directory
                
                //Check for existing file
                foreach (string existingFileInDirectory in navigationNode.file)
                {
                    if (filename == existingFileInDirectory)
                    {
                        Console.WriteLine("File already exists in this directory.");
                        return navigationNode = null;
                    }
                }
                // File or Directory can be added
                return navigationNode;
            }
            else
            {
                if (dir == "/")
                {
                    //Skip, root directory
                }
                else
                {
                    //Check leftmost child, determine if there are subdirectories
                    if (navigationNode.leftMostChild == null)
                    {
                        //No subdirectories
                        Console.WriteLine("Path contains directorie(s) not created in this filesystem.");
                        return navigationNode = null;
                    }
                    else
                    {
                        //Check left child matchs
                        if (dir == navigationNode.leftMostChild.directory)
                        {
                            //Move to this directory
                            navigationNode = navigationNode.leftMostChild;
                        }
                        else
                        {
                            //Check directories (right-children) on the same level as the left-child
                            while (navigationNode.rightSibling != null)
                            {
                                if (dir == navigationNode.rightSibling.directory)
                                {
                                    //Move to this directory
                                    navigationNode = navigationNode.rightSibling;
                                }
                            }
                        }
                    }
                }
            }
        }
        return navigationNode = null;
    }
    
    // Adds a file at the given address
    // Returns false if the file already exists at that address or the path is undefined; true otherwise
    public bool AddFile(string address) 
    {
        //Check proper address given
        if (CheckFilePath(address)) {
            //Address is valid
        }
        else {
            Console.WriteLine("File not added. Invalid path");
            return false;
        }

        //Go to the file's directory
        Node navigationNode = NavigateToDirectory(root, address);

        //Add file
        if (navigationNode != null) {
            string file = Path.GetFileName(address);
            navigationNode.file.Add(file);

            return true;
        }
        return false;
    }

    // Removes the file at the given address
    // Returns false if the file is not found at that address or the path is undefined; true otherwise
    public bool RemoveFile(string address) 
    {
        //Check proper address given
        if (CheckFilePath(address))
        {
            //address is valid
        }
        else
        {
            Console.WriteLine("File not removed. Invalid path");
            return false;
        }

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
        return NumberFiles(root); //calls recursive version
    }
    private int NumberFiles(Node localRoot)
    {
        int count = localRoot.file.Count; //get filecount of local directory
        int leftMostChildCount = 0; //will be count of first subdirectory
        int rightSiblingCount = 0; //will be count of right sibling directory
        if(localRoot.leftMostChild != null) //if child null then count stays at 0
        {
            leftMostChildCount = NumberFiles(localRoot.leftMostChild); //recursively get count of all files connected to leftMostChild
        }
        if(localRoot.rightSibling != null) //if sibling null then count stays 0
        {
            rightSiblingCount = NumberFiles(localRoot.rightSibling); //recursively get count of all files connected to rightSibling
        }
        return count + leftMostChildCount + rightSiblingCount; //return results, keep in mind this is how recursions get back to their initial calls too
    }

    // Prints the directories in a pre-order fashion along with their files
    public void PrintFileSystem() {
        //Pre-order traversal
        Console.WriteLine("Directory: " + this.root.directory);
        foreach (var file in this.root.file) {
            Console.WriteLine("File: " + file);
        }
    }
}

public class Demo
{
    public static void Main()
    {
        //testing space

        //Test fileSystem constructor
        FileSystem testFileSystem = new FileSystem();
        //testFileSystem.PrintFileSystem();

        //Test filenames
        string[]  testStrings  = {" ", "/", "/.txt", "A", "/A", "/A/FileA", "/A/FileA.txt", "/File>A.txt", "/File/A.,>.txt", "/FileA.txt", "/FileA.txt", "/FileB.txt" };
        //string[] testStrings = { "/FileA.txt", "/FileA.txt", "/FileB.txt" };
        foreach (string item in testStrings)
        {
            Console.WriteLine(item);
            testFileSystem.AddFile(item);
            testFileSystem.PrintFileSystem();
            Console.WriteLine(" ");
        }

        //test NumberFiles method
        Console.WriteLine("\nCount:"+testFileSystem.NumberFiles());
        
        //keeps console open for VS
        Console.ReadLine();
    }
}