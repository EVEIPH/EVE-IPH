# UI / Backend Separation Notes

## New Backend Services

### `CharacterDataService`

Centralizes refresh workflows that were previously repeated across forms:

- personal assets
- corporation assets
- personal blueprints
- corporation blueprints
- research agents

### `CharacterSelectionService`

Centralizes character selection and loading workflows:

- load current/default character
- select a character by name
- inspect available/default character state

### `CharacterSelectionViewModel`

Represents character-selection state in a UI-friendly shape:

- available character names
- default character name
- currently selected character name
- whether a default selection is required
- whether only dummy data is loaded

### `CharacterMenuService` and `CharacterMenuViewModel`

Centralize the state used to render the bottom-right character menu in `frmMain`:

- loaded character display text
- menu entry names
- menu entry gender metadata for icon selection

### `ManageAccountsService` and `ManageAccountsViewModel`

Centralize the state and workflows used by `frmManageAccounts`:

- account list loading
- role flags for the selected account
- token refresh orchestration
- character deletion workflow

### `ResearchAgentsService` and `ResearchAgentsViewModel`

Centralize the state used by `frmResearchAgents`:

- loading UI-ready agent rows
- valuing datacores from cached market prices
- calculating total current datacore value

## Transitional State

This codebase still contains legacy global helpers in `Globals.vb`.
Some of those helpers now delegate to the new services so we can migrate safely without changing every call site at once.

## Updated UI Call Sites

The following forms now rely on services instead of directly orchestrating backend work:

- `frmAssetsViewer`
- `frmBlueprintManagement`
- `frmMain`
- `frmManageAccounts`
- `frmResearchAgents`
- `frmSetCharacterDefault`
- `frmSettings`

## Easy Follow-On Cleanup Targets

- Move `frmMain` post-character-change UI refresh steps into a small coordinator method or UI service.
- Replace repeated `MsgBox` + wait-cursor patterns with a lightweight UI helper.
- Start extracting read/write DB helpers into repository-style classes under a future `Infrastructure/` area.

## Recommended Pattern For Future Changes

1. Put orchestration and persistence-facing behavior in a service.
2. Return simple view-model state objects for the form to consume.
3. Keep form code focused on:
   - events
   - control updates
   - messages
   - progress/cursor behavior

## Folder Direction

The codebase is still largely flat, but new modernization work should prefer:

- `Services/` for orchestration and backend workflows
- `ViewModels/` for UI-ready state objects
- `docs/` for architectural notes and migration plans

Longer term, good next folders would be:

- `Forms/` for WinForms code-behind and designers
- `Infrastructure/` for DB and external-service access helpers
- `Models/` or `Domain/` for core entities
