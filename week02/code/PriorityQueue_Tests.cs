using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities and dequeue them.
    // Expected Result: Items should be dequeued in descending order of priority (highest first).
    // Defect(s) Found: 
    // 1. Loop condition was `index < _queue.Count - 1`, missing the last element. Fixed to `index < _queue.Count`.
    // 2. Condition was `>=` which broke FIFO for same priorities. Fixed to `>`.
    // 3. The dequeued item was not removed from the list. Fixed by adding `_queue.RemoveAt(highPriorityIndex)`.
    public void TestPriorityQueue_HighestPriorityFirst()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("Low", 1);
        priorityQueue.Enqueue("High", 10);
        priorityQueue.Enqueue("Medium", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
        Assert.AreEqual("Medium", priorityQueue.Dequeue());
        Assert.AreEqual("Low", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add multiple items with the SAME priority.
    // Expected Result: Items should be dequeued in FIFO order (the first one added is removed first).
    // Defect(s) Found: See TestPriorityQueue_HighestPriorityFirst (Fixed `>=` to `>` to preserve FIFO order).
    public void TestPriorityQueue_SamePriorityFIFO()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("First", 5);
        priorityQueue.Enqueue("Second", 5);
        priorityQueue.Enqueue("Third", 5);

        Assert.AreEqual("First", priorityQueue.Dequeue());
        Assert.AreEqual("Second", priorityQueue.Dequeue());
        Assert.AreEqual("Third", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: An InvalidOperationException with the message "The queue is empty." should be thrown.
    // Defect(s) Found: None. The original code already handled this requirement correctly.
    public void TestPriorityQueue_EmptyQueueThrowsException()
    {
        var priorityQueue = new PriorityQueue();
        
        var exception = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", exception.Message);
    }
}
