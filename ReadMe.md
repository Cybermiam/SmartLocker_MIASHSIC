# Service de contr�le parental

## Description
Ce projet est un service de contr�le parental qui permet de bloquer l'acc�s � certaines applications pour certains utilisateurs.

## Installation

### Pr�requis
SQLSERVER doit �tre install� sur la machine.
SQLSERVER doit �tre en cours de fonctionnement.

### Installation

#### Base de donn�es 
Dans le dossier SmartLockerData/DBQueries, modifier le fichier CreateAllDatabase.sql tel que les lignes 7 ET 9 contienent le chemin absolu vers
DBQueries sur votre machine: 
FILENAME = N'ABSOLUTE/PATH/TO/DBQUERIES/SmartLockerDB.mdf'
FILENAME = N'ABSOLUTE/PATH/TO/DBQUERIES/SmartLockerDB_log.ldf'
 
Dans le dossier SmartLockerData/DBQueries, copier le fichier CreateAllDatabase.sql et l'ex�cuter dans SQLSERVER.

Ce script va cr�er la base de donn�es SmartLockerDB, les tables et les proc�dures stock�es, ainsi que cr�er les autorisations
d'acc�s pour le service windows SmartLocker.

Dans le dossier SmartLockerData, modifier le fichier AppDbContext.cs tel que la ligne 16 pointe bien vers votre serveur de bdd:
optionsBuilder.UseSqlServer("Server=***VOTRE SERVEUR***;Database=SmartLockerDB; Integrated Security=True; TrustServerCertificate=True;");
Pareil dans le fichier SmartLocker/DataService.cs, ligne 15:
private string connectionString = "Server=***VOTRE SERVEUR***;Database=SmartLockerDB; Integrated Security=True; TrustServerCertificate=True;";

#### Build de l'application
Ouvrir le projet SmartLocker.sln dans Visual Studio en mode administrateur (Imp�ratif pour la suite). 
Dans le menu Build, cliquer sur Build Solution.

#### Installation du service
Ouvrir une invite de commande en mode administrateur.
Naviguer jusqu'au dossier SmartLocker/bin/Debug. 'cd ./SmartLocker/bin/Debug'
Ex�cuter la commande suivante: 'installutil SmartLocker.exe'

#### Premi�re execution du service
Dans le menu D�marrer, rechercher Services.
Dans la liste des services, rechercher SmartLocker.
Faire un clic droit sur SmartLocker et cliquer sur D�marrer.
Par la suite, SmartLocker d�marrera automatiquement � chaque d�marrage de la machine.

## Utilisation
Demmarer le projet SmartLockerWindows dans Visual Studio.
L'application SmartLockerWindows permet de g�rer les utilisateurs et les applications bloqu�es.
Dans le premier ecran, les applications se mettent � jour automatiquement. Vous pouvez en selectionner une, 
selectionner le type de restriction et cliquer sur "Bloquer"

Le service SmartLocker va alors bloquer l'application pour l'utilisateur selectionn� quand les conditions de restriction sont remplies.
Toutes les restrictions sont enregistr�es dans la base de donn�es SmartLockerDB, et sont r�initialis�es tout les jours a minuit.

Depuis l'�cran Gestion, vous pouvez supprimer les restrictions en cliquant sur "Supprimer" sur la ligne, ce qui enl�vera la restriction pour
l'utilisateur / application cibl�e.

## Suppression du service apr�s le test

Dans Visual Studio, ouvrir le projet SmartLocker.sln en mode administrateur.
Ouvrir une invite de commande.
Naviguer jusqu'au dossier SmartLocker/bin/Debug. 'cd ./SmartLocker/bin/Debug'
Ex�cuter la commande suivante: 'installutil /uninstall SmartLocker.exe'