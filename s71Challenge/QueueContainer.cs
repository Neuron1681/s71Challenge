using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace s71Challenge
{
    class QueueContainer
    {
        Queue<string> queue = new Queue<string>();

        /// <summary>
        /// Pushes the given messages to the queue.
        /// </summary>
        /// <param name="values"></param>
        /// <returns>A list of booleans indicating whether or not each message was successfully added to the queue.</returns>
        public List<bool> Push(List<string> values)
        {
            List<bool> wasValueAdded = new List<bool>();
            foreach (var value in values)
            {
                try
                {
                    queue.Enqueue(value);
                    wasValueAdded.Add(true);
                }
                catch (Exception)
                {
                    wasValueAdded.Add(false);
                }
            }
            return wasValueAdded;
        }

        /// <summary>
        /// Returns one or more messages from the queue. Messages are still visible after this call is made
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        public List<string> Peek(int limit = 1)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < limit; ++i)
            {
                try
                {
                    values.Add(queue.ElementAt(i));
                }
                catch (Exception)
                {
                    throw new Exception("The queue is not that big!");
                }
            }
            return values;
        }

        /// <summary>
        /// Returns one or more messages from the queue. Messages are hidden for the duration(in sec) specified by the required ttl arg, after which they return to the front of the queue.
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="durationOfWait"></param>
        /// <returns></returns>
        public List<string> Pop(int limit, int waitDurationSeconds)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < limit; ++i)
            {
                values.Add(queue.Dequeue());
            }
            confirm(values);
            Thread.Sleep(waitDurationSeconds * 1000);
            PushToFront(values);
            return values;
        }

        /// <summary>
        /// Returns one or many messages from the queue.
        /// </summary>
        /// <param name="numberOfValues"></param>
        /// <returns></returns>
        public List<string> Pop(int numberOfValues)
        {
            List<string> values = new List<string>();
            for (int i = 0; i < numberOfValues; ++i)
            {
                values.Add(queue.Dequeue());
            }
            return values;
        }

        /// <summary>
        /// Places values at the front of the queue
        /// </summary>
        /// <param name="values"></param>
        private void PushToFront(List<string> values)
        {
            Queue<string> newQueue = new Queue<string>();
            foreach (string value in values)
            {
                newQueue.Enqueue(value);
            }
            foreach (string value in queue)
            {
                newQueue.Enqueue(value);
            }
            queue = newQueue;
        }

        /// <summary>
        /// Deletes the given messages from the queue. This function should be called to confirm the successful handling of messages returned by the pop function.
        /// </summary>
        /// <param name="values"></param>
        public void confirm(List<string> values)
        {
            for (int i = 0; i < values.Count(); ++i)
            {
                // Because this finction is built to be called directly 
                // after values were popped, we can assume that the 
                // values in the parameterized list correspond directly
                // to their previous position within the queue.
                try
                {
                    // We must handle the condition where the pop function reduced the queue size to less than that of the list of values being checked
                    if (i > queue.Count() - 1)
                    {
                        // if we're accessing an element outside of the queue size, then (due to the fact that this is called after pop()), we can assume the element was removed.
                        break;
                    }
                    else
                    {
                        if (values[i] == queue.ElementAt(i)) 
                        {
                            // Since a queue won't allow us to remove an element from a position that is not i = 0, we'll have to generate a new queue and omit the value.
                            Queue<string> newQueue = new Queue<string>();
                            for (int k = 0; k < queue.Count; ++k)
                            {
                                if (k == i)
                                    continue;
                                newQueue.Enqueue(queue.ElementAt(k));
                            }
                            queue = newQueue;
                        } 
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    throw new Exception("Attempted to access an element outside the size of the queue!");
                }
            }
        }

        /// <summary>
        /// Returns a count of the number of messages on the queue.
        /// </summary>
        /// <returns></returns>
        public int QueueLength()
        {
            return queue.Count();
        }
    }
}
