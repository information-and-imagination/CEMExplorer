# CEM Explorer / Navigator --- Work TODO List

## 1. Project Open / Create

-   [ ] Add an **Open / New Project** button to the main CEM Explorer
    form.
    -   Allow the user to leave the current project and return to the
        project open/create wizard.
    -   Respect the existing dirty/unsaved-document handling before
        changing projects.
-   [ ] Add a **License** selector to the Project Creation Wizard.
    -   Initial choices:
        -   MIT
        -   Apache License 2.0
        -   GNU GPL v3
        -   None / Choose Later
    -   Generate `LICENSE.md` from the selected license template.
    -   Fill applicable placeholders such as year and copyright holder.
    -   Keep the selected license ID in CEM project metadata.
-   [ ] Store official license text/templates with CEM Explorer.
    -   Example application resources:
        -   `Templates/Licenses/MIT.txt`
        -   `Templates/Licenses/Apache-2.0.txt`
        -   `Templates/Licenses/GPL-3.0.txt`
    -   Do not require generated CEM projects to carry CEM Explorer's
        master template library.
-   [ ] Add support for changing a project's license.
    -   When `LICENSE.md` is selected/reviewed, enable a **Change
        License** button.
    -   Show the available licenses.
    -   Replace `LICENSE.md` with the selected official template.
    -   Update the project's stored license ID.
    -   If the license is manually modified, allow it to be identified
        as `Custom` rather than incorrectly identifying it as an
        unchanged standard license.

------------------------------------------------------------------------

## 2. System Architecture Management

-   [ ] Make the existing **Name X File** button work on a
    `SystemArchitecture-xxxxxxxx` folder.
    -   Use the existing replacement-name prompt.
    -   Replace `xxxxxxxx` in the selected System Architecture folder
        name.
    -   Recursively walk all child/subchild folders and files.
    -   Replace `xxxxxxxx` only in descendant names that actually
        contain the placeholder.
    -   Leave already-named descendant files alone.
    -   Rename descendant items first and the selected System
        Architecture folder last.
    -   Do not replace text inside external documents as part of this
        operation.
-   [ ] Add an **Add System Architecture** button.
    -   Allow another System Architecture to be created for the CEM
        project.
    -   Support both:
        -   New implementation/version.
        -   New device/subsystem.
    -   Create the standard System Architecture skeleton.
    -   Prompt for the architecture/device/version name.
    -   Apply the supplied name to the new architecture and its
        `xxxxxxxx` descendant filenames during creation.
    -   Refresh the project tree.
    -   Select the newly created architecture.
-   [ ] Preserve previous System Architecture versions.
    -   Do not overwrite an established implementation when its scope
        materially changes.
    -   Later, CEM should guide/force the user to create a new System
        Architecture version for material changes.
    -   Keep old implementations available as historical snapshots.

------------------------------------------------------------------------

## 3. Add Numbered File Behavior

-   [ ] Change **Add Numbered File** to a folder-level operation.
    -   Enable it when `DecisionRecords` is selected.
    -   Enable it when `Requirements` is selected.
    -   Disable it when individual `.md` files are selected.
    -   Disable it for unrelated folders/nodes.
-   [ ] Generate the next numbered Decision Record automatically.
    -   Scan the selected `DecisionRecords` folder.
    -   Determine the highest existing DR number.
    -   Generate the next number.
-   [ ] Generate the next numbered System Requirement automatically.
    -   Scan the selected `Requirements` folder.
    -   Determine the highest existing SR number.
    -   Generate the next number.
-   [ ] Inherit the System Architecture name when adding numbered files.
    -   If the architecture has already been named, use that name rather
        than creating another `xxxxxxxx` filename.
    -   Example:
        -   `SIOM-SR-003-Pressure Gauge and Data Monitor [v1.0].md`

------------------------------------------------------------------------

## 4. Concept → System Architecture Outline Workflow

-   [ ] When a blank System Architecture `.ceo` outline is selected,
    open a **modeless outline-selection form**.
    -   Load the project's Concept `.ceo` outline.
    -   Display the Concept outline as a hierarchical TreeView with
        checkboxes.
    -   Allow the user to indicate which Concept items are addressed by
        this System Architecture/version.
    -   Keep the form modeless so the user can continue working in CEM
        Explorer.
