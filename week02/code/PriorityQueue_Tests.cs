using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities and remove the highest priority.
    // Expected Result: The item with the highest priority should be returned.
    // Defect(s) Found: Original code did not scan the entire queue and did not remove the item from the list.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("B", result);
    }

    [TestMethod]
    // Scenario: Multiple items with the same priority should follow FIFO order.
    // Expected Result: The first inserted item should be removed first.
    // Defect(s) Found: Using >= caused the last item with the same priority to be removed instead of the first.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 5);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 5);

        var result = priorityQueue.Dequeue();

        Assert.AreEqual("A", result);
    }

    [TestMethod]
    // Scenario: Attempt to dequeue from an empty queue.
    // Expected Result: InvalidOperationException should be thrown with message "The queue is empty."
    // Defect(s) Found: Works correctly once queue logic is fixed.
    public void TestPriorityQueue_EmptyQueue()
    {
        var priorityQueue = new PriorityQueue();

        Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Ensure items are actually removed after dequeue.
    // Expected Result: Second dequeue returns the next highest priority item.
    // Defect(s) Found: Original code did not remove the item from the queue.
    public void TestPriorityQueue_Removal()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);
        priorityQueue.Enqueue("B", 5);
        priorityQueue.Enqueue("C", 3);

        priorityQueue.Dequeue();
        var result = priorityQueue.Dequeue();

        Assert.AreEqual("C", result);
    }
}