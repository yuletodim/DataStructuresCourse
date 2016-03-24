using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsAndCourses
{
    class StudentsAndCourses
    {
        static void Main(string[] args)
        {
            var studentsByCourses = ReadInput("students.txt");

            foreach (var course in studentsByCourses)
            {
                Console.WriteLine("{0}: {1}", course.Key, string.Join(", ", course.Value));
            }
        }

        private static SortedDictionary<string, SortedSet<Student>> ReadInput(string fileName)
        {
            var studentsByCourses = new SortedDictionary<string, SortedSet<Student>>();
            using (var streamReader = new StreamReader(@fileName))
            {
                var line = streamReader.ReadLine();
                while(line != null)
                {
                    string[] newEntry = line.Split('|')
                        .Select(s => s.Trim())
                        .ToArray();
                    var student = new Student(newEntry[0], newEntry[1]);
                    var courseKey = newEntry[2];

                    if (!studentsByCourses.ContainsKey(courseKey))
                    {
                        var students = new SortedSet<Student>();
                        studentsByCourses.Add(courseKey, students);
                    }

                    studentsByCourses[courseKey].Add(student);

                    line = streamReader.ReadLine();
                }
            }
                return studentsByCourses;
        }
    }
}
