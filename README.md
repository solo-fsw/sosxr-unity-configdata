# Config Data

- By: Maarten R. Struijk Wilbrink
- For: Leiden University SOSXR
- Fully open source: Feel free to add to, or modify, anything you see fit.

## Installation
1. Open the Unity project you want to install this package in.
2. Open the Package Manager window.
3. Click on the `+` button and select `Add package from git URL...`.
4. Paste the URL of this repo into the text field and press `Add`.
 
## Usage
1. Create a ScriptableObject which holds your data by deriving from `ConfigDataBase`. See `DemoConfigData` as an example. 
   1. You should have only one `ConfigData` in use.
2. Create a new instance of your justly created asset by right-clicking in the Project window and selecting the same path as you set in the `[CreateAssetMenu(...)]` section of the ScriptableObject (e.g. `Create > SOSXR > Config Data > Demo Config Data`. The name you use here in the Project view for this Asset will also be the name for of the JSON that is to be created in the next step.
3. Create a JSON file by hitting the `CreateNewConfigJsonFile` button. See the console to find out where it has been created.
4. Edit the JSON file, or the values in the Inspector, to your liking.
5. If you are editing the JSON directly, hit `LoadConfigFromJsonFile` once you've made changes in the JSON.
6. If you're making changes to your data file in code, make sure to always use the `ConfigDataHandler.AmendConfigData(YourConfigData)` method to store the changes in the JSON asset.

## Best practices
``` csharp
[SerializeField] private string m_demoThing = "config";

public string DemoThing
{
    get => m_demoThing;
    set
    {
        if (m_demoThing == value) return;
        m_demoThing = value;
        HandleConfigData.AmendConfigJsonFile(this);
    }
}
```
Use a `[SerializeField] private` field combined with a `public` property. Have `HandleConfigData.AmendConfigJsonFile(this)` as the last line in the `set` side of things. This way, any time you change any of the properties of the (derived) `ConfigData` class, those changes will automatically get stored into the JSON on disk. See `DemoConfigData` for more examples.

A similar thing is done in the `OnValidat` on the base class: after any change (in the Inspector for example), the `HandleConfigData.AmendConfigJsonFile(this)` kicks in and will save your changes to disk

Use any of the `ConfigXXXToUnityEvent` classes to pipe through any of the data from the `ConfigData` to a UnityEvent. This will then pass along that info to whatever component you like.

You can subscribe to the `HandleConfigData.OnConfigDataChanged` event to get notified when the data has changed. This is useful for when you want to update your UI, for example. Really make sure that the `if (m_yourField == value) return;` line is in the `set` right before the `HandleConfigData.AmendConfigJsonFile(this)`, otherwise you'll get an infinite loop of events. At least, that occurs if you  subscribe to the `OnConfigDataChanged` event in the same place as where you're changing the data... Speaking from experience here

