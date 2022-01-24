# mcsw-project
Transformare date din SQL in RDF

Populare Baza de date MySQL cu baza de date de test sakila 

Conectare la MySQL server prin linie de comanda:
mysql -h localhost -u root -p

Importarea schemei si a datelor sakila:
source C:/poli/master/anu2/mcsw/proiect/sakila-db/sakila-data.sql;
source C:/poli/master/anu2/mcsw/proiect/sakila-db/sakila-schema.sql;

Foaf Classes Documentation
doc: http://xmlns.com/foaf/spec/
foaf:Project http://xmlns.com/foaf/spec/#term_Project
foaf:Person http://xmlns.com/foaf/spec/#term_Person

Clases used
foaf:Person
foaf:Project
foaf:Group 

foaf:Person este clasa ce reprezinta actorii 
foaf:Project este clasa ce reprezinta filmele
foaf:Group este clasa ce reprezinta categoriile


Librari folosite in C#

RDF
https://dotnetrdf.org/
MySQL
https://dev.mysql.com/doc/connector-net/en/connector-net-tutorials-connection.html
