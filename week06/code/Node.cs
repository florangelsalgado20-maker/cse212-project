using System;

public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // Problema 1: Prevenir la inserción de valores duplicados
        if (value == Data)
        {
            return;
        }
        else if (value < Data)
        {
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else
        {
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // Problema 2: Verificar si el valor existe en el árbol
        if (value == Data)
        {
            return true;
        }
        else if (value < Data && Left is not null)
        {
            return Left.Contains(value);
        }
        else if (value > Data && Right is not null)
        {
            return Right.Contains(value);
        }
        
        return false;
    }

    public int GetHeight()
    {
        // Problema 4: Calcular la altura del árbol de forma recursiva
        int leftHeight = Left?.GetHeight() ?? 0;
        int rightHeight = Right?.GetHeight() ?? 0;
        
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}
