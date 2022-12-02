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
    public FileSystem()
    {
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
    private bool CheckFilePath(string address)
    {
        //Check if the path is null
        if (address != null)
        {
            //Valid
        }
        else
        {
            throw new Exception("String can not be null.");
        }

        //Check if the path or file name contains invalid characters
        try
        {
            string filename = Path.GetFileName(address);
        }
        catch
        {
            throw new Exception("Invalid characters found");
        }

        //Check if the path starts at the root (a full path)
        if (Path.IsPathRooted(address))
        {
            //Valid
        }
        else
        {         
            throw new Exception("Please enter a valid file path in the form: /Directory/.../Filename.extension");
        }

        //All previous tests passed
        return true;
    }

    //Takes a full path address and navigates to the directory where the file or directory should be inserted into
    private Node NavigateToDirectory(Node navigationNode, string address, bool addFile)
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
                    //addFile flag to determine if this throws an error. If the file is being removed, it's expected that the file already exists
                    if (filename == existingFileInDirectory && addFile == true)
                    {
                        throw new Exception("File already exists in this directory.");
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
                        throw new Exception("Path contains directorie(s) not created in this filesystem.");
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
        if (CheckFilePath(address))
        {
            //Address is valid
        }
        else
        {
            throw new Exception("File not added. Invalid path");
        }

        //Go to the file's directory
        Node navigationNode = NavigateToDirectory(root, address, true);

        //Add file
        if (navigationNode != null)
        {
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
            throw new Exception("File not removed. Invalid path");
        }

        //Go to the file's directory
        Node navigationNode = NavigateToDirectory(root, address, false);

        //Remove file
        if (navigationNode != null)
        {
            string file = Path.GetFileName(address);
            if (navigationNode.file.Contains(file))
            {
                navigationNode.file.Remove(file);
                return true;
            }
            else
            {
                throw new Exception("No file found, cannot remove.");
            }
        }
        return false;
    }

    // Adds a directory at the given address
    // Returns false if the directory already exists or the path is undefined; true otherwise
    public bool AddDirectory(string address)
    {
        string[] nav = address.Split('/'); //navigation array
        if (root.leftMostChild == null) //if first possible location is empty (no non-root directories at all),
        {
            if (nav.Length > 2)
            {
                return false; //failure for case of depth mismatch in initially empty FileSystem
            }
            root.leftMostChild = new Node //place new directory at beginning of previously empty tree.
            {
                directory = nav[1], //at this point correct directory name will always be at index 1 of given address (0 will be empty and nothing beyond 1 should exist)
                file = new List<string>(),
                rightSibling = null,
                leftMostChild = null
            };
            return true; //indicate success and prevent rest of code in method from running
        }
        int i = 1; //navigation index
        Node p = root.leftMostChild; //traversal node, root.leftMostChild should not be null at this point (checked earlier)
        while (i < nav.Length - 1) //repeats until return or until final index reached
        {
            if (p == null) //if p is null here then the traversal node ran off the end of a sibling chain or jumped into an empty but not final child
            {
                return false; //failure for case of desired path not found
            }
            if (p.directory == nav[i]) //if a current point on path found
            {
                i++; //move on to next point
                if (i == nav.Length - 1 && p.leftMostChild == null) //if about to jump into empty child but that child is would-be destination,
                {
                    p.leftMostChild = new Node //make the node there,
                    {
                        directory = nav[i],
                        file = new List<string>(),
                        rightSibling = null,
                        leftMostChild = null
                    };
                    return true; //return true to indicate success and prevent rest of code in method from running.
                }
                else //otherwise just jump into the child like normal
                {
                    p = p.leftMostChild;
                }

            }
            else //if current point on path is not found
            {
                p = p.rightSibling; //keep going right
            }
        }
        //final index reached at this point, i now refers to desired directory name and a duplicate is illegal
        if (p.directory == nav[i]) //if duplicate of desired directory name found at first point
        {
            return false; //failure for case of first point being duplicate
        }
        while (p.rightSibling != null) //looks and moves right until failure or end of line
        {
            if (p.rightSibling.directory == nav[i]) //if duplicate found to right
            {
                return false; //failure for case of right sibling being duplicate
            }
            p = p.rightSibling; //move to next point
        }
        p.rightSibling = new Node //all existing siblings checked for duplicates, right sibling is null at this point
        {
            directory = nav[i],
            file = new List<string>(),
            rightSibling = null,
            leftMostChild = null
        };
        return true; //successful insert
    }

    // Removes the directory (and its subdirectories) at the given address
    // Returns false if the directory is not found or the path is undefined; true otherwise
    public bool RemoveDirectory(string address)
    {
        if (root.leftMostChild != null && address == '/' + root.leftMostChild.directory) //checks first removable directory, if it's the one we want to delete,
        {
            root.leftMostChild = root.leftMostChild.rightSibling; //we delete it by redirecting around it
            return true; //true because we sucessfully removed
        }
        string[] nav = address.Split('/'); //navigation array
        int i = 1; //navigation index
        Node p = root.leftMostChild; //traversal node, set to first directory (already checked so it's "safe")
        while (i < nav.Length - 1) //repeats until return or final index reached
        {
            if (p == null)//if p is null at this point then it ran off the end of a sibling chain or jumped into an empty child 
            {
                return false; //failure for case of path not found
            }
            if (p.directory == nav[i]) //if point on path is found
            {
                i++; //move on to next point
                if (p.leftMostChild != null && i == nav.Length - 1 && p.leftMostChild.directory == nav[i]) //if jumping into child that's would-be desired delete
                {
                    p.leftMostChild = p.leftMostChild.rightSibling; //delete child by redirecting around it
                    return true; //return true to mark success and prevent further code in method from running
                }
                else //otherwise just jump into child like normal
                {
                    p = p.leftMostChild;
                }
            }
            else //if point on path not found this loop
            {
                p = p.rightSibling;
            }
        }
        //final index reached at this point, i now refers to desired deletion name and first instance (should also be only) will be deleted
        while (p.rightSibling != null) //no scenario where first p hasn't already been checked, either caught and dealt with at very beginning or before jumping to it 
        {
            if (p.rightSibling.directory == nav[i]) //if desired delete found
            {
                p.rightSibling = p.rightSibling.rightSibling; //delete by redirecting around
                return true;
            }
        }
        return false; //gets here in the event of a non-exceptional failure, failure for case method ended but did not succeed, (usually because path not found)
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
        if (localRoot.leftMostChild != null) //if child null then count stays at 0
        {
            leftMostChildCount = NumberFiles(localRoot.leftMostChild); //recursively get count of all files connected to leftMostChild
        }
        if (localRoot.rightSibling != null) //if sibling null then count stays 0
        {
            rightSiblingCount = NumberFiles(localRoot.rightSibling); //recursively get count of all files connected to rightSibling
        }
        return count + leftMostChildCount + rightSiblingCount; //return results, keep in mind this is how recursions get back to their initial calls too
    }

    // Prints the directories in a pre-order fashion along with their files
    public void PrintFileSystem()
    {
        Node currentNode = root;
        preorderPrintDirAndFiles(currentNode, 0);
    }

    //Prints the directory name and files in the current directory/node passed, then recursively calls itself for any left-cildren or right-siblings 
    private void preorderPrintDirAndFiles(Node rootDirectory, int indent) {
        Console.WriteLine(new String(' ', indent) + "Directory: " + rootDirectory.directory);
        foreach (var file in rootDirectory.file)
        {
            Console.WriteLine(new String(' ', indent) + "File: " + file);
        }

        //Check for left child
        if (rootDirectory.leftMostChild != null) {
            preorderPrintDirAndFiles(rootDirectory.leftMostChild, indent + 5);
        }

        //Check for right sibling
        if (rootDirectory.rightSibling != null)
        {
            preorderPrintDirAndFiles(rootDirectory.rightSibling, indent);
        }
    }
}

