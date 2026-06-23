# Modernisation Plan

## Goals

- Reduce business logic inside Windows Forms code-behind.
- Introduce reusable backend services for character and data loading.
- Move gradually toward view-model style UI state objects.
- Keep refactors incremental so the legacy WinForms app stays stable.

## Completed

- Added `CharacterDataService` to centralize refresh flows for:
  - assets
  - blueprints
  - research agents
- Added `CharacterSelectionService` to centralize:
  - loading the current/default character
  - selecting a character by name
  - querying available/default character state
- Added `CharacterSelectionViewModel` to represent character-selection UI state.
- Added `CharacterMenuService` and `CharacterMenuViewModel` to separate character menu state from `frmMain` rendering.
- Added `ManageAccountsService` and `ManageAccountsViewModel` so account-grid loading, role lookup, token refresh, and delete workflows are no longer composed inside `frmManageAccounts`.
- Added `ResearchAgentsService` and `ResearchAgentsViewModel` so datacore valuation and row-shaping logic are no longer composed inside `frmResearchAgents`.
- Started moving modernized backend code into dedicated folders:
  - `Services/`
  - `ViewModels/`
- Updated several forms to use service calls instead of directly orchestrating backend loads.

## Current Architecture Direction

- Forms should focus on:
  - user interaction
  - cursor/progress state
  - rendering controls
  - navigation between screens
- Services should own:
  - data loading
  - refresh orchestration
  - selection rules
  - persistence-facing workflows
- View models should carry:
  - UI-ready state
  - selected item names/ids
  - flags the UI can bind or render from

## Next Refactor Candidates

1. Extract character menu/list population into a reusable service + view model.
2. Introduce view models for:
   - Blueprint Management
   - Assets Viewer
   - Settings screens with larger editable state
3. Move DB query composition out of forms and into repository-style classes.
4. Replace global workflow helpers in `Globals.vb` with service calls until `Globals.vb` becomes mostly utility code.
5. Add tests around service-layer behavior where practical.
6. Continue moving modernized files into structure-first folders while leaving high-risk WinForms moves incremental.

## Guardrails

- Prefer small vertical refactors over large rewrites.
- Preserve existing behavior first, then simplify.
- Keep services free of direct form manipulation where possible.
- Allow transitional wrappers while call sites are being migrated.
