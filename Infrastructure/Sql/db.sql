-- Active: 1722303815535@@bzqowhsjm9nn7sfoweey-mysql.services.clever-cloud.com@3306@bzqowhsjm9nn7sfoweey

------------------------* TABLE USERS *---------------------------
CREATE TABLE Users(
    id_user INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100),
    email VARCHAR(150) UNIQUE,
    password VARCHAR(100),
    rol ENUM("Admin", "User")
);

------------------------* TABLE BOOKS *---------------------------
CREATE TABLE Books(
    id_book INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(50),
    author VARCHAR(100),
    gender VARCHAR(100),
    datePublication DATETIME,
    copiesAvailable INT,
    status ENUM('Borrowed', 'Available', "Delete")
);

------------------------* TABLE LOANS *---------------------------
CREATE TABLE Loans(
    id_loan INT AUTO_INCREMENT PRIMARY KEY,
    userId INT,
    bookId INT,
    dateLoan DATETIME,
    dateReturn DATETIME,
    status ENUM("Wait", "Authorized", "Complete", "Denied"),
    FOREIGN KEY (userId) REFERENCES Users(id_user),
    FOREIGN KEY (bookId) REFERENCES Books(id_book)
);

------------------------* DROP TABLES *---------------------------
DROP TABLE `Books`;
DROP TABLE `Loans`;
DROP TABLE `Users`;

------------------------* SELECT TABLES *---------------------------
SELECT * FROM Books;
SELECT * FROM Loans;
SELECT * FROM Users;

------------------------* SHOW TABLES *---------------------------
SHOW TABLES;