---
# ATTENTION : Ne modifiez pas ce fichier
# Ce fichier est généré automatiquement par un script d'après les données du module Foundry VTT officiel et de sa traduction
title: Prodiguer les premiers soins
titleEn: Administer First Aid
id: MHLuKy4nQO2Z4Am1
urlFr: https://gitlab.com/pathfinder-fr/foundryvtt-pathfinder2-fr/-/blob/master/data/actions/MHLuKy4nQO2Z4Am1.htm
urlEn: https://gitlab.com/hooking/foundry-vtt---pathfinder-2e/-/blob/master/packs/data/actions.db/administer-first-aid.json
layout: actions
type: action
typeFr: Action Unique
---
**Conditions** Vous tenez ou portez des [instruments de guérisseur](../équipements/outils-de-guérisseur.html)

Vous prodiguez les premiers soins à une créature adjacente qui est mourante ou victime de saignements. Si une créature est mourante et victime de saignements, choisissez quelle affection vous voulez soigner avant de faire le test. Vous pouvez Prodiguer les premiers soins à nouveau pour tenter de soigner l’autre affliction.

- **Stabiliser** Faites un test de Médecine sur une créature dans l’état mourant à qui il reste 0 point de vie. Le DD est égal à 5 + le DD du test de récupération de cette créature (pour un total s’élevant le plus souvent à 15 + la valeur de son état mourant).
- **Stopper l'hémorragie** Faites un test de Médecine sur une créature qui subit des dégâts de saignement persistants, ce qui lui permet d’effectuer un autre test nu pour éliminer les dégâts persistants. Le DD est le plus souvent égal à celui de l’effet qui a provoqué le saignement.

**Succès** Si vous tentez de la stabiliser, la créature n’a plus l’état mourant (mais elle reste [inconsciente](../conditions/inconscient.html)). Si vous tentez de stopper l’hémorragie, la créature effectue un test nu pour mettre un terme au saignement.

**Échec critique**  Si vous tentez de la stabiliser, la valeur de l’état mourant de la créature augmente de 1. Si vous tentez de stopper l’hémorragie, elle subit immédiatement un montant de dégâts égal à ses dégâts de saignements persistants.
