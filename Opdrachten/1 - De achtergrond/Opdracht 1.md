#Opdracht 1 - De achtergrond

* Maak een leeg gameobject onder Main Camera met de volgende properties:
    * Name: Stars
    * Position: X=0, Y=9, Z=10
    * Rotation: X=90, Y=0, Z=0
* Voeg een particle system toe met de volgende properties:
    * Prewarm: True
    * Start Lifetime: 15
    * Start Speed: 1
    * Start Size: (Random between two constants) 0.15 0.25
    * Emission - Rate over Time: 2
    * Renderer - Order in Layer: -100
* Maak een nieuw material in de Materials folder met de volgende properties:
    * Name: Star
    * Shader: Particles/Alpha Blended
    * Particle Texture: star1 (uit de Graphics/Effects folder)
* Voeg dit nieuwe material toe aan de particle system onder Renderer - Material
* Sleep het gameobject "Stars" van de scene hierarchy naar de Prefabs folder
* Sleep de prefab "SmallRocks" van de Prefabs folder naar de Main Camera zodat deze onder Stars komt te staan