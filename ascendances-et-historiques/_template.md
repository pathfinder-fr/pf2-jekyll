À partir d'une page de regroupement, les ascendances sont accessibles en les affichant par ordre alphabétique avec une couleur ou une indication en fonction de la rareté.

    - générer automatiquement les pages des ascendances *à partir de la structure des pages du compendium des ascendances de Foundry* issues de la ancestries.db :

Sur chaque page d'ascendance afficher
- Titre l'ascendance en français (en plus petit en anglais)
- La descriptionFr en html en français au centre de la page
- La liste des héritages correspondant à cette ascendance : issues de la db **ancestryfeatures** Pour le tri (aller récupérer `"featType": {"value": "heritage"}, puis si la value du trait correspondant à l'ascendance correspond "traits": {value": elf})`, afficher l'héritage par ordre alphabétique en indiquant : le nom en français, le nom en anglais suivi de sa description

Dans une colonne à droite de la page avec dans l'ordre
    - *Points de vie au niveau 1* suivi en dessous de la valeur de hp dans le json de l'ancestry 
    - *langues* languages donne les langues parlées d'office par l'ascendance
    - *Langues supplémentaires* suivi en dessous des langues parlées accessibles à extraites du champ additionallanguages du json. Elles sont en anglais et peuvent être traduites à partir de Languagenomdelaraceanglaise dans le fr.system  
    - *Primes de caractéristique* suivi en dessous des primes accessibles extraites des champs boosts 0 et 1 du json de l'ancestry. Elles sont en anglais et sous forme d'abbréviation. Elles peuvent être récupérées à partir du champ "AbilityXxx" "AbilityStr": pour Str dans le fr.system pour pouvoir afficher Force. puis Prime de caractéristique libre NB (voir pour l'humain qui dispose de deux primes de caractéristique libre)
    - *Pénalité de caractéristique* suivi en dessous des primes accessibles extraites du champ flaws du json de l'ancestry. Elles sont en anglais et sous forme d'abbréviation. Elles peuvent être récupérées à partir du champ "AbilityXxx" "AbilityStr": pour Str dans le fr.system pour pouvoir afficher Force. 
    - Particularités :
        - items name = 0898m donne une id correspondant à la vision en anglais (vision dans le noir, vision nocturne)
        - fvpio , donne une particularité raciale (dans la ancestryfeatures.db avec son id qui permet d'aller la chercher pour l'affichage avec le nom et la description)
        - ouwoo (id et name), donne une particularité raciale (dans la ancestryfeatures.db avec son id qui permet d'aller la chercher pour l'affichage avec le nom et la description) 
    - *Taille* le champ size donne la taille
    - *Vitesse* le champ speed donne la vitesse mais en pieds à convertir en m en multipliant par 1,5 et en ajoutant m
    - *Rareté* traits rarity value donne la rareté permettant de colorer le titre de la page en css à l'affichage ou d'ajouter une mention ?
        - traits value donne les traits de l'ascendance en anglais
    
- Idéalement, générer soit à part, soit à la suite une page des dons de chaque ascendance en extrayant les dons ayant une valeur ancestry dans la feats.db, le nom de l'ascendance `"traits": {value": elf})` puis afficher en triant par niveau puis ordre alphabétique en affichant : nom du don en fr, type d'action, niveau puis en dessous le nom anglais puis les prérequis, puis la descriptionFr. 
    - Pour certaines ascendances (demi-orc, demi-elfe), les pages n'existent pas dans le compendium Foundry en tant que telles et elles permettent d'obtenir les dons des deux ascendances mêlées ex elfe et humain. Voir ce qui peut être réalisé ou créé mais en option
