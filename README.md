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

Instructiunile SPARQL se pot rula atat in applicatia de tip Console din C#, cat si in GraphDB, importand fisierul generat de applicatia C#.



Interogare SPARQL

PREFIX foaf: <http://xmlns.com/foaf/0.1/>
#(count(distinct ?tag) as ?count)
#(group_concat(?cont) as ?contributors)
SELECT ?MovieName ?CategoryType ?Description (COUNT(DISTINCT ?Contributors) AS ?NumberOfActors) (GROUP_CONCAT(DISTINCT ?FullName; SEPARATOR = "\n" ) as ?ListOfActors)
WHERE
{
?project a foaf:Project .
?project foaf:title ?MovieName .
?project foaf:description ?Description .
?project foaf:contributors ?Contributors .
?group a foaf:Group .
?group foaf:member ?project .
?group foaf:name ?CategoryType .
?Contributors a foaf:Person .
?Contributors foaf:givenname ?GivenName .
?Contributors foaf:family_name ?FamilyName .
BIND(concat(?GivenName," ",?FamilyName) AS ?FullName)
}GROUP BY ?MovieName ?CategoryType ?Description
