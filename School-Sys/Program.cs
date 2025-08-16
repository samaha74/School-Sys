using System.ComponentModel.Design;
using System.Transactions;

namespace School_Sys
{
    class Instructor
    {
        public Instructor(int id, string name, string specialization)
        {
            Id = id;
            Name = name;
            Specialization = specialization;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Specialization{ get; set; }

        public void displayInfo()
        {
            Console.WriteLine("id: " + this.Id);
            Console.WriteLine("Name: "+this.Name);
            Console.WriteLine("Specialization: "+this.Specialization);
            Console.WriteLine("=====================================");
        }
    }

    class Course
    {
        public Course()
        {
            Id = -1;
        }

        public Course(int id, string name, Instructor instructor)
        {
            Id = id;
            Name = name;
            this.instructor = instructor;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public Instructor instructor { get; set; }

        public void displayInfo() {
            Console.WriteLine("Course Id: " + this.Id);
            Console.WriteLine("Course Name: " + this.Name);
            Console.WriteLine("Instructor: " + this.instructor.Name);
            Console.WriteLine("===================================");
        }
    }
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses = new List<Course>();

        public Student(int id, string name, int age)
        {
            Id = id;
            Name = name;
            Age = age;
        }

        public void displayInfo()
        {
            Console.WriteLine("ID: " + this.Id);
            Console.WriteLine("Name" + this.Name);
            Console.WriteLine("Age" + this.Age);
            Console.WriteLine("Enrolled Courses: ");
            foreach (Course c in Courses)
            {
                c.displayInfo();
                Console.WriteLine("\n------------------------------");
            }
            Console.WriteLine("\n=====================================");
        }
    }
    
    class School
    {
        List<Student> Students = new List<Student>();
        List<Course> Courses = new List<Course>();
        List<Instructor> Instructors = new List<Instructor>();

        static int sId = 1;
        static int cId = 1;
        static int iId = 1;

        public void AddStud(string name, int age)
        {
            Students.Add(new Student(sId, name, age));
            sId++;
        }
        public void AddCourse(string name, int IId)
        {
            Courses.Add(new Course(cId, name, Instructors[IId - 1 ]));
            cId++;
        }
        public void AddInstructor(string name, string spec)
        {
            Instructors.Add(new Instructor(iId, name, spec));
            iId++;
        }

        public bool Enroll(int sid, int cid)
        {
            Course course = new Course();
            foreach (Course c in Courses)
                if (c.Id == cid)
                {
                    course = c;
                    break;
                }

            if (course.Id == -1)
                return false;

            foreach (Student student in Students)
                if (student.Id == sid)
                {
                    student.Courses.Add(course);
                    break;
                }
            return true;
        }

        public void dispStud()
        {
            foreach (Student student in Students)
                student.displayInfo();
        }
        public void dispCourses()
        {
            foreach (Course c in Courses)
                c.displayInfo();
        }
        public void dispInst()
        {
            foreach (Instructor i in Instructors)
                i.displayInfo();
        }
        public void findStudent(int id)
        {
            bool f = false;
            foreach (Student student in Students)
                if(student.Id == id)
                {
                    student.displayInfo();
                    f= true;
                }
            if(!f)
                Console.WriteLine("Student not Found\n\n\n\n");
        }
        public void findCourse(int id)
        {
            bool f = false;
            foreach (Course c in Courses)
                if(c.Id == id)
                {
                    c.displayInfo();
                    f= true;
                }
            if(!f)
                Console.WriteLine("Course not Found\n\n\n\n");
        }

        public bool CheckStoC(int sid , int cid)
        {
            Course course = new Course();
            foreach (Course c in Courses)
                if (c.Id == cid)
                {
                    course = c;
                    break;
                }

            if (course.Id == -1)
                return false;

            foreach (Student student in Students)
                if (student.Id == sid)
                {
                    foreach(Course c in student.Courses)
                    {
                        if (c.Id == course.Id)
                        {
                            return true;
                        }
                    }
                    break;
                    
                }

            return false;
        }
        public void InameByC(int id) { 
            foreach (Course c in Courses)
                if (c.Id == id)
                    Console.WriteLine(c.instructor.Name + " is the intructor for this Course\n\n");
        }

    }
    internal class Program
    {
        static int menu() {
            Console.WriteLine("\n\n\n\n1. Add Student\r\n2. Add Instructor\r\n3. Add Course\r\n4. Enroll Student in Course\r\n5. Show All Students\r\n6. Show All Courses\r\n7. Show All Instructors\r\n8. Find the student by id\r\n9. Fine the course by id\r\n10. Check if the student enrolled in specific course\r\n11. Return the instructor name by course name\r\n12.Exit\r\n\nEnter Choice");
            return Convert.ToInt32(Console.ReadLine());
        }

        static void Main(string[] args)
        {
            School s = new School();
            while (true)
            {
                switch (menu())
                {
                    case 1:
                        Console.WriteLine("Enter student name: ");
                        string sname = Console.ReadLine();
                        Console.WriteLine("Enter student age: ");
                        int age = Convert.ToInt32(Console.ReadLine());
                        s.AddStud(sname, age);
                        break;

                    case 2:
                        Console.WriteLine("instructor name: ");
                        string iname = Console.ReadLine();
                        Console.WriteLine("Specialization: ");
                        string spec = Console.ReadLine();
                        s.AddInstructor(iname, spec);
                        break;

                    case 3:
                        Console.WriteLine("Course name: ");
                        string cname = Console.ReadLine();
                        Console.WriteLine("Instructor id: ");
                        int id = Convert.ToInt32(Console.ReadLine());
                        s.AddCourse(cname, id);
                        break;

                    case 4:
                        Console.WriteLine("enter Student id: ");
                        int sid = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("enter Course id: ");
                        int cid= Convert.ToInt32(Console.ReadLine());
                        s.Enroll(sid, cid);
                        break;

                    case 5:
                        Console.WriteLine('\n');
                        s.dispStud();
                        break;

                    case 6:
                        Console.WriteLine('\n');
                        s.dispCourses();
                        break;

                    case 7:
                        Console.WriteLine('\n');
                        s.dispInst();
                        break;
                    case 8:
                        Console.WriteLine("Enter student id: ");
                        s.findStudent(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 9:
                        Console.WriteLine("Enter Course id: ");
                        s.findCourse(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 10:
                        Console.WriteLine("enter Student id: ");
                        int ssid = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("enter Course id: ");
                        int ccid = Convert.ToInt32(Console.ReadLine());
                        if (!s.CheckStoC(ssid, ccid))
                            Console.WriteLine("ERRORRRRRR!!?!!?");
                        else Console.WriteLine("Found");
                            
                        break;

                    case 11:
                        Console.WriteLine("course id: ");

                        s.InameByC(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 12:
                        Console.WriteLine("BYE!");
                            return;
                    default:
                        Console.WriteLine("invalid input try again");
                        break;

                }
            }
        }
    }
}
