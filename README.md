# LC_EZAdmin

#### *A Lethal Company extension plugin for server admins*

## Installation

### Thunderstore Mod Manager (reccommended)

#### Prequistes
- BepInEx

#### Steps to install
1. Install the plugin through the Thunderstore Mod Manager.
2. To check if you have installed correctly, check your BepInEx console for the following text:
    ```
    [Message:LC_EZAdmin] LC_EZAdmin has started
    [Info   :LC_EZAdmin] Reading banned_ids.txt ...
    [Info   :LC_EZAdmin] Sucessfully loaded in 0 players into ban list
    [Message:LC_EZAdmin] LC_EZAdmin has finished loading
    ```

### Manual

### Prequistes
- `dotnet` >= 8.0
- BepInEx

#### Steps to install
0. Within `LC_EZAdmin.csproj`, make sure to replace the placeholder value inside the `<Reference>` tag
to point to your `Lethal Company/Lethal Company_Data/Managed` folder otherwise, you will get a lot
of missing dll errors.
1. To build, run:
    ```
    dotnet build
    ```
    The built files will then be located within `build/Debug/netstandard2.1`.
	
	If you want to build under the release configuration, run:
	```
	dotnet build --configuration Release /p:DebugSymbols=false /p:DebugType=None
	```
	and the build files will be located within `build/Release/netstandard2.1`.

2. Copy the `org.bepinex.plugins.LC_EZAdmin` files into a separate folder with a non-conflicting
name (e.g. Thunderstore client would default to `Nydauron-EZ-Admin`) and place the created folder
into your `BepInEx/plugins` folder.
3. To check if you have installed correctly, check your BepInEx console for the following text:
    ```
    [Message:LC_EZAdmin] LC_EZAdmin has started
    [Info   :LC_EZAdmin] Reading banned_ids.txt ...
    [Info   :LC_EZAdmin] Sucessfully loaded in 0 players into ban list
    [Message:LC_EZAdmin] LC_EZAdmin has finished loading
    ```

## Features
- Persistent ban list
  - Turns the server kick into a semi-permanent ban
  - Players that are kicked are subsequently banned from future servers the host runs
  - Ban list is stored at `<YOUR_LETHAL_COMPANY_INSTALLATION>/banned_ids.txt`
  - To remove someone from the ban list, find their steamid in `banned_ids.txt` and remove that line
from the file

This plugin has been tested to work with MoreCompany and LateCompany.

## Planned features
-[] Add UI for adding a dedicated "ban" button
  - Will be togglable via configuration

## Inspired by
- [LC_Blacklist](https://thunderstore.io/c/lethal-company/p/VeryUnlethalCoalition/LC_Blacklist/) - A
blacklist mod for players you never want to see again
- [Lethal Company Tiny Admin](https://thunderstore.io/c/lethal-company/p/TeamTinyAdmin/Lethal_Company_Tiny_Admin/) -
A small admin plugin to allow kicking even in modded lobbies
