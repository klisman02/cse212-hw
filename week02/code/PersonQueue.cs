using System;
using System.Collections.Generic;

public class TakingTurnsQueue
{
    private Queue<Person> _queue = new();

    public void AddPerson(string name, int turns)
    {
        _queue.Enqueue(new Person(name, turns));
    }

    public string GetNextPerson()
    {
        if (_queue.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty.");
        }

        var person = _queue.Dequeue();

        // Se tiver turnos infinitos (0 ou menos), volta pra fila
        if (person.Turns <= 0)
        {
            _queue.Enqueue(person);
        }
        else
        {
            // decrementa turnos
            person.Turns--;

            // se ainda tiver turnos, volta pra fila
            if (person.Turns > 0)
            {
                _queue.Enqueue(person);
            }
        }

        return person.Name;
    }
}

public class Person
{
    public string Name { get; set; }
    public int Turns { get; set; }

    public Person(string name, int turns)
    {
        Name = name;
        Turns = turns;
    }
}