-- Active: 1722341335283@@bzqowhsjm9nn7sfoweey-mysql.services.clever-cloud.com@3306

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
    datePublication DATE,
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

------------------------* SELECTS *---------------------------
SELECT * from Books;
SELECT * from Loans;
SELECT * from Users;

------------------------* DROPS *---------------------------
-- DROP TABLE Loans;
-- DROP TABLE Books;
-- DROP TABLE Users;

------------------------* DELETE FOR ID *---------------------------
DELETE FROM Books WHERE id_book=6;