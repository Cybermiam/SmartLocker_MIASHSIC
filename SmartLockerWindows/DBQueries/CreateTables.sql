

-- Création de la table Utilisateur
CREATE TABLE Utilisateur (
    id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(256) NOT NULL,
    role BIT NOT NULL
);

-- Création de la table Application
CREATE TABLE App (
    id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(256) NOT NULL
);

-- Création de la table StatistiquesUtilisation
CREATE TABLE StatistiquesUtilisation (
    id INT PRIMARY KEY IDENTITY(1,1),
    time INT NOT NULL,
    date TIME NOT NULL,
	userId INT NOT NULL,
	appId INT NOT NULL,
    FOREIGN KEY (userId) REFERENCES Utilisateur(id),
    FOREIGN KEY (appId) REFERENCES App(id)
);

-- Création de la table ContrainteJour
CREATE TABLE ContrainteJour (
    id INT PRIMARY KEY IDENTITY(1,1),
    userId INT NOT NULL,
    appId INT NOT NULL,
    maxTime INT NOT NULL,
    usedTime INT NOT NULL,
    FOREIGN KEY (userId) REFERENCES Utilisateur(id),
    FOREIGN KEY (appId) REFERENCES App(id)
);

-- Création de la table ContrainteHoraire
CREATE TABLE ContrainteHoraire (
    id INT PRIMARY KEY IDENTITY(1,1),
    userId INT NOT NULL,
    appId INT NOT NULL,
    maxTime INT NOT NULL,
    blockTime INT NOT NULL,
    usedTime INT NOT NULL,
    FOREIGN KEY (userId) REFERENCES Utilisateur(id),
    FOREIGN KEY (appId) REFERENCES App(id)
);

-- Création de la table ContrainteSemaine
CREATE TABLE ContrainteSemaine (
    id INT PRIMARY KEY IDENTITY(1,1),
    userId INT NOT NULL,
    appId INT NOT NULL,
    mondayTime INT NOT NULL,
    tuesdayTime INT NOT NULL,
    wednesdayTime INT NOT NULL,
    thursdayTime INT NOT NULL,
    fridayTime INT NOT NULL,
    saturdayTime INT NOT NULL,
    sundayTime INT NOT NULL,
    usedTime INT NOT NULL,
    FOREIGN KEY (userId) REFERENCES Utilisateur(id),
    FOREIGN KEY (appId) REFERENCES App(id)
);
