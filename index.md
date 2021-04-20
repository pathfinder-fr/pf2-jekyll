---
title: Bienvenue
---
# Licences
- OGL et PCUP à placer pour le jeu - lien vers Paizo - lien vers le site BBE pour la gamme
- Licences techniques logicielles pour le site

# Généralités (partie terminée manuellement)
- [Introduction](generalites/introduction.md)
  - [Les bases du jeu](generalites/Bases-du-jeu.md)
  - [Jouer une partie](generalites/jouer-une-partie.md)
  - Les [mots clefs](generalites/mots-clefs.md)
  - [Présentation des éléments de règle](generalites/presentation-des-elements-de-regle.md)
  - La [Création d'un personnage](generalites/creation-d-un-personnage.md)
  - [Gagner un niveau](generalites/gagner-un-niveau.md)
- Voir si on peut placer un lien téléchargeable permettant de bénéficier d'une feuille de personnage

# Les ascendances et les historiques
- [Ascendances et historiques](ascendances-et-historiques/ascendances-et-historiques.md)
  - les [ascendances](ascendances-et-historiques/ascendances.md)
    - [Liste des ascendances](ascendances-et-historiques/liste-des-ascendances.md) 
  - les [Historiques](ascendances-et-historiques/historiques.md)
    - [Liste des historiques](ascendances-et-historiques/historiques.md) *contient une liste des historiques à générer à partir du compendium des historiques de Foundry :  
      - deux propositions de présentation : 
      - un **tableau** sur plusieurs colonnes affichant juste le nom en français et le nom en anglais avec un lien vers chaque historique dont il faudra pouvoir créer la page pour chacun en affichant le nom de vf, le nom en vo et la description.
      - une **unique page** de regroupement rassemblant tous les historiques établies sur deux colonnes avec le nom en vf (entre parenthèses le nom en vo en plus petit) suivi de la description en vf. Cette page risque d'être volumineuse puisque chaque campagne apporte son lot d'historiques.
  - les [Langues](ascendances-et-historiques/langues.md) *j'ai fait un tableau repris du LdB mais il faudra sans doute raffiner pour ajouter les langues extraites des différents bestiaires qui sont présentes dans le fichier de support fr de foundry mais pas dans les compendiums (https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2e-lang-fr/-/blob/master/fr.json) avec un ctrl F pour trouver les language*

# Les classes
- les [classes](classes/classes.md) *la page contient une liste à générer à partir du compendium des classes reprenant le nom de chaque classe*
- [lire les rubriques consacrées aux classes](classes/lire-les-classes.md)
    - Idéalement ce serait bien de pouvoir générer automatiquement les *pages de chaque classe* mais il faut voir s'il faut scinder la description ou insérer des balises permettant de récupérer les informations sur la page pour les traiter.
    - Idéalement, générer une page des dons de chaque classe en extrayant les dons ayant une valeur class dans la db des dons, le nom de la classe concernée puis afficher par niveau en affichant le nom du don, le type d'actions, le niveau puis en dessous en petit le nom anglais puis les prérequis, puis la description
- les [compagnons animaux](classes/compagnons-animaux.md)
  - les différents [types de compagnons animaux](classes/types-de-compagnons-animaux.md)
  - les [compagnons animaux spécialisés](classes/compagnons-animaux-specialises.md)
- les [familiers](classes/familiers.md)
  - les [pouvoirs de familier](classes/pouvoirs-de-familiers.md) *C'est une page à générer à partir du compendium foundry des pouvoirs de familier pour afficher simplement le nom de chaque pouvoir en vf (en vo en plus petit) suivi de sa description fr*
  - les [pouvoirs des maîtres](classes/pouvoirs-de-maitres.md) *NB : le compendium foundry ne comprend pas encore les pouvoirs des maîtres*
- les [archétypes](classes/archetypes.md)
  - les archétypes multiclasse *(idéalement, il faudrait générer la page à partir de fonction de tri du json des fichiers du compendium des archétypes en triant les archétypes multiclasse dont le nom est celui d'une classe de base comme alchimiste, barde,, druide) et la page de chaque archétype devrait pouvoir être ensuite générée*
  - les archétypes de classe *(Idéalement, il faudrait générer la page à partir de fonction de tri du json des fichiers du compendium des archétypes en retirant de la liste à afficher les archétypes multiclasse et générer chaque page de la même manière que pour les archétypes multiclasse)*

# Les compétences (partie terminée manuellement)
- les [compétences](competences/competences.md)
  - Les [tests et DD de compétence](competences/tests-et-DD-de-competence.md)
  - Les [actions de compétences générales](competences/actions-de-competences-generales.md)
  - [Description des compétences](competences/description-de-competences.md)
    - [Acrobaties](competences/acrobaties.md)
    - [Arcanes](competences/acrobaties.md)
    - [Artisanat](competences/artisanat.md)
    - [Athlétisme](competences/athlétisme.md)
    - [Connaissance](competences/connaissance.md)
    - [Diplomatie](competences/diplomatie.md)
    - [Discrétion](competences/discretion.md)
    - [Duperie](competences/duperie.md)
    - [Intimidation](competences/intimidation.md)
    - [Médecine](competences/medecine.md)
    - [Nature](competences/nature.md)
    - [Occultisme](competences/occultisme.md)
    - [Religion](competences/religion.md)
    - [Représentation](competences/representation.md)
    - [Société](competences/societe)
    - [Survie](competences/survie)
    - [Vol](competence/vol)

# Les dons
- La présentation des [dons](dons/presentation)
- [Liste des dons](dons/index.html)
  - Liste des dons **généraux** : extraire de la **feats.db** les dons ayant la valeur general mais pas ceux ayant la valeur skill, les classer par niveau, par ordre alphabétique. Afficher les champs suivant : nom en fr, Niveau, Prérequis, Avantage
  - Liste des dons de **compétence** : extraire de la **feats.db** les dons ayant la valeur skill sans la valeur general. Les présenter en les triant d'abord par niveau puis par prérequis, puis par ordre alphabétique. Idéalement, il faudrait trier tous les dons par compétence prérequise. Afficher un tableau avec les champs suivant : nom en fr, Niveau, Prérequis, Avantage


## Rubriques diverses
- [Liste des actions](actions/index.html)
- [Liste des états préjudiciables](etats/index.html)


Pour la rareté, voir comment la traiter (couleur ou indication C PC ou R pour Courant, peu courant ou rare)
