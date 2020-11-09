# Site SRD Pathfinder 2 Français

Ce projet contient le source et les données nécessaire pour créer un exemplaire du site Pathfinder 2 SRD.

## Génération du site

Plusieurs logiciels et composants sont requis pour pouvoir générer le site sur votre ordinateur.

### Pré-requis

*Linux* ou *Sous-système Windows pour Linux* (WSL 1 ou 2) : les guides d'installations sont fournis dans la [documentation jekyll](https://jekyllrb.com/docs/installation/).

Si vous utilisez `wsl`, vous devrez ensuite lancer toutes les commandes indiquées ici depuis un invite de commande sous linux.

### Génération

Une fois les pre-requis installés et ce dépôt copié sur votre disque, vous devriez pouvoir lancer la commande `bundle exec jekyll build` qui génèrera une copie du site dans le dossier `_site`.

Utilisez la commande `bindle exec jekyll server` pour lancer une copie du site. En général le site pourra ensuite être consulté à l'adresse [](http://127.0.0.1:4000/srd/pf2/).