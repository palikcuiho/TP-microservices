# TP Microservices

Application de gestion d'emprunts de livres avec une architecture microservices

## Tech stack

. API Gateway : 
. Config Server : 
. Services :


## Installation et mise en route

Cloner le dépôt git

###bdd
Se rendre dans le dossier Databases, et exécuter la commande `docker-compose up -d`
créer les migrations avec la commande `dotnet ef migrations add NomDeLaMigration` et les appliquer aux bases de données avec `dotnet ef database update`


Se rendre dans le dossier Kafka
-exécuter la commande `docker-compose up -d`
- dans le container, exécuter les commandes qui se trouvnet dans le script `commands.sh`

Se rendre dans le dossier config-server, et exécuter la commande `mvn spring-boot:run`
