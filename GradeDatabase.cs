using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class GradeDatabase
{
    private readonly string _filePath;
    private List<GradeRecord> _records;

    public GradeDatabase(string filePath)
    {
        _filePath = filePath;
        EnsureDirectoryExists();
        Load();
    }

    private void EnsureDirectoryExists()
    {
        var directory = Path.GetDirectoryName(_filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public void Load()
    {
        if (File.Exists(_filePath))
        {
            var json = File.ReadAllText(_filePath);
            _records = JsonSerializer.Deserialize<List<GradeRecord>>(json) ?? new List<GradeRecord>();
        }
        else
        {
            _records = new List<GradeRecord>();
        }
    }

    public void Save()
    {
        var json = JsonSerializer.Serialize(_records, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    public void View()
    {
        if (_records.Count == 0)
        {
            Console.WriteLine("База данных пуста.");
            return;
        }

        foreach (var record in _records)
        {
            Console.WriteLine(record);
        }
    }

    public void Add(GradeRecord record)
    {
        _records.Add(record);
        Save();
    }

    public void Delete(int studentId)
    {
        _records.RemoveAll(r => r.StudentId == studentId);
        Save();
    }

    public IEnumerable<GradeRecord> GetGradesBySubject(string subject)
    {
        return _records
            .Where(r => r.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<GradeRecord> GetGradesByStudent(string studentName)
    {
        return _records
            .Where(r => r.StudentName.Equals(studentName, StringComparison.OrdinalIgnoreCase));
    }

    public double GetAverageGradeByStudent(int studentId)
    {
        var grades = _records.Where(r => r.StudentId == studentId).Select(r => r.Grade).ToList();
        if (grades.Count == 0)
        {
            throw new InvalidOperationException("Нет оценок для указанного ученика.");
        }

        return grades.Average();
    }

    public int GetCountOfGradesBelow(double threshold)
    {
        return _records.Count(r => r.Grade < threshold);
    }
}