public class Demo
{ //creating comprehensive menu for Main program, current testing space will stay intact and be accessible through the menu -MH
    public static void Main()
    {
        Console.WriteLine("-== File System =--= Authors (Alphabetical): Cole Miller, Jesse Laframboise, Matthew Hellard ==-");
        bool run = true;
        bool valid;
        char Input = 'q'; //set to something or VS complains
        FileSystem filetree = new FileSystem();
        while (run)
        {

            Console.WriteLine("Menu:" +
                "\nReset File System (n)" +
                "\nAdd File ---------(f)" +
                "\nAdd Directory ----(a)" +
                "\nRemove File ------(d)" +
                "\nRemove Directory -(r)" +
                "\nPrint File Count -(c)" +
                "\nPrint File System (p)" +
                "\nUse Test Space ---(t)" +
                "\nQuit -------------(q)");
            valid = false;
            while (valid == false)
            {
                Console.Write("Input :");
                try
                {
                    Input = char.ToLower(Convert.ToChar(Console.ReadLine()));
                    valid = true;
                }
                catch
                {
                    Console.WriteLine("Input error, please try again");
                }
            }
            switch (Input)
            {
                case 'n': //case reset filesystem
                    filetree = new FileSystem();
                    break;

                case 'f': //case add file
                    try
                    {
                        Console.Write("new file path:");
                        valid = filetree.AddFile(Console.ReadLine());
                        if (valid)
                        {
                            Console.WriteLine("File added");
                        }
                        else
                        {
                            Console.WriteLine("Could not add file");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("file add error");
                    }
                    break;

                case 'a': //case add directory
                    try
                    {
                        Console.Write("new directory path:");
                        valid = filetree.AddDirectory(Console.ReadLine());
                        if (valid)
                        {
                            Console.WriteLine("Directory added");
                        }
                        else
                        {
                            Console.WriteLine("Could not add directory");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Directory add error");
                    }
                    break;

                case 'd': //case delete file
                    try
                    {
                        Console.Write("file removal path:");
                        valid = filetree.RemoveFile(Console.ReadLine());
                        if (valid)
                        {
                            Console.WriteLine("file removed");
                        }
                        else
                        {
                            Console.WriteLine("Could not remove file");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("File removal error");
                    }
                    break;

                case 'r': //case delete directory
                    try
                    {
                        Console.Write("Directory removal path:");
                        valid = filetree.RemoveDirectory(Console.ReadLine());
                        if (valid)
                        {
                            Console.WriteLine("Directory removed");
                        }
                        else
                        {
                            Console.WriteLine("Could not remove directory");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("directory removal error");
                    }
                    break;

                case 'c': //case count
                    Console.WriteLine("File count is " + filetree.NumberFiles());
                    Console.WriteLine("Hit enter to return");
                    Console.ReadLine(); //keep filecount open
                    break;

                case 'p': //case print file system whole
                    filetree.PrintFileSystem();
                    Console.WriteLine("\nHit enter to return");
                    Console.ReadLine(); //keep printed filesystem open
                    break;

                case 't': //case original testing space
                          //testing space

                    //Test fileSystem constructor
                    FileSystem testFileSystem = new FileSystem();
                    //testFileSystem.PrintFileSystem();

                    //Test filenames
                    string[] testStrings = { " ", "/", "/.txt", "A", "/A", "/A/FileA", "/A/FileA.txt", "/File>A.txt", "/File/A.,>.txt", "/FileA.txt", "/FileA.txt", "/FileB.txt" };
                    //string[] testStrings = { "/FileA.txt", "/FileA.txt", "/FileB.txt" };
                    foreach (string item in testStrings)
                    {
                        Console.WriteLine("File to add: " + item);
                        testFileSystem.AddFile(item);
                        testFileSystem.PrintFileSystem();
                        Console.WriteLine(" ");
                    }

                    //Test remove file
                    string[] testRemoveStrings = { "/FileA.txt", "/FileA.txt", "/", "/fileasdb.txt" };
                    foreach (string item in testRemoveStrings)
                    {
                        Console.WriteLine("File to remove: " + item);
                        testFileSystem.RemoveFile(item);
                        testFileSystem.PrintFileSystem();
                        Console.WriteLine(" ");
                    }

                    //test NumberFiles method
                    Console.WriteLine("\nCount:" + testFileSystem.NumberFiles());

                    Console.WriteLine("Hit enter to return");
                    Console.ReadLine(); //keeps testing space open

                    //end of testing space
                    break;

                case 'q': //case quit
                    run = false;
                    break;

                default: //gets here if input is char but not valid menu selection
                    Console.WriteLine("Input not recognized");
                    break;
            }
        }
    }
}