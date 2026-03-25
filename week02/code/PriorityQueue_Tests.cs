using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class TakingTurnsQueue_Tests
{
    [TestMethod]
    // Scenario: Add one person with 1 turn
    // Expected Result: Person is returned once and then removed
    // Defect(s) Found: Person was not removed correctly after last turn
    public void Test_Turns_OnePerson_OneTurn()
    {
        var queue = new TakingTurnsQueue();

        queue.AddPerson("A", 1);

        Assert.AreEqual("A", queue.GetNextPerson());

        Assert.ThrowsException<InvalidOperationException>(() => queue.GetNextPerson());
    }

    [TestMethod]
    // Scenario: Person with multiple turns should be re-added until turns expire
    // Expected Result: Person appears correct number of times
    // Defect(s) Found: Turns were not decrementing correctly
    public void Test_Turns_MultipleTurns()
    {
        var queue = new TakingTurnsQueue();

        queue.AddPerson("A", 3);

        Assert.AreEqual("A", queue.GetNextPerson());
        Assert.AreEqual("A", queue.GetNextPerson());
        Assert.AreEqual("A", queue.GetNextPerson());

        Assert.ThrowsException<InvalidOperationException>(() => queue.GetNextPerson());
    }

    [TestMethod]
    // Scenario: Multiple people rotate in queue
    // Expected Result: FIFO rotation with turns respected
    // Defect(s) Found: Queue order was not maintained correctly
    public void Test_Turns_MultiplePeople()
    {
        var queue = new TakingTurnsQueue();

        queue.AddPerson("A", 2);
        queue.AddPerson("B", 2);

        Assert.AreEqual("A", queue.GetNextPerson());
        Assert.AreEqual("B", queue.GetNextPerson());
        Assert.AreEqual("A", queue.GetNextPerson());
        Assert.AreEqual("B", queue.GetNextPerson());

        Assert.ThrowsException<InvalidOperationException>(() => queue.GetNextPerson());
    }

    [TestMethod]
    // Scenario: Person with infinite turns (0 or less)
    // Expected Result: Person always returns and never leaves queue
    // Defect(s) Found: Infinite turns were not handled correctly
    public void Test_Turns_Infinite()
    {
        var queue = new TakingTurnsQueue();

        queue.AddPerson("A", 0);

        Assert.AreEqual("A", queue.GetNextPerson());
        Assert.AreEqual("A", queue.GetNextPerson());
        Assert.AreEqual("A", queue.GetNextPerson());
    }

    [TestMethod]
    // Scenario: Empty queue
    // Expected Result: Exception thrown
    // Defect(s) Found: Queue did not throw exception when empty
    public void Test_Turns_EmptyQueue()
    {
        var queue = new TakingTurnsQueue();

        Assert.ThrowsException<InvalidOperationException>(() => queue.GetNextPerson());
    }
}