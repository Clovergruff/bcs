BCS - Basic Components an Systems
===========================================


BCS is a basic Components and Systems framework, built specifically for Unity. I built this tool for my personal projects, and as such use this with caution, as I made it with my needs in mind.
*Please note that this is not an ECS framework* and does not provide the same benefits. BCS is built for handling entities that require various different configurations, such as characters with different stats and physical abilities or weapons with various damage types, etc.

BCS code generator generates various script files for user specified entity types (e.g. Characters, Items, Trees, etc.) and component scripts that can be attached to those specific entity types (e.g. Health for Characters, value for Items, etc.). These scripts include scriptable objects and editor classes which then allow to construct a specific type of entity insdie of the Unity Editor.

A factory class is also generated for each of the entities to allow an easy way to instantiate said entities as GameObjects by passing in their scriptable objects.

---

Generating Entities and Components
==================================

To generate an entity, place BCS inside of the Unity project folder (Or a subfolder), and run BCS with the name of an entity as an option:
```console
./bcs [New Entity Name]
```

To generate one or more components, run BCS with the following options. Note that You have to specify which entity You want the components to be generated for:
```console
./bcs [Entity Name] [Component 1] [Component 2] [Component 3] [...]
```

Usage in Unity
==============
For usage in Unity see [BCS-Unity](https://github.com/Clovergruff/BCS-Unity)
