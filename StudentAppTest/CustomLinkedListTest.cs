using System;
using System.Collections.Generic;
using NUnit.Framework;
using StudentApp;
using StudentApp.List;

namespace StudentAppTest
{
    public class LinkedListTest
    {
        [Test]
        public void ShouldAddElementToEnd()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();


            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);

            // when
            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student2);
            linkedList.PushToEnd(student3);


            // then
            Console.WriteLine(linkedList);

            Assert.AreEqual("F1", linkedList.Get(0).FirstName);
            Assert.AreEqual("F2", linkedList.Get(1).FirstName);
            Assert.AreEqual("F3", linkedList.Get(2).FirstName);
        }

        [Test]
        public void ShouldAddElementToStart()
        {
            // given
            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);

            var linkedList = new CustomLinkedList<Student>();

            // when
            linkedList.PushToStart(student1);
            linkedList.PushToStart(student2);

            // then
            Console.WriteLine(linkedList);

            Assert.AreEqual("F2", linkedList.Get(0).FirstName);
            Assert.AreEqual("F1", linkedList.Get(1).FirstName);
        }

        [Test]
        public void ShouldThrowIndexOutOfBounds()
        {
            var linkedList = new CustomLinkedList<Student>();

            Assert.Throws<IndexOutOfRangeException>(() => { linkedList.Get(0); });
            Assert.Throws<IndexOutOfRangeException>(() => { linkedList.Get(100); });
            Assert.Throws<IndexOutOfRangeException>(() => { linkedList.Get(-1); });
        }

        [Test]
        public void ShouldPutElementByOrderInside()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);


            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student3);

            // when
            linkedList.Put(1, student2);

            // then
            Assert.AreEqual("F1", linkedList.Get(0).FirstName);
            Assert.AreEqual("F2", linkedList.Get(1).FirstName);
            Assert.AreEqual("F3", linkedList.Get(2).FirstName);
        }

        [Test]
        public void ShouldPutElementByOrderInStart()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);


            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student3);

            // when
            linkedList.Put(0, student2);


            // then
            Assert.AreEqual("F2", linkedList.Get(0).FirstName);
            Assert.AreEqual("F1", linkedList.Get(1).FirstName);
            Assert.AreEqual("F3", linkedList.Get(2).FirstName);
        }

        [Test
            // , Ignore("not implemented")
        ]
        public void ShouldPutElementByOrderInTheEnd()
        {
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);


            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student3);

            linkedList.Put(2, student2);

            Assert.AreEqual(3, linkedList.Size);
        }

        [Test]
        public void ShouldMoveCurrentToHead()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);

            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student2);
            linkedList.PushToEnd(student3);

            // when
            linkedList.MoveToHead();

            // then
            Assert.AreEqual("F1", linkedList.Current.FirstName);
        }

        [Test]
        public void ShouldMoveAheadWithIncOperator()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);

            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student2);
            linkedList.PushToEnd(student3);

            // when
            linkedList.MoveToHead();
            linkedList++;

            // then
            Assert.AreEqual("F2", linkedList.Current.FirstName);
        }

        [Test]
        public void ShouldMoveBackwardsWithDecOperator()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);

            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student2);
            linkedList.PushToEnd(student3);

            // when
            linkedList.MoveToHead();
            linkedList--;

            // then
            Assert.AreEqual("F3", linkedList.Current.FirstName);
        }

        [Test]
        public void ShouldMoveCurrentToTail()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);

            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student2);
            linkedList.PushToEnd(student3);

            // when
            linkedList.MoveToTail();

            // then
            Assert.AreEqual("F3", linkedList.Current.FirstName);
        }

        [Test]
        public void ExclamationMarkShouldCheckIfNotEmpty()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);

            linkedList.PushToEnd(student1);

            // when
            var hasElements = !linkedList;

            // then
            Assert.True(hasElements);
        }

        [Test]
        public void ExclamationMarkShouldCheckIfEmpty()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            // when
            var hasElements = !linkedList;

            // then
            Assert.False(hasElements);
        }

        [Test]
        public void ShouldDeleteAll()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);

            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student2);
            linkedList.PushToEnd(student3);
            // when
            linkedList.Clear();

            // then
            Assert.AreEqual(0, linkedList.Size);
            Assert.Null(linkedList.Current);
        }

        [Test]
        public void ShouldDeleteCurrent()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);

            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student2);
            linkedList.PushToEnd(student3);
            linkedList.MoveToHead();
            linkedList++;

            // when
            linkedList.DeleteCurrent();


            // then
            Assert.AreEqual("F3", linkedList.Current.FirstName);
            Assert.AreEqual(2, linkedList.Size);
            linkedList--;
            Assert.AreEqual("F1", linkedList.Current.FirstName);
        }

        [Test]
        public void ShouldClone()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);

            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student2);
            linkedList.MoveToTail();

            // when
            var clone = (CustomLinkedList<Student>) linkedList.Clone();

            // then
            Assert.False(ReferenceEquals(linkedList, clone));
            Assert.AreEqual("F2", clone.Current.FirstName);

            Assert.AreEqual(student1, clone.Get(0));
            Assert.False(ReferenceEquals(student1, clone.Get(0)));

            Assert.AreEqual(student2, clone.Get(1));
            Assert.False(ReferenceEquals(student2, clone.Get(1)));
        }

        [Test]
        public void ShouldDeleteByData()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F1", "L", "N", 1999);
            var student3 = new Student("4", "L", "N", 1999);
            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student2);
            linkedList.PushToEnd(student3);


            // when
            linkedList.Delete(student2);

            // then
            Assert.AreEqual(1, linkedList.Size);
            Assert.AreEqual("4", linkedList.Current.FirstName);
        }
        
        [Test]
        public void EnumeratorShouldWork()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F1", "L", "N", 1999);
            var student3 = new Student("4", "L", "N", 1999);
            linkedList.PushToEnd(student1);
            linkedList.PushToEnd(student2);
            linkedList.PushToEnd(student3);


            // when
            var enumerator = linkedList.GetEnumerator();


            enumerator.MoveNext();
            Assert.True(ReferenceEquals(student1, enumerator.Current));
            enumerator.MoveNext();
            Assert.True(ReferenceEquals(student2, enumerator.Current));
            enumerator.MoveNext();
            Assert.True(ReferenceEquals(student3, enumerator.Current));
        }


        [Test]
        public void ShouldSort()
        {
            // given
            var linkedList = new CustomLinkedList<Student>();

            for (char letter = 'Z'; letter >= 'A'; letter--)
            {
                linkedList.PushToEnd(StudentWithFirstName(letter.ToString())); 
            } 
            

            // when
            linkedList.Sort();

            // then
            Assert.AreEqual("Z", linkedList.Current.FirstName);

            linkedList.MoveToHead();
            
            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                Assert.AreEqual(letter.ToString(), linkedList.Current.FirstName);
                linkedList++;
            }
        }

        private Student StudentWithFirstName(string fName)
        {
            return new Student(fName, "L", "N", 1999);
        }
    }
}