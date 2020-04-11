using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using StudentApp.Common;

namespace StudentApp.List
{
    public class CustomLinkedList<T> : ICloneable, IEnumerable<T>
        where T : IComparable<T>, ICloneable
    {
        protected ListNode<T> CurrentNode;
        protected ListNode<T> Head;
        protected ListNode<T> Tail;

        public CustomLinkedList()
        {
        }

        public CustomLinkedList(CustomLinkedList<T> source)
        {
            var node = source.Head;

            for (var i = 0; i < source.Size; i++)
            {
                var clonedData = (T) node.Data.Clone();
                PushToEnd(clonedData);

                if (ReferenceEquals(node, source.CurrentNode)) MoveToTail();

                node = node.NextNode;
            }
        }

        public int Size { get; protected set; }

        public T Current => ReferenceEquals(null, CurrentNode) ? default : CurrentNode.Data;

        public virtual object Clone()
        {
            return new CustomLinkedList<T>(this);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>) GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            var node = Head;
            for (var i = 0; i < Size; i++)
            {
                yield return node.Data;
                node = node.NextNode;
            }
        }

        public void Sort()
        {
            var aNode = Head;
            for (var i = 0; i < Size; i++)
            {
                var bNode = Tail;
                for (var j = 0; j < Size - i; j++)
                {
                    if (aNode.Data.CompareTo(bNode.Data) > 0) SwapNodes(aNode, bNode);

                    bNode = bNode.PrevNode;
                }

                aNode = aNode.NextNode;
            }
        }

        private void SwapNodes(ListNode<T> aNode, ListNode<T> bNode)
        {
            var tmp = aNode.Data;
            aNode.Data = bNode.Data;
            bNode.Data = tmp;

            if (ReferenceEquals(CurrentNode, aNode)) CurrentNode = bNode;

            if (ReferenceEquals(CurrentNode, bNode)) CurrentNode = bNode;
        }

        public void DeleteCurrent()
        {
            if (ReferenceEquals(CurrentNode, null)) return;
            DeleteNode(CurrentNode);
        }

        private void DeleteNode(ListNode<T> node)
        {
            var prev = node.PrevNode;
            var next = node.NextNode;

            prev.NextNode = next;
            next.PrevNode = prev;
            Size -= 1;

            if (ReferenceEquals(node, CurrentNode)) CurrentNode = next;
        }

        public void MoveToHead()
        {
            CurrentNode = Head;
        }

        public void MoveToTail()
        {
            CurrentNode = Tail;
        }

        public static CustomLinkedList<T> operator ++(CustomLinkedList<T> list)
        {
            list.CurrentNode = list.CurrentNode.NextNode;
            return list;
        }

        public static CustomLinkedList<T> operator --(CustomLinkedList<T> list)
        {
            list.CurrentNode = list.CurrentNode.PrevNode;
            return list;
        }

        public static bool operator !(CustomLinkedList<T> list)
        {
            return list.Size != 0;
        }

        public void PushToEnd(T data)
        {
            Guard.NotNull(data, nameof(data));

            var node = new ListNode<T>(data);

            if (ReferenceEquals(Head, null))
            {
                InsertFirstNode(node);
                return;
            }

            Tail.NextNode = node;
            node.PrevNode = Tail;
            node.NextNode = Head;
            Head.PrevNode = node;

            Tail = node;
            Size += 1;
        }

        public void AddAll(params T[] data)
        {
            foreach (var datum in data) PushToEnd(datum);
        }

        public void PushToStart(T data)
        {
            Guard.NotNull(data, nameof(data));

            var node = new ListNode<T>(data);

            if (ReferenceEquals(Head, null))
            {
                InsertFirstNode(node);
                return;
            }

            Head.PrevNode = node;
            node.NextNode = Head;
            node.PrevNode = Tail;
            Tail.NextNode = node;

            Head = node;
            Size += 1;
        }

        private void InsertFirstNode(ListNode<T> node)
        {
            node.NextNode = node;
            node.PrevNode = node;

            Head = node;
            Tail = node;

            Size = 1;
            CurrentNode = Head;
        }

        public T Get(int index)
        {
            return GetNode(index).Data;
        }

        private ListNode<T> GetNode(int index)
        {
            if (index < 0 || index >= Size) throw new IndexOutOfRangeException();
            ListNode<T> node;


            if (Size / 2 - index >= 0)
            {
                node = Head;
                for (var i = 0; i < index; i++) node = node.NextNode;
            }
            else
            {
                node = Tail;
                for (var i = 0; i < Size - 1 - index; i++) node = node.PrevNode;
            }

            return node;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            foreach (var data in this)
            {
                strBuilder.Append(data);
                strBuilder.Append("->");
            }

            return strBuilder.ToString();
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            CurrentNode = null;
            Size = 0;
        }

        public void Put(int index, T data)
        {
            var currNode = index == Size ? Head : GetNode(index);

            var node = new ListNode<T>(data);

            var prev = currNode.PrevNode;
            prev.NextNode = node;
            node.PrevNode = prev;
            node.NextNode = currNode;
            currNode.PrevNode = node;

            Size += 1;

            if (index == 0) Head = node;
            if (index == Size) Tail = node;
        }

        public void Delete(T data)
        {
            var node = Head;
            var sizeBeforeDeletion = Size;
            for (var i = 0; i < sizeBeforeDeletion; i++)
            {
                if (Equals(node.Data, data)) DeleteNode(node);

                node = node.NextNode;
            }
        }

        public void SortCurrent()
        {
            if (!ReferenceEquals(CurrentNode, Head) && CurrentNode.Data.CompareTo(CurrentNode.PrevNode.Data) < 0)
            {
                SwapNodes(CurrentNode, CurrentNode.PrevNode);
                SortCurrent();
            }

            if (!ReferenceEquals(CurrentNode, Tail) && CurrentNode.Data.CompareTo(CurrentNode.NextNode.Data) > 0)
            {
                SwapNodes(CurrentNode, CurrentNode.NextNode);
                SortCurrent();
            }
        }

        protected internal sealed class ListNode<TR>
        {
            public ListNode(TR data)
            {
                Data = data;
            }

            public TR Data { get; protected internal set; }

            protected internal ListNode<TR> NextNode { get; set; }
            protected internal ListNode<TR> PrevNode { get; set; }

            public override string ToString()
            {
                return $"[Node with data: {Data}]";
            }
        }
    }
}