-   [ ] Build the System `.ceo` outline from the Concept outline
    selection.
    -   Preserve the Concept hierarchy.
    -   Retain the full relevant Concept outline rather than deleting
        unselected items.
    -   Mark items that are **not used/addressed in this
        implementation** with a distinct stored status.
    -   Display a distinct icon beside excluded/not-used items in the
        System outline.
-   [ ] Extend the `.ceo` format to persist the not-used/excluded state.
    -   Choose a simple status marker that does not conflict with the
        existing dash-based parent/child format.
    -   The TreeView icon must be derived from persisted `.ceo` state,
        not just temporary UI state.
-   [ ] Treat excluded System-outline items as deliberately out of
    scope.
    -   Preserve them so CEM can distinguish:
        -   Forgotten/not addressed.
        -   Deliberately not used in this implementation.

------------------------------------------------------------------------

## 5. System Outline → Requirement / Design Document Workflow

-   [ ] When an empty applicable Requirement or Design `.md` file is
    selected, open a **modeless outline-selection form**.
    -   Load the System `.ceo` belonging to that System Architecture.
    -   Display its hierarchy with checkboxes.
    -   Keep excluded/not-used System items visible but not selectable.
    -   Allow the user to select the System items that the document will
        address.
-   [ ] Generate Markdown headings from selected System-outline items.
    -   Preserve outline order.
    -   Preserve ancestry/context when a child is selected.
    -   Generate appropriate Markdown heading levels.
    -   Open/show the resulting `.md` in the normal CEM document editor.
    -   Allow normal editing after generation.
-   [ ] Only automatically invoke the selector for an empty document.
    -   Existing populated `.md` files should open normally.
    -   Do not repeatedly prompt when revisiting an already-created
        document.
-   [ ] Do not automatically delete generated sections later.
    -   Once Markdown has been generated and edited, the document
        becomes authoritative.
    -   Changing checkbox selections must not silently delete written
        engineering content.
-   [ ] Preserve enough linkage for future traceability.
    -   Eventually allow CEM to determine which System/Concept items are
        addressed by Requirements, Decisions, or Design documents.
    -   Do not require a separate traceability matrix for the initial
        implementation.

------------------------------------------------------------------------

## 6. External / Native Application Files

-   [ ] Support external/native application files in CEM projects.
    -   Examples include CAD, KiCad, FreeCAD, Office/LibreOffice, PDF,
        images, video, spreadsheets, etc.
    -   CEM should organize these artifacts without attempting to become
        their editor.
-   [ ] Display native Windows-associated icons in the project TreeView.
    -   Obtain icons from Windows/Shell file associations.
    -   Avoid maintaining a hard-coded CEM icon table for every
        application/file type.
-   [ ] Define external-file selection behavior.
    -   Single-click/keyboard selection displays file
        information/metadata in the document/editor pane.
    -   Double-click or **Open** launches the file with its
        Windows-associated native application.
-   [ ] Add an external-file information/metadata view to the document
    pane.
    -   Show common properties such as:
        -   Filename.
        -   File type.
        -   Extension.
        -   Location.
        -   Size.
        -   Created date.
        -   Modified date.
        -   Read-only state.
        -   Associated application, when available.
    -   Show a larger native file/application icon.
    -   Provide an **Open** button.
    -   Consider **Open Folder** and **Windows Properties** buttons.
-   [ ] Read format-specific metadata through the Windows Shell/Property
    System where possible.
    -   Display meaningful properties exposed by the installed
        file/property handler.
    -   Examples:
        -   Images: dimensions, date taken, camera information.
        -   Video: duration, resolution, frame information.
        -   Office documents: title, author, pages, tags.
    -   Do not initially build custom parsers for every external format.

------------------------------------------------------------------------

## 7. Import External Artifacts

-   [ ] Add an **Add Existing File / External Artifact** operation.
    -   Select an existing outside file.
    -   Copy it into the CEM project rather than merely linking to it by
        default.
    -   Keep the CEM project self-contained and Git-friendly.
-   [ ] Use the selected CEM branch to propose the destination.
    -   If the destination is clear, propose it and ask for
        confirmation.
    -   If the destination is ambiguous, ask the user to select the
        appropriate valid CEM folder.
    -   Provide a **Change Location** option before copying.
