using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class Generator
{
	public static bool FileDoesntExist(string filePath)
	{
		if (Program.forceOverwrite)
			return true;
			
		return !File.Exists(filePath);
	}

	public static void CreateExtensionClass(string filePath, string actor, string prefix, string component) =>
		new UnityExtensionClass(filePath).Generate(actor: actor, prefix: prefix, component: component);

	public static void CreateSystemClass(string filePath, string actor, string component) =>
		new UnitySystemClass(filePath).Generate(actor: actor, component: component);

	public static void CreateConfigClass(string filePath, string actor, string component) =>
		new UnityConfigClass(filePath).Generate(actor: actor, component: component);

	public static void CreateMainActorClass(string filePath, string actor, bool hasUpdate, bool hasFixedUpdate, bool hasLateUpdate) =>
		new UnityActorClass(filePath).Generate(actor: actor, hasUpdate: hasUpdate, hasFixedUpdate: hasFixedUpdate, hasLateUpdate: hasLateUpdate);

	public static void CreateActorConfigEditorClass(string filePath, string actor) =>
		new UnityActorConfigEditorClass(filePath).Generate(actor: actor);

	public static void CreateComponentConfigClass(string filePath, string actor) =>
		new UnityComponentConfigClass(filePath).Generate(actor: actor);

	public static void CreateComponentConfigEditorClass(string filePath, string actor, string component) =>
		new UnityComponentConfigEditorClass(filePath).Generate(actor: actor, component: component);

	public static void CreateActorConfigClass(string filePath, string actor) =>
		new UnityActorConfigClass(filePath).Generate(actor: actor);

	public static void CreateActorManagerClass(string filePath, string actor) =>
		new UnityActorManagerClass(filePath).Generate(actor: actor);

	public static void CreateActorFactoryClass(string filePath, string actor) =>
		new UnityActorFactoryClass(filePath).Generate(actor: actor);

	public static void CreateSystemBaseClass(string filePath, string actor, bool hasUpdate, bool hasFixedUpdate, bool hasLateUpdate) =>
		new UnitySystemBaseClass(filePath).Generate(actor: actor, hasUpdate: hasUpdate, hasFixedUpdate: hasFixedUpdate, hasLateUpdate: hasLateUpdate);
}
