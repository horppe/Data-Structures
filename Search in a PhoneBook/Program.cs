using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Search_in_a_PhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader reader = new StreamReader("file.txt");
            SortedDictionary<string, List<Student>> courses = new SortedDictionary<string, List<Student>>();
            using (reader)
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] splits = line.Split('|');
                    string name = splits[0].Trim();
                    string course = splits[1].Trim();
                    List<Student> students;
                    if (!courses.TryGetValue(course, out students))
                    {
                        students = new List<Student>();
                        courses[course] = students;
                    }
                    string[] names = name.Split(' ');
                    students.Add(new Student(names[0], names[1]));
                }

            }

            foreach (var course in courses)
            {
                Console.WriteLine("{0}:", course.Key);
                List<Student> studArr = course.Value;
                studArr.Sort();
                foreach (var student in studArr)
                {
                    Console.WriteLine("\t{0}", student);
                }
            }
            Console.ReadKey();
        }
    }
    public class Student : IComparable<Student>
    {
        public string firstName;
        public string lastName;
        public Student(string name, string lastName)
        {
            firstName = name;
            this.lastName = lastName;
        }

        public int CompareTo(Student stud)
        {
            int condition = this.lastName.CompareTo(stud.lastName);
            if (condition == 0)
            {
                return this.firstName.CompareTo(stud.firstName);
            }
            return condition;
        }

        public override string ToString()
        {
            return firstName.ToString() + " " + lastName.ToString();
        }
    }
}
