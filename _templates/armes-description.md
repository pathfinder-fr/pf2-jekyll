---
Title: Description individuelle des armes
---
Chacune des armes répertoriées est décrite ci-dessous.

### Tri et extraction des données
Dans equipment.db du projet anglais
- on trie les fichiers dont la valeur du champ `type` est **weapon** 
- en triant les armes dont la valeur du champ `level` est **0** pour éviter de récupérer les armes magiques
- On récupère l'`ID` et le champ `name`

Dans le compendium equipment de Foundry, à partir de l'ID on récupère les champs
- Nom
- Description (Fr)

### Affichage sur la page
On Affiche sur la page dans l'ordre alphabétique de la valeur du champ Nom 
- la valeur de **Nom** en gras 
- suivi de la valeur de (Name) en plus petit
- Suivi de la description Fr