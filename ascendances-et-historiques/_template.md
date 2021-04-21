----
Title: template des ascendances
---
- Créer une page de regroupement pour faire apparaître toutes les ascendances accessibles en les affichant par ordre alphabétique avec une couleur ou une indication en fonction de la rareté. Pour gérer la Rareté : il y a un trait `rarity` dans chaque ascendance qui détermine la rareté de l'ascendance donnée.

- générer automatiquement les pages des ascendances *à partir de la structure des pages du compendium des ascendances de Foundry* issues de la ancestries.db :

Sur chaque page d'ascendance afficher :
- **Nom** en français avec un titrage h1
- **Name** en anglais en plus petit
- Traduction : **État d'origine:** et en cas d'absence **État** qui permettra de dire si c'est libre ou officielle 

- **description (fr)** le html doit être cleanup et constituera le centre de la page (fait avec juste un souci de cleanup sur les noms propres de l'ascendance)

- **Héritages de l'ascendance** :
les héritages de l'ascendance sont à aller chercher dans **ancestryfeatures.db**.
    - Pour effectuer le tri dans les données :
       - récupérer d'abord tous les fichiers dont le champ `featType` : `"featType": {"value": "heritage"}. Cela va d'abord permettre de récupérer tous les héritages 
       - puis dans ces fichiers, il faut faire le tri pour s'assurer que la value du trait correspondant à l'ascendance correspond à l'ascendance sur laquelle on souhaite place l'héritage ex :`"traits": {value": elf})` si l'ascendance est celle de l'elfe,
    - une fois le tri des données effectué, il faut alors faire un cleanup du html pour passer en markdown, puis afficher les données de l'héritage sur la page les uns à la suite des autres en affichant par ordre alphabétique : 
        - **nom** en gros suivi de **Name** en petit, 
        - description (Fr) 

Dans une colonne séparée à droite de la page, il faut aller chercher des valeurs du json de l'ascendance pour afficher  
- **Points de vie**  
puis en dessous afficher la valeur du champ de `hp` du json de l'ancestry 
 
- **Taille**  
puis en dessous Afficher la valeur du champ `size`du json de l'ancestry. La taille est en anglais il faut la traduire à partir du fr.json

- **Vitesse**
puis le champ `speed`donne la vitesse mais en pieds, à convertir en multipliant par 1,5 la valeur et en ajoutant la lettre m. On peut réfléchir à afficher en pieds, en m et en cases

**Primes de caractéristiques**
les champs `boosts 0, 1, 2` du json de chaque ancestry contiennent les primes de caractéristiques. Elles sont en anglais et sous forme d'abbréviation. Pour les traduire, on peut partir du fr.json et du champ "AbilityXxx" où Xxx est égal aux trois premières lettre de la carac ("AbilityStr": pour Str) pour récupérer l'affichage de la Force. Cela devrait afficher deux primes de caractéristique
puis mention Prime de caractéristique libre

NB (voir pour l'humain qui dispose de deux primes de caractéristique libre)

- **Pénalité de caractéristique**
puis récupérer le champ `flaws` du json de l'ancestry. comme les primes, la pénalité est en anglais et sous forme abrégée (ex : Str). Idem que ci-dessus pour afficher la caractéristique en français. 

- **langues** languages donne les langues parlées d'office par l'ascendance et peuvent être traduites à partir de Languagenomdelaraceanglaise en récupérant la valeur d'affichage dans le fr.json 

- *Langues supplémentaires* champ `additionallanguages` du json. Elles sont en anglais et peuvent être traduites à partir de Languagenomdelaraceanglaise pour récupérer la valeur dans le fr.json  

- **traits**
trait value donne les traits de l'ascendance en anglais à traduire à partir du fr.json avant affichage 


// viennent ensuite les capacités de l'ascendance. Cela renvoie à l'id d'une particularité qui se trouve dans la ancestryfeatures.db. L'ID permet d'aller la chercher pour ensuite l'afficher
champ `items name = 0898m` donne une id qui correspond à la vision en anglais (vision dans le noir, vision nocturne)
champ `fvpio` donne une id qui correspond à une capacité raciale qui se trouve dans la ancestryfeatures.db
champ `ouwoo` donne une particularité raciale (dans la ancestryfeatures.db avec son id qui permet d'aller la chercher pour l'affichage avec le nom et la description) 
    - **nom**  
    - descriptionFr



- Idéalement, générer soit à part, soit à la suite une page des dons de chaque ascendance en extrayant les dons ayant une valeur ancestry dans la feats.db, le nom de l'ascendance `"traits": {value": elf})` puis afficher en triant par niveau puis ordre alphabétique en affichant : Nom du don en fr, type d'action, niveau puis en dessous le nom anglais puis les prérequis, puis la descriptionFr. 

    - Pour certaines ascendances (demi-orc, demi-elfe), les pages n'existent pas dans le compendium Foundry en tant que telles et elles permettent d'obtenir les dons des deux ascendances mêlées ex elfe et humain. On aura aussi à gérer les héritages versatiles qui peuvent être ajoutés à toutes les ascendances