-   [ ] Do not create a generic `ExternalFiles` folder.
    -   Store artifacts where they belong methodologically.
-   [ ] Use these general placement rules:
    -   Overall/project-wide external artifacts may be stored under:
        -   Vision.
        -   Concept Architecture.
    -   Version/implementation-specific artifacts should normally be
        stored under the applicable System Architecture.
    -   Within a System Architecture, most artifacts should normally go
        under:
        -   Design.
        -   Requirements.
        -   Other existing appropriate architecture branches.
    -   Keep the overall project structure around 4--5 folder levels
        deep whenever practical.
-   [ ] Show an **External Artifact Import** confirmation before
    copying.
    -   Source path.
    -   Proposed CEM destination.
    -   Generated CEM filename.
    -   **Change Location**.
    -   **Add**.
    -   **Cancel**.

------------------------------------------------------------------------

## 8. External Artifact Naming and Provenance

-   [ ] Timestamp every imported external artifact, including the first
    import.

    -   Standard filename convention:
        -   `OriginalName_YYYY-MM-DD_HHmm.ext`
    -   Timestamp represents the time the artifact entered CEM.
    -   Avoid `(1)`, `(2)`, `(3)` revision naming.

-   [ ] Keep source-document dates separate from the CEM import
    timestamp.

    -   CEM filename/import timestamp = when CEM received the artifact.
    -   Source created/modified timestamps = what is known about the
        source file itself.

-   [ ] Store provenance for each imported external artifact.

    -   Original filename.
    -   Original full path.
    -   CEM import date/time.
    -   Source created date/time.
    -   Source modified date/time.
    -   Source size.
    -   SHA-256 or similar content hash.

-   [ ] Keep provenance in CEM metadata rather than modifying the
    external/native document.

-   [ ] Check whether the remembered original source has changed.

    -   Do not implement full automatic synchronization.
    -   Check at appropriate times such as selection, project checks, or
        an explicit future **Check External Files** operation.
    -   Use timestamp/size as quick indicators.
    -   Use the stored hash when definitive content comparison is
        needed.
    -   If the original source no longer exists, report that without
        treating the CEM artifact as invalid.

-   [ ] When a changed source is imported, create another timestamped
    artifact.

    -   Prefer **Import New Version** rather than silently overwriting
        the existing CEM artifact.
    -   Preserve prior imported versions.

-   [ ] Leave machine/user identification out of filenames for now.

    -   Future shared/multi-user CEM can record:
        -   Imported By.
        -   Imported From Machine.
    -   Only record facts CEM actually knows at import time.
    -   Machine name may become an optional metadata field later rather
        than being assumed to identify where the file was edited.

------------------------------------------------------------------------

## 9. CHANGELOG.md

-   [ ] Populate `CHANGELOG.md` when a CEM project is created.
    -   It should no longer remain an unused/empty root document.
-   [ ] Reuse existing meaningful status-bar change notices as changelog
    events.
    -   Avoid creating a completely separate set of change-description
        strings.
-   [ ] Extend the existing status-message mechanism with a log/no-log
    flag or event classification.
    -   Meaningful structural/project events can be persisted.
    -   Routine UI notices should remain status-only.
-   [ ] Timestamp persisted changelog events.
    -   Prefer date and time rather than date only.
-   [ ] Log meaningful CEM-managed changes such as:
    -   Project creation.
    -   License selection/change.
    -   System Architecture creation.
    -   System Architecture naming/renaming.
    -   Numbered Decision Record creation.
    -   Numbered Requirement creation.
    -   External artifact import.
    -   Updated external artifact import.
    -   Other existing status-bar notices classified as project changes.
-   [ ] Do not flood the changelog with routine actions such as:
    -   File selected.
    -   Tree refreshed.
    -   Project loaded.
    -   Ordinary document saves, unless later determined to be useful.
-   [ ] Design the changelog mechanism so future user identification can
    be added without changing every feature that generates an event.

------------------------------------------------------------------------

## 10. CEM Project Metadata / Internal Data

-   [ ] Define a CEM project metadata location/format.
    -   Candidate:
        -   `.cem/project.json`
    -   Store shared project information such as selected license and
        other future project settings.
