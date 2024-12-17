# Service de contrôle parental

## Description
Ce projet est un service de contrôle parental qui permet de bloquer l'accès à certaines applications pour certains utilisateurs.

## Installation

### Prérequis
SQLSERVER doit être installé sur la machine.
SQLSERVER doit être en cours de fonctionnement.

### Installation

#### Base de données 
Dans le dossier SmartLockerData/DBQueries, modifier le fichier CreateAllDatabase.sql tel que les lignes 7 ET 9 contienent le chemin absolu vers
DBQueries sur votre machine: 
FILENAME = N'ABSOLUTE/PATH/TO/DBQUERIES/SmartLockerDB.mdf'
FILENAME = N'ABSOLUTE/PATH/TO/DBQUERIES/SmartLockerDB_log.ldf'
 
Dans le dossier SmartLockerData/DBQueries, copier le fichier CreateAllDatabase.sql et l'exécuter dans SQLSERVER.

Ce script va créer la base de données SmartLockerDB, les tables et les procédures stockées, ainsi que créer les autorisations
d'accès pour le service windows SmartLocker.

Dans le dossier SmartLockerData, modifier le fichier AppDbContext.cs tel que la ligne 16 pointe bien vers votre serveur de bdd:
optionsBuilder.UseSqlServer("Server=***VOTRE SERVEUR***;Database=SmartLockerDB; Integrated Security=True; TrustServerCertificate=True;");
Pareil dans le fichier SmartLocker/DataService.cs, ligne 15:
private string connectionString = "Server=***VOTRE SERVEUR***;Database=SmartLockerDB; Integrated Security=True; TrustServerCertificate=True;";

#### Build de l'application
Ouvrir le projet SmartLocker.sln dans Visual Studio en mode administrateur (Impératif pour la suite). 
Dans le menu Build, cliquer sur Build Solution.

#### Installation du service
Ouvrir une invite de commande en mode administrateur.
Naviguer jusqu'au dossier SmartLocker/bin/Debug. 'cd ./SmartLocker/bin/Debug'
Exécuter la commande suivante: 'installutil SmartLocker.exe'

#### Première execution du service
Dans le menu Démarrer, rechercher Services.
Dans la liste des services, rechercher SmartLocker.
Faire un clic droit sur SmartLocker et cliquer sur Démarrer.
Par la suite, SmartLocker démarrera automatiquement à chaque démarrage de la machine.

## Utilisation
Demmarer le projet SmartLockerWindows dans Visual Studio.
L'application SmartLockerWindows permet de gérer les utilisateurs et les applications bloquées.
Dans le premier ecran, les applications se mettent à jour automatiquement. Vous pouvez en selectionner une, 
selectionner le type de restriction et cliquer sur "Bloquer"

Le service SmartLocker va alors bloquer l'application pour l'utilisateur selectionné quand les conditions de restriction sont remplies.
Toutes les restrictions sont enregistrées dans la base de données SmartLockerDB, et sont réinitialisées tout les jours a minuit.

Depuis l'écran Gestion, vous pouvez supprimer les restrictions en cliquant sur "Supprimer" sur la ligne, ce qui enlèvera la restriction pour
l'utilisateur / application ciblée.

## Suppression du service après le test

Dans Visual Studio, ouvrir le projet SmartLocker.sln en mode administrateur.
Ouvrir une invite de commande.
Naviguer jusqu'au dossier SmartLocker/bin/Debug. 'cd ./SmartLocker/bin/Debug'
Exécuter la commande suivante: 'installutil /uninstall SmartLocker.exe'