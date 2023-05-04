BCS - Basic Components an Systems for Unity
===========================================


BCS is a basic Components and Systems framework, built specifically for Unity. I built this tool for my personal projects, and as such use this with caution, as I made it with my needs in mind.
*Please note that this is not an ECS framework* and does not provide the same benefits. BCS is built for handling entities that require various different configurations, such as characters with different stats and physical abilities or weapons with various damage types, etc.

BCS code generator generates various script files for user specified entity types (e.g. Characters, Items, Trees, etc.) and component scripts that can be attached to those specific entity types (e.g. Health for Characters, value for Items, etc.). These scripts include scriptable objects and editor classes which then allow to construct a specific type of entity insdie of the Unity Editor.

A factory class is also generated for each of the entities to allow an easy way to instantiate said entities as GameObjects by passing in their scriptable objects.

**_NOTE:_** This whole thing is largely very experimental and, at the moment, should NOT be used in a commercial project!
---

Generating Entities and Components
==================================

To generate an entity, place the BCS binary folder inside of the Unity project, and run BCS with the name of an entity as an option:
```console
./bcs [New Entity Name]
```

To generate one or more components and run BCS with the following options. Note that You have to specify which entity You want the components to be generated for:
```console
./bcs [Entity Name] [Component 1] [Component 2] [Component 3] [...]
```

Usage in Unity
==============
Lets try to create an `Animal` entity with an `Alive` component. There are also some extra options that can be used, such as `-p` or `--prefix`, which allows to set up a custom prefix for the component (The default is `has`). The `-f` and `--force` options allow to overwrite existing classes, otherwise the code generator will skip them. The following code will generate an `Animal` entity with an `Alive` component.
```console
./bcs Animal Alive -p is -f
```
The previously mentioned setup allows to check if an `Animal` object has the `Alive` component attached to it by using the following code. Notice how an `isAlive` boolean has been generated.
```csharp
if (animalInstance.isAlive)
	Debug.Log("Boomer will live!");
```
Other values have also been added to the `Animal` class:
```csharp
public bool isAlive { private set; get; }
public AliveSystem alive { private set; get; }
public AliveConfig aliveConfig { private set; get; }
```
As well as a couple of methods for adding and removing the component (Note that Adding an existing component will remove the previous one!)
```csharp
public AliveSystem AddAlive(AliveConfig config)
{ ... }
public void RemoveAlive()
{ ... }
```

After classes are properly generated, component and entity scriptable objects can be created under `Create/Data/Animal/`. Component scriptable objects can then be added to an entity scriptable object to create a unique entity.<br /> <br />Let's create a "Boomer the dog" entity!

<img src="https://i.imgur.com/nGrQGVV.png">
<br /><br />

Each component can have custom values in them, and since each one of them have a generated editor script, creating custom inspectors for them is easy!

<img src="https://i.imgur.com/VUavGxM.png">
<br /><br />

After everything is set up, the `Boomer` entities can now be instantiated as `GameObjects` using the following code:
```csharp
AnimalFactory.Create([Boomer Scriptable Object], position, rotation);
```
