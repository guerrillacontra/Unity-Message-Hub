# Unity Message Hub

A small id based messaging system that allows gameobjects to communicate in a decoupled, event driven way.

Useful for when you need plug-in-and-play behaviour, or simply want to reduce the complexity of your code.

Message Hub sends int based id's (as they are lightweight) as well as a single but optional object, to
all of the listeners for that message id!

Features:

1. Lightweight id based messages
2. Add/Remove message listeners
3. Defer callbacks to Update or FixedUpdate stages of the gameloop
4. Easy to use, no dependencies!

##Example

We can represent our message id's via an enum or a standard int (I personally use enums as they are more readable).
```
enum MessageID
{
  PlayButtonClicked
}
```

When the play button is clicked in the UI controller, dispatch the message.
```
MessageBus.Post((int)MessageID.PlayButtonClicked);//Any observer will instantly execute their callback...
```
Our game manager can listen for the clicked message to launch our game.
```
MessageBus.AddListener((int)MessageID.PlayButtonClicked, StartTheGame);

void StartTheGame(object optionalData = null)
{
  ...
}
```
Done!

You can add/remove as many listeners as you want per message, pass data around and all sorts.

If you need to use the PostOnUpdate or PostOnFixedUpdate features, simply add the MessageBus prefab to your scene
and it will work with no hassle.




