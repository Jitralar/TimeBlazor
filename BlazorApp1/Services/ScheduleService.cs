using BlazorApp1.Models;

namespace BlazorApp1.Services;

public class ScheduleService
{
    private readonly List<Classroom> _classrooms = new();
    private readonly List<Department> _departments = new();
    private readonly List<Subject> _subjects = new();
    private readonly List<Role> _roles = new();
    private readonly List<Person> _people = new();
    private readonly List<LessonType> _lessonTypes = new();
    private readonly List<Lesson> _lessons = new();

    public ScheduleService()
    {
        SeedDepartments();
        SeedRoles();
        SeedPeople();
        SeedClassrooms();
        SeedLessonTypes();
        SeedSubjects();
        SeedLessons();
    }

    public IReadOnlyCollection<Classroom> Classrooms => _classrooms;
    public IReadOnlyCollection<Department> Departments => _departments;
    public IReadOnlyCollection<Subject> Subjects => _subjects;
    public IReadOnlyCollection<Role> Roles => _roles;
    public IReadOnlyCollection<Person> People => _people;
    public IReadOnlyCollection<LessonType> LessonTypes => _lessonTypes;
    public IReadOnlyCollection<Lesson> Lessons => _lessons;

    public void AddClassroom(Classroom classroom) => _classrooms.Add(classroom);

    public void AddDepartment(Department department) => _departments.Add(department);

    public void AddSubject(Subject subject) => _subjects.Add(subject);

    public void AddRole(Role role) => _roles.Add(role);

    public void AddPerson(Person person) => _people.Add(person);

    public void AddLessonType(LessonType lessonType) => _lessonTypes.Add(lessonType);

    public void AddLesson(Lesson lesson) => _lessons.Add(lesson);

    public Subject? GetSubjectByCode(string code) =>
        _subjects.FirstOrDefault(s => s.SubjectCode.Equals(code, StringComparison.OrdinalIgnoreCase));

    public Classroom? GetClassroomByCode(string code) =>
        _classrooms.FirstOrDefault(c => c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

    public LessonType? GetLessonType(Guid id) => _lessonTypes.FirstOrDefault(l => l.Id == id);

    public Person? GetPerson(Guid id) => _people.FirstOrDefault(p => p.Id == id);

    public IEnumerable<Lesson> GetWeeklyLessons() => _lessons
        .OrderBy(l => l.Day)
        .ThenBy(l => l.StartTime);

    private void SeedDepartments()
    {
        _departments.AddRange([
            new Department { Code = "KMA", Name = "Katedra matematiky" },
            new Department { Code = "KIT", Name = "Katedra informačních technologií" }
        ]);
    }

    private void SeedRoles()
    {
        _roles.AddRange([
            new Role { RoleType = "Vyučující" },
            new Role { RoleType = "Přednášející" },
            new Role { RoleType = "Garant" }
        ]);
    }

    private void SeedPeople()
    {
        var lecturerRole = _roles.First();
        _people.AddRange([
            new Person
            {
                FirstName = "Jiří",
                LastName = "Král",
                Title = "Ing.",
                Affiliation = "Akademický pracovník",
                RoleId = lecturerRole.Id
            },
            new Person
            {
                FirstName = "Petra",
                LastName = "Nováková",
                Affiliation = "Student"
            },
            new Person
            {
                FirstName = "Jan",
                LastName = "Dvořák",
                Affiliation = "Student"
            }
        ]);
    }

    private void SeedClassrooms()
    {
        _classrooms.AddRange([
            new Classroom { Name = "Velká posluchárna", Code = "VP101", Floor = 1, Capacity = 120, Purpose = "Přednášková" },
            new Classroom { Name = "Počítačová laboratoř", Code = "PC204", Floor = 2, Capacity = 32, Purpose = "Počítačová" }
        ]);
    }

    private void SeedLessonTypes()
    {
        _lessonTypes.AddRange([
            new LessonType { Name = "Přednáška" },
            new LessonType { Name = "Cvičení" },
            new LessonType { Name = "Seminář" }
        ]);
    }

    private void SeedSubjects()
    {
        _subjects.AddRange([
            new Subject
            {
                Name = "Programování v C#",
                DepartmentCode = "KIT",
                SubjectCode = "KIT-CSP",
                Credits = 5
            },
            new Subject
            {
                Name = "Lineární algebra",
                DepartmentCode = "KMA",
                SubjectCode = "KMA-LA",
                Credits = 4
            }
        ]);
    }

    private void SeedLessons()
    {
        var lecture = _lessonTypes.First();
        var lab = _lessonTypes.First(lt => lt.Name == "Cvičení");
        var lecturer = _people.First();
        var studentIds = _people.Where(p => p.Affiliation == "Student").Select(p => p.Id).ToList();

        _lessons.AddRange([
            new Lesson
            {
                SubjectCode = "KIT-CSP",
                Day = DayOfWeek.Monday,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(10, 30, 0),
                ClassroomCode = "VP101",
                LessonTypeId = lecture.Id,
                LecturerId = lecturer.Id,
                StudentIds = studentIds
            },
            new Lesson
            {
                SubjectCode = "KIT-CSP",
                Day = DayOfWeek.Wednesday,
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(15, 30, 0),
                ClassroomCode = "PC204",
                LessonTypeId = lab.Id,
                LecturerId = lecturer.Id,
                StudentIds = studentIds
            },
            new Lesson
            {
                SubjectCode = "KMA-LA",
                Day = DayOfWeek.Thursday,
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(9, 30, 0),
                ClassroomCode = "VP101",
                LessonTypeId = lecture.Id,
                LecturerId = lecturer.Id,
                StudentIds = studentIds
            }
        ]);
    }
}
