# Frenzy-Fighter

Jeu développé en 17h pour le test technique de Madbox.

Jouer au jeu sur [itch.io](https://thomas-bringer.itch.io/frenzy-fighter).

## Temps passé

#### Phase 1 - Obligatoire : 5h30

#### Phase 2 - UI : 1h30

#### Phase 3 - Ennemis : 3h30

#### Phase 4 - Armes : 1h

#### Phase 5 - Polish : 2h30

#### Phase 6 - Améliorations visuelles : 3h

Durant cette phase, j'ai fait les ajouts suivants :
- Effet de freeze frame lors des attaques
- Screen shake lors des attaques
- Post-processing et aberration chromatique lors des attaques
- Particules (feu, électricité, lumière) sur les armes
- Réorganisation du code

#### Total : 17h

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

### Phase 5 - Polish
- Pas de difficulté particulière pour les menus.
- Il y a plusieurs effets visuels possibles pour les impacts d'armes. Je choisis un flash lumineux blanc pour les ennemis, et un flash lumineux rouge pour le joueur.
- Pour le son, je choisis de jouer les sons sous forme d'Animation Events de Unity. Ça me permet de synchroniser les sons sur les animations. Cela est très utile pour le bruit d'impact d'épée ou encore pour les bruits de pas.

### Phase 6 - Améliorations visuelles
Après ces phases recommandées dans le document fourni, j'ai voulu ajouter quelques détails pour améliorer le game feel et les visuels.
- Je voulais rendre le combat plus impactant. Pour ça, je rajoute un freeze frame au niveau des impacts d'armes.
- Je voulais rendre plus évidents les moments où le joueur prend des dégâts. Je rajoute un screen shake avec Cinemachine, et je rajoute un effet d'aberration chromatique avec le Post-processing, qui véhicule bien l'idée de dégâts.
- Le jeu manquait cruellement de particules, alors j'ai rajouté des effets de feu et d'électricité sur les armes. De plus, les armes laissées par terre ont effet de particules lumineux pour véhiculer l'idée que le joueur devrait les ramasser.

## Comment m'améliorer

Globalement, je me suis senti à l'aise avec ce test technique. Les mécaniques étaient assez standard et tous les systèmes à développer se rapprochaient plus ou moins d'un de mes projets précédents.

Si je devais m'améliorer, je pourrais considérer les points suivants :
- Pour les attaques de mêlée, j'aurais pu envisager d'utiliser les collisions de Unity (par exemple : Physics.OverlapSphere). Ici, j'ai fait des opérations de calcul de distance (je regarde quel ennemi est le plus proche du joueur en calculant les distances au carré). S'il y avait un très grand nombre d'ennemis, il pourrait être plus performant d'utiliser le système de collisions de Unity.
- Je devrais étudier les Addressable Assets de Unity pour pouvoir charger les assets de manière asynchrone. Je n'ai encore jamais utilisé ce système. Ceci dit, pour ce petit projet, il n'y a pas de problème particulier de temps de chargement d'assets.

## Que pourrais-je ajouter

Il y a encore énormément d'améliorations mineures que je voudrais faire. Je pense que le polish de ce jeu ne fait que commencer. Parmi mes idées de polish mineures :
- Afficher le score du joueur dans le menu Game Over.
- Ajouter un bruit d'abeille pour les ennemis.
- Ajouter une animation de drop d'arme, avec un effet sonore, et une trail à l'arme. Le but est de mettre en valeur le loot.
- Les armes déposées devraient flotter en l'air avec une animation et tourner au cours du temps pour les mettre en valeur.
- Les armes devraient avoir un shader / effet visuel métallique étincelant (améliorer le matériau et ajouter une particule étincelante).
- Les ennemis devraient spawn avec une animation de spawn.
- Les ennemis devraient disparaître avec une animation (simplement rétrécir rapidement jusqu'à disparaître).
- Il faudrait une trail sur les armes qui apparaît seulement lors de l'attaque, un effet de swing qui mettrait en valeur l'attaque.
- Ajouter des particules ambientes dans l'air.
- Ajouter des particules de poussière lorsque le joueur court.
- Il devrait y avoir une animation et un effet sonore pour le compteur de score lorsque le joueur marque un point.
- Des transitions entre les menus et le jeu.
- Améliorer le lighting de la scène avec plusieurs lumières et une sur le joueur.

Au-delà de ces ajustements mineurs, je pense que le gameplay a besoin de plus de profondeur pour rendre le jeu intéressant.
- Pour le moment, le fait que les ennemis et le joueur n'aient que des attaques rapprochées n'est pas très intéressant. Je proposerais soit une mécanique d'arme à portée (arc, magie) pour le joueur ou les ennemis. Je proposerais également une mécanique de roulade d'esquive : si le joueur appuie sur l'écran pour se déplacer alors qu'un ennemi s'apprête à l'attaquer, le joueur fait une roulade, ce qui lui donne quelques centièmes de seconde d'invincibilité. Cette mécanique équilibrerait le combat entre attaque et esquive.
- Il faudrait beaucoup plus d'armes à découvrir pour le joueur avec des statistiques différentes. Les armes devraient faire des dégâts différents.
- Il faudrait beaucoup plus d'ennemis différents, avec des attaques différentes et des comportements différents. Pourquoi pas des boss.
- Il faudrait améliorer l'environnement avec des objets, structures, peut-être du relief.
- Il faudrait trouver une manière de progresser durablement pour le joueur. Par exemple, un système classique de niveaux, avec une carte du monde sur laquelle le joueur peut avancer.
