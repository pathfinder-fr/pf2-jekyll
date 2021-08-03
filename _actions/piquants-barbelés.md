---
# ATTENTION : Ne modifiez pas ce fichier
# Ce fichier est généré automatiquement par un script d'après les données du module Foundry VTT officiel et de sa traduction
title: Piquants barbelés
titleEn: Barbed Quills
id: oAWNluJaMlaGysXA
urlFr: https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/blob/master/data/actions/oAWNluJaMlaGysXA.htm
urlEn: https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/blob/master/packs/data/actions.db/barbed-quills.json
layout: actions
type: reaction
typeFr: Réaction
---
**Fréquence** une fois par jour

**Déclencheur** Vous êtes touché par une Frappe à mains nues ou une frappe avec une arme au corps à corps sans allonge.

Vos piquants se brisent dans la peau de votre agresseur. Vous infligez [[/r 1d8 #perforants]] dégâts perforants à la créature déclencheuse (jet de Réflexe basqieu contre le plus élevé entre votre DD de classe ou votre DD de sort). Sur un échec critique, la créature subit aussi [[/r 1d4 #saignement persistant]] dégâts de saignement persistant lorsque vos piquants se fichent dans sa peau. Au niveau 3 et chaque niveau impair par la suite, ces dégâts augmentent d'1d8 et les dégâts perforants persistants augmentent de 1.

dégâts perforants [[/r ceil(@details.level.value/2)d8 #piercing]]{selon le niveau}

dégâts de saignement persistants [[/r 1d4+ceil(@details.level.value/2)-1 #saignement persistant]]{selon le niveau}

*Note: le bonus de +1 lié à Hirsute n'est pas automatiquement ajouté*
