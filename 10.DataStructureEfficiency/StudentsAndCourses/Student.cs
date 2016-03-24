namespace StudentsAndCourses
{
    using System;

    public class Student : IComparable<Student>
    {
        public Student(string firstName, string lastName)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int CompareTo(Student otherStudent)
        {
            if(this.LastName.CompareTo(otherStudent.LastName) == 0)
            {
                return this.FirstName.CompareTo(otherStudent.FirstName );
            }

            return this.LastName.CompareTo(otherStudent.LastName);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.FirstName, this.LastName);
        }
    }
}
