CREATE DATABASE CollegeDB;
GO

USE CollegeDB;
GO

CREATE TABLE Groups (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(50) NOT NULL
);

CREATE TABLE Teachers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL
);

CREATE TABLE Subjects (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    TeacherId INT,
    FOREIGN KEY (TeacherId) REFERENCES Teachers(Id)
);

CREATE TABLE Students (
    Id INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(100) NOT NULL,
    GroupId INT,
    FOREIGN KEY (GroupId) REFERENCES Groups(Id)
);

CREATE TABLE Grades (
    Id INT PRIMARY KEY IDENTITY(1,1),
    StudentId INT,
    SubjectId INT,
    GradeValue INT CHECK (GradeValue >= 1 AND GradeValue <= 5),
    FOREIGN KEY (StudentId) REFERENCES Students(Id),
    FOREIGN KEY (SubjectId) REFERENCES Subjects(Id)
);
GO

INSERT INTO Groups (Name) VALUES ('ИС-21'), ('ПР-22');
INSERT INTO Teachers (FullName) VALUES ('Иванов И.И.'), ('Петрова А.С.');
INSERT INTO Subjects (Name, TeacherId) VALUES ('Базы данных', 1), ('Программирование', 2);
INSERT INTO Students (FullName, GroupId) VALUES ('Алексей Смирнов', 1), ('Мария Иванова', 1), ('Дмитрий Козлов', 2);
INSERT INTO Grades (StudentId, SubjectId, GradeValue) VALUES (1, 1, 5), (1, 2, 4), (2, 1, 4);
GO