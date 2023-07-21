=============================
Scene Management example
=============================

···········································
| How to |
···········································

• Open the "SceneManagementSample.unity" scene.
• In the "Sequences" window, right click on "SceneManagementSample".
• Select "Load Scenes" and see the three scenes, "set01.unity", "shot_10_lights.unity" and "shot_20_lights.unity", opening.
• Hit play in "Timeline" to observe the result.

··································
| Scene Activation track |
··································

• In the "Sequences" window, select "seq_001".

• In the Timeline window, there is a yellow track named "set01". This is a Scene Activation track. It drives the activation of the "set01.unity" scene.

• If you select "shot_10" or "shot_20", you see in Timeline that they also have a Scene Activation track to drive the lighting for each shots.

• In the "Sequences" window, you can right click on any Sequences and select "Create Scene...". That will create a new Scene and a new Scene Activation track to control it.

[ Note ]
A Scene must be loaded before it can be activated or deactivated by a Scene Activation track.
This can be done:
• Via the sequences contextual menu "Load Scenes" or "Load Specific Scene"
• Via the Scene Activation track inspector
• Manually by drag & dropping the wanted Scene in the Hierarchy.

··································
| Play mode and Player Build |
··································

• In the Hierarchy, select "SceneManagementSample".
• On the "Cinematic Filter" component, next to `Scene Load Policy`, select "Add".
• This adds the "Cinematic Scene Management Policy" component.
• Play with the different options to define what should be the loading behaviour in Editor Play mode and/or in a Player Build.

···········································
| Troubleshooting |
···········································

It is possible that, after loading the Sample, you don't see the "SceneManagementSample" Master Sequence in the Sequences window. This is a refresh problem. To force it, create a dummy Master Sequence. This should be enough to make the "SceneManagementSample" Master Sequence and its sequences appear.
