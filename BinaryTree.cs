using System;
using System.Collections.Generic;
using System.Text;

namespace BinaryTreeImplementation
{
    /// <summary>
    /// The node class used by the Binary Tree
    /// </summary>
    public class Node
    {
        public Node leftNode = null;
        public Node rightNode = null;
        public Node parentNode = null;
        public int value;
        public bool hasChildren 
        {
            get
            {
                if (leftNode == null && rightNode == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public Node(int value)
        {
            this.value = value;
            this.leftNode = null;
            this.rightNode = null;
        }

        public static Node Zero()
        {
            return new Node(0);
        }
    }

    public class BinaryTree 
    {
        /// <summary>
        /// The root node of the tree
        /// </summary>
        public Node root;

        /// <summary>
        /// The depth of the tree
        /// </summary>
        public int depth
        {
            get
            {
                return maxDepth(root);
            }
        }

        private enum Direction
        {
            left = 0,
            right = 1,
            both = 2
        }

        /// <summary>
        /// Creates a new binary tree and assigns a value to the root node
        /// </summary>
        /// <param name="value"></param>
        public BinaryTree(int value)
        {
            root = new Node(value);
        }

        /// <summary>
        /// Adds a new node with an assigned value to a parent node
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [Obsolete]
        public Node Add(Node parentNode, int value)
        {
            if (parentNode == null) // Sanity Check
            {
                return Node.Zero();
            }

            if (value >= parentNode.value && parentNode.rightNode == null)
            {
                Node newNode = new Node(value);
                newNode.parentNode = parentNode;
                parentNode.rightNode = newNode;
                return parentNode.rightNode;
            } else if (value >= parentNode.value && parentNode.rightNode != null)
            {
                Add(parentNode.rightNode, value);
            }
            else if (value < parentNode.value && parentNode.leftNode == null)
            {
                Node newNode = new Node(value);
                newNode.parentNode = parentNode;
                parentNode.leftNode = newNode;
                return parentNode.leftNode;
            } else if (value < parentNode.value && parentNode.leftNode != null)
            {
                Add(parentNode.leftNode, value);
            }
            return Node.Zero();
        }

        /// <summary>
        /// Inserts a value into the tree at its correct place
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node Insert(int value)
        {
#pragma warning disable CS0612 // Type or member is obsolete
            return Add(root, value);
#pragma warning restore CS0612 // Type or member is obsolete
        }

        /// <summary>
        /// Returns a list of all the sub-nodes of the parent node
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public List<Node> getChildren(Node parentNode)
        {
            List<Node> nodes = new List<Node>();
            if (parentNode.hasChildren)
            {
                if (parentNode.rightNode != null)
                {
                    nodes.Add(parentNode.rightNode);
                    if (parentNode.rightNode.hasChildren)
                    {
                        foreach (Node node in getChildren(parentNode.rightNode))
                        {
                            nodes.Add(node);
                        }
                    }
                }
                if (parentNode.leftNode != null)
                {
                    nodes.Add(parentNode.leftNode);
                    if (parentNode.leftNode.hasChildren)
                    {
                        foreach (Node node in getChildren(parentNode.leftNode))
                        {
                            nodes.Add(node);
                        }
                    }
                }
            }
            return nodes;
        }

        /// <summary>
        /// Returns a list of all the nodes in the tree
        /// </summary>
        /// <returns></returns>
        public List<Node> getEntireTree()
        {
            List<Node> nodes = new List<Node>();
            nodes.Add(root);
            foreach (Node node in getChildren(root))
            {
                nodes.Add(node);
            }
            return nodes;
        }

        private int maxDepth(Node startNode)
        {
            int rightDepth = 0;
            int leftDepth = 0;
            // Recursively find the depth of each subtree.
            if (startNode.rightNode != null)
            {
                rightDepth = maxDepth(startNode.rightNode);
            }
            if (startNode.leftNode != null)
            {
                leftDepth = maxDepth(startNode.leftNode);
            }
            // Get the larger depth and add 1 to it to
            // account for the root.
            if (leftDepth > rightDepth)
                return (leftDepth + 1);
            else
                return (rightDepth + 1);
        }

        /// <summary>
        /// If the node provided exists in the tree it will be removed
        /// </summary>
        /// <param name="node"></param>
        public void Remove(Node node)
        {
            if (root == node)
            {
                throw new Exception("Cannot remove the root of the tree");
            }
            if (root.hasChildren)
            {
                foreach(Node myNode in getEntireTree())
                {
                    if (myNode.hasChildren)
                    {
                        if (myNode.rightNode == node)
                        {
                            if (myNode.rightNode.hasChildren)
                            {
                                List<Node> subNodes = getChildren(myNode.rightNode);
                                myNode.rightNode = null;
                                foreach (Node childNode in subNodes)
                                {
                                    Insert(childNode.value);
                                }
                                return;
                            }
                        }
                        if (myNode.leftNode == node)
                        {
                            if (myNode.leftNode.hasChildren)
                            {
                                List<Node> subNodes = getChildren(myNode.leftNode);
                                myNode.leftNode = null;
                                foreach (Node childNode in subNodes)
                                {
                                    childNode.parentNode = null;
                                    Insert(childNode.value);
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// If a node with the specified value exists it is removed
        /// </summary>
        /// <param name="value"></param>
        public void Remove(int value)
        {
            if (root.value == value)
            {
                throw new Exception("Cannot remove the root of the tree");
            }
            if (root.hasChildren)
            {
                foreach (Node myNode in getEntireTree())
                {
                    if (myNode.hasChildren)
                    {
                        if (myNode.rightNode != null && myNode.rightNode.value == value)
                        {
                            if (myNode.rightNode.hasChildren)
                            {
                                List<Node> subNodes = getChildren(myNode.rightNode);
                                myNode.rightNode = null;
                                foreach (Node childNode in subNodes)
                                {
                                    Insert(childNode.value);
                                }
                                return;
                            }
                        }
                        if (myNode.leftNode != null && myNode.leftNode.value == value)
                        {
                            if (myNode.leftNode.hasChildren)
                            {
                                List<Node> subNodes = getChildren(myNode.leftNode);
                                myNode.leftNode = null;
                                foreach (Node childNode in subNodes)
                                {
                                    childNode.parentNode = null;
                                    Insert(childNode.value);
                                }
                                return;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Returns true if a node exists
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public bool Exists(Node node)
        {
            foreach(Node myNode in getEntireTree())
            {
                if (myNode == node)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if a node with the specified value exist
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Exists(int value)
        {
            foreach (Node myNode in getEntireTree())
            {
                if (myNode.value == value)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns the first node that has the specified value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public Node Find(int value)
        {
            Node currentNode = root;
            while (currentNode.value != value)
            {
                Console.WriteLine("--" + currentNode.value);
                if (currentNode.hasChildren)
                {
                    if (currentNode.rightNode != null && value >= currentNode.rightNode.value)
                    {
                        currentNode = currentNode.rightNode;
                    }
                    else if (currentNode.leftNode != null)
                    {
                        currentNode = currentNode.leftNode;
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            if (currentNode.value == value)
            {
                return currentNode;
            }
            else
            {
                return Node.Zero();
            }
        }
    }
}
