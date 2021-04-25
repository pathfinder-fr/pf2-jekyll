---
Title : template des listes de sorts
---
Il y a plusieurs listes à générer. 

- Il y a une liste des sorts par **tradition** : arcanique, divine, primordiale ou occulte. Il y a une liste pour les sorts focalisés et une liste pour les rituels. Chacune doit être générée sur une page liste-nomdelatradition.md
- Il y a également la liste des sorts focalisés pour les rituels sur la page liste-rituels.md

ATTENTION la liste des sorts focalisés fait l'objet d'un template différent 

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

## Liste des rituels qui forment des sorts particuliers
C'est le même générateur que le précédent avec comme seule différence que l'on doit faire le Tri dans la spells.db avec la valeur **ritual** dans le champ `spellcategtorie` 



## Autres listes
On peut créer des listes :
- par école ?
- par niveau ?