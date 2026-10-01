# CEM Explorer

CEM Explorer is a Windows Forms application for browsing and creating a CEM project structure.

## Development requirements

- Visual Studio 2019
- .NET 5 SDK
- Windows Forms workload

Open `CEMExplorer.sln`, build the solution, and run the `CEMExplorer` project.

## Current behavior

- Selects a root folder with the supplied `ucFileSelector` control in folder mode.
- Shows files above folders within every branch of the project tree and folder-contents pane.
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
- **New System Architecture** shows the project's `Concept.ceo` (or `ConceptOutline.ceo`) with checkboxes. Use **Select All** or **Unselect All**, then **Create New SA** to enter a title and optional model/version. The app creates `docs/ConceptArchitecture/SystemArchitecture-<Title>[-<ModelVersion>]` with the folders and files from the skeleton's System Architecture branch; its `.ceo` outline contains the selected Concept items. Existing architectures are never overwritten.

The skeleton parser uses leading dashes for parent/child relationships: one dash for a child of the root, two for the next level, and so on. It also tolerates blank lines, standalone `|` lines, and trailing `|` characters so the supplied draft can be edited without changing its basic format.

## Initial design choice

The generated project is placed in an abbreviation-named folder beneath the selected root. After creation, CEM Explorer switches its selected root to that new project folder.
