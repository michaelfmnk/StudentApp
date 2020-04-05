using System;

namespace StudentApp
{
    public class Student
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string MiddleName { get; }
        public int BirthYear { get; }
        public int AvgScore { get; set; }

        public string FullName => $"{FirstName} {MiddleName} {LastName}";


        public Student(string firstName, string lastName, string middleName, int birthYear, int avgScore = 0)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            BirthYear = birthYear;
            AvgScore = avgScore;
        }

        private bool Equals(Student other)
        {
            return FirstName == other.FirstName && LastName == other.LastName && MiddleName == other.MiddleName && BirthYear == other.BirthYear;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Student) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, LastName, MiddleName, BirthYear);
        }

        public static bool operator ==(Student a, Student b)
        {
            if (ReferenceEquals(null, a) || ReferenceEquals(null, b))
            {
                return ReferenceEquals(null, a) && ReferenceEquals(null, b);
            }
            
            return a.FirstName == b.FirstName && a.LastName == b.LastName && a.MiddleName == b.MiddleName;
        }

        public static bool operator !=(Student a, Student b)
        {
            return !(a == b);
        }

        public static bool operator >=(Student a, Student b)
        {
            if (a == null) return b == null;
            if (b == null) return false;
            
            return string.CompareOrdinal(a.FullName, b.FullName) <= 0;
        }

        public static bool operator <=(Student a, Student b)
        {
            if (a == null) return b == null;
            if (b == null) return false;
            
            return string.CompareOrdinal(a.FullName, b.FullName) >= 0;
        }

        public static bool operator >(Student a, Student b)
        {
            if (a == null) return b == null;
            if (b == null) return false;
            
            return string.CompareOrdinal(a.FullName, b.FullName) < 0;
        }

        public static bool operator <(Student a, Student b)
        {
            if (a == null) return b != null;
            if (b == null) return false;
            
            return string.CompareOrdinal(a.FullName, b.FullName) > 0;
        }

        public void PrintInfo()
        {
            Console.WriteLine(ToString());
        }

        public override string ToString()
        {
            return $"Student {FirstName} {MiddleName} {LastName}. " +
                   $"Year oof birth: {BirthYear}. " +
                   $"Average score: {AvgScore}";
        }
    }
}