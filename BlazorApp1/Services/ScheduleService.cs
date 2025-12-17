using BlazorApp1.Data;
using BlazorApp1.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace BlazorApp1.Services;

public class ScheduleService
{
    private readonly IDbContextFactory<ScheduleDbContext> _dbFactory;

    public ScheduleService(IDbContextFactory<ScheduleDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public async Task InitializeAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var created = await db.Database.EnsureCreatedAsync();

        if (!created && !await HasExpectedColumnsAsync(db))
        {
            await db.Database.EnsureDeletedAsync();
            await db.Database.EnsureCreatedAsync();
        }

        if (!await db.Departments.AnyAsync())
        {
            await SeedAsync(db);
        }
    }

    public async Task<List<Classroom>> GetClassroomsAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Classrooms.AsNoTracking().OrderBy(c => c.Name).ToListAsync();
    }

    public async Task<List<Department>> GetDepartmentsAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Departments.AsNoTracking().OrderBy(d => d.Code).ToListAsync();
    }

    public async Task<List<Subject>> GetSubjectsAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Subjects.AsNoTracking().OrderBy(s => s.SubjectCode).ToListAsync();
    }

    public async Task<List<Role>> GetRolesAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Roles.AsNoTracking().OrderBy(r => r.RoleType).ToListAsync();
    }

    public async Task<List<Person>> GetPeopleAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.People.AsNoTracking().OrderBy(p => p.LastName).ThenBy(p => p.FirstName).ToListAsync();
    }

    public async Task<List<LessonType>> GetLessonTypesAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.LessonTypes.AsNoTracking().OrderBy(l => l.Name).ToListAsync();
    }

    public async Task<List<Lesson>> GetLessonsAsync()
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var lessons = await db.Lessons.AsNoTracking().ToListAsync();
        var studentLinks = await db.LessonStudents.AsNoTracking().ToListAsync();
        var studentLookup = studentLinks
            .GroupBy(l => l.LessonId)
            .ToDictionary(g => g.Key, g => g.Select(s => s.PersonId).ToList());

        foreach (var lesson in lessons)
        {
            if (studentLookup.TryGetValue(lesson.Id, out var students))
            {
                lesson.StudentIds = students;
            }
        }

        return lessons
            .OrderBy(l => l.Day)
            .ThenBy(l => l.StartTime)
            .ToList();
    }

    public async Task<Subject?> GetSubjectByCodeAsync(string code)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Subjects.AsNoTracking().FirstOrDefaultAsync(s => s.SubjectCode == code);
    }

    public async Task<Classroom?> GetClassroomByCodeAsync(string code)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.Classrooms.AsNoTracking().FirstOrDefaultAsync(c => c.Code == code);
    }

    public async Task<LessonType?> GetLessonTypeAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.LessonTypes.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);
    }

    public async Task<Person?> GetPersonAsync(int id)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        return await db.People.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Classroom> AddClassroomAsync(Classroom classroom)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        db.Classrooms.Add(classroom);
        await db.SaveChangesAsync();
        return classroom;
    }

    public async Task<Department> AddDepartmentAsync(Department department)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        db.Departments.Add(department);
        await db.SaveChangesAsync();
        return department;
    }

    public async Task<Subject> AddSubjectAsync(Subject subject)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        db.Subjects.Add(subject);
        await db.SaveChangesAsync();
        return subject;
    }

    public async Task<Role> AddRoleAsync(Role role)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        db.Roles.Add(role);
        await db.SaveChangesAsync();
        return role;
    }

    public async Task<Person> AddPersonAsync(Person person)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();

        Console.WriteLine("DB file: " + db.Database.GetDbConnection().DataSource);
        db.People.Add(person);
        await db.SaveChangesAsync();
        return person;
    }

    public async Task<LessonType> AddLessonTypeAsync(LessonType lessonType)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        db.LessonTypes.Add(lessonType);
        await db.SaveChangesAsync();
        return lessonType;
    }

    public async Task<Lesson> AddLessonAsync(Lesson lesson)
    {
        await using var db = await _dbFactory.CreateDbContextAsync();
        var studentIds = lesson.StudentIds.ToList();
        lesson.StudentIds = new List<int>();

        db.Lessons.Add(lesson);
        await db.SaveChangesAsync();

        if (studentIds.Any())
        {
            db.LessonStudents.AddRange(studentIds.Select(s => new LessonStudent
            {
                LessonId = lesson.Id,
                PersonId = s
            }));
            await db.SaveChangesAsync();
        }

        return lesson;
    }

    private static async Task SeedAsync(ScheduleDbContext db)
    {
        var departments = new List<Department>
        {
            new() { Code = "KMA", Name = "Katedra matematiky" },
            new() { Code = "KIT", Name = "Katedra informačních technologií" }
        };
        db.Departments.AddRange(departments);
        await db.SaveChangesAsync();

        var roles = new List<Role>
        {
            new() { RoleType = "Vyučující" },
            new() { RoleType = "Přednášející" },
            new() { RoleType = "Garant" }
        };
        db.Roles.AddRange(roles);
        await db.SaveChangesAsync();

        var people = new List<Person>
        {
            new()
            {
                FirstName = "Jan",
                LastName = "Novík",
                Title = "Ing.",
                Affiliation = "Akademický pracovník",
                RoleId = roles.First().Id
            },
            new()
            {
                FirstName = "Petra",
                LastName = "Nováková",
                Affiliation = "Student"
            },
            new()
            {
                FirstName = "Jan",
                LastName = "Dvořák",
                Affiliation = "Student"
            }
        };
        db.People.AddRange(people);
        await db.SaveChangesAsync();

        var classrooms = new List<Classroom>
        {
            new() { Name = "Velká posluchárna", Code = "VP101", Floor = 1, Capacity = 120, Purpose = "Přednášková" },
            new() { Name = "Počítačová laboratoř", Code = "PC204", Floor = 2, Capacity = 32, Purpose = "Počítačová" }
        };
        db.Classrooms.AddRange(classrooms);
        await db.SaveChangesAsync();

        var lessonTypes = new List<LessonType>
        {
            new() { Name = "Přednáška" },
            new() { Name = "Cvičení" },
            new() { Name = "Seminář" }
        };
        db.LessonTypes.AddRange(lessonTypes);
        await db.SaveChangesAsync();

        var subjects = new List<Subject>
        {
            new()
            {
                Name = "Programování v C#",
                DepartmentCode = "KIT",
                SubjectCode = "KIT-CSP",
                Credits = 5
            },
            new()
            {
                Name = "Lineární algebra",
                DepartmentCode = "KMA",
                SubjectCode = "KMA-LA",
                Credits = 4
            }
        };
        db.Subjects.AddRange(subjects);
        await db.SaveChangesAsync();

        var lecturer = people.First(p => p.Affiliation == "Akademický pracovník");
        var studentIds = people.Where(p => p.Affiliation == "Student").Select(p => p.Id).ToList();

        var lecture = lessonTypes.First();
        var lab = lessonTypes.First(lt => lt.Name == "Cvičení");

        var lessons = new List<Lesson>
        {
            new()
            {
                SubjectCode = "KIT-CSP",
                Day = DayOfWeek.Monday,
                StartTime = new TimeSpan(9, 0, 0),
                EndTime = new TimeSpan(10, 30, 0),
                ClassroomCode = "VP101",
                LessonTypeId = lecture.Id,
                LecturerId = lecturer.Id
            },
            new()
            {
                SubjectCode = "KIT-CSP",
                Day = DayOfWeek.Wednesday,
                StartTime = new TimeSpan(14, 0, 0),
                EndTime = new TimeSpan(15, 30, 0),
                ClassroomCode = "PC204",
                LessonTypeId = lab.Id,
                LecturerId = lecturer.Id
            },
            new()
            {
                SubjectCode = "KMA-LA",
                Day = DayOfWeek.Thursday,
                StartTime = new TimeSpan(8, 0, 0),
                EndTime = new TimeSpan(9, 30, 0),
                ClassroomCode = "VP101",
                LessonTypeId = lecture.Id,
                LecturerId = lecturer.Id
            }
        };
        db.Lessons.AddRange(lessons);
        await db.SaveChangesAsync();

        db.LessonStudents.AddRange(lessons.SelectMany(l => studentIds.Select(s => new LessonStudent
        {
            LessonId = l.Id,
            PersonId = s
        })));

        await db.SaveChangesAsync();
    }

    private static async Task<bool> HasExpectedColumnsAsync(ScheduleDbContext db)
    {
        var connection = db.Database.GetDbConnection();
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = "PRAGMA table_info('People');";
        var columns = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var name = reader.GetString(1);
            var type = reader.GetString(2);
            columns[name] = type;
        }

        await connection.CloseAsync();

        var expectedColumns = new[] { "Id", "FirstName", "LastName", "Title", "Affiliation", "RoleId" };
        if (expectedColumns.Any(c => !columns.ContainsKey(c)))
        {
            return false;
        }

        return columns.TryGetValue("Id", out var idType)
            && idType.Contains("INT", StringComparison.OrdinalIgnoreCase);
    }
}
