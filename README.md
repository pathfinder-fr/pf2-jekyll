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

## Transformation FoundryVTT -> markdown

Le projet est branché sur 3 autres projets sous forme de submodules git:

**Module Pathfinder Anglais.** (dossier `_ext/module-en`).  
Module Foundry VTT pathfinder 2 officiel en anglais : [https://gitlab.com/hooking/foundry-vtt---pathfinder-2e]().

Il contient toutes les pages en anglais concernant des données.
Les données qui nous intéressent se trouvent dans le dossier `packs/data`, puis dans chaque dossier correspondant au type de données (ex: les actions se trouvent dans le dossier `action.db`).
Chaque donnée est stockée dans un fichier .json.

**Traduction FR.** (dossier `_ext/trads`).  
Traductions du module anglais en français : [https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr]()

Il contient toutes les données de traduction des fichiers du module anglais.
Les fichiers qui nous intéressent se trouvent dans le dossier `data`, puis dans le dossier correspondant au type de données.

Chaque donnée est stockée dans un fichier .htm nommé d'après son id anglais.

Note : l'id unique est généré dans le module anglais, dans une propriété `_id` au début du fichier json.

**Module Pathfinder Français.** (dossier `_ext/module-fr`).  
Module Foundry VTT FR : [https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2e-lang-fr]().

Module directement utilisable pour Foundry.
Il contient les données nécessaires pour traduire le module officiel anglais en français.

Note : toutes les données du projet de traduction ne sont pas forcément utilisées dans le module fr.

## Génération du contenu

Pour générer le contenu des pages, vous allez devoir exécuter les scripts qui se trouvent dans le dossier `_scripts`.

Chaque script permet de générer un type de pages précis (generate-actions.csv génère toutes les pages des actions).

Vous devez avoir installé le [dernier SDK .NET](https://dotnet.microsoft.com/download) sur votre poste.
Pour vérifier si il est installé, lancez simplement la commande `dotnet --info`.
Si cette commande fonctionne vous êtes OK.

Vous devez ensuite installer le moteur de scripts, via la commande `dotnet tool install dotnet-script -g`.
Si vous avez déjà installé cet outil et souhaitez vérifier si il existe une mise à jour lancez la commande `dotnet tool update dotnet-script -g`.

Une fois tous les outils en place, vous pouvez utiliser la commande `dotnet script _scripts/generate-actions.csx` par exemple pour générer toutes les pages des actions d'après les dernières traductions.

## Génération du site en local

Plusieurs logiciels et composants sont requis pour pouvoir générer le site sur votre ordinateur.

### Pré-requis

*Linux* ou *Sous-système Windows pour Linux* (WSL) : les guides d'installations sont fournis dans la [documentation jekyll](https://jekyllrb.com/docs/installation/).

#### WSL Sous Windows

Activez WSL : Depuis le menu démarrer lancez `Activer ou désactiver des fonctionnalités Windows` puis cochez la case `Sous-système Windows pour Linux`.
Vous devrez certainement redémarrer votre poste.

Ensuite depuis le Windows Store, recherchez et installez "Ubuntu 20.04 LTS".
Une fois Ubuntu installé, lancez le depuis le menu démarrer et préparez votre environnement linux en saisissant un mot de passe qui sera nécessaire pour faire entre autre les mises à jour.

Une fois Ubuntu lancé, suivez le guide d'installation [Jekyll pour Ubuntu](https://jekyllrb.com/docs/installation/ubuntu/).

Pour toutes les commandes qui seront ensuite indiquées, vous devrez les lancer depuis votre environnement Ubuntu.

Notez bien que sous linux, vos disques sont accessibles via le dossier `/mnt/c` pour le lecteur `c`. Exemple: si vous avez cloné le projet dans votre dossier `D:\Projets\pf2-jekyll`, le dossier linux correspondant sera `/mnt/d/Projets/pf2-jekyll`.

Notez aussi que vous pouvez utiliser VS Code avec l'extension [Shell launcher](https://marketplace.visualstudio.com/items?itemName=Tyriar.shell-launcher) pour utiliser directement le shell windows et WSL depuis VS Code.

#### Linux

(TODO)

### Génération du site

Avant de générer le site pour la première fois, vous devrez lancer la commande `bundle install` depuis le dossier du projet pour installer tous les composants nécessaires.

Une fois les pre-requis installés, vous pouvez lancer la commande `bundle exec jekyll build` qui génèrera une copie du site dans le dossier `_site`.

Utilisez la commande `bundle exec jekyll serve` pour lancer une copie du site. En général le site pourra ensuite être consulté à l'adresse [](http://127.0.0.1:4000/srd/pf2/).