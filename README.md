# Frenzy-Fighter

## Temps passé :

à compléter

## Difficultés :

- Le joystick virtuel a posé des questions de conception. En effet il m'est vite apparu qu'il était important que le joystick ait la même taille sur tous les écrans. Pour que ce soit le cas, j'ai configuré le Canvas Scaler de Unity en Constant Physical Size. Cela a aussi posé un rapide problème de conversion. En effet, dans mon script du joystick, j'ai besoin de renseigner une distance qui correspond à la distance que doit parcourir le stick interne au sein du joystick. Cette distance doit être renseignée en points, afin que sa taille physique soit constante sur tous les écrans. Il y a alors une conversion de points en pixels à faire. Cette conversion n'est pas un défi technique particulier, il suffit d'utiliser un facteur de conversion propre à chaque écran (le DPI).
