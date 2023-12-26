SELECT student.StudentName, subject.SubjectName, score.Marks
    FROM Score score
    JOIN Student student ON score.StudentID = student.StudentID
    JOIN Subject subject ON score.SubjectID = subject.SubjectID;