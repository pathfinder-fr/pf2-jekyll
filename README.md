# Site SRD Pathfinder 2 Français

Ce projet contient le source et les données nécessaire pour créer un exemplaire du site Pathfinder 2 SRD.

## Préparation du projet

Pour récupérer les fichiers du projet, vous devez installer **git** sur votre poste.

Une fois git installé, vous devez cloner le projet avec la commande suivante : `git clone --recurse-submodules https://gitlab.com/pathfinder-fr/pf2-jekyll.git`.
Cette commande va créer un dossier `pf2-jekyll`.

## Interactions avec git

Pour mettre à jour votre contenu du site en local et les scripts, vous devez utiliser la commande `git pull` qui va se charger de récupérer toutes les données du site depuis le serveur.

Pour mettre à jour les données officielles en anglais et les données de traduction, vous devez utiliser la commande `git submodule foreach git pull`, éventuellement précédée de `git submodule update`.

Si l'une des commandes ci dessus a modifié votre contenu, ou si vous effectuez des changements, vous devrez envoyer les données modifiées au serveur.
Pour vérifier si vous avez des données modifiez, tapez la commande `git status` : elle vous indiquera si des changements sont détectés.

Si vous avez des changements, utilisez successivement la commande `git add .` pour inclure toutes les modifications, suivie de `git commit -m "message"` (où message sera remplacé par un commentaire sur vos changements), et enfin suivi de `git push` pour envoyer les données au serveur. Avant d'envoyer vos données, faites toujours un `git pull` pour être sur d'avoir votre dossier à jour.

## Génération du contenu

Pour générer le contenu des pages, vous allez devoir exécuter les scripts qui se trouvent dans le dossier `_scripts`.

Chaque script permet de générer un type de pages précis (generate-actions.csv génère toutes les pages des actions).

Vous devez avoir installé le [dernier SDK .NET](https://dotnet.microsoft.com/download) sur votre poste.
Pour vérifier si il est installé, lancez simplement la commande `dotnet --info`.
Si cette commande fonctionne vous êtes OK.

Vous devez ensuite installer le moteur de scripts, via la commande `dotnet tool install dotnet-script -g`.
Si vous avez déjà installé cet outil et souhaitez vérifier si il existe une mise à jour lancez la commande `dotnet tool update dotnet-script -g`.

Une fois tous les outils en place, vous pouvez utiliser la commande `dotnet script _scripts/generate-actions.csx` par exemple pour générer toutes les pages des actions d'après les dernières traductions.

## Génération du site

Plusieurs logiciels et composants sont requis pour pouvoir générer le site sur votre ordinateur.

### Pré-requis

*Linux* ou *Sous-système Windows pour Linux* (WSL 1 ou 2) : les guides d'installations sont fournis dans la [documentation jekyll](https://jekyllrb.com/docs/installation/).

Si vous utilisez `wsl`, vous devrez ensuite lancer toutes les commandes indiquées ici depuis un invite de commande sous linux.

### Génération

Une fois les pre-requis installés et ce dépôt copié sur votre disque, vous devriez pouvoir lancer la commande `bundle exec jekyll build` qui génèrera une copie du site dans le dossier `_site`.

Utilisez la commande `bundle exec jekyll serve` pour lancer une copie du site. En général le site pourra ensuite être consulté à l'adresse [](http://127.0.0.1:4000/srd/pf2/).