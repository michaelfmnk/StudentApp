using NUnit.Framework;
using StudentApp;
using StudentApp.List;

namespace StudentAppTest
{
    public class NamedCustomLinkedListTest
    {
        [Test]
        public void NamedCustomLinkedListShouldHaveName()
        {
            var namedList = new NamedCustomLinkedList<Student>("Custom Name");

            Assert.AreEqual("Custom Name", namedList.Name);
        }

        [Test]
        public void ShouldCompareByName()
        {
            var aList = new NamedCustomLinkedList<Student>("A");
            var bList = new NamedCustomLinkedList<Student>("B");

            Assert.True(aList > bList);
            Assert.True(bList < aList);
            Assert.True(aList >= bList);
            Assert.True(bList <= aList);

            Assert.False(aList < bList);
            Assert.False(bList > aList);
            Assert.False(aList <= bList);
            Assert.False(bList >= aList);
        }

        [Test]
        public void ShouldCompareWithNull()
        {
            var aList = new NamedCustomLinkedList<Student>("A");

            Assert.True(aList < null);
            Assert.True(null > aList);
            Assert.True(aList <= null);
            Assert.True(null >= aList);

            Assert.False(null < aList);
            Assert.False(aList > null);
            Assert.False(null <= aList);
            Assert.False(aList >= null);
        }

        [Test]
        public void CloningShouldWork()
        {
            // given
            var namedList = new NamedCustomLinkedList<Student>("Name");


            var student1 = new Student("F1", "L", "N", 1999);
            var student2 = new Student("F2", "L", "N", 1999);
            var student3 = new Student("F3", "L", "N", 1999);
            namedList.AddAll(student1, student2, student3);
            namedList.MoveToTail();

            // when
            var clone = (NamedCustomLinkedList<Student>) namedList.Clone();


            // then
            Assert.AreEqual("Name", clone.Name);
            Assert.AreEqual(3, clone.Size);
            Assert.AreEqual("F3", clone.Current.FirstName);
            Assert.False(ReferenceEquals(student3, clone.Current));
        }

        [Test]
        public void ShouldCompareCorrectly()
        {
            var aList = new NamedCustomLinkedList<Student>("A");
            var bList = new NamedCustomLinkedList<Student>("B");

            Assert.AreEqual(-1, aList.CompareTo(bList));
            Assert.AreEqual(1, bList.CompareTo(aList));
            Assert.AreEqual(0, aList.CompareTo(aList));
            Assert.AreEqual(0, bList.CompareTo(bList));
        }
    }
}