-- Create the SQL Server database and table for the GarageAPI project.
CREATE DATABASE GarageDb;
GO
USE GarageDb;
GO

CREATE TABLE Vehicles (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName NVARCHAR(150) NOT NULL,
    MobileNumber NVARCHAR(50) NOT NULL,
    VehicleModel NVARCHAR(150) NULL,
    VehicleNumber NVARCHAR(100) NULL,
    ProblemDescription NVARCHAR(MAX) NULL,
    DateReceived DATE NOT NULL,
    ExpectedReturnDate DATE NOT NULL
);

-- Sample data to test the API and frontend.
INSERT INTO Vehicles (CustomerName, MobileNumber, VehicleModel, VehicleNumber, ProblemDescription, DateReceived, ExpectedReturnDate)
VALUES
('Rahul Sharma', '9876543210', 'Honda City', 'MH12AB1234', 'Engine noise', '2026-04-24', '2026-04-28'),
('Priya Singh', '9123456780', 'Maruti Swift', 'MH14CD5678', 'Oil change', '2026-04-25', '2026-04-27'),
('Aman Verma', '9988776655', 'Toyota Innova', 'MH20EF9012', 'AC repair', '2026-04-23', '2026-05-02');
