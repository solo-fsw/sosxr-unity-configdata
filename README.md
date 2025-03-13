# Config Data

- By: Maarten R. Struijk Wilbrink
- For: Leiden University SOSXR
- Fully open source: Feel free to add to, or modify, anything you see fit.

## Deprecation Notice
Config Data is now part of [SeaShark](https://github.com/solo-fsw/sosxr-unity-seashark) and will be maintained there

## Installation
1. Open the Unity project you want to install this package in.
2. Open the Package Manager window.
3. Click on the `+` button and select `Add package from git URL...`.
4. Paste the URL of this repo into the text field and press `Add`. Make sure it ends with `.git`.
 

## Usage
1. Create a ScriptableObject which holds your data by deriving from `ConfigDataBase`. See `DemoConfigData` as an example. 
   1. You should have only one (derived) `ConfigData` in use.
2. Create a new instance of your justly created asset by right-clicking in the Project window and selecting the same path as you set in the `[CreateAssetMenu(...)]` section of the ScriptableObject (e.g. `Create > SOSXR > Config Data > Demo Config Data`). 
3. Create a JSON file by hitting the `CreateNewConfigJsonFile` button. See the console to find out where it has been created. You can hit the `Reveal in Finder` button too.
4. To edit the data:
   1. Either edit the `.json` directly, and hit `LoadConfigFromJson` when you're done
   2. Edit the fields in the Inspector, and hit `UpdateConfigJson`
5. If you are editing the JSON directly, hit `LoadConfigFromJson` once you've made changes in the JSON.


## Best practices

Use a `[SerializeField] private` backing-field with a `public` property for your data. Have the `SetValue` method linked in the `set` portion like so:

```csharp
[SerializeField] private string m_baseURL = "https://youtu.be/xvFZjo5PgG0?si=F3cJFXtwofUAeA";
public string BaseURL
{
    get => m_baseURL;
    set => SetValue(ref m_baseURL, value, nameof(BaseURL));
}
```

## Responding to Data (changes) in the Config

Use any of the `ConfigXXXToUnityEvent` classes to pipe through any of the data from the `ConfigData` to a UnityEvent. This will then pass along that info to whatever component you like.

## Writing to JSON

You can list which variable changes will trigger a JSON update:

``` json
"m_updateJsonOnSpecificValueChanged": [
   "QueryStringURL",
   "PPN"
],
```
In this case the JSON gets rewrit when QueryStringURL changes or the PPN changes. 

In the Editor you can use the checkboxes to add or remove variables that should trigger this update. 

(These functions leverage the [PubSub](#pubsub-) system mentioned below)

----

While in the Editor, the OnValidate should pick up on any changes to the ScriptableObject, if you name your fields and properties correctly (see below). If it doesn't work, you have to hit the `UpdateConfigJson` button to save your changes to disk.

The DemoConfigData's OnValidate and the corresponding auto-storing of the JSON to disk when changing values in the Inspector works only when:
- Your `[SerializeField] private ...` backing-field is named starting with "m_" and in camelCase (e.g. `m_likeThis`)
- Your corresponding `public` property is named the exact same name, but without te "m_", and in PascalCase (e.g. `LikeThis`)
This issue doesn't exist when writing directly to the `public` property.


## PubSub 

register that you want to update the Json when a value changes. This way, any time you change any of the properties of the (derived) `ConfigData` class, those changes will automatically get stored into the JSON on disk. See `DemoConfigData` for more examples.

### (Un)subscribe to specific value chance
``` csharp
private void OnEnable()
{
    configData.Subscribe(nameof(configData.ShowDebug), _ => RespondToNotification());
}

private void RespondToNotification()
{
    Debug.LogFormat("ShowDebug changed to {0}", configData.ShowDebug);
}

private void OnDisable()
{
    configData.Unsubscribe(nameof(configData.ShowDebug), _ => RespondToNotification());
}
```

### (Un)subscribe to any value change
```csharp
private void OnEnable()
{
    configData.SubscribeToAny(OnAnyValueChanged);
}

private void OnAnyValueChanged(string propertyName, object newValue)
{
    Debug.Log($"{propertyName} changed to: {newValue}");
}

private void OnDisable()
{
    configData.UnsubscribeFromAny(OnAnyValueChanged);
}
```

A similar thing is done in the `OnValidate` on the `BaseConfigData` class: after any change (in the Inspector for example) to any field that's in the `m_updateJsonOnSpecificValueChanged` list, the `HandleConfigData.UpdateConfigJson(this);` kicks in and will save your changes to disk (but see [Bonus](#bonus)).
