# Frenzy-Fighter

## Temps passé

Phase 1 - Obligatoire : 5h30
Phase 2 - UI : 1h30

Total: 7h

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

## Comment m'améliorer

À compléter

## Que pourrais-je ajouter

À compléter
