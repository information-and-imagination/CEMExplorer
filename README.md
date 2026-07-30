# CEM Explorer

> A Windows desktop application for creating, navigating, and maintaining projects built using the Cyples Engineering Methodology (CEM).

CEM Explorer is a desktop application that understands the structure of a Cyples Engineering Methodology (CEM) project and provides an engineering workspace instead of simply displaying folders and files.



Built with C# and Windows Forms, it allows CEM projects to remain completely file-based using standard folders and Markdown documents, making them easy to manage with Git, edit using any text editor, and collaborate on with both humans and AI.

---

## Features

### Current

- Browse CEM project folders
- Create new CEM project skeletons
- TreeView navigation
- View and edit Markdown documents
- Save document changes
- Project title management through `README.md`
- Configurable project abbreviation
- Automatically generates the standard CEM folder structure

### Planned

- Human-friendly folder and document names
- Markdown preview mode
- Project validation
- Status indicators
- Expand / Collapse / Refresh navigation controls
- Breadcrumb navigation
- Project version upgrades
- Intelligent document navigation
- AI-assisted engineering workflow support

---

## Design Philosophy

Unlike traditional project management software, CEM Explorer stores projects entirely as normal folders and Markdown documents.

This approach offers several advantages:



- Human-readable

- Git-friendly

- AI-friendly

- Platform-independent project format

- No proprietary database

- Long-term maintainable

- Future-proof through open file formats

The application provides an engineering-focused workspace while leaving the underlying project open and accessible.

---

## Project Structure

New projects are generated using a configurable skeleton.

Example:

```text
Project/
│
├── README.md
├── LICENSE
├── CHANGELOG.md
├── .gitignore
│
└── docs/
    ├── Vision/
    ├── ConceptArchitecture/
    ├── Requirements/
    ├── DecisionRecords/
    └── SystemArchitecture/
```

Project abbreviations are substituted throughout the generated documents during creation.

---

## Current Workflow

- Select a root folder using the included folder selection control.
- Browse folders and files using the TreeView.
- View or edit Markdown documents.
- Read the project title from the first level-one heading in `README.md`.
- Update the project title using **Setup**.
- Create a new project using **Create**.
- Prompt for a project abbreviation.
- Generate the complete project structure from `CEMEXPLORERSKELETON.txt`.
- Replace every `SKLTN` token with the supplied project abbreviation.
- Automatically switch to the newly created project after generation.

---

## Technology

- C#
- Windows Forms
- .NET 5
- Visual Studio 2019

---

## Roadmap

CEM Explorer is intended to evolve from a project browser into a complete engineering workspace.

Future versions are expected to navigate not only documents, but also the engineering concepts contained within them. Selecting items such as Goals, Constraints, Assumptions, or Requirements will automatically position the document workspace at the appropriate section.

Ultimately, CEM Explorer aims to become the primary navigation and authoring environment for CEM-based engineering projects.

Over time, CEM Explorer will evolve from a document navigator into a project navigator, allowing engineers to work with concepts, decisions, requirements, and architecture directly rather than thinking in terms of files and folders.

---

## Building

Requirements:

- Visual Studio 2019
- .NET 5 SDK
- Windows Forms workload

Open `CEMExplorer.sln`, build the solution, and run the `CEMExplorer` project.

---

## Project Status

Early development.

The current focus is establishing a solid navigation experience before expanding into document intelligence, validation, and AI-assisted engineering capabilities.

---

## Vision

CEM Explorer is being developed alongside the Cyples Engineering Methodology itself.

Rather than serving as a generic file explorer, the long-term goal is to provide an engineering workspace that understands the structure of CEM projects, helping engineers, developers, and AI assistants navigate complex systems through organized concepts, requirements, decisions, and architecture.

As both CEM and CEM Explorer evolve, each will inform and improve the other.
