using System;
using System.Collections.Generic;

namespace Assets.Scripts.Collections
{
    /// <summary>
    /// An enumerator that endlessly rotates back to the front of the list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RotatingEnumerator<T>
    {
        private readonly List<T> _innerList;
        private int _index;

        public RotatingEnumerator(List<T> innerList)
        {
            _innerList = innerList;
            _index = -1;
        }

        public T Next()
        {
            if (_innerList == null || _innerList.IsEmpty())
            {
                throw new Exception("Can't iterate to the next item if the list is empty.");
            }
            _index++;
            if (_index >= _innerList.Count)
            {
                _index = 0;
            }
            return _innerList[_index];
        }
    }
}