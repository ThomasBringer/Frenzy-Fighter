# Frenzy-Fighter

## Temps passé

Phase 1 - Obligatoire : 5h30

Phase 2 - UI : 1h30

Phase 3 - Ennemis : 3h30

Phase 4 - Armes : 1h

Phase 5 - Polish : 2h30

-----

Total : 14h

## Difficultés et problématiques techniques

### Phase 1 - Obligatoire
- J'ai eu une erreur de Unity qui m'empêchait de build mon projet sous Android. Après avoir upgradé le projet sous Unity 2022.3.21f1, j'ai pu à nouveau build le projet.
- Le joystick virtuel a posé des questions de conception. En effet, il m'est vite apparu qu'il était important que le joystick ait la même taille sur tous les écrans. Pour que ce soit le cas, j'ai configuré le Canvas Scaler de Unity en Constant Physical Size. Cela a aussi posé un problème de conversion. En effet, dans mon script du joystick, j'ai besoin de renseigner une distance qui correspond à la distance que doit parcourir le stick interne au sein du joystick. Cette distance doit être renseignée en points, afin que sa taille physique soit constante sur tous les écrans. Il y a alors une conversion de points en pixels à faire. Cette conversion n'est pas un défi technique particulier, il suffit d'utiliser un facteur de conversion propre à chaque écran (le DPI).
- Pour le déplacement du personnage, les attaques et les animations, je n'ai pas eu de difficulté particulière. J'ai déjà développé des systèmes très semblables, je savais donc immédiatement comment m'y prendre.
- Quelques cas limites ont été un peu difficiles à voir de prime abord au sujet des animations. Par exemple, un bug était le suivant : en équipant une arme rapide, le joueur continuait d'attaquer la cible, et ce même quand l'ennemi était mort. En fait, comme l'attaque du joueur était un Animation Event de Unity, et que l'on transitionne en 250 ms de l'animation de combat vers l'animation de idle, eh bien selon la vitesse d'animation (liée à l'arme), l'événement d'attaque peut être déclenché pendant la transition entre les états combat et idle ! Une fois cette découverte établie, la résolution était simple, il suffisait de vérifier que le joueur avait une cible devant lui avant d'attaquer. Ce genre de difficulté est ordinaire et j'ai l'habitude de pister ce genre de bugs quand je travaille avec des animations, par exemple.
- L'implémentation des armes et de leurs statistiques pose une question de conception, mais c'est un problème très classique auquel j'ai l'habitude de répondre en utilisant les Scriptable Objects de Unity. Cela permet d'ajouter des statistiques distinctes très facilement et de manière propre.

### Phase 2 - UI
- Pour les barres de vie, il y a un problème classique d'étirement des coins de la barre si on stretch une image de barre de vie. Je connais déjà ce problème et sa solution, les 9-sliced sprites, donc aucun souci.
- Il y a un autre problème spécifique aux barres de vie dans les jeux 3D : si on place la barre au-dessus de la tête du personnage, mais que celui-ci fait une animation où le personnage va vers le haut, alors le personnage clip avec la barre de vie, ce qui est assez peu esthétique. On pourrait bien sûr remonter la barre de vie, mais on n'adresse pas le fond du problème. Il faut que la barre de vie soit toujours affichée devant le personnage à l'écran. Pour ça, j'utilise une technique classique, que j'avais déjà utilisé dans des projets pour éviter que les armes d'un FPS clip à travers le mur. La technique consiste à avoir deux caméras dont une qui ne s'occupe que de render le UI en overlay. Seul problème, il m'a fallu quelques minutes avant de trouver tous les paramètres pour faire ce setup avec URP sous Unity (j'ai un petit peu plus l'habitude avec la render pipeline par défaut). Rien de bien méchant, une recherche sur Internet m'a débloqué.
- Il y a aussi une question de conception : je ne veux pas modifier le script Health (qui gère la vie et les dégâts) lorsque je rajoute des effets visuels comme la barre de vie ou l'affichage des dégâts. J'utilise donc des Unity Events pour que le script Health soit indépendant des scripts HealthBar et DamageText. Ainsi, HealthBar et DamageText sont des briques de code qui se rajoutent à la fonctionalité Health et viennent le compléter. Avec cette structure il est très facile d'ajouter ou supprimer des fonctionnalités : il suffit de désactiver l'objet HealthBar pour enlever les barres de vie, pas besoin de modifier le script Health.

### Phase 3 - Ennemis
- Problématique du choix de technologie pour faire des IA d'ennemi. Je choisis le plus standard, le NavMesh de Unity. Ça a l'avantage d'être puissant (on peut par exemple faire du pathfinding) et facile et rapide à mettre en place.
- Léger problème : les ennemis peuvent pousser le joueur. Normal, les ennemis essaient de se rapprocher au plus du joueur, qui est un NavMesh Agent. Ma solution est de changer le paramètre Priority du joueur à 1 (priorité maximale). Ainsi le joueur peut pousser les ennemis, mais la réciproque est fausse.
- Question de conception du comportement des ennemis. Le comportement le plus simple, qui consiste à ce que les ennemis se rapprochent constamment du joueur, est très lassant, et les ennemis tendent à se grouper très rapidement du fait de leur comportement semblable. Je choisis un comportement un peu plus compliqué pour les ennemis (mais toujours très standard) : les ennemis patrouillent au hasard. Seulement quand ils voient le joueur, ils se mettent à poursuivre le joueur. Cela reste un comportement basique, à améliorer pour rendre les ennemis plus crédibles.

### Phase 4 - Armes
- Pas de difficulté particulière pour la mécanique de spawn d'armes. Avec le système en place des ScriptableObject, le code pour spawn et équiper les armes est similaire au code déjà effectué plus tôt pour spawn les ennemis et pour les attaquer.
- Seule problématique : afficher l'arme (un modèle 3D) sur le UI. Je connais deux manière de le faire, soit utiliser des Render textures, soit mettre la caméra en mode Screen space - camera et placer l'arme directement sur le GUI. J'ai choisi la deuxième option. À noter qu'en choisissant la deuxième option, il faut veiller à ce que le GUI ne clip pas avec le jeu. Ici pas de souci, j'ai déjà mis en place une deuxième caméra pour éviter ce problème (comme décrit phase 2 tiret 2).

## Comment m'améliorer

À compléter

## Que pourrais-je ajouter

À compléter
