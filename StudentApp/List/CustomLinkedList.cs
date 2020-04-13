using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using StudentApp.Common;

namespace StudentApp.List
{
    public class CustomLinkedList<T> : ICloneable, IEnumerable<T>, IComparable<CustomLinkedList<T>>
        where T : IComparable<T>, ICloneable
    {
        protected ListNode<T> CurrentNode;
        protected ListNode<T> Head;

        public CustomLinkedList()
        {
        }

        public CustomLinkedList(CustomLinkedList<T> source)
        {
            Guard.NotNull(source, nameof(source));

            var node = source.Head;

            for (var i = 0; i < source.Size; i++)
            {
                var clonedData = (T) node.Data.Clone();
                PushToEnd(clonedData);

                if (ReferenceEquals(node, source.CurrentNode)) MoveToTail();

                node = node.NextNode;
            }
        }

        protected ListNode<T> Tail => Head?.PrevNode;

        public int Size { get; protected set; }

        public T Current => ReferenceEquals(null, CurrentNode) ? default : CurrentNode.Data;

        public virtual object Clone()
        {
            return new CustomLinkedList<T>(this);
        }

        public int CompareTo(CustomLinkedList<T> other)
        {
            if (ReferenceEquals(other, null)) return -1;
            return other.Size - Size;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            var enumerator = GetEnumerator();
            while (enumerator.MoveNext()) yield return (T) enumerator.Current;
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
            Tail.NextNode = null;
            Head.PrevNode = null;
            Head = MergeSort(Head);

            var tail = Head;
            while (!ReferenceEquals(tail.NextNode, null)) tail = tail.NextNode;

            Head.PrevNode = tail;
            tail.NextNode = Head;
        }

        private ListNode<T> MergeSort(ListNode<T> node)
        {
            if (ReferenceEquals(node.NextNode, null)) return node;

            var second = Split(node);

            node = MergeSort(node);
            second = MergeSort(second);

            return Merge(node, second);
        }

        private ListNode<T> Merge(ListNode<T> first, ListNode<T> second)
        {
            if (ReferenceEquals(first, null)) return second;

            if (ReferenceEquals(second, null)) return first;

            if (first.Data.CompareTo(second.Data) < 0)
            {
                first.NextNode = Merge(first.NextNode, second);
                first.NextNode.PrevNode = first;
                first.PrevNode = null;
                return first;
            }

            second.NextNode = Merge(first, second.NextNode);
            second.NextNode.PrevNode = second;
            second.PrevNode = null;
            return second;
        }

        private ListNode<T> Split(ListNode<T> head)
        {
            var fast = head;
            var slow = head;

            while (!ReferenceEquals(fast?.NextNode?.NextNode, null))
            {
                fast = fast.NextNode.NextNode;
                slow = slow.NextNode;
            }

            var temp = slow.NextNode;
            slow.NextNode = null;
            return temp;
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
            if (ReferenceEquals(node, Head)) Head = next;
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
            if (ReferenceEquals(list.CurrentNode, null)) return list;
            list.CurrentNode = list.CurrentNode.NextNode;
            return list;
        }

        public static CustomLinkedList<T> operator --(CustomLinkedList<T> list)
        {
            if (ReferenceEquals(list.CurrentNode, null)) return list;
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

            node.NextNode = Head;
            node.PrevNode = Tail;
            Tail.NextNode = node;
            Head.PrevNode = node;

            Head = node;
            Size += 1;
        }

        private void InsertFirstNode(ListNode<T> node)
        {
            node.NextNode = node;
            node.PrevNode = node;

            Head = node;

            Size = 1;
            CurrentNode = Head;
        }

        public T Get(uint index)
        {
            return GetNode(index).Data;
        }

        private ListNode<T> GetNode(uint index)
        {
            if (index >= Size) throw new IndexOutOfRangeException();
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
            CurrentNode = null;
            Size = 0;
        }

        public void Put(uint index, T data)
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

        public void DeleteFirst(T data)
        {
            var node = Head;
            var sizeBeforeDeletion = Size;
            for (var i = 0; i < sizeBeforeDeletion; i++)
            {
                if (Equals(node.Data, data))
                {
                    DeleteNode(node);
                    return;
                }

                node = node.NextNode;
            }
        }

        public void DeleteLast(T data)
        {
            var node = Tail;
            var sizeBeforeDeletion = Size;
            for (var i = 0; i < sizeBeforeDeletion; i++)
            {
                if (Equals(node.Data, data))
                {
                    DeleteNode(node);
                    return;
                }

                node = node.PrevNode;
            }
        }

        public void SortCurrent()
        {
            var node = AddSort(Current);
            DeleteNode(CurrentNode);
            CurrentNode = node;
        }

        private ListNode<T> AddSort(T data)
        {
            var node = Tail;
            for (var i = 0; i < Size; i++)
            {
                if (data.CompareTo(node.Data) > 0) break;

                node = node.PrevNode;
            }

            var newNode = new ListNode<T>(data);

            var next = node.NextNode;
            next.PrevNode = newNode;
            newNode.NextNode = next;


            newNode.PrevNode = node;
            node.NextNode = newNode;

            return newNode;
        }

        protected internal sealed class ListNode<TR>
        {
            public ListNode(TR data)
            {
                Data = data;
            }

            public TR Data { get; }

            protected internal ListNode<TR> NextNode { get; set; }
            protected internal ListNode<TR> PrevNode { get; set; }

            public override string ToString()
            {
                return $"[Node with data: {Data}]";
            }
        }
    }
}