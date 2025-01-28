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
1. Create a new `ConfigData` asset by right-clicking in the Project window and selecting `Create > SOSXR > ConfigData`.
2. Create a new `ConfigDataHandler` asset by right-clicking in the Project window and selecting `Create > SOSXR > ConfigDataHandler`.
3. Assign the `ConfigData` asset to the `ConfigDataHandler` asset.
4. Create a JSON file by hitting the `Create Config Json` button in the `ConfigDataHandler` asset.
5. Edit the JSON file, or the values in the Inspector, to your liking.

Make sure to always use the `configDataHandler.AmendConfigData()` method to change the `ConfigData` JSON asset, as this will also save the changes to the asset.