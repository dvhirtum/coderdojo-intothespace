#Opdracht 2 - De speler

* Sleep "playerShip2_orange" van Graphics\Player naar de scene, dit maakt er een gameobject van
    * Hernoem het nieuwe gameobject "Player"
    * Verander positie naar x=0, y=-3.69, z=0
    * Voeg een RigidBody2D toe, dit zorgt er voor dat we snelheid en acceleratie van het gameobject kunnen regelen
    * Start het spel, wat gebeurt er?
    * In de ruimte is er geen zwaartekracht, dus verander Gravity Scale naar 0
    * Voeg een BoxCollider2D toe, dit zorgt er voor dat we kunnen weten als het gameobject een ander object raakt
        * Zorg dat IsTrigger is aangevinkt
        * Offset: x=0, y=-0.06
        * Size: x=0.91, y=0.5
* We gaan nu zorgen dat we het ruimteschip kunnen besturen
    * Maak een folder onder Assets en noem deze "Scripts"
    * Maak in de nieuwe folder een C# script en noem deze "PlayerScript"
    * Vervang de inhoud van de script file met de volgende code:

```cs
using UnityEngine;

public class PlayerScript : MonoBehaviour 
{
	public float Speed;

	void FixedUpdate() 
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");
		Vector2 movement = new Vector2(moveHorizontal, moveVertical);

		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.velocity = movement * Speed;

		rigidBody.position = new Vector2
		(
			Mathf.Clamp(rigidBody.position.x, -3, 3),
			Mathf.Clamp(rigidBody.position.y, -4.5f, 2)
		);
	}
}
```

    * In Unity, koppel het script aan de Player gameobject
    * Stel Speed in op 6
    * Probeer het spel uit
* Als laatste gaan we het ruimteschip nog wat verfraaien
    * Voeg "fire13" vanuit Graphics\Effects to aan Player in de Hierarchy zodat deze aan Player gekoppeld is
        * Position: x=-0.27, y=-0.431715, z=0
        * Naam: MotorFireLeft
    * Voeg "fire13" nogmaals to aan Player
        * Position: x=0.2565265, y=-0.431715, z=0
        * Naam: MotorFireRight
    * Koppel de animation controller "MotorFireLeft" uit de folder Animations toe aan het gameobject MotorFireLeft
    * Koppel de animation controller "MotorFireRight" uit de folder Animations toe aan het gameobject MotorFireRight
    * Dit koppelt een hele simpele animatie aan de twee motor-uitlaten die elke 0.1 seconden het plaatje verwisseld.
        * Animaties in Unity is een heel groot en complex onderwerp, te groot voor deze Dojo. Wie weet komen we daar in een latere Dojo nog op terug.
