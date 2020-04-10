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
    }
}