CREATE TABLE [dbo].[Employee] (
    [EmployeeID]         INT            IDENTITY (3000, 1) NOT NULL,
    [EmployeeFirstName]  NVARCHAR (100) NULL,
    [EmployeeLastName]   NVARCHAR (100) NULL,
    [EmployeeDOB]        INT            NULL,
    [EmployeeWage]       INT            NULL,
    [EmployeeSuperVisor] NVARCHAR (100) NULL
);

