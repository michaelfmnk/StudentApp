using NUnit.Framework;
using StudentApp;

namespace StudentAppTest
{
    public class Tests
    {
        [Test]
        public void ShouldCreateUser()
        {
            var student = new Student("fname", "lname", "mname", 1999);

            Assert.AreEqual("fname", student.FirstName);
            Assert.AreEqual("lname", student.LastName);
            Assert.AreEqual("mname", student.MiddleName);
            Assert.AreEqual(1999, student.BirthYear);
            Assert.AreEqual(0, student.AvgScore);
        }

        [Test]
        public void ShouldUpdateAvgScore()
        {
            var student = new Student("fname", "lname", "mname", 1999);
            student.AvgScore = 8;

            Assert.AreEqual(8, student.AvgScore);
        }

        [Test]
        public void ShouldGetStringInfo()
        {
            var student = new Student("fname", "lname", "mname", 1999);
            Assert.AreEqual("Student fname mname lname. Year oof birth: 1999. Average score: 0", student.ToString());
        }

        [Test]
        public void ShouldGetHashCode()
        {
            var student = new Student("fname", "lname", "mname", 1999);
            Assert.NotNull(student.GetHashCode());
        }

        [Test]
        public void ShouldCheckEqualityWithMethod()
        {
            var studentA = new Student("fname", "lname", "mname", 1999);
            var studentB = new Student("fname", "lname", "mname", 1999);

            Assert.True(studentA.Equals(studentB));
            Assert.True(studentA.Equals((object) studentB));
            Assert.True(studentB.Equals(studentA));
            Assert.True(studentB.Equals((object) studentA));
        }

        [Test]
        public void ShouldCheckEqualityWithMethodAndReturnTrueOnTheSameInstance()
        {
            var student = new Student("fname", "lname", "mname", 1999);
            Assert.True(student.Equals(student));
            Assert.True(student.Equals((object) student));
        }

        [Test]
        public void ShouldCheckEqualityWithMethodAndFailWhenSecondOperandNull()
        {
            var student = new Student("fname", "lname", "mname", 1999);
            Assert.False(student.Equals(null));
        }

        [Test]
        public void ShouldCheckEqualityWithOperator()
        {
            var studentA = new Student("fname", "lname", "mname", 1999);

            // birth year should not matter
            var studentB = new Student("fname", "lname", "mname", 1998);


            Assert.True(studentA == studentB);
            Assert.False(studentA != studentB);
        }

        [Test]
        public void ShouldCheckEqualityWithOperatorAndFailWhenSecondOperandNull()
        {
            var student = new Student("fname", "lname", "mname", 1998);

            Assert.False(null == student);
            Assert.True(null != student);
        }

        [Test]
        public void ShoudCheckEqualityWithOperatorAndReturnTrueOnTheSameInstance()
        {
            var student = new Student("fname", "lname", "mname", 1998);

            Assert.True(student == student);
            Assert.False(student != student);
        }

        [Test]
        public void ShouldMarkOneUserAaGreaterByFirstName()
        {
            var studentA = new Student("AAA", "lname", "mname", 1998);
            var studentB = new Student("BBB", "lname", "mname", 1998);

            Assert.True(studentA > studentB);
            Assert.True(studentA >= studentB);
            Assert.False(studentA < studentB);
            Assert.False(studentA <= studentB);
        }

        [Test]
        public void ShouldMarkOneUserAaGreaterByLastName()
        {
            var studentA = new Student("Michael", "A", "mname", 1998);
            var studentB = new Student("Michael", "B", "mname", 1998);

            Assert.True(studentA > studentB);
            Assert.True(studentA >= studentB);
            Assert.False(studentA < studentB);
            Assert.False(studentA <= studentB);
        }

        [Test]
        public void EqualUserShouldBeEqualOrGreater()
        {
            var student = new Student("AAA", "lname", "mname", 1998);

            Assert.True(student >= student);
            Assert.True(student <= student);
        }

        [Test]
        public void NullShouldBeGreaterThanObj()
        {
            var student = new Student("AAA", "lname", "mname", 1998);

            Assert.False(student > null);
            Assert.False(student >= null);
            Assert.True(null > student);
            Assert.True(null >= student);

            Assert.False(null < student);
            Assert.False(null <= student);
            Assert.True(student < null);
            Assert.True(student <= null);
        }

        [Test]
        public void ShouldCompareNullToNull()
        {
            Student student = null;

            Assert.False(student > null);
            Assert.True(student >= null);
            Assert.False(null > student);
            Assert.True(null >= student);

            Assert.False(null < student);
            Assert.True(null <= student);
            Assert.False(student < null);
            Assert.True(student <= null);
        }

        [Test]
        public void ShouldCompareUserToUser()
        {
            var studentA = new Student("fname", "Aname", "mname", 1999);
            var studentB = new Student("fname", "Bname", "mname", 1999);

            Assert.AreEqual(-1, studentA.CompareTo(studentB));
            Assert.AreEqual(0, studentA.CompareTo(studentA));
            Assert.AreEqual(1, studentB.CompareTo(studentA));
        }

        [Test]
        public void StudentShouldBeCloned()
        {
            var student = new Student("fname", "lname", "mname", 1999);

            var clone = student.Clone();
            Assert.True(clone is Student);

            var studentClone = (Student) clone;
            Assert.AreEqual("fname", studentClone.FirstName);
            Assert.AreEqual("lname", studentClone.LastName);
            Assert.AreEqual("mname", studentClone.MiddleName);
            Assert.AreEqual(1999, studentClone.BirthYear);
            Assert.AreEqual(0, studentClone.AvgScore);
        }
    }
}