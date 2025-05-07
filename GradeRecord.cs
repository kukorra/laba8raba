using System;

public class GradeRecord
{
    public string StudentName { get; set; }
    public int StudentId { get; set; }
    public string Subject { get; set; }
    public DateTime Date { get; set; }
    public double Grade { get; set; }

    public GradeRecord()
    {
    }

    public GradeRecord(string studentName, int studentId, string subject, DateTime date, double grade)
    {
        StudentName = studentName;
        StudentId = studentId;
        Subject = subject;
        Date = date;
        Grade = grade;
    }

    public override string ToString()
    {
        return $"{StudentName} (ID: {StudentId}) | {Subject} | {Date:d} | Оценка: {Grade}";
    }
}
