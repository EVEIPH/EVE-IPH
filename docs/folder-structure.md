# Folder Structure Direction

## Why

This repository started as a mostly flat VB.NET WinForms project, which makes navigation harder as the codebase grows.
We want a structure that improves discoverability without forcing a risky big-bang move of every form and designer file.

## Current Incremental Direction

Newly modernized code should prefer these folders first:

- `Services/`
  - backend orchestration
  - persistence-facing workflows
  - cross-form reusable behavior
- `ViewModels/`
  - UI-ready state objects
  - form rendering models
- `docs/`
  - refactor notes
  - architecture guidance
  - migration plans

## Recommended Target Layout

- `Forms/`
  - `frmMain.vb`
  - `frmManageAccounts.vb`
  - other form code-behind files
- `Forms/Designers/`
  - designer-generated partial classes if we decide to separate them later
- `Services/`
  - character loading/selection services
  - account-management services
  - UI workflow coordinators
- `ViewModels/`
  - menu state
  - account state
  - selection state
- `Infrastructure/`
  - database helpers
  - repository-style query classes
  - ESI integration helpers that are not domain entities
- `Models/`
  - core domain/data objects such as characters, corporations, materials, blueprints
- `Legacy/`
  - optional landing area for transitional wrappers if we choose to reduce the surface of `Globals.vb`

## Migration Rules

1. Prefer moving new files first.
2. Move existing files only when a refactor already touches them.
3. Avoid relocating designer files in bulk until we are comfortable with the project-file maintenance overhead.
4. Keep behavior-preserving refactors separate from large physical moves whenever possible.
