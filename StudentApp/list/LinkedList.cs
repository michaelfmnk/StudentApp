using System;
using System.Text;

namespace StudentApp.list
{
    public class LinkedList<T> : ICloneable where T : IComparable<T>, ICloneable
    {
        protected ListNode<T> CurrentNode;
        protected ListNode<T> Head;
        protected ListNode<T> Tail;
        public int Size { get; protected set; }

        public T Current => ReferenceEquals(null, CurrentNode) ? default : CurrentNode.Data;

        public object Clone()
        {
            var clonedList = new LinkedList<T>();

            var node = Head;
            for (var i = 0; i < Size; i++)
            {
                var clonedData = (T) node.Data.Clone();
                clonedList.PushToEnd(clonedData);

                if (ReferenceEquals(node, CurrentNode)) clonedList.MoveToTail();

                node = node.NextNode;
            }

            return clonedList;
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

        public static LinkedList<T> operator ++(LinkedList<T> list)
        {
            list.CurrentNode = list.CurrentNode.NextNode;
            return list;
        }

        public static LinkedList<T> operator --(LinkedList<T> list)
        {
            list.CurrentNode = list.CurrentNode.PrevNode;
            return list;
        }

        public static bool operator !(LinkedList<T> list)
        {
            return list.Size != 0;
        }

        public void PushToEnd(T data)
        {
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

        public void PushToStart(T data)
        {
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
                for (var i = Size - 1; i < Size - index; i++) node = node.PrevNode;
            }

            return node;
        }

        public override string ToString()
        {
            var strBuilder = new StringBuilder();

            var curr = Head;

            for (var i = 0; i < Size; i++)
            {
                strBuilder.Append(curr);
                strBuilder.Append("->");

                curr = curr.NextNode;
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

            if (index == 0) {Head = node;}
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
        
        protected internal sealed class ListNode<TR>
        {
            public TR Data { get; }

            protected internal ListNode<TR> NextNode { get; set; }
            protected internal ListNode<TR> PrevNode { get; set; }

            public ListNode(TR data)
            {
                Data = data;
            }
        
            public override string ToString()
            {
                return $"[Node with data: {Data}]";
            }
        }
    }
    
    
}