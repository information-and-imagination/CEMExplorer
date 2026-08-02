# CEM Explorer

CEM Explorer is a Windows Forms application for browsing and creating a CEM project structure.

## Development requirements

- Visual Studio 2019
- .NET 5 SDK
- Windows Forms workload

Open `CEMExplorer.sln`, build the solution, and run the `CEMExplorer` project.

## Current behavior

- Selects a root folder with the supplied `ucFileSelector` control in folder mode.
- Shows folders and files in a tree.
- Shows either a folder's file list or the selected text file's contents in the right pane.
- Allows text files to be edited and saved.
- Opens `.ceo` outline documents as editable trees. The root occupies the first line; each hierarchy level is saved with one additional leading hyphen.
- Provides Add Child, Add Sibling, Rename, and Remove commands for outline branches. F2, Insert, and Delete are also supported.
- Provides **Name X File** for replacing only a filename's `XXXXXX` placeholder text while preserving the rest of the name.
- Provides **Add Numbered File** to find the highest matching sequence number, show the proposed complete filename, replace its `XXXXXX` placeholder, and create the new empty document.
- Reads the project title from the first level-one heading in `README.md`.
- Uses **Setup** to hold a title for a new empty folder or update the title in an existing project's `README.md`.
- Uses **Create** to prompt for a project abbreviation and generate the editable skeleton from `CEMEXPLORERSKELETON.txt`.
- Replaces every `SKLTN` token in the skeleton with the entered abbreviation.

The skeleton parser uses tab indentation for parent/child relationships. It also tolerates blank lines, four-space indentation, standalone `|` lines, and trailing `|` characters so the supplied draft can be edited without changing its basic format.

## Initial design choice

The generated project is placed in an abbreviation-named folder beneath the selected root. After creation, CEM Explorer switches its selected root to that new project folder.
