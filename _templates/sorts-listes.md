---
Title : template des listes de sorts
---
Il y a plusieurs listes à générer. Il y a une liste par tradition : arcanique, divine, primordiale ou occulte. Il y a une liste pour les sorts focalisés et une liste pour les rituels.

## Listes des sorts proprement dite
Pour l'affichage à partir de la spells.db , il faut pouvoir trier les sorts :
- par type, (sorts, rituels, focalisés) à partir de la valeur spell dans le champ `spellcategorie` 
- par tradition, dans le champ

| Nom du sort | Niveau | Intensif. | Rareté | École | Effet |
|:------------|:------:|:---------:|:------:|:-----:|:------|

Il faut afficher par niveau, puis par ordre alphabétique

Il faut afficher :
- Le **Nom** en vf du sort en gras (infobulle avec le name ?)
- La valeur du champ `level` sans changement
- La valeur du champ `rarity` traduit en vf
- La valeur du champ `school` traduit en vf
- Le résumé court de l'effet (il n'est actuellement pas encore traduit en vf et il faudra intégrer le champs dans les compendium pour Foundry en vf pour permettre de traduire les champs ou copier coller ceux du Ldb)

## Liste des rituels
Tri dans la spells.db avec la valeur ritual dans le champ `spellcategorie` 

## Autres listes
On peut créer des listes :
- par école ?
- par niveau ?