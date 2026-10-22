-- Database Creation
CREATE DATABASE LearningDB;
GO

USE LearningDB;
GO

DROP TABLE IF EXISTS dbo.Employee;
GO

-- Create the Employee table
CREATE TABLE dbo.Employee (
    ID          INT PRIMARY KEY,
    Name        NVARCHAR(100) NOT NULL,
    Department  NVARCHAR(50) NOT NULL,
    Salary      DECIMAL(10, 2) NOT NULL,
    JoinedOn    DATE NOT NULL
);
GO

-- Insert 6 employee records
INSERT INTO dbo.Employee (ID, Name, Department, Salary, JoinedOn) VALUES
(1, 'Aarav Sharma',   'Engineering', 85000.00, '2019-03-15'),
(2, 'Priya Verma',    'Marketing',   62000.00, '2020-07-01'),
(3, 'Rohan Gupta',    'Finance',     70000.00, '2018-11-20'),
(4, 'Neha Singh',     'Engineering', 92000.00, '2017-05-10'),
(5, 'Vikram Patel',   'Human Resources', 58000.00, '2021-01-25'),
(6, 'Sneha Reddy',    'Sales',       65000.00, '2022-09-05');
GO