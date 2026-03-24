# Wiki Viewer Overview

## Files

### `MarkdownParser.cs`
Utility that converts markdown to BBCode for Godot's `RichTextLabel`. Handles:
- H1 / H2 / H3 headings
- Bold `**text**`, italic `*text*`, bold-italic `***text***`
- Inline code `` `text` ``
- Horizontal rules `---`
- Unordered lists `- item`
- Hard line breaks (trailing `\`)
- Links `[text](path)` → clickable colored BBCode URLs

### `Wiki.cs`
Main scene controller. On `_Ready`:
1. Scans `res://Lore/` recursively via `DirAccess`
2. Groups `.md` entries by their immediate parent folder
   - e.g. `Locations/Praxis/Praxis.md` → category `Praxis`
3. Populates the sidebar `Tree` with categories as non-selectable headers
4. Wires up all signals

Features:
- **Search** — live-filters the sidebar tree by entry title
- **Back / Forward** — history stack, ← → buttons enabled/disabled automatically
- **Link navigation** — `MetaClicked` resolves relative markdown paths (e.g. `../../Systems/Demons.md`) and navigates to the target entry

### `Wiki.tscn`
Scene layout (1600×900):
- **Header bar** — title label + "Back to Menu" button
- **`HSplitContainer`** — draggable divider, sidebar starts at 300px
  - **Left (Sidebar)** — search `LineEdit` + scrollable entry `Tree`
  - **Right (Content)** — nav bar (← →, entry title) + `ScrollContainer` wrapping `RichTextLabel`

All nodes are marked `unique_name_in_owner = true` and accessed via `%NodeName` in code — no manual inspector wiring required.

## Adding New Entries

Drop any `.md` file into a subfolder of `res://Lore/` and it will appear automatically under a category named after its parent folder. No code changes needed.

## Planned

- Checkpoint-gated visibility (entries locked until gameplay milestones are reached)
