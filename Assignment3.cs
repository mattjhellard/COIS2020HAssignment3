/*
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

    /*//Check if the filename contains invalid characters
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
    }*/

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

        //Splits the address into a list of directories
        string[] directoryArray = address.Split('/');

        //Split() takes everything before the '/' so it doesn't capture the root name, so I add it back in
        if (directoryArray[0] == "") {
            directoryArray[0] = "/";
        }

        Node navigationNode = root;
        string filename = Path.GetFileName(address);
        foreach (var dir in directoryArray)
        {
            if (dir == filename)
            {
                //Correct directory

                //Check for existing file
                foreach (string existingFileInDirectory in navigationNode.file)
                {
                    if (filename == existingFileInDirectory) {
                        Console.WriteLine("File already exists in this directory.");
                        return false;
                    }
                }

                //Add file
                navigationNode.file.Add(filename);
                return true;  
            }
            else {
                if (dir == "/")
                {
                    //Skip, root directory
                }
                else {
                    //Navigate to correct directory

                    //Check leftmost child, determine if there are subdirectories
                    if (navigationNode.leftMostChild == null)
                    {
                        //No subdirectories
                        Console.WriteLine("Path contains directorie(s) not created in this filesystem.");
                        return false;
                    }
                    else {
                        //Check left child matchs
                        if (dir == navigationNode.leftMostChild.directory) {
                            //Move to this directory
                            navigationNode = navigationNode.leftMostChild;
                        }
                        else {
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
        return false; //placeholder, replace with real code
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
    { //working on this - MH
        return NumberFiles(root);
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
        //Console.WriteLine(this.root.directory);

        /*Node traversalNode = this.root;
        while (traversalNode.leftMostChild != null) { 
            //Print all files in directory
                foreach (string file in navigationNode.file) {
                    Console.WriteLine(file);
                }
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

        //Test filenames
        string[]  testStrings  = {" ", "/", "/.txt", "A", "/A", "/A/FileA", "/A/FileA.txt", "/File>A.txt", "/File/A.,>.txt", "/FileA.txt" };
        foreach (string item in testStrings)
        {
            Console.WriteLine(item);
            testFileSystem.AddFile(item);
            Console.WriteLine(" ");
        }

        //test duplicate files
        testFileSystem.AddFile("/FileA.txt");
        testFileSystem.AddFile("/FileB.txt");
        testFileSystem.AddFile("/FileA.txt");

        //test NumberFiles method
        Console.WriteLine("\nCount:"+testFileSystem.NumberFiles());
        
        //keeps console open for VS
        Console.ReadLine();
    }
}