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
    - [Liste des ascendances](ascendances-et-historiques/liste-des-ascendances.md) Il s'agirait d'une page re regroupement en les affichant par ordre alphabétique avec une couleur ou une indication en fonction de la rareté.
    - générer automatiquement les pages des ascendances *à partir de la structure des pages du compendium des ascendances de Foundry* issues de la ancestries.db :
      - descriptionFr en html en français permet l'affichage central
      - extraire les champs existants utiles qui sont en anglais pour constituer la colonne de droite de la page : 
        - additionalLanguages donne les langues parlées accessibles à la race
        - boosts 0 et 1, donnent les primes de caractéristiques de la race
        - flaws value, donne la pénalité de caractéristiques de la race
        - hp, donne les PV de l'ascendance au niveau 1
        - items name : 
          - 0898m donne une id correspondant à la vision en anglais (vision dans le noir, vision nocturne)
          - fvpio , donne une particularité raciale (dans la ancestryfeatures.db avec son id qui permet d'aller la chercher pour l'affichage avec le nom et la description)
          - ouwoo (id et name), donne une particularité raciale (dans la ancestryfeatures.db avec son id qui permet d'aller la chercher pour l'affichage avec le nom et la description) 
        - languages donne les langues parlées d'office par l'ascendance
        - size donne la taille
        - speed donne la vitesse mais en pieds à convertir en m en multipliant par 1,5 et en ajoutant m
        - traits rarity value donne la rareté permettant de colorer le titre de la page en css à l'affichage ou d'ajouter une mention ?
        - traits value donne les traits de l'ascendance en anglais
      - en affichant sur la page la liste des héritages correspondant à chaque ascendance : issues de la db **ancestryfeatures** Pour le tri (aller récupérer `"featType": {"value": "heritage"}, puis le bon trait correspondant à la race "traits": {value": elf})`, afficher par ordre alphabétique en indiquant : le nom en français, le nom en anglais et la description

    - Idéalement, générer à part une page des dons de chaque ascendance en extrayant les dons ayant une valeur ancestry dans la feats.db, le nom de l'ascendance `"traits": {value": elf})` puis afficher en triant par niveau puis ordre alphabétique en affichant : nom du don en fr, type d'action, niveau puis en dessous le nom anglais puis les prérequis, puis la descriptionFr. 
    - Pour certaines ascendances (demi-orc, demi-elfe), les pages n'existent pas dans le compendium Foundry en tant que telles et elles permettent d'obtenir les dons des deux ascendances mêlées ex elfe et humain. Voir ce qui peut être réalisé ou créé mais en option
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
  - Liste des dons généraux : extraire du tableau les dons ayant la valeur général mais pas ceux ayant la valeur skill, les classer par niveau et ordre alphabétique afficher les champs suivant : nom en fr, Niveau, Prérequis, Avantage
  - Liste des dons de compétence : extraire du tableau les dons ayant la valeur skill, sans la valeur skill. Les présenter en les triant par niveau, puis par prérequis, puis par ordre alphabétique. I déalement, il faudrait trier tous les dons par compétence prérequise. Le tableau doit également afficher les champs suivant : nom en fr, Niveau, Prérequis, Avantage


## Rubriques diverses
- [Liste des actions](actions/index.html)
- [Liste des états préjudiciables](etats/index.html)


Pour la rareté, voir comment la traiter (couleur ou indication C PC ou R pour Courant, peu courant ou rare)
