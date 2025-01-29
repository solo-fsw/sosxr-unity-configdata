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
1. Create a ScriptableObject which holds your data by deriving from `ConfigDataBase`. See `DemoConfigData` and `DemoConfigDataTwo` as an example. 
2. Create a new instance of your justly created asset by right-clicking in the Project window and selecting the same path as you set in the `[CreateAssetMenu(...)]` section of the ScriptableObject (e.g. `Create > SOSXR > Config Data > Demo Config Data`.
3. Create a JSON file by hitting the `CreateNewConfigJsonFile` button. See the console to find out where it has been created.
4. Edit the JSON file, or the values in the Inspector, to your liking.
5. Hit the `AmendConfigJsonFile` button in the Inspector once you've done changes in the Inspector, or `LoadConfigFromJsonFile` if you've made changes in the JSON directly.
6. If you're making changes to your data file in code, make sure to always use the `ConfigDataHandler.AmendConfigData(YourConfigData)` method to store the changes in the JSON asset.
