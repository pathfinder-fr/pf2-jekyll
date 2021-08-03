---
# ATTENTION : Ne modifiez pas ce fichier
# Ce fichier est généré automatiquement par un script d'après les données du module Foundry VTT officiel et de sa traduction
title: Peau toxique
titleEn: Toxic Skin
id: kKKHwVUnroKuAnOt
urlFr: https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/blob/master/data/actions/kKKHwVUnroKuAnOt.htm
urlEn: https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/blob/master/packs/data/actions.db/toxic-skin.json
layout: actions
type: reaction
typeFr: Réaction
---
**Fréquence** une fois par heure

**Déclencheur** Une créature vous touche, par exemple en vous Saisissant, en réussissant à vous toucher avec une attaque à mains nues ou en utilisant un sort de contact contre vous.

Vous exsudez une toxine mortelle. La créature déclencheuse subit [[/r 1d4 #poison]] dégâts de poison (jet de Vigueur basique en utilisant le plus élevé entre votre DD de classe ou votre DD de sort). Au niveau 3 et tous les niveaux impairs par la suite, les dégâts augmentent de 1d4.

Dégâts de poison [[/r ceil(@details.level.value/2)d4 #poison]]{selon le niveau}
