using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList : IEnumerable
{
    private Node? _head;
    private Node? _tail;

    /// <summary>
    /// Insert a new node at the front (i.e. the head) of the linked list.
    /// </summary>
    public void InsertHead(int value)
    {
        Node newNode = new(value);
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Next = _head;
            _head.Prev = newNode;
            _head = newNode;
        }
    }

    /// <summary>
    /// Problem 1: Insert a new node at the back (i.e. the tail) of the linked list.
    /// Complexity: O(1)
    /// </summary>
    public void InsertTail(int value)
    {
        Node newNode = new(value);
        if (_head is null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            newNode.Prev = _tail;
            _tail.Next = newNode;
            _tail = newNode;
        }
    }

    /// <summary>
    /// Remove the first node (i.e. the head) of the linked list.
    /// </summary>
    public void RemoveHead()
    {
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_head is not null)
        {
            _head.Next!.Prev = null;
            _head = _head.Next;
        }
    }

    /// <summary>
    /// Problem 2: Remove the last node (i.e. the tail) of the linked list.
    /// Complexity: O(1)
    /// </summary>
    public void RemoveTail()
    {
        if (_head == _tail)
        {
            _head = null;
            _tail = null;
        }
        else if (_tail is not null)
        {
            _tail.Prev!.Next = null;
            _tail = _tail.Prev;
        }
    }

    /// <summary>
    /// Insert 'newValue' after the first occurrence of 'value' in the linked list.
    /// </summary>
    public void InsertAfter(int value, int newValue)
    {
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _tail)
                {
                    InsertTail(newValue);
                }
                else
                {
                    Node newNode = new(newValue);
                    newNode.Prev = curr;
                    newNode.Next = curr.Next;
                    curr.Next!.Prev = newNode;
                    curr.Next = newNode;
                }
                return;
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Problem 3: Remove the first node that contains 'value'.
    /// Complexity: O(n) in the worst case.
    /// </summary>
    public void Remove(int value)
    {
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == value)
            {
                if (curr == _head)
                {
                    RemoveHead();
                }
                else if (curr == _tail)
                {
                    RemoveTail();
                }
                else
                {
                    curr.Prev!.Next = curr.Next;
                    curr.Next!.Prev = curr.Prev;
                }
                return; // Exit immediately after the first match is removed
            }
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Problem 4: Search for all instances of 'oldValue' and replace the value to 'newValue'.
    /// Complexity: O(n), as it must inspect every node.
    /// </summary>
    public void Replace(int oldValue, int newValue)
    {
        Node? curr = _head;
        while (curr is not null)
        {
            if (curr.Data == oldValue)
            {
                curr.Data = newValue;
            }
            curr = curr.Next; // Continue searching to replace all instances
        }
    }

    /// <summary>
    /// Explicit implementation of the IEnumerable interface.
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    /// <summary>
    /// Iterate forward through the Linked List.
    /// </summary>
    public IEnumerator GetEnumerator()
    {
        var curr = _head;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Next;
        }
    }

    /// <summary>
    /// Problem 5: Iterate backward through the Linked List.
    /// Complexity: O(1) per yielded item, O(n) overall.
    /// </summary>
    public IEnumerable Reverse()
    {
        var curr = _tail;
        while (curr is not null)
        {
            yield return curr.Data;
            curr = curr.Prev;
        }
    }

    public override string ToString()
    {
        return "{" + string.Join(", ", this) + "}";
    }

    public Boolean HeadAndTailAreNull()
    {
        return _head is null && _tail is null;
    }

    public Boolean HeadAndTailAreNotNull()
    {
        return _head is not null && _tail is not null;
    }
}

// NOTE: If your repository already has the Node class defined in a separate file (Node.cs), 
// DELETE the following class definition to avoid a duplicate class compilation error. 
// If your template did not have a separate Node.cs file, leave it here.
public class Node
{
    public int Data { get; set; }
    public Node? Next { get; set; }
    public Node? Prev { get; set; }

    public Node(int data)
    {
        Data = data;
        Next = null;
        Prev = null;
    }
}
