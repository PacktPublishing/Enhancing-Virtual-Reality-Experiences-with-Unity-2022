Hello and welcome to "Simple modular humans" and thanks for buying it.

The concept of modular is, that you can cloth/style you character from ground on, like putting a black skin on the character/human and wearing a chain without a t-shirt.





1. Modular parts


Drag the prefab "custom simple human_prefab" into the scene, which is in the folder " prefabs "

Where do i find the modular parts in it ? See on the picture " Info_a ", which is in the folder " simple modular human ".




2. Textures for the modular parts


Each modular part has an own texture folder, which can you find ( See on the picture " Info_b " , which is in the folder " simple modular human " )




3. Changing the texture of a modular part



Each modular part has one material, to change the texture on it, must you change the texture of the material.

See the follow Links : https://docs.unity3d.com/ScriptReference/Renderer-materials.html




Little example how :

Gameobject Jacket;   <- A modular part.
Texture A;           <- Texture, which i want to put on Jacket.

////////

Jacket.GetComponent<Renderer>().materials[0].mainTexture = A;

GetComponent<Renderer>() = Accessing the skinnedmeshrenderer; skinnedmeshrenderer -> each mesh has that component.

materials[0]             = The skinnedmeshrenderer has the property " Materials " -> here are all materials listed, 
                           but the way, that we only have 1 material , do we just need access materials[0], which is the first material of the materials list.

mainTexture              = It's the maintexture from the accessed material. <- And that's, what we want to get changed.

////////



4. Additionally

FPSbone  = It's an extra bone, if you want to use the human in FPS, to look down and up, if the upper bone is rotated, does the FPSbone fix all differs from the upper bone,
           like if the upper bone is rotated 25 degree to the right.



Human_mesh_head = If you want to use the human in FPS view, can you disappear the head, for a better view.




5. Animations

Animator propertys




legs = Int number from the animator for the legs status.
arms = Int number from the animator for the arms status, which is all, except the legs.

legs and arms have the same animator number , as example //   arms : 1 , legs : 1 -> normal walking    |  arms : 24 , legs 1 -> walking + holding the glock in idle

legs : 3 + arms : 10  = Sitting and looking around_2
legs : 3 + arms : 30  = Sitting and eating
legs : 1 + arms : 16  = walk and hello



Here are the animator numbers :


walk : 1
run : 2
sitting : 3
idle : 5

walk ducking : 6
smoking : 7
goto phone call : 8
looking around_1 : 9

looking around_2 : 10
looking around_3 : 11
looking around_4 : 12
hit idle  : 13

hit right : 14
hit left : 15
hello : 16
hands up : 17

goto hanging up wall : 18
goto ducking afraid : 21
goto crowbar work(breaking up) : 22
glock reload : 23

glock idle : 24
glock idle shoot : 25
glock aim : 26
glock aim shoot : 27

fireaxe idle : 28
fireaxe hit : 29
eating : 30
drinking : 31

picking stuff up(as item) : 32
crowbar idle : 33
crowbar hit : 34
ducking : 36




much greetings polygon land  

for asks : tmg@fn.de
