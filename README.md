## Sackboy: A Big Adventure PS4 World Unlocker

This tool allows to force unlock worlds in Sackboy: A Big Adventure on PS4.
This effectively allows to fix the issue when the next world is not automatically unloked after completing the last level of the world. Requires HEN.

**BACKUP SAVE FILES BEFORE USING!**

# Download

Get the [latest version here][app_latest].

# Instructions

### How to use:
	1. Get the **unencrypted** save file from the game. We are interested in the `General` file. To get the unencrypted save file use one of the available save tools like [Apollo Save Tool](https://github.com/bucanero/apollo-ps4). Refer bellow for Apollo instructions.
	2. Make a save **BACKUP** using PS4's Application Save Data Management.
	3. Drag'n'drop the `General` save file over the program executable. Alternatively open the program and enter full file path to the `General` save file.
	4. Select the world to unlock. It's untested whether or not you can unlock the levels not in correct order, so the program will unlock everything before the selected world as well.
	5. The program updates the save file in place and creates an additional backup of the `General` save file in the same folder called `General.bak`. Move this file to a safe location if anything happens.
	6. Copy the modified `General` save file back to the PS4.

### How to restore original save file:
	Either rename the `General.bak` to `General`, and replace the modified file using a save tool OR restore the save backup using Application Save Data Management.

### How to get the unencypted save files using [Apollo Save Tool](https://github.com/bucanero/apollo-ps4):
	1. Go to HDD Saves.
	2. Find the Sackboy game title. If your game has title id CUSA18867, then it should be called "Save Game". Need confirmation for other regions.
	3. Copy save game to USB.

### How to apply the modified save file using [Apollo Save Tool](https://github.com/bucanero/apollo-ps4):
	1. Go to USB Saves.
	2. Find the Sackboy game title.
	3. Copy save game to HDD.

### Credits
 * [Bucanero](https://github.com/bucanero) for the [Apollo Save Tool](https://github.com/bucanero/apollo-ps4);
 * [13xforever](https://github.com/13xforever) for the [Unreal Engine 4 save game converter](https://github.com/13xforever/gvas-converter), that helped to understand the save file structure.