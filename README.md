# Unity Message Hub

A fast, type-safe way to communicate between sub-systems for Unity 4.6+.

Features:

1. Performance in mind (good for mobile)
2. Type safety. Both message content types AND message keys are typesafe so it's hard to make a mistake!
3. Key based (of any type!) messaging so you can observe when a message is happening very simply

##How to use.

A brief introduction on how to use MessageHub in your Unity projects.

###Scenario

A GUI that has a button and when you click it you want the player to log a message, sent from the UI!

#### Step 1 : Decide on a key

For every MessageHub you create you will want to give it a type of 'key' so we can link messages to delegates.

Luckily for you, MessageHub was written to allow you to use it in the most convenient way possible.

Other messaging systems out there may force a specific type of message id, or attempt to be simplistic
by using string/int primitives...

...but that actually makes it fairly complex as your constantly looking in other source files to figure out what key's to use!

In this example we are using an enum as it is very easy to use and light weight.

```
enum UiMessage
{
  ButtonPressed
}
```
Simple!

#### Step 2 : Create your hub
```
IMessageHub<UiMessage> hub = new MessageHub<UiMessage>();
```
This example will use a local variable to make it easy to show how it works however in practice, you may want to derive your own Singleton variation, or some kind of ServiceLocator so you can access your Hub outside of where it is created.

#### Step 3 : Listen

Inside our player script we can listen for when the button press message arrives and react to it!
```
hub.Connect<string>(UiMessage.ButtonPressed, SayTheMessage);
```
And this is what the delegate looks like:
```
private void SayTheMessage(string content)
{
  //'content' is what the UI has sent to the player!
  Debug.Log(content);
}
```

Done, when the message with a key 'UiMessage.ButtonPressed' is posted, the player will say the message for us.

#### Step 4 : Post

When our UI button is clicked, post the message to the Hub.

hub.Post<string>(UiMessage.ButtonPressed, "Hello World");

#### Conclusion

Hopefully you can see that MessageHub can decouple the interactions between different contexts in a clean, type-safe
manner.


## Tips

1. Enums are brilliant for key representations...
2. Check out the Example scene in the project and look at 'UiContext', I am using a ServiceLocator pattern which is a nice way to instance and access the Hub without needing to use Singletons
3. It's always faster to Post messages that have no content. Try to limit how many 'content' messages you send.



Let me know what you think/feel and how I can improve MessageHub!

ps:

You can download just the Asset Package [here](https://drive.google.com/file/d/0B-rWfhS_vt16cEZmeFFvVnFuX1E/view?usp=sharing).

