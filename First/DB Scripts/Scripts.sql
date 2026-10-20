-- Database Creation
CREATE DATABASE BasicInformation;
GO

USE BasicInformation;
GO

-- Table Creation
CREATE TABLE Employees (
    ID INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Appointment VARCHAR(100),
    Technology VARCHAR(200),
    Task VARCHAR(MAX)
);
GO

-- Data Insertion
INSERT INTO Employees (Name, Appointment, Technology, Task) VALUES
('A.P. Godse', 'Software Developer', '.NET, Database', 'Computer Graphics');

INSERT INTO Employees (Name, Appointment, Technology, Task) VALUES
('Yashwant Kanitker', 'Programmer', '.NET, PHP, JSON', 'Let us C');

INSERT INTO Employees (Name, Appointment, Technology, Task) VALUES
('E. Balaguruswami', 'Programmer', '.NET, Javascript', 'Object Oriented System');

INSERT INTO Employees (Name, Appointment, Technology, Task) VALUES
('R.S. Aggarwal', 'Software Developer', 'Web API, MVC4', 'Design Analysis and Algorithm');

INSERT INTO Employees (Name, Appointment, Technology, Task) VALUES
('S. Chand', 'Senior Developer', 'React, Node.js', 'Frontend Development');

INSERT INTO Employees (Name, Appointment, Technology, Task) VALUES
('Arihant Publications', 'Technical Lead', 'Angular, TypeScript', 'Project Management');

INSERT INTO Employees (Name, Appointment, Technology, Task) VALUES
('Cengage Learning', 'System Architect', 'Cloud, AWS', 'Infrastructure Design');

INSERT INTO Employees (Name, Appointment, Technology, Task) VALUES
('O''Reilly Media', 'DevOps Engineer', 'Docker, Kubernetes', 'CI/CD Pipeline');