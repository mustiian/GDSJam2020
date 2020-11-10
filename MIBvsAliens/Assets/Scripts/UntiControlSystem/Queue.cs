using System.Collections.Generic;

namespace DataStructures
{

    public class Queue<T>
    {
        private readonly LinkedList<T> _internalList = new LinkedList<T>();
        
        public void Enqueue(T value)
        {
            _internalList.AddLast(value);
        }

        public bool TryRemove(T value)
        {
            var valToDelete = _internalList.Find(value);
            if (valToDelete == null)
                return false;

            _internalList.Remove(valToDelete);
            return true;
        }

        public bool IsEmpty()
        {
            return _internalList.Count == 0;
        }

        public bool TryDequeue(out T val)
        {
            if (!IsEmpty())
            {
                val = Dequeue();
                return true;
            }

            val = default;
            return false;
        }
        
        private T Dequeue()
        {
            var first = _internalList.First;
            _internalList.RemoveFirst();
            return first.Value;
        }
    }
}