-   [ ] Define storage for external-artifact provenance.
    -   Candidate:
        -   `.cem/artifacts.json`
    -   Track source path, source dates, hashes, import timestamps, and
        artifact lineage.
-   [ ] Keep shared project metadata separate from local user/UI
    settings.

------------------------------------------------------------------------

## 11. .gitignore and Local CEM Data

-   [ ] Keep CEM project artifacts tracked by Git by default.

    -   Do not globally ignore imported CAD, PDF, spreadsheet, image,
        video, or other engineering artifacts merely because they came
        from external applications.

-   [ ] Do not ignore project-specific templates.

    -   Templates that define or belong to the project should be
        committed.

-   [ ] Keep CEM Explorer installation/master templates outside
    generated projects.

-   [ ] Add local/user-specific CEM information to `.gitignore`.

    -   Candidate structure:
        -   `.cem/user/`
        -   `.cem/ui/`
        -   `.cem/temp/`

-   [ ] Ignore CEM temporary/lock files as they are introduced.

    -   Possible patterns:
        -   `*.cemtmp`
        -   `*.cemlock`

-   [ ] Ignore common temporary/backup files where appropriate.

    -   Examples:
        -   `~$*`
        -   `*.tmp`
        -   Selected `*.bak` files where safe.

-   [ ] Do not ignore the entire `.cem/` directory.

    -   Shared metadata such as `project.json` and `artifacts.json`
        should remain under source control.
    -   Only user-local/UI/temp portions should be ignored.

-   [ ] Add external-program-specific cache/autosave patterns only as
    actual needs are discovered.

------------------------------------------------------------------------

## 12. Future Versioning / Traceability

-   [ ] Add a future **Create New Version** workflow for established
    System Architectures.
    -   Material scope changes should result in a new
        implementation/version rather than silently rewriting the
        historical implementation.
    -   Clone the prior architecture as the starting point where
        appropriate.
-   [ ] Preserve scope decisions between versions.
    -   Included Concept items.
    -   Explicitly excluded/not-used Concept items.
    -   Requirements.
    -   Decisions.
    -   Design artifacts.
-   [ ] Eventually provide traceability from:
    -   Concept item.
    -   System Architecture inclusion/exclusion.
    -   Requirement.
    -   Decision/Design.
    -   External engineering artifacts.
-   [ ] Eventually identify coverage gaps.
    -   Concept/System items with no Requirement.
    -   Required items with no Design/Decision coverage.
    -   Items explicitly excluded from a particular implementation.

------------------------------------------------------------------------

## 13. Future Activity / Effort Visibility

-   [ ] Preserve timestamps for meaningful CEM-managed events so
    activity can be analyzed later.

-   [ ] Eventually add a GitHub-style daily activity view.

    -   Calendar/square visualization of CEM project activity.
    -   Allow a day to be inspected to see which projects, System
        Architectures, Requirements, Decisions, or artifacts were worked
        on.

-   [ ] Eventually support activity reporting for both:

    -   Individual engineers working across multiple machines/projects.
    -   Larger shops with multiple users and shared CEM projects.

-   [ ] Use actual CEM project events rather than requiring a separate
    manual time/activity log wherever practical.

-   [ ] Eventually use version history and activity data to help show
    where engineering effort is being spent.

------------------------------------------------------------------------

## Guiding Rules Captured by These TODOs

-   CEM organizes engineering work; it does not try to replace
    specialized engineering applications.
-   Keep the folder hierarchy shallow, generally no more than about 4--5
    levels.
-   Put artifacts where they belong in the methodology rather than
    collecting them in generic attachment folders.
-   Concept defines what is considered.
-   System Architecture defines what a particular implementation/version
    addresses or explicitly does not address.
-   Requirements and Design work from the applicable System Architecture
    outline.
-   Preserve deliberate exclusions so "not used" is distinguishable from
    "forgotten."
-   Preserve old implementations rather than rewriting history.
-   External artifacts retain provenance without CEM becoming a full
    synchronization system.
-   CEM-generated status/change information should be reused for project
    history rather than duplicated.
-   Keep shared project information under source control while keeping
    user-specific UI/local state out of Git.
