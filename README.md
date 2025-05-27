# 3D Basketball Shooting Game

## How to Play

### Running the Game
- Open the project in **Unity Editor**.
- Click the **Play** button to start the game.

### Controls

#### Movement
- **(If you can't move don't check input setting check your brain settings)**
- **WASD** – Move around
- **Spacebar** – Jump
- **Mouse** – Look around

#### Shooting
- **Hold Left Click** – Charge the shot
- **Release Left Click** – Shoot the basketball
    - 💡 *Tip: Releasing the ball right before it starts shaking gives you extra power boost!*

## How It Works
### Player Movement
- This projects uses Unity's Starter Assets Pack for Character movement.

### Shooting Mechanics
- The basketball shooting mechanics are implemented using Unity's physics system(RigidBody).
- The player has `Basketball Shooter` Component That handles the shooting/spawning logic and Updating variables Related to Shooting.
   - ToDo : Player ScriptableObject to hold Different Player like (Lebron James, Michel Jordan , Stephen Curry).

### Scoring
- The game keeps track of the score based on successful shots out of Attempted Shots.
- All the scoring logic is handled in the `UIData` ScriptableObject which is updated by the `Basketball Shooter` and `Basket`.

### BasketBall
- `Basketball` Script is used to Set up the type of BasketBall from ScriptableObject(Set up by the `Basketball Shooter`).
- `BasketBallType` ScriptableObject is used to hold the data for different types of balls, such as size, weight, and bounce properties.

### Basket
- `Basket` Script is used to detect when a shot is made and updating the score.

### Miscellaneous(might have gone a little overboard)
- The CountDownTimer to turn on and tick the `TimerData` ScriptableObject.
- I have also done shaking effect implemented using Prime Tween(Open Source Tweening Library Better than DoTween 😏).
- You might see something called `Game Event` and `Game Event Listener` ScriptableObjects. Useful for decoupling code.
- `UImanager` only updates the UI based on the data from `UIData` ScriptableObject.
- Clicking the UI Buttons will trigger local unity events to do stuff.
