using System;

public static class Trees
{
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree();
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        // Problema 5: Crear un árbol balanceado a partir de una lista ordenada
        if (first > last)
        {
            return;
        }
        
        // Calcular el índice medio de manera segura
        int mid = first + (last - first) / 2;
        
        // Insertar el elemento medio
        bst.Insert(sortedNumbers[mid]);
        
        // Llamadas recursivas para las mitades izquierda y derecha
        InsertMiddle(sortedNumbers, first, mid - 1, bst);
        InsertMiddle(sortedNumbers, mid + 1, last, bst);
    }
}
