using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace s71Challenge
{
    class QueueContainer
    {
        Queue<string> queue = new Queue<string>(); // TODO explain that Queue<T> loses functions

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
                    // TODO explain in readme why exception over protection.
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
            Thread.Sleep(waitDurationSeconds * 1000); // TOOD explain shortfall of omitting multithreading here.
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
                newQueue.Enqueue(value); // TODO explain the problem with multithreading queue additions like this while also copying a new queue. Maybe would introduce a lock. Also explain why using a blockingcollection might be better here too.
            }
            queue = newQueue;
        }

        /// <summary>
        /// Deletes the given messages from the queue. This function should be called to confirm the successful handling of messages returned by the pop function.
        /// </summary>
        /// <param name="values"></param>
        public void confirm(List<string> values)
        {
            // TODO explain that this felt like a replacement for a native c# function, but was meant to be implemented as a clojure function with augmented functionality. "This was an awkward function to fill in, I believe because this is in c#. What if my queue had duplicate values? What if I was multithreading and new values were put in as I was checking the deletes?"
            // TODO explain that I would probably begin custom building a queue from the ground up that was accessible via static pointers to confirm that a value at a specific position was removed.
            // TODO explain challenge of using c# queue instead of custom queue because a message cannot be deleted from the middle of a queue, making this method redundant. This method is also redundant because .DeQueue is garunteed to work inless the index position was incorrect.
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
                        if (values[i] == queue.ElementAt(i)) // TODO explain challenges of multithreading regarding this check as well.
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
