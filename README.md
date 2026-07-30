# CEMExplorer
A desktop application for creating, navigating, and maintaining Cyples Engineering Methodology (CEM) projects.

CEM Explorer is a Windows desktop application built in C# that provides a dedicated workspace for engineering projects organized using the Cyples Engineering Methodology (CEM).

Unlike a traditional file explorer, CEM Explorer understands the structure of a CEM project and presents it as an engineering workspace rather than simply a collection of folders and files.

Projects remain completely file-based using standard folders and Markdown documents, making them easy to version with Git, share between developers, and edit with any text editor.

Current Features
Browse CEM project folders
Tree view navigation
Document viewing workspace
Project title management
Create new CEM project skeleton
Save project changes
Simple project setup and validation
Planned Features
Navigation
Human-friendly folder and document names
Expand / Collapse / Refresh controls
Project status indicators
Recently opened projects
Breadcrumb navigation
Document Workspace
Markdown viewer
Markdown editor
Rendered preview
Image viewing
Future support for diagrams and additional document types
Project Intelligence
CEM project validation
Missing document detection
Automatic project upgrades
Version-aware project templates
Naming convention verification
Project Navigation

Future versions will navigate more than files.

Documents will be represented as structured engineering information, allowing navigation directly to sections within a document rather than requiring manual scrolling.

Example:

Concept Architecture
    Concept Overview
    Concept Outline
        Goals
        Constraints
        Assumptions
        Risks

Selecting an outline item will open the corresponding document and position the workspace at that section.

Design Philosophy

CEM Explorer intentionally stores all project information as normal files and folders.

This approach provides several advantages:

Human readable
Git friendly
AI friendly
Long-term maintainable
No proprietary database
Portable across operating systems
Easy backup and archival

The application serves as a project navigator and engineering workspace while keeping the underlying project structure simple and open.

Technology
C#
.NET 5
Windows Forms
Markdown documents
Standard Windows file system
Project Status

Early development.

The current focus is building the core project navigation experience before expanding into document editing, validation, and AI-assisted engineering workflows.
