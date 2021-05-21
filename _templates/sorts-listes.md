---
Title : template des listes de sorts
---
Ce template est à faire après avoir réalisé celui de l'affichage de la description des sorts qui contient la liste des champs à extraire que l'on retrouvera sur ce template

Il y a plusieurs listes à générer. 
- une liste des sorts par **tradition** sur la page liste-nomdelatradition.md 
    - arcanique, liste-arcanique.md
    - divine, liste-divine.md
    - primordiale liste-primordiale.md
    - occulte liste-occulte.md.
- une liste des rituels sur la page liste-rituels.md

ATTENTION les sorts focalisés font l'objet d'un template différent et ne font pas l'objet de listes 

## Listes des sorts proprement dite
### Format d'affichage

| Nom du sort | Niveau | Intensif. | Rareté | École | Effet |
|:------------|:------:|:---------:|:------:|:-----:|:------|

### Champs nécessaires pour les fonctions de tri
Pour l'affichage à partir de la spells.db , il faut donc avoir les champs :
- par type, (sorts, rituels, focalisés) à partir de la valeur du champ `spellcategorie` est **spell** 
- par tradition, dans le champ `tradition` 

Il faut extraire :
- Le **Nom** en vf du sort en gras (infobulle avec le Name ?)
- La valeur du champ `level` sans changement
- Une indicateur *'oui'* s'il existe dans la description l'expression exacte *"<p>Intensif"* et un indicateur *'non'* s'il n'existe pas
- La valeur du champ `rarity` traduit en vf
- La valeur du champ `school` traduit en vf
- Le résumé court de l'effet (il n'est actuellement pas encore traduit en vf et il faudra intégrer le champs dans les compendium pour Foundry en vf pour permettre de traduire les champs ou copier coller ceux du Ldb)

Il faut afficher dans le tableau par niveau, puis par ordre alphabétique

## Liste des rituels 
### Tri 
On doit faire le Tri dans la spells.db avec la valeur **ritual** dans le champ `spellcategorie`  

### Champs utiles
On récupère uniquement les champs `Nom` et `level`

### Format de sortie
| Nom du sort | Niveau | 
|:------------|:------:|

L'affichage se fait par ordre de *niveau* puis *ordre alphabétique*  
Et on affiche simplement la valeur du champ `Nom` et celle du champ `level`  
La page est créée et liste-rituels.md 

| Nom du sort | Niveau | 
|:------------|:------:|

