# Unity-Utility-Package
Utility Scripts I commonly use for the Unity game engine.

I'd say the *most* useful script is in my Cameras repository and called the [Scene-Like-View](https://github.com/KampinKarl1/Scene-View-Camera-in-Play-Mode/blob/master/SceneLikeCamera.cs) camera or something like that. That's not included here.

If you want to limit frame rate, easily make singletons, activate a random gameobject from an array (Say you have a bunch of character models and you'd like a random one to be activated), all that stuff is here.

There is some "Scene Management" which just let's you easily link a button or a KeyPressTrigger (more later) to a Scene Management option (like quitting the application).

KeypressTrigger is a script that just let's you combine a keypress with a UnityEvent. So, for example, if you want to test a method you just created without writing something like 

```
void Update ()
{
  if(Input.GetKeyDown(KeyCode.K)){

      PerformMethodIJustCreated();
  }
}
```
You can, in the editor via the Inspector tab, create an empty object with a KeyPressTrigger Component and choose a keycode and then drag and drop your script and method into the OnKeypressEvent.
