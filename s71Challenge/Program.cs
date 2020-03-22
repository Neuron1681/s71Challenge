using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace s71Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            QueueContainer queue = new QueueContainer();
            List<string> insertValues = new List<string> { "a", "b" };

            // To demonstrate values were added: 
            queue.Push(insertValues);
            List<string> peekedValues = queue.Peek(2);
            Console.WriteLine("Queue values:");
            foreach(string value in peekedValues)
            {
                Console.WriteLine(value);
            }

            // To demonstrate pop and peek:
            queue.Pop(1, 1);
            peekedValues = queue.Peek(2);
            Console.WriteLine("\r\nValues peeked from queue:");
            foreach (string value in peekedValues)
            {
                Console.WriteLine(value);
            }

            // To demonstrate sql insert:
            MySqlSender mySqlSender = new MySqlSender();
            foreach(string value in queue.Pop(2))
            {
                mySqlSender.InsertToMySql(value);
            }
            Console.WriteLine("\r\nDatabase contents:");
            mySqlSender.WriteDbContentsToConsole();
        }
    }
}